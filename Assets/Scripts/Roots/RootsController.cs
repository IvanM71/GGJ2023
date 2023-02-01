using System.Collections.Generic;
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

    // Grow countdown
    //private float growCountdown = 2f;
    //private float stageUpCountdown = 4f;

        // Start is called before the first frame update
        void Start()
        {
            BoundsInt bounds = field.cellBounds;
            Vector3 cellSize = field.cellSize;

            // Create model array
            model = new RootsModel();
            model.roots = new Root[field.size.y, field.size.x];

            // Create view array
            roots = new GameObject[field.size.y, field.size.x];

            for (int x = bounds.yMin, xModel = 0; x < bounds.yMax; x++, xModel++)
            {
                for (int y = bounds.xMin, yModel = 0; y < bounds.xMax; y++, yModel++)
                {
                    Vector3 cellPosition = field.CellToWorld(new Vector3Int(y, x, 0));
                    Vector3 rootPosition = new Vector3(cellPosition.x + cellSize.x / 2, cellPosition.y + cellSize.y / 2, -1);

                    model.roots[xModel, yModel] = new Root(Enums.RootStages.STAGE_0);
                    roots[xModel, yModel] = Instantiate(rootPrefab, rootPosition, Quaternion.identity);
                }
            }

            model.roots[8, 15].stage = Enums.RootStages.MAIN;
            model.roots[8, 15]._growDirections[0] = true;   // up
            model.roots[8, 15]._growDirections[1] = true;   // down
            model.roots[8, 15]._growDirections[2] = true;   // left
            model.roots[8, 15]._growDirections[3] = true;   // right

        field.gameObject.SetActive(false);

        StartCoroutine(IE_Timer());
    }

    // Update is called once per frame
    void Update()
    {
        //if (growCountdown > 0)
        //    growCountdown -= Time.deltaTime;
        //else
        //{
        //    StageUp();
        //    Grow();
        //    growCountdown += 2;
        //}

            for (int x = 0; x < roots.GetLength(0); x++)
            {
                for (int y = 0; y < roots.GetLength(1); y++)
                {
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
        //int maxWeight = 0;
        for(int x = 0; x < roots.GetLength(0); x++)
        {
            for(int y = 0; y < roots.GetLength(1); y++)
            {
                if (model.roots[x, y].stage == Enums.RootStages.STAGE_0)
                {
                    int weight = 0;
                    if (x > 0)
                    {
                        if (model.roots[x - 1, y].stage == Enums.RootStages.MAIN)
                            weight += (int)model.roots[x - 1, y].stage + 5;
                        else
                            weight += (int)model.roots[x - 1, y].stage;
                    }
                    //if (model.roots[x - 1, y].stage == Enums.RootStages.STAGE_0)
                    //    possibleGrow.Add(model.roots[x - 1, y]);

                    if (y > 0)
                    {
                        if (model.roots[x, y - 1].stage == Enums.RootStages.MAIN)
                            weight += (int)model.roots[x, y - 1].stage + 5;
                        else
                            weight += (int)model.roots[x, y - 1].stage;
                    }
                    //if (model.roots[x, y - 1].stage == Enums.RootStages.STAGE_0)
                    //    possibleGrow.Add(model.roots[x, y - 1]);

                    if (x < roots.GetLength(0) - 1)
                    {
                        if (model.roots[x + 1, y].stage == Enums.RootStages.MAIN)
                            weight += (int)model.roots[x + 1, y].stage + 5;
                        else
                            weight += (int)model.roots[x + 1, y].stage;
                    }
                    //if (model.roots[x + 1, y].stage == Enums.RootStages.STAGE_0)
                    //    possibleGrow.Add(model.roots[x + 1, y]);

                    if (y < roots.GetLength(1) - 1)
                    {
                        if (model.roots[x, y + 1].stage == Enums.RootStages.MAIN)
                            weight += (int)model.roots[x, y + 1].stage + 5;
                        else
                            weight += (int)model.roots[x, y + 1].stage;
                    }
                    //if (model.roots[x, y + 1].stage == Enums.RootStages.STAGE_0)
                    //    possibleGrow.Add(model.roots[x, y + 1]);
                    if(weight > 0)
                    {
                        //if (weight > maxWeight)
                        //    maxWeight = weight;

                        //possibleGrow.Add(model.roots[x, y]);
                        //weights.Add(weight);

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
            //List<Root> maxWeightsRoots = new List<Root>();

            //for (int i = 0; i < possibleGrow.Count; i++)
            //{
            //    if (weights[i] == maxWeight)
            //        maxWeightsRoots.Add(possibleGrow[i]);
            //}

            //if (maxWeightsRoots.Count > 0)
            //    maxWeightsRoots[Random.Range(0, maxWeightsRoots.Count)].stage++;

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
        //int maxWeight = 0;
        for (int x = 0; x < roots.GetLength(0); x++)
        {
            for (int y = 0; y < roots.GetLength(1); y++)
            {
                if (model.roots[x, y].stage > Enums.RootStages.STAGE_0 && model.roots[x, y].stage < Enums.RootStages.STAGE_3)
                {
                    int weight = 0;
                    int isRounded = 0;
                    if (x > 0)
                    {
                        if (model.roots[x - 1, y].stage != Enums.RootStages.STAGE_0)
                        {
                            weight += (int)model.roots[x - 1, y].stage - 1;

                            if (model.roots[x - 1, y].stage >= model.roots[x, y].stage)
                                isRounded++;

                            //if (model.roots[x, y].stage == Enums.RootStages.STAGE_2)
                            //{
                            //    if (model.roots[x - 1, y].stage == Enums.RootStages.STAGE_2)
                            //        isRounded++;
                            //}
                            //else
                            //    isRounded++;

                            //switch (model.roots[x, y].stage)
                            //{
                            //    case Enums.RootStages.STAGE_1:
                            //        if (model.roots[x - 1, y].stage == Enums.RootStages.STAGE_1)
                            //            isRounded++;
                            //        break;
                            //    case Enums.RootStages.STAGE_2:
                            //        if (model.roots[x - 1, y].stage == Enums.RootStages.STAGE_1)
                            //            isRounded++;
                            //        break;
                            //    default:
                            //        break;
                            //}
                        }
                    }
                    //if (model.roots[x - 1, y].stage > Enums.RootStages.STAGE_0 && model.roots[x - 1, y].stage < Enums.RootStages.STAGE_3)
                    //    possibleGrow.Add(model.roots[x - 1, y]);

                    if (y > 0)
                    {
                        if (model.roots[x, y - 1].stage != Enums.RootStages.STAGE_0)
                        {
                            weight += (int)model.roots[x, y - 1].stage - 1;

                            if (model.roots[x, y - 1].stage >= model.roots[x, y].stage)
                                isRounded++;

                            //if (model.roots[x, y].stage == Enums.RootStages.STAGE_2)
                            //{
                            //    if (model.roots[x, y - 1].stage == Enums.RootStages.STAGE_2)
                            //        isRounded++;
                            //}
                            //else
                            //    isRounded++;

                            //switch (model.roots[x, y].stage)
                            //{
                            //    case Enums.RootStages.STAGE_1:
                            //        if (model.roots[x, y - 1].stage == Enums.RootStages.STAGE_1)
                            //            isRounded++;
                            //        break;
                            //    case Enums.RootStages.STAGE_2:
                            //        if (model.roots[x, y - 1].stage == Enums.RootStages.STAGE_2)
                            //            isRounded++;
                            //        break;
                            //    default:
                            //        break;
                            //}
                        }
                    }
                    //if (model.roots[x, y - 1].stage > Enums.RootStages.STAGE_0 && model.roots[x, y - 1].stage < Enums.RootStages.STAGE_3)
                    //    possibleGrow.Add(model.roots[x, y - 1]);

                    if (x < roots.GetLength(0) - 1)
                    {
                        if (model.roots[x + 1, y].stage != Enums.RootStages.STAGE_0)
                        {
                            weight += (int)model.roots[x + 1, y].stage - 1;

                            if (model.roots[x + 1, y].stage >= model.roots[x, y].stage)
                                isRounded++;

                            //if (model.roots[x, y].stage == Enums.RootStages.STAGE_2)
                            //{
                            //    if (model.roots[x + 1, y].stage == Enums.RootStages.STAGE_2)
                            //        isRounded++;
                            //}
                            //else
                            //    isRounded++;

                            //switch (model.roots[x, y].stage)
                            //{
                            //    case Enums.RootStages.STAGE_1:
                            //        if (model.roots[x + 1, y].stage == Enums.RootStages.STAGE_1)
                            //            isRounded++;
                            //        break;
                            //    case Enums.RootStages.STAGE_2:
                            //        if (model.roots[x + 1, y].stage == Enums.RootStages.STAGE_2)
                            //            isRounded++;
                            //        break;
                            //    default:
                            //        break;
                            //}
                        }
                    }
                    //if (model.roots[x + 1, y].stage > Enums.RootStages.STAGE_0 && model.roots[x + 1, y].stage < Enums.RootStages.STAGE_3)
                    //    possibleGrow.Add(model.roots[x + 1, y]);

                    if (y < roots.GetLength(1) - 1)
                    {
                        if (model.roots[x, y + 1].stage != Enums.RootStages.STAGE_0)
                        {
                            weight += (int)model.roots[x, y + 1].stage - 1;

                            if (model.roots[x, y + 1].stage >= model.roots[x, y].stage)
                                isRounded++;

                            //if (model.roots[x, y].stage == Enums.RootStages.STAGE_2)
                            //{
                            //    if (model.roots[x, y + 1].stage == Enums.RootStages.STAGE_2)
                            //        isRounded++;
                            //}
                            //else
                            //    isRounded++;

                            //switch (model.roots[x, y].stage)
                            //{
                            //    case Enums.RootStages.STAGE_1:
                            //        if (model.roots[x, y + 1].stage == Enums.RootStages.STAGE_1)
                            //            isRounded++;
                            //        break;
                            //    case Enums.RootStages.STAGE_2:
                            //        if (model.roots[x, y + 1].stage == Enums.RootStages.STAGE_2)
                            //            isRounded++;
                            //        break;
                            //    default:
                            //        break;
                            //}
                        }
                    }
                    //if (model.roots[x, y + 1].stage > Enums.RootStages.STAGE_0 && model.roots[x, y + 1].stage < Enums.RootStages.STAGE_3)
                    //    possibleGrow.Add(model.roots[x, y + 1]);

                    if (model.roots[x, y].stage == Enums.RootStages.STAGE_2)
                        weight += 1;
                    else if (model.roots[x, y].stage == Enums.RootStages.STAGE_1)
                        weight += 2;

                    if (isRounded < 3)
                        weight = 0;

                    if(weight > 0)
                    {
                        //if (weight > maxWeight)
                        //    maxWeight = weight;

                        for (int i = 0; i < weight; i++)
                        {
                            possibleGrow.Add(model.roots[x, y]);
                            weights.Add(weight);
                        }

                        //possibleGrow.Add(model.roots[x, y]);
                        //weights.Add(weight);
                    }
                }
            }
        }

        if (possibleGrow.Count > 0)
        {
            //List<Root> maxWeightsRoots = new List<Root>();

            //for (int i = 0; i < possibleGrow.Count; i++)
            //{
            //    if (weights[i] == maxWeight)
            //        maxWeightsRoots.Add(possibleGrow[i]);
            //}

            //if (maxWeightsRoots.Count > 0)
            //    maxWeightsRoots[Random.Range(0, maxWeightsRoots.Count)].stage++;

            if (possibleGrow.Count > 0)
                possibleGrow[Random.Range(0, possibleGrow.Count)].stage++;

            return true;
        }
        else
            return false;
    }

    private IEnumerator IE_Timer()
    {
        var tick = new WaitForSeconds(2f);
        while (true) //or while root is alive
        {
            if(TryStageUp())
                yield return tick;
            if(TryGrow())
                yield return tick;
        }
    }
}
