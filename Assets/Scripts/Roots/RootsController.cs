using System.Collections.Generic;
using UnityEngine;

namespace Apollo11.Roots
{
    public class RootsController : MonoBehaviour
    {
        // Roots model
        public RootsModel rootsModel;

        // Sprites for this controller
        [SerializeField] Sprite[] sprites;

        // Root type for this controller
        [SerializeField] public Enums.RootType rootType;

        private Vector2Int mainRootPos = new Vector2Int(-1, -1);

        public Vector2Int? TryGrow()
        {
            List<Root> possibleGrow = new List<Root>();
            List<Vector2Int> checkNext = new List<Vector2Int>();
            List<Vector2Int> possibleGrowIndexes = new List<Vector2Int>();
            List<Root> isChecked = new List<Root>();

            if (mainRootPos.x < 0)
                FindMain();

            if(mainRootPos.x >= 0)
                checkNext.Add(mainRootPos);

            while (checkNext.Count > 0)
                possibleGrowIndexes.AddRange(CheckNextGrow(checkNext, isChecked));

            #region Old algorithm
            //List<int> weights = new List<int>();
            //for (int x = 0; x < rootsModel.roots.GetLength(0); x++)
            //{
            //    for (int y = 0; y < rootsModel.roots.GetLength(1); y++)
            //    {
            //        if (rootsModel.roots[x, y] == null) continue;
            //        if (rootsModel.roots[x, y].Stage == Enums.RootStages.STAGE_0)
            //        {
            //            int weight = 0;
            //            if (x > 0)
            //            {
            //                if (rootsModel.roots[x - 1, y] != null)
            //                {
            //                    if (rootsModel.roots[x - 1, y].Type == rootType)
            //                    {
            //                        if (rootsModel.roots[x - 1, y].Stage == Enums.RootStages.MAIN)
            //                            weight += (int)rootsModel.roots[x - 1, y].Stage + 5;
            //                        else
            //                            weight += (int)rootsModel.roots[x - 1, y].Stage;
            //                    }
            //                }
            //            }

            //            if (y > 0)
            //            {
            //                if (rootsModel.roots[x, y - 1] != null)
            //                {
            //                    if (rootsModel.roots[x, y - 1].Type == rootType)
            //                    {
            //                        if (rootsModel.roots[x, y - 1].Stage == Enums.RootStages.MAIN)
            //                            weight += (int)rootsModel.roots[x, y - 1].Stage + 5;
            //                        else
            //                            weight += (int)rootsModel.roots[x, y - 1].Stage;
            //                    }
            //                }
            //            }

            //            if (x < rootsModel.roots.GetLength(0) - 1)
            //            {
            //                if (rootsModel.roots[x + 1, y] != null)
            //                {
            //                    if (rootsModel.roots[x + 1, y].Type == rootType)
            //                    {
            //                        if (rootsModel.roots[x + 1, y].Stage == Enums.RootStages.MAIN)
            //                            weight += (int)rootsModel.roots[x + 1, y].Stage + 5;
            //                        else
            //                            weight += (int)rootsModel.roots[x + 1, y].Stage;
            //                    }
            //                }
            //            }

            //            if (y < rootsModel.roots.GetLength(1) - 1)
            //            {
            //                if (rootsModel.roots[x, y + 1] != null)
            //                {
            //                    if (rootsModel.roots[x, y + 1].Type == rootType)
            //                    {
            //                        if (rootsModel.roots[x, y + 1].Stage == Enums.RootStages.MAIN)
            //                            weight += (int)rootsModel.roots[x, y + 1].Stage + 5;
            //                        else
            //                            weight += (int)rootsModel.roots[x, y + 1].Stage;
            //                    }
            //                }
            //            }

            //            if (weight > 0)
            //            {
            //                for (int i = 0; i < weight; i++)
            //                {
            //                    possibleGrow.Add(rootsModel.roots[x, y]);
            //                    weights.Add(weight);
            //                }
            //            }
            //        }
            //    }
            //}
            #endregion

            if (possibleGrowIndexes.Count > 0)
            {
                int index = Random.Range(0, possibleGrowIndexes.Count);
                int x = possibleGrowIndexes[index].x;
                int y = possibleGrowIndexes[index].y;
                rootsModel.roots[x, y].Stage++;
                rootsModel.roots[x, y].Type = rootType;
                //possibleGrow[in dex].Stage++;
                //possibleGrow[index].Type = rootType;
                return possibleGrowIndexes[index];
            }
            else
                return null;
        }

