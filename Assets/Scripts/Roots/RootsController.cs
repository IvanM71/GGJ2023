using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Apollo11.Roots
{
    public class RootsController : MonoBehaviour
    {
        // Roots model
        RootsModel model;

        // Tilemap for roots
        [SerializeField] Tilemap field;

        // Root prefab
        [SerializeField] GameObject rootPrefab;

        // Roots sprites
        [SerializeField] Sprite[] sprites;

        // 2d array of roots
        private GameObject[,] roots;

        // Start is called before the first frame update
        void Awake()
        {
            BoundsInt bounds = field.cellBounds;
            Vector3 cellSize = field.cellSize;

            // Create model array
            model = new RootsModel();
            model.roots = new Root[bounds.size.y, bounds.size.x];

            // Create view array
            roots = new GameObject[bounds.size.y, bounds.size.x];

            for (int x = bounds.yMin, xModel = 0; x < bounds.yMax; x++, xModel++)
            {
                for (int y = bounds.xMin, yModel = 0; y < bounds.xMax; y++, yModel++)
                {
                    Vector3 cellPosition = field.CellToWorld(new Vector3Int(y, x, 0));
                    Vector3 rootPosition = new Vector3(cellPosition.x + cellSize.x / 2, cellPosition.y + cellSize.y / 2, -1);

                    if (field.GetTile(new Vector3Int(y, x, 0)) != null)
                    {
                        model.roots[xModel, yModel] = new Root(Enums.RootStages.STAGE_0);
                        roots[xModel, yModel] = Instantiate(rootPrefab, rootPosition, Quaternion.identity, this.transform);
                    }
                    else
                    {
                        model.roots[xModel, yModel] = null;
                        roots[xModel, yModel] = null;
                    }
                }
            }

            model.roots[0, 10].stage = Enums.RootStages.MAIN;

            field.gameObject.SetActive(false);

            StartCoroutine(IE_Timer());
        }


        private void UpdateView()
        {
            for (int x = 0; x < model.roots.GetLength(0); x++)
            {
                for (int y = 0; y < model.roots.GetLength(1); y++)
                {
                    if (model.roots[x, y] == null) continue;
                    switch (model.roots[x, y].stage)
                    {
                        case Enums.RootStages.STAGE_0:
                            roots[x, y].SetActive(false);
                            break;
                        case Enums.RootStages.STAGE_1:
                            roots[x, y].SetActive(true);
                            roots[x, y].GetComponent<SpriteRenderer>().sprite = sprites[0];
                            break;
                        case Enums.RootStages.STAGE_2:
                            roots[x, y].GetComponent<SpriteRenderer>().sprite = sprites[1];
                            break;
                        case Enums.RootStages.STAGE_3:
                            roots[x, y].GetComponent<SpriteRenderer>().sprite = sprites[2];
                            break;
                        case Enums.RootStages.MAIN:
                            roots[x, y].GetComponent<SpriteRenderer>().sprite = sprites[3];
                            break;
                    }
                }
            }
        }

        bool TryGrow()
        {
            List<Root> possibleGrow = new List<Root>();
            List<int> weights = new List<int>();
            for (int x = 0; x < model.roots.GetLength(0); x++)
            {
                for (int y = 0; y < model.roots.GetLength(1); y++)
                {
                    if (model.roots[x, y] == null) continue;
                    if (model.roots[x, y].stage == Enums.RootStages.STAGE_0)
                    {
                        int weight = 0;
                        if (x > 0)
                        {
                            if (model.roots[x - 1, y] != null)
                            {
                                if (model.roots[x - 1, y].stage == Enums.RootStages.MAIN)
                                    weight += (int)model.roots[x - 1, y].stage + 5;
                                else
                                    weight += (int)model.roots[x - 1, y].stage;
                            }
                        }

                        if (y > 0)
                        {
                            if (model.roots[x, y - 1] != null)
                            {
                                if (model.roots[x, y - 1].stage == Enums.RootStages.MAIN)
                                    weight += (int)model.roots[x, y - 1].stage + 5;
                                else
                                    weight += (int)model.roots[x, y - 1].stage;
                            }
                        }

                        if (x < model.roots.GetLength(0) - 1)
                        {
                            if (model.roots[x + 1, y] != null)
                            {
                                if (model.roots[x + 1, y].stage == Enums.RootStages.MAIN)
                                    weight += (int)model.roots[x + 1, y].stage + 5;
                                else
                                    weight += (int)model.roots[x + 1, y].stage;
                            }
                        }

                        if (y < model.roots.GetLength(1) - 1)
                        {
                            if (model.roots[x, y + 1] != null)
                            {
                                if (model.roots[x, y + 1].stage == Enums.RootStages.MAIN)
                                    weight += (int)model.roots[x, y + 1].stage + 5;
                                else
                                    weight += (int)model.roots[x, y + 1].stage;
                            }
                        }

                        if (weight > 0)
                        {
                            for (int i = 0; i < weight; i++)
                            {
                                possibleGrow.Add(model.roots[x, y]);
                                weights.Add(weight);
                            }
                        }
                    }
                }
            }

            if (possibleGrow.Count > 0)
            {
                possibleGrow[Random.Range(0, possibleGrow.Count)].stage++;
                return true;
            }
            else
                return false;
        }

        bool TryStageUp()
        {
            List<Root> possibleGrow = new List<Root>();
            List<int> weights = new List<int>();
            for (int x = 0; x < model.roots.GetLength(0); x++)
            {
                for (int y = 0; y < model.roots.GetLength(1); y++)
                {
                    if (model.roots[x, y] == null) continue;
                    if (model.roots[x, y].stage > Enums.RootStages.STAGE_0 && model.roots[x, y].stage < Enums.RootStages.STAGE_3)
                    {
                        int weight = 0;
                        int isRounded = 0;
                        if (x > 0)
                        {
                            if (model.roots[x - 1, y] != null)
                            {
                                if (model.roots[x - 1, y].stage != Enums.RootStages.STAGE_0)
                                {
                                    weight += (int)model.roots[x - 1, y].stage - 1;

                                    if (model.roots[x - 1, y].stage >= model.roots[x, y].stage)
                                        isRounded++;
                                }
                            }
                        }

                        if (y > 0)
                        {
                            if (model.roots[x, y - 1] != null)
                            {
                                if (model.roots[x, y - 1].stage != Enums.RootStages.STAGE_0)
                                {
                                    weight += (int)model.roots[x, y - 1].stage - 1;

                                    if (model.roots[x, y - 1].stage >= model.roots[x, y].stage)
                                        isRounded++;
                                }
                            }
                        }

                        if (x < model.roots.GetLength(0) - 1)
                        {
                            if (model.roots[x + 1, y] != null)
                            {
                                if (model.roots[x + 1, y].stage != Enums.RootStages.STAGE_0)
                                {
                                    weight += (int)model.roots[x + 1, y].stage - 1;

                                    if (model.roots[x + 1, y].stage >= model.roots[x, y].stage)
                                        isRounded++;
                                }
                            }
                        }

                        if (y < model.roots.GetLength(1) - 1)
                        {
                            if (model.roots[x, y + 1] != null)
                            {
                                if (model.roots[x, y + 1].stage != Enums.RootStages.STAGE_0)
                                {
                                    weight += (int)model.roots[x, y + 1].stage - 1;

                                    if (model.roots[x, y + 1].stage >= model.roots[x, y].stage)
                                        isRounded++;
                                }
                            }
                        }

                        if (model.roots[x, y].stage == Enums.RootStages.STAGE_2)
                            weight += 1;
                        else if (model.roots[x, y].stage == Enums.RootStages.STAGE_1)
                            weight += 2;

                        if (isRounded < 3)
                            weight = 0;

                        if (weight > 0)
                        {
                            for (int i = 0; i < weight; i++)
                            {
                                possibleGrow.Add(model.roots[x, y]);
                                weights.Add(weight);
                            }
                        }
                    }
                }
            }

            if (possibleGrow.Count > 0)
            {
                if (possibleGrow.Count > 0)
                    possibleGrow[Random.Range(0, possibleGrow.Count)].stage++;

                return true;
            }
            else
                return false;
        }

        private IEnumerator IE_Timer()
        {
            var tick = new WaitForSeconds(0.5f);
            while (true) //or while root is alive
            {
                var canStageUp = TryStageUp();
                UpdateView();
                if (canStageUp)
                    yield return tick;
                
                var canGrow = TryGrow();
                UpdateView();
                if (canGrow)
                    yield return tick;
            }
        }
    }
}
