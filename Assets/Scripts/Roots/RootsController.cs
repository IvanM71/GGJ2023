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
        public RootsModel rootsModel;

        // Roots view
        public RootsView rootsView;

        // Sprites for this controller
        [SerializeField] Sprite[] sprites;

        // Root type for this controller
        [SerializeField] Enums.RootType rootType;

        public void UpdateView()
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

        public bool TryGrow()
        {
            List<Root> possibleGrow = new List<Root>();
            List<int> weights = new List<int>();
            for (int x = 0; x < rootsModel.roots.GetLength(0); x++)
            {
                for (int y = 0; y < rootsModel.roots.GetLength(1); y++)
                {
                    if (rootsModel.roots[x, y] == null) continue;
                    if (rootsModel.roots[x, y].stage == Enums.RootStages.STAGE_0)
                    {
                        int weight = 0;
                        if (x > 0)
                        {
                            if (rootsModel.roots[x - 1, y] != null)
                            {
                                if (rootsModel.roots[x - 1, y].type == rootType)
                                {
                                    if (rootsModel.roots[x - 1, y].stage == Enums.RootStages.MAIN)
                                        weight += (int)rootsModel.roots[x - 1, y].stage + 5;
                                    else
                                        weight += (int)rootsModel.roots[x - 1, y].stage;
                                }
                            }
                        }

                        if (y > 0)
                        {
                            if (rootsModel.roots[x, y - 1] != null)
                            {
                                if (rootsModel.roots[x, y - 1].type == rootType)
                                {
                                    if (rootsModel.roots[x, y - 1].stage == Enums.RootStages.MAIN)
                                        weight += (int)rootsModel.roots[x, y - 1].stage + 5;
                                    else
                                        weight += (int)rootsModel.roots[x, y - 1].stage;
                                }
                            }
                        }

                        if (x < rootsModel.roots.GetLength(0) - 1)
                        {
                            if (rootsModel.roots[x + 1, y] != null)
                            {
                                if (rootsModel.roots[x + 1, y].type == rootType)
                                {
                                    if (rootsModel.roots[x + 1, y].stage == Enums.RootStages.MAIN)
                                        weight += (int)rootsModel.roots[x + 1, y].stage + 5;
                                    else
                                        weight += (int)rootsModel.roots[x + 1, y].stage;
                                }
                            }
                        }

                        if (y < rootsModel.roots.GetLength(1) - 1)
                        {
                            if (rootsModel.roots[x, y + 1] != null)
                            {
                                if (rootsModel.roots[x, y + 1].type == rootType)
                                {
                                    if (rootsModel.roots[x, y + 1].stage == Enums.RootStages.MAIN)
                                        weight += (int)rootsModel.roots[x, y + 1].stage + 5;
                                    else
                                        weight += (int)rootsModel.roots[x, y + 1].stage;
                                }
                            }
                        }

                        if (weight > 0)
                        {
                            for (int i = 0; i < weight; i++)
                            {
                                possibleGrow.Add(rootsModel.roots[x, y]);
                                weights.Add(weight);
                            }
                        }
                    }
                }
            }

            if (possibleGrow.Count > 0)
            {
                int index = Random.Range(0, possibleGrow.Count);
                possibleGrow[index].stage++;
                possibleGrow[index].type = rootType;
                return true;
            }
            else
                return false;
        }

        public bool TryStageUp()
        {
            List<Root> possibleGrow = new List<Root>();
            List<int> weights = new List<int>();
            for (int x = 0; x < rootsModel.roots.GetLength(0); x++)
            {
                for (int y = 0; y < rootsModel.roots.GetLength(1); y++)
                {
                    if (rootsModel.roots[x, y] == null) continue;
                    if (rootsModel.roots[x, y].stage > Enums.RootStages.STAGE_0 && rootsModel.roots[x, y].stage < Enums.RootStages.STAGE_3)
                    {
                        int weight = 0;
                        int roundedBySame = 0;
                        int roundedByHigher = 0;
                        if (x > 0)
                        {
                            if (rootsModel.roots[x - 1, y] != null)
                            {
                                if (rootsModel.roots[x - 1, y].type == rootType)
                                {
                                    if (rootsModel.roots[x - 1, y].stage != Enums.RootStages.STAGE_0)
                                    {
                                        weight += (int)rootsModel.roots[x - 1, y].stage - 1;

                                        if (rootsModel.roots[x - 1, y].stage > rootsModel.roots[x, y].stage)
                                            roundedByHigher++;
                                        else if (rootsModel.roots[x - 1, y].stage > rootsModel.roots[x, y].stage)
                                            roundedBySame++;
                                    }
                                }
                            }
                        }

                        if (y > 0)
                        {
                            if (rootsModel.roots[x, y - 1] != null)
                            {
                                if (rootsModel.roots[x, y - 1].type == rootType)
                                {
                                    if (rootsModel.roots[x, y - 1].stage != Enums.RootStages.STAGE_0)
                                    {
                                        weight += (int)rootsModel.roots[x, y - 1].stage - 1;

                                        if (rootsModel.roots[x, y - 1].stage > rootsModel.roots[x, y].stage)
                                            roundedByHigher++;
                                        else if (rootsModel.roots[x, y - 1].stage > rootsModel.roots[x, y].stage)
                                            roundedBySame++;
                                    }
                                }
                            }
                        }

                        if (x < rootsModel.roots.GetLength(0) - 1)
                        {
                            if (rootsModel.roots[x + 1, y] != null)
                            {
                                if (rootsModel.roots[x + 1, y].type == rootType)
                                {
                                    if (rootsModel.roots[x + 1, y].stage != Enums.RootStages.STAGE_0)
                                    {
                                        weight += (int)rootsModel.roots[x + 1, y].stage - 1;

                                        if (rootsModel.roots[x + 1, y].stage > rootsModel.roots[x, y].stage)
                                            roundedByHigher++;
                                        else if (rootsModel.roots[x + 1, y].stage > rootsModel.roots[x, y].stage)
                                            roundedBySame++;
                                    }
                                }
                            }
                        }

                        if (y < rootsModel.roots.GetLength(1) - 1)
                        {
                            if (rootsModel.roots[x, y + 1] != null)
                            {
                                if (rootsModel.roots[x, y + 1].type == rootType)
                                {
                                    if (rootsModel.roots[x, y + 1].stage != Enums.RootStages.STAGE_0)
                                    {
                                        weight += (int)rootsModel.roots[x, y + 1].stage - 1;

                                        if (rootsModel.roots[x, y + 1].stage > rootsModel.roots[x, y].stage)
                                            roundedByHigher++;
                                        else if (rootsModel.roots[x, y + 1].stage > rootsModel.roots[x, y].stage)
                                            roundedBySame++;
                                    }
                                }
                            }
                        }

                        if (rootsModel.roots[x, y].stage == Enums.RootStages.STAGE_2)
                            weight += 1;
                        else if (rootsModel.roots[x, y].stage == Enums.RootStages.STAGE_1)
                            weight += 2;

                        if (roundedBySame + roundedByHigher < 3)
                            weight = 0;

                        weight += roundedByHigher;

                        if (weight > 0)
                        {
                            for (int i = 0; i < weight; i++)
                            {
                                possibleGrow.Add(rootsModel.roots[x, y]);
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