        public Vector2Int? TryStageUp()
        {
            List<Vector2Int> possibleGrowIndexes = new List<Vector2Int>();
            List<Vector2Int> checkNext = new List<Vector2Int>();
            List<Root> isChecked = new List<Root>();

            if (mainRootPos.x < 0)
                FindMain();

            if (mainRootPos.x >= 0)
                checkNext.Add(mainRootPos);

            while (checkNext.Count > 0)
                possibleGrowIndexes.AddRange(CheckNextUp(checkNext, isChecked));

            #region Old algorithm
            //List<int> weights = new List<int>();
            //for (int x = 0; x < rootsModel.roots.GetLength(0); x++)
            //{
            //    for (int y = 0; y < rootsModel.roots.GetLength(1); y++)
            //    {
            //        if (rootsModel.roots[x, y] == null) continue;
            //        if (rootsModel.roots[x, y].Stage > Enums.RootStages.STAGE_0 && rootsModel.roots[x, y].Stage < Enums.RootStages.STAGE_3)
            //        {
            //            int weight = 0;
            //            int roundedBySame = 0;
            //            int roundedByHigher = 0;
            //            if (x > 0)
            //            {
            //                if (rootsModel.roots[x - 1, y] != null)
            //                {
            //                    if (rootsModel.roots[x - 1, y].Type == rootType)
            //                    {
            //                        if (rootsModel.roots[x - 1, y].Stage != Enums.RootStages.STAGE_0)
            //                        {
            //                            weight += (int)rootsModel.roots[x - 1, y].Stage - 1;

            //                            if (rootsModel.roots[x - 1, y].Stage > rootsModel.roots[x, y].Stage)
            //                                roundedByHigher++;
            //                            else if (rootsModel.roots[x - 1, y].Stage > rootsModel.roots[x, y].Stage)
            //                                roundedBySame++;
            //                        }
            //                    }
            //                }
            //            }

            //            if (y > 0)
            //            {
            //                if (rootsModel.roots[x, y - 1] != null)
            //                {
            //                    if (rootsModel.roots[x, y - 1].Type == rootType)
            //                    {
            //                        if (rootsModel.roots[x, y - 1].Stage != Enums.RootStages.STAGE_0)
            //                        {
            //                            weight += (int)rootsModel.roots[x, y - 1].Stage - 1;

            //                            if (rootsModel.roots[x, y - 1].Stage > rootsModel.roots[x, y].Stage)
            //                                roundedByHigher++;
            //                            else if (rootsModel.roots[x, y - 1].Stage > rootsModel.roots[x, y].Stage)
            //                                roundedBySame++;
            //                        }
            //                    }
            //                }
            //            }

            //            if (x < rootsModel.roots.GetLength(0) - 1)
            //            {
            //                if (rootsModel.roots[x + 1, y] != null)
            //                {
            //                    if (rootsModel.roots[x + 1, y].Type == rootType)
            //                    {
            //                        if (rootsModel.roots[x + 1, y].Stage != Enums.RootStages.STAGE_0)
            //                        {
            //                            weight += (int)rootsModel.roots[x + 1, y].Stage - 1;

            //                            if (rootsModel.roots[x + 1, y].Stage > rootsModel.roots[x, y].Stage)
            //                                roundedByHigher++;
            //                            else if (rootsModel.roots[x + 1, y].Stage > rootsModel.roots[x, y].Stage)
            //                                roundedBySame++;
            //                        }
            //                    }
            //                }
            //            }

            //            if (y < rootsModel.roots.GetLength(1) - 1)
            //            {
            //                if (rootsModel.roots[x, y + 1] != null)
            //                {
            //                    if (rootsModel.roots[x, y + 1].Type == rootType)
            //                    {
            //                        if (rootsModel.roots[x, y + 1].Stage != Enums.RootStages.STAGE_0)
            //                        {
            //                            weight += (int)rootsModel.roots[x, y + 1].Stage - 1;

            //                            if (rootsModel.roots[x, y + 1].Stage > rootsModel.roots[x, y].Stage)
            //                                roundedByHigher++;
            //                            else if (rootsModel.roots[x, y + 1].Stage > rootsModel.roots[x, y].Stage)
            //                                roundedBySame++;
            //                        }
            //                    }
            //                }
            //            }

            //            if (rootsModel.roots[x, y].Stage == Enums.RootStages.STAGE_2)
            //                weight += 1;
            //            else if (rootsModel.roots[x, y].Stage == Enums.RootStages.STAGE_1)
            //                weight += 2;

            //            if (roundedBySame + roundedByHigher < 3)
            //                weight = 0;

            //            weight += roundedByHigher;

            //            if (weight > 0)
            //            {
            //                for (int i = 0; i < weight; i++)
            //                {
            //                    possibleGrow.Add(rootsModel.roots[x, y]);
            //                    weights.Add(weight);
            //                }
            //            }
            //        }
            //    }
            //}
            #endregion

            if (possibleGrowIndexes.Count > 0)
            {
                int index = Random.Range(0, possibleGrowIndexes.Count);
                int x = possibleGrowIndexes[index].x;
                int y = possibleGrowIndexes[index].y;
                rootsModel.roots[x, y].Stage++;
                return possibleGrowIndexes[index];
            }
            else
                return null;
        }

