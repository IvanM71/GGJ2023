using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RootsController : MonoBehaviour
{
    // Roots model
    [SerializeField] RootsModel model;

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
        // Get grid
        Grid grid = field.layoutGrid;
        BoundsInt bounds = field.cellBounds;
        Vector3 cellSize = field.cellSize;

        // Create model array
        model.roots = new Root[field.size.x, field.size.y];

        // Create view array
        roots = new GameObject[field.size.x, field.size.y];

        for (int x = bounds.xMin, modelX = 0; x < bounds.xMax; x++, modelX++)
        {
            for (int y = bounds.yMin, modelY = 0; y < bounds.yMax; y++, modelY++)
            {
                // Get left bottom point of tile
                Vector3 cellPosition = grid.CellToWorld(new Vector3Int(x, y, 0));

                // Get root position at center of tile
                Vector3 rootPosition = new Vector3(cellPosition.x + cellSize.x / 2, cellPosition.y + cellSize.y / 2, -1);

                // Set stage to 0
                model.roots[modelX, modelY] = new Root(RootStages.STAGE_0, new bool[4] { false, false, false, false });

                // Create root from prefab
                roots[modelX, modelY] = Instantiate(rootPrefab, rootPosition, Quaternion.identity);
            }
        }

        // Create two random main roots
        int randX = Random.Range(0, field.size.x);
        int randY = Random.Range(0, field.size.y);
        model.roots[randX, randY].stage = RootStages.MAIN;
        model.roots[randX, randY]._growDirections[0] = randY != 0;                  // up
        model.roots[randX, randY]._growDirections[1] = randY != field.size.y - 1;   // down
        model.roots[randX, randY]._growDirections[2] = randX != 0;                  // left
        model.roots[randX, randY]._growDirections[3] = randX != field.size.x - 1;       // right


        Debug.Log($"{randX} --- {randY}");

        foreach(var i in model.roots[randX, randY]._growDirections )
            Debug.Log(i);

        randX = Random.Range(0, field.size.x);
        randY = Random.Range(0, field.size.y);
        model.roots[randX, randY].stage = RootStages.MAIN;
        model.roots[randX, randY]._growDirections[0] = randY != 0;                  // up
        model.roots[randX, randY]._growDirections[1] = randY != field.size.y - 1;   // down
        model.roots[randX, randY]._growDirections[2] = randX != 0;                  // left
        model.roots[randX, randY]._growDirections[3] = randX != field.size.x - 1    ;       // right

        Debug.Log($"{randX} --- {randY}");

        foreach (var i in model.roots[randX, randY]._growDirections)
            Debug.Log(i);
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
                switch (model.roots[x, y].stage)
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
        for (int x = 0; x < model.roots.GetLength(0); x++)
        {
            for (int y = 0; y < model.roots.GetLength(1); y++)
            {
                // up
                if (model.roots[x, y]._growDirections[0])
                {
                    if (model.roots[x, y + 1].stage != RootStages.STAGE_3 && model.roots[x, y - 1].stage != RootStages.MAIN)
                    {
                        model.roots[x, y + 1].stage += 1;
                        model.roots[x, y + 1]._growDirections[0] = true;
                        model.roots[x, y + 1]._growDirections[1] = false;
                        model.roots[x, y + 1]._growDirections[2] = true;
                        model.roots[x, y + 1]._growDirections[3] = true;
                    }
                }

                // down
                if (model.roots[x, y]._growDirections[1])
                {
                    if (model.roots[x, y - 1].stage != RootStages.STAGE_3 && model.roots[x, y + 1].stage != RootStages.MAIN)
                    {
                        model.roots[x, y - 1].stage += 1;
                        model.roots[x, y - 1]._growDirections[0] = false;
                        model.roots[x, y - 1]._growDirections[1] = true;
                        model.roots[x, y - 1]._growDirections[2] = true;
                        model.roots[x, y - 1]._growDirections[3] = true;
                    }
                }

                // left
                if (model.roots[x, y]._growDirections[2])
                {
                    if (model.roots[x - 1, y].stage != RootStages.STAGE_3 && model.roots[x - 1, y].stage != RootStages.MAIN)
                    {
                        model.roots[x - 1, y].stage += 1;
                        model.roots[x - 1, y]._growDirections[0] = true;
                        model.roots[x - 1, y]._growDirections[1] = true;
                        model.roots[x - 1, y]._growDirections[2] = false;
                        model.roots[x - 1, y]._growDirections[3] = true;
                    }
                }

                // right
                if (model.roots[x, y]._growDirections[3])
                {
                    if (model.roots[x + 1, y].stage != RootStages.STAGE_3 && model.roots[x + 1, y].stage != RootStages.MAIN)
                    {
                        model.roots[x + 1, y].stage += 1;
                        model.roots[x + 1, y]._growDirections[0] = true;
                        model.roots[x + 1, y]._growDirections[1] = true;
                        model.roots[x + 1, y]._growDirections[2] = true;
                        model.roots[x + 1, y]._growDirections[3] = false;
                    }
                }
            }
        }
    }
}
