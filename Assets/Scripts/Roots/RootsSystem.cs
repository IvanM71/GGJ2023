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

        [SerializeField] private Tilemap spawnpointsTilemap;
        [SerializeField] private GameObject rootPrefab;
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

            List<int> xPossible = new List<int>();
            List<int> yPossible = new List<int>();

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
                        xPossible.Add(xModel);
                        yPossible.Add(yModel);
                    }
                    else
                    {
                        rootsModel.roots[xModel, yModel] = null;
                        rootsView.roots[xModel, yModel] = null;
                    }
                }
            }

            int index = Random.Range(0, xPossible.Count);

            rootsModel.roots[xPossible[index], yPossible[index]].stage = RootStages.MAIN;
            rootsModel.roots[xPossible[index], yPossible[index]].type = RootType.TypeA;

            xPossible.RemoveAt(index);
            yPossible.RemoveAt(index);

            index = Random.Range(0, xPossible.Count);

            rootsModel.roots[xPossible[index], yPossible[index]].stage = RootStages.MAIN;
            rootsModel.roots[xPossible[index], yPossible[index]].type = RootType.TypeB;

            xPossible.RemoveAt(index);
            yPossible.RemoveAt(index);

            index = Random.Range(0, xPossible.Count);

            rootsModel.roots[xPossible[index], yPossible[index]].stage = RootStages.MAIN;
            rootsModel.roots[xPossible[index], yPossible[index]].type = RootType.TypeC;

            spawnpointsTilemap.gameObject.SetActive(false);

            for (int i = 0; i < rootsControllers.Length; i++)
            {
                //rootsControllers[i].StartCoroutine(rootsControllers[i].IE_Timer());
                rootsControllers[i].StartRoutine(rootsModel, rootsView);
            }
        }
    }
}