        private void FindMain()
        {
            for(int i = 0; i < rootsModel.roots.GetLength(0); i++) 
            {
                for(int j = 0; j < rootsModel.roots.GetLength(1); j++)
                {
                    if (rootsModel.roots[i, j] is null) continue;
                    if(rootsModel.roots[i, j].Stage == Enums.RootStages.MAIN && rootsModel.roots[i,j].Type == rootType)
                    {
                        mainRootPos = new Vector2Int(i, j);
                        return;
                    }
                }
            }
        }

        //private List<Root> CheckNextGrow(List<Vector2Int> checkNext, List<Root> isChecked)
        //{
        //    var possibleGrow = new List<Root>();
        //    var newCheckNext = new List<Vector2Int>();
        //    for(int i = 0; i < checkNext.Count; i++)
        //    {
        //        int x = checkNext[i].x;
        //        int y = checkNext[i].y;

        //        if (x < rootsModel.roots.GetLength(0) - 1)
        //        {
        //            if (rootsModel.roots[x + 1, y] is not null)
        //            {
        //                if (rootsModel.roots[x + 1, y].Stage == Enums.RootStages.STAGE_0)
        //                    possibleGrow.Add(rootsModel.roots[x + 1, y]);
        //                else if (rootsModel.roots[x + 1, y].Type == rootType && !isChecked.Contains(rootsModel.roots[x + 1, y]))
        //                    newCheckNext.Add(new Vector2Int(x + 1, y));
        //            }
        //        }

        //        if (x > 0)
        //        {
        //            if (rootsModel.roots[x - 1, y] is not null)
        //            {
        //                if (rootsModel.roots[x - 1, y].Stage == Enums.RootStages.STAGE_0)
        //                    possibleGrow.Add(rootsModel.roots[x - 1, y]);
        //                else if (rootsModel.roots[x - 1, y].Type == rootType && !isChecked.Contains(rootsModel.roots[x - 1, y]))
        //                    newCheckNext.Add(new Vector2Int(x - 1, y));
        //            }
        //        }


        //        if (y < rootsModel.roots.GetLength(1) - 1)
        //        {
        //            if (rootsModel.roots[x, y + 1] is not null)
        //            {
        //                if (rootsModel.roots[x, y + 1].Stage == Enums.RootStages.STAGE_0)
        //                    possibleGrow.Add(rootsModel.roots[x, y + 1]);
        //                else if (rootsModel.roots[x, y + 1].Type == rootType && !isChecked.Contains(rootsModel.roots[x, y + 1]))
        //                    newCheckNext.Add(new Vector2Int(x, y + 1));
        //            }
        //        }

        //        if (y > 0)
        //        {
        //            if (rootsModel.roots[x, y - 1] is not null)
        //            {
        //                if (rootsModel.roots[x, y - 1].Stage == Enums.RootStages.STAGE_0)
        //                    possibleGrow.Add(rootsModel.roots[x, y - 1]);
        //                else if (rootsModel.roots[x, y - 1].Type == rootType && !isChecked.Contains(rootsModel.roots[x, y - 1]))
        //                    newCheckNext.Add(new Vector2Int(x, y - 1));
        //            }
        //        }

        //        isChecked.Add(rootsModel.roots[x, y]);
        //    }

        //    checkNext.Clear();
        //    checkNext.AddRange(newCheckNext);

