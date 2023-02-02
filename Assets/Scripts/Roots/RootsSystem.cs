using Apollo11.Roots;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Apollo11.Enums;

namespace Apollo11
{
    public class RootsSystem : MonoBehaviour
    {
        RootsView rootsView;
        RootsModel rootsModel;

        public RootsView RootsView 
        { 
            get { return rootsView; }
            set { rootsView = value; }
        }
        public RootsModel RootsModel
        { 
            get { return rootsModel; } 
            set { rootsModel = value; }
        }

        //[SerializeField] private MainRoot[] mainRoots;

        [SerializeField] private Tilemap spawnpointsTilemap;
        [SerializeField] private Tilemap mainrootsSpawnpointsTilemap;
        [SerializeField] private GameObject rootPrefab;
        [SerializeField] private GameObject mainrootPrefab;
        [SerializeField] private RootsController[] rootsControllers;

        private void Awake()
        {
            BoundsInt bounds = spawnpointsTilemap.cellBounds;
            Vector3 cellSize = spawnpointsTilemap.cellSize;

            // Create model array
            rootsModel = new RootsModel();
            rootsModel.roots = new Root[bounds.size.y, bounds.size.x];

            // Create view array
            rootsView = new RootsView();
            rootsView.roots = new GameObject[bounds.size.y, bounds.size.x];

            Enums.RootType rootType = Enums.RootType.TypeA;

            for (int x = bounds.yMin, xModel = 0; x < bounds.yMax; x++, xModel++)
            {
                for (int y = bounds.xMin, yModel = 0; y < bounds.xMax; y++, yModel++)
                {
                    Vector3 cellPosition = spawnpointsTilemap.CellToWorld(new Vector3Int(y, x, 0));
                    Vector3 rootPosition = new Vector3(cellPosition.x + cellSize.x / 2, cellPosition.y + cellSize.y / 2, -1);

                    if (spawnpointsTilemap.GetTile(new Vector3Int(y, x, 0)) != null)
                    {
                        if(mainrootsSpawnpointsTilemap.GetTile(new Vector3Int(y, x, 0)) != null)
                        {
                            rootsModel.roots[xModel, yModel] = new Root(Enums.RootStages.MAIN, rootType);
                            rootType++;
                            rootsView.roots[xModel, yModel] = Instantiate(rootPrefab, rootPosition, Quaternion.identity, this.transform);
                            Instantiate(mainrootPrefab, rootPosition, Quaternion.identity, this.transform);
                            continue;
                        }
                        rootsModel.roots[xModel, yModel] = new Root(Enums.RootStages.STAGE_0, Enums.RootType.Unknown);
                        rootsView.roots[xModel, yModel] = Instantiate(rootPrefab, rootPosition, Quaternion.identity, this.transform);
                        rootsView.roots[xModel, yModel].GetComponent<RootDamageReceiver>().X = xModel;
                        rootsView.roots[xModel, yModel].GetComponent<RootDamageReceiver>().Y = yModel;
                    }
                    else
                    {
                        rootsModel.roots[xModel, yModel] = null;
                        rootsView.roots[xModel, yModel] = null;
                    }
                }
            }

            spawnpointsTilemap.gameObject.SetActive(false);

            for (int i = 0; i < rootsControllers.Length; i++)
            {
                rootsControllers[i].rootsModel = rootsModel;
                rootsControllers[i].rootsView = rootsView;
            }

            for (int i = 0; i < rootsControllers.Length; i++)
            {
                StartCoroutine(IE_Timer(rootsControllers[i], 3f));
            }
        }
        private IEnumerator IE_Timer(RootsController controller, float delay)
        {
            var tick = new WaitForSeconds(delay);
            while (true) //or while root is alive
            {
                var canStageUp = controller.TryStageUp();
                controller.UpdateView();
                if (canStageUp)
                    yield return tick;

                var canGrow = controller.TryGrow();
                controller.UpdateView();

                if (canGrow)
                {
                    if (isLose())
                    {
                        StopAllCoroutines();
                        break;
                    }
                    yield return tick;
                }
            }
        }
        bool isLose()
        {
            for(int x = 0; x < rootsModel.roots.GetLength(0); x++) 
            {
                for(int y = 0; y < rootsModel.roots.GetLength(1); y++)
                {
                    if (rootsModel.roots[x, y] != null)
                        if (rootsModel.roots[x, y].Stage == RootStages.STAGE_0)
                            return false;
                }
            }
            return true;
        }
        public void UpdateView()
        {
            for(int i = 0; i < rootsControllers.Length; i++)
            {
                rootsControllers[i].UpdateView();
            }
        }
    }
}
