using Apollo11.Roots;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

        private List<MainRoot> mainRoots;

        [SerializeField] private Tilemap spawnpointsTilemap;
        [SerializeField] private GameObject rootPrefab;
        [SerializeField] private List<RootsController> rootsControllers;
        [SerializeField] private Sprite[] typeASprites;
        [SerializeField] private Sprite[] typeBSprites;
        [SerializeField] private Sprite[] typeCSprites;

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

            //Enums.RootType rootType = Enums.RootType.TypeA;

            for (int x = bounds.yMin, xModel = 0; x < bounds.yMax; x++, xModel++)
            {
                for (int y = bounds.xMin, yModel = 0; y < bounds.xMax; y++, yModel++)
                {
                    Vector3 cellPosition = spawnpointsTilemap.CellToWorld(new Vector3Int(y, x, 0));
                    Vector3 rootPosition = new Vector3(cellPosition.x + cellSize.x / 2, cellPosition.y + cellSize.y / 2, -1);

                    if (spawnpointsTilemap.GetTile(new Vector3Int(y, x, 0)) != null)
                    {
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

            for (int i = 0; i < rootsControllers.Count; i++)
            {
                rootsControllers[i].rootsModel = rootsModel;
            }

            StartCoroutine(IE_Timer());
        }
        private void Start()
        {
            mainRoots = new List<MainRoot>(FindObjectsOfType<MainRoot>());

            for (int i = 0; i < mainRoots.Count; i++)
            {
                Vector3Int cellPosition = spawnpointsTilemap.WorldToCell(mainRoots[i].transform.position);
                int x = cellPosition.y - spawnpointsTilemap.cellBounds.yMin;
                int y = cellPosition.x - spawnpointsTilemap.cellBounds.xMin;
                rootsModel.roots[x, y].Stage = RootStages.MAIN;
                rootsModel.roots[x, y].Type = mainRoots[i].GetRootType();
            }
        }
        private IEnumerator IE_Timer()
        {
            var tick = new WaitForSeconds(3f);
            while (true) //or while root is alive
            {
                bool isTick = false;
                for (int i = 0; i < rootsControllers.Count; i++)
                {
                    var canStageUp = rootsControllers[i].TryStageUp();
                    if (canStageUp)
                        isTick = true;
                }

                UpdateView();

                if (isTick)
                    yield return tick;

                isTick = false;

                for (int i = 0; i < rootsControllers.Count; i++)
                {
                    var canGrow = rootsControllers[i].TryGrow();
                    if (canGrow)
                    {
                        if (isLose())
                        {
                            StopAllCoroutines();
                            break;
                        }
                        isTick = true;
                    }
                }

                UpdateView();

                if (isTick)
                {
                    yield return tick;
                }

                yield return null;
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
            for (int x = 0; x < rootsModel.roots.GetLength(0); x++)
            {
                for (int y = 0; y < rootsModel.roots.GetLength(1); y++)
                {
                    if (rootsModel.roots[x, y] == null) continue;

                    int spriteIndex = 0;
                    if (rootsModel.roots[x, y].Stage == RootStages.STAGE_0)
                        rootsView.roots[x, y].SetActive(false);
                    else if (rootsModel.roots[x, y].Stage == RootStages.MAIN)
                    {
                        rootsView.roots[x, y].SetActive(true);
                        spriteIndex = 2;
                    }
                    else
                    {
                        rootsView.roots[x, y].SetActive(true);
                        spriteIndex = (int)rootsModel.roots[x, y].Stage - 1;
                    }

                    switch (rootsModel.roots[x, y].Type)
                    {
                        case RootType.TypeA:
                            rootsView.roots[x, y].GetComponent<SpriteRenderer>().sprite = typeASprites[spriteIndex];
                            break;
                        case RootType.TypeB:
                            rootsView.roots[x, y].GetComponent<SpriteRenderer>().sprite = typeBSprites[spriteIndex];
                            break;
                        case RootType.TypeC:
                            rootsView.roots[x, y].GetComponent<SpriteRenderer>().sprite = typeCSprites[spriteIndex];
                            break;
                    }
                }
            }
        }
        public void OnMainRootDeath(Enums.RootType type)
        {
            for(int i = 0; i < rootsControllers.Count; i++)
            {
                if (rootsControllers[i].rootType == type)
                {
                    RootsController remove = rootsControllers[i];
                    rootsControllers.RemoveAt(i);
                    Destroy(remove);
                }
            }

            for (int i = 0; i < mainRoots.Count; i++)
            {
                if (mainRoots[i].GetRootType() == type)
                {
                    mainRoots.RemoveAt(i);
                }
            }
        }
    }
}