        //    return possibleGrow;
        //}
        private List<Vector2Int> CheckNextGrow(List<Vector2Int> checkNext, List<Root> isChecked)
        {
            var possibleGrowIndexes = new List<Vector2Int>();
            var newCheckNext = new List<Vector2Int>();
            for(int i = 0; i < checkNext.Count; i++)
            {
                int x = checkNext[i].x;
                int y = checkNext[i].y;

                if (x < rootsModel.roots.GetLength(0) - 1)
                {
                    if (rootsModel.roots[x + 1, y] is not null)
                    {
                        if (rootsModel.roots[x + 1, y].Stage == Enums.RootStages.STAGE_0)
                            possibleGrowIndexes.Add(new Vector2Int(x + 1, y));
                        else if (rootsModel.roots[x + 1, y].Type == rootType && !isChecked.Contains(rootsModel.roots[x + 1, y]))
                            newCheckNext.Add(new Vector2Int(x + 1, y));
                    }
                }

                if (x > 0)
                {
                    if (rootsModel.roots[x - 1, y] is not null)
                    {
                        if (rootsModel.roots[x - 1, y].Stage == Enums.RootStages.STAGE_0)
                            possibleGrowIndexes.Add(new Vector2Int(x - 1, y));
                        else if (rootsModel.roots[x - 1, y].Type == rootType && !isChecked.Contains(rootsModel.roots[x - 1, y]))
                            newCheckNext.Add(new Vector2Int(x - 1, y));
                    }
                }


                if (y < rootsModel.roots.GetLength(1) - 1)
                {
                    if (rootsModel.roots[x, y + 1] is not null)
                    {
                        if (rootsModel.roots[x, y + 1].Stage == Enums.RootStages.STAGE_0)
                            possibleGrowIndexes.Add(new Vector2Int(x, y + 1));
                        else if (rootsModel.roots[x, y + 1].Type == rootType && !isChecked.Contains(rootsModel.roots[x, y + 1]))
                            newCheckNext.Add(new Vector2Int(x, y + 1));
                    }
                }

                if (y > 0)
                {
                    if (rootsModel.roots[x, y - 1] is not null)
                    {
                        if (rootsModel.roots[x, y - 1].Stage == Enums.RootStages.STAGE_0)
                            possibleGrowIndexes.Add(new Vector2Int(x, y - 1));
                        else if (rootsModel.roots[x, y - 1].Type == rootType && !isChecked.Contains(rootsModel.roots[x, y - 1]))
                            newCheckNext.Add(new Vector2Int(x, y - 1));
                    }
                }

                isChecked.Add(rootsModel.roots[x, y]);
            }

            checkNext.Clear();
            checkNext.AddRange(newCheckNext);

            return possibleGrowIndexes;
        }

        //private List<Root> CheckNextUp(List<Vector2Int> checkNext, List<Root> isChecked)
        //{
        //    var possibleGrow = new List<Root>();
        //    var newCheckNext = new List<Vector2Int>();
        //    for (int i = 0; i < checkNext.Count; i++)
        //    {
        //        int x = checkNext[i].x;
        //        int y = checkNext[i].y;
        //        if (x < rootsModel.roots.GetLength(0) - 1)
        //        {
        //            if (rootsModel.roots[x + 1, y] is not null)
        //            {
        //                if (rootsModel.roots[x + 1, y].Stage < Enums.RootStages.STAGE_3
        //                   && rootsModel.roots[x + 1, y].Stage > Enums.RootStages.STAGE_0
        //                   && rootsModel.roots[x + 1, y].Type == rootType)
        //                {
        //                    possibleGrow.Add(rootsModel.roots[x + 1, y]);

        //                    if (!isChecked.Contains(rootsModel.roots[x + 1, y]))
        //                        newCheckNext.Add(new Vector2Int(x + 1, y));
        //                }
        //            }
        //        }

        //        if (x > 0)
        //        {
        //            if (rootsModel.roots[x - 1, y] is not null)
        //            {
        //                if (rootsModel.roots[x - 1, y].Stage < Enums.RootStages.STAGE_3
        //                   && rootsModel.roots[x - 1, y].Stage > Enums.RootStages.STAGE_0
        //                   && rootsModel.roots[x - 1, y].Type == rootType)
        //                {
        //                    possibleGrow.Add(rootsModel.roots[x - 1, y]);

        //                    if (!isChecked.Contains(rootsModel.roots[x - 1, y]))
        //                        newCheckNext.Add(new Vector2Int(x - 1, y));
        //                }
        //            }
        //        }


        //        if (y < rootsModel.roots.GetLength(1) - 1)
        //        {
        //            if (rootsModel.roots[x, y + 1] is not null)
        //            {
        //                if (rootsModel.roots[x, y + 1].Stage < Enums.RootStages.STAGE_3
        //                   && rootsModel.roots[x, y + 1].Stage > Enums.RootStages.STAGE_0
        //                   && rootsModel.roots[x, y + 1].Type == rootType)
        //                {
        //                    possibleGrow.Add(rootsModel.roots[x, y + 1]);

