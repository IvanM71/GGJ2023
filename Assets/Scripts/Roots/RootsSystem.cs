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
        Sprite[,] rootsSprites;

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

            rootsSprites = new Sprite[bounds.size.y, bounds.size.x];

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
                        rootsSprites[xModel, yModel] = rootsView.roots[xModel, yModel].GetComponent<SpriteRenderer>().sprite;
                        xPossible.Add(xModel);
                        yPossible.Add(yModel);
                    }
                    else
                    {
                        rootsModel.roots[xModel, yModel] = null;
                        rootsView.roots[xModel, yModel] = null;
                        rootsSprites[xModel, yModel] = null;
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
                rootsControllers[i].rootsModel = rootsModel;
                rootsControllers[i].rootsView= rootsView;
            }

            for (int i = 0; i < rootsControllers.Length; i++)
            {
                StartCoroutine(IE_Timer(rootsControllers[i], i + 0.5f));
            }
        }
        private IEnumerator IE_Timer(RootsController controller, float delay)
        {
            var tick = new WaitForSeconds(delay);
            while (true) //or while root is alive
            {
                //for (int i = 0; i < rootsControllers.Length; i++)
                //{
                //    var canStageUp = rootsControllers[i].TryStageUp();
                //    rootsControllers[i].UpdateView();
                //    if (canStageUp)
                //        yield return tick;

                //    var canGrow = rootsControllers[i].TryGrow();
                //    rootsControllers[i].UpdateView();
                //    if (canGrow)
                //        yield return tick;
                //}
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
        private void UpdateView()
        {
            for (int x = 0; x < rootsModel.roots.GetLength(0); x++)
            {
                for (int y = 0; y < rootsModel.roots.GetLength(1); y++)
                {
                    if (rootsModel.roots[x, y] == null) continue;
                    switch (rootsModel.roots[x, y].stage)
                    {
                        case Enums.RootStages.STAGE_0:
                            rootsView.roots[x, y].SetActive(false);
                            break;
                        case Enums.RootStages.STAGE_1:
                            if (rootsModel.roots[x, y].type == rootType)
                            {
                                rootsView.roots[x, y].SetActive(true);
                                rootsView.roots[x, y].GetComponent<SpriteRenderer>().sprite = sprites[0];
                            }
                            break;
                        case Enums.RootStages.STAGE_2:
                            if (rootsModel.roots[x, y].type == rootType)
                                rootsView.roots[x, y].GetComponent<SpriteRenderer>().sprite = sprites[1];
                            break;
                        case Enums.RootStages.STAGE_3:
                            if (rootsModel.roots[x, y].type == rootType)
                                rootsView.roots[x, y].GetComponent<SpriteRenderer>().sprite = sprites[2];
                            break;
                        case Enums.RootStages.MAIN:
                            if (rootsModel.roots[x, y].type == rootType)
                                rootsView.roots[x, y].GetComponent<SpriteRenderer>().sprite = sprites[3];
                            break;
                    }
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
                        if (rootsModel.roots[x, y].stage == RootStages.STAGE_0)
                            return false;
                }
            }
            return true;
        }
    }
}
