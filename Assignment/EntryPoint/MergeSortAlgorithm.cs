using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Is meant for exercise 1 - Sorting

namespace EntryPoint
{
    public class MergeSortAlgorithm
    {
        public List<Vector2> MergeSort(List<Vector2> unsortedList, Vector2 house) 
        {
            if (unsortedList.Count <= 1)
            {
                return unsortedList;
            }

            var left = new List<Vector2>(); 
            var right = new List<Vector2>();

            for (int i = 0; i < unsortedList.Count; i++)
            {
                if (i % 2 > 0)
                {
                    left.Add(unsortedList[i]);
                }
                else
                {
                    right.Add(unsortedList[i]);
                }
            }

            left = MergeSort(left, house);
            right = MergeSort(right, house);
            
            return Merge(left, right, house);
        }

        public List<Vector2> Merge(List<Vector2> left, List<Vector2> right, Vector2 house) 
        {
            var result = new List<Vector2>(left.Count + right.Count);

            while (left.Count > 0 && right.Count > 0)
            {
                if (Vector2.Distance(left.First(), house) <= Vector2.Distance(right.First(), house))
                {
                    MoveValueToResult(left, result);
                }
                else
                {
                    MoveValueToResult(right, result);
                }
                
                while (left.Count > 0)
                {
                    MoveValueToResult(left, result);
                }
                while (right.Count > 0)
                {
                    MoveValueToResult(right, result);
                }
            }

            return result;
        }

        private static void MoveValueToResult(List<Vector2> list, List<Vector2> result)
        {
            result.Add(list.First());
            list.RemoveAt(0);
        }
    }
}