        //                    if (!isChecked.Contains(rootsModel.roots[x, y + 1]))
        //                        newCheckNext.Add(new Vector2Int(x, y + 1));
        //                }
        //            }
        //        }

        //        if (y > 0)
        //        {
        //            if (rootsModel.roots[x, y - 1] is not null)
        //            {
        //                if (rootsModel.roots[x, y - 1].Stage < Enums.RootStages.STAGE_3
        //                   && rootsModel.roots[x, y - 1].Stage > Enums.RootStages.STAGE_0
        //                   && rootsModel.roots[x, y - 1].Type == rootType)
        //                {
        //                    possibleGrow.Add(rootsModel.roots[x, y - 1]);

        //                    if (!isChecked.Contains(rootsModel.roots[x, y - 1]))
        //                        newCheckNext.Add(new Vector2Int(x , y - 1));
        //                }
        //            }
        //        }

        //        isChecked.Add(rootsModel.roots[x, y]);
        //    }

        //    checkNext.Clear();
        //    checkNext.AddRange(newCheckNext);

        //    return possibleGrow;
        //}
        private List<Vector2Int> CheckNextUp(List<Vector2Int> checkNext, List<Root> isChecked)
        {
            var possibleGrow = new List<Vector2Int>();
            var newCheckNext = new List<Vector2Int>();
            for (int i = 0; i < checkNext.Count; i++)
            {
                int x = checkNext[i].x;
                int y = checkNext[i].y;
                if (x < rootsModel.roots.GetLength(0) - 1)
                {
                    if (rootsModel.roots[x + 1, y] is not null)
                    {
                        if (rootsModel.roots[x + 1, y].Stage < Enums.RootStages.STAGE_3
                           && rootsModel.roots[x + 1, y].Stage > Enums.RootStages.STAGE_0
                           && rootsModel.roots[x + 1, y].Type == rootType)
                        {
                            possibleGrow.Add(new Vector2Int(x + 1, y));

                            if (!isChecked.Contains(rootsModel.roots[x + 1, y]))
                                newCheckNext.Add(new Vector2Int(x + 1, y));
                        }
                    }
                }

                if (x > 0)
                {
                    if (rootsModel.roots[x - 1, y] is not null)
                    {
                        if (rootsModel.roots[x - 1, y].Stage < Enums.RootStages.STAGE_3
                           && rootsModel.roots[x - 1, y].Stage > Enums.RootStages.STAGE_0
                           && rootsModel.roots[x - 1, y].Type == rootType)
                        {
                            possibleGrow.Add(new Vector2Int(x - 1, y));

                            if (!isChecked.Contains(rootsModel.roots[x - 1, y]))
                                newCheckNext.Add(new Vector2Int(x - 1, y));
                        }
                    }
                }


                if (y < rootsModel.roots.GetLength(1) - 1)
                {
                    if (rootsModel.roots[x, y + 1] is not null)
                    {
                        if (rootsModel.roots[x, y + 1].Stage < Enums.RootStages.STAGE_3
                           && rootsModel.roots[x, y + 1].Stage > Enums.RootStages.STAGE_0
                           && rootsModel.roots[x, y + 1].Type == rootType)
                        {
                            possibleGrow.Add(new Vector2Int(x, y + 1));

                            if (!isChecked.Contains(rootsModel.roots[x, y + 1]))
                                newCheckNext.Add(new Vector2Int(x, y + 1));
                        }
                    }
                }

                if (y > 0)
                {
                    if (rootsModel.roots[x, y - 1] is not null)
                    {
                        if (rootsModel.roots[x, y - 1].Stage < Enums.RootStages.STAGE_3
                           && rootsModel.roots[x, y - 1].Stage > Enums.RootStages.STAGE_0
                           && rootsModel.roots[x, y - 1].Type == rootType)
                        {
                            possibleGrow.Add(new Vector2Int(x, y - 1));

                            if (!isChecked.Contains(rootsModel.roots[x, y - 1]))
                                newCheckNext.Add(new Vector2Int(x , y - 1));
                        }
                    }
                }

                isChecked.Add(rootsModel.roots[x, y]);
            }

            checkNext.Clear();
            checkNext.AddRange(newCheckNext);

            return possibleGrow;
        }
    }
}
