using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
    private float growCountdown = 5f;

    // Start is called before the first frame update
    void Start()
    {
        BoundsInt bounds = field.cellBounds;
        Vector3 cellSize = field.cellSize;

        // Create model array
        model = new RootsModel();
        model.view = new Root[field.size.y, field.size.x];
        model.buffer = new Root[field.size.y, field.size.x];

        // Create view array
        roots = new GameObject[field.size.y, field.size.x];

        for (int x = bounds.yMin, xModel = 0; x < bounds.yMax; x++, xModel++)
        {
            for (int y = bounds.xMin, yModel = 0; y < bounds.xMax; y++, yModel++)
            {
                Vector3 cellPosition = field.CellToWorld(new Vector3Int(y, x, 0));
                Vector3 rootPosition = new Vector3(cellPosition.x + cellSize.x / 2, cellPosition.y + cellSize.y / 2, -1);

                model.view[xModel, yModel] = new Root(RootStages.STAGE_0);
                model.buffer[xModel, yModel] = new Root(RootStages.STAGE_0);
                roots[xModel, yModel] = Instantiate(rootPrefab, rootPosition, Quaternion.identity);
            }
        }

        model.view[8, 15].stage = RootStages.MAIN;
        model.view[8, 15]._growDirections[0] = true;   // up
        model.view[8, 15]._growDirections[1] = true;   // down
        model.view[8, 15]._growDirections[2] = true;   // left
        model.view[8, 15]._growDirections[3] = true;   // right

        model.buffer[8, 15].stage = RootStages.MAIN;
        model.buffer[8, 15]._growDirections[0] = true;   // up
        model.buffer[8, 15]._growDirections[1] = true;   // down
        model.buffer[8, 15]._growDirections[2] = true;   // left
        model.buffer[8, 15]._growDirections[3] = true;   // right

        field.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (growCountdown > 0)
            growCountdown -= Time.deltaTime;
        else
        {
            Grow();
            growCountdown += 5;
        }

        for (int x = 0; x < roots.GetLength(0); x++)
        {
            for (int y = 0; y < roots.GetLength(1); y++)
            {
                switch (model.view[x, y].stage)
                {
                    case RootStages.STAGE_0:
                        roots[x, y].SetActive(false);
                        break;
                    case RootStages.STAGE_1:
                        roots[x, y].SetActive(true);
                        roots[x, y].GetComponent<SpriteRenderer>().sprite = sprites[0];
                        break;
                    case RootStages.STAGE_2:
                        roots[x, y].GetComponent<SpriteRenderer>().sprite = sprites[1];
                        break;
                    case RootStages.STAGE_3:
                        roots[x, y].GetComponent<SpriteRenderer>().sprite = sprites[2];
                        break;
                    case RootStages.MAIN:
                        roots[x, y].GetComponent<SpriteRenderer>().sprite = sprites[3];
                        break;
                }
            }
        }
    }

    void Grow()
    {
        for (int x = 0; x < model.buffer.GetLength(0); x++)
        {
            for (int y = 0; y < model.buffer.GetLength(1); y++)
            {
                if (model.view[x, y].stage == RootStages.STAGE_0)
                    continue;
                // up
                if (model.view[x, y]._growDirections[0])
                {
                    Debug.Log($"{x} {y} up");
                    if (x + 1 < model.buffer.GetLength(0))
                    {
                        if (model.view[x + 1, y].stage != RootStages.STAGE_3 && model.view[x + 1, y].stage != RootStages.MAIN)
                        {
                            if (model.view[x + 1, y].stage == RootStages.STAGE_0)
                            {
                                model.buffer[x + 1, y]._growDirections[0] = true;
                                model.buffer[x + 1, y]._growDirections[1] = true;
                                model.buffer[x + 1, y]._growDirections[2] = true;
                                model.buffer[x + 1, y]._growDirections[3] = true;
                            }
                            model.buffer[x + 1, y].stage++;
                        }
                        else
                            model.buffer[x, y]._growDirections[0] = false;
                    }
                    else
                        model.buffer[x, y]._growDirections[0] = false;
                }

                // down
                if (model.view[x, y]._growDirections[1])
                {
                    Debug.Log($"{x} {y} down");
                    if (x - 1 >= 0)
                    {
                        if (model.view[x - 1, y].stage != RootStages.STAGE_3 && model.view[x - 1, y].stage != RootStages.MAIN)
                        {
                            if (model.view[x - 1, y].stage == RootStages.STAGE_0)
                            {
                                model.buffer[x - 1, y]._growDirections[0] = true;
                                model.buffer[x - 1, y]._growDirections[1] = true;
                                model.buffer[x - 1, y]._growDirections[2] = true;
                                model.buffer[x - 1, y]._growDirections[3] = true;
                            }
                            model.buffer[x - 1, y].stage++;
                        }
                        else
                            model.buffer[x, y]._growDirections[1] = false;
                    }
                    else
                        model.buffer[x, y]._growDirections[1] = false;
                }

                // left
                if (model.view[x, y]._growDirections[2])
                {
                    if (x - 1 >= 0)
                    {
                        if (y - 1 >= 0)
                        {
                            if (model.view[x, y - 1].stage != RootStages.STAGE_3 && model.view[x - 1, y].stage != RootStages.MAIN)
                            {
                                if (model.view[x, y - 1].stage == RootStages.STAGE_0)
                                {
                                    model.buffer[x, y - 1]._growDirections[0] = true;
                                    model.buffer[x, y - 1]._growDirections[1] = true;
                                    model.buffer[x, y - 1]._growDirections[2] = true;
                                    model.buffer[x, y - 1]._growDirections[3] = true;
                                }
                                Debug.Log($"{x} {y} left");
                                model.buffer[x, y - 1].stage++;
                            }
                            else
                                model.buffer[x, y]._growDirections[2] = false;
                        }
                        else
                            model.buffer[x, y]._growDirections[2] = false;
                    }
                }

                // right
                if (model.view[x, y]._growDirections[3])
                {
                    Debug.Log($"{x} {y} right");
                    if (y + 1 < model.buffer.GetLength(1))
                    {
                        if (model.view[x, y + 1].stage != RootStages.STAGE_3 && model.view[x, y + 1].stage != RootStages.MAIN)
                        {
                            if (model.view[x, y + 1].stage == RootStages.STAGE_0)
                            {
                                model.buffer[x, y + 1]._growDirections[0] = true;
                                model.buffer[x, y + 1]._growDirections[1] = true;
                                model.buffer[x, y + 1]._growDirections[2] = true;
                                model.buffer[x, y + 1]._growDirections[3] = true;
                            }
                            model.buffer[x, y + 1].stage++;
                        }
                        else
                            model.buffer[x, y]._growDirections[3] = false;
                    }
                    else
                        model.buffer[x, y]._growDirections[3] = false;
                }
            }
        }
        model.Swap();
    }
}
