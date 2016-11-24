using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Is meant for exercise 1 - Sorting

namespace EntryPoint
{
    public static class MergeSortAlgorithm
    {
        public static IEnumerable<Vector2> MergeSort(List<Vector2> unsortedList, Vector2 house) 
        {
            if (unsortedList.Count <= 1) // In cases that list contains 0 or only 1 element
            {
                return unsortedList;
            }

            var left = new List<Vector2>(); 
            var right = new List<Vector2>();

            for (int i = 0; i < unsortedList.Count; i++)
            {
                if (i % 2 > 0) // Checks 
                {
                    left.Add(unsortedList[i]);
                }
                else
                {
                    right.Add(unsortedList[i]);
                }
           }

            left = MergeSort(left, house).ToList<Vector2>();
            right = MergeSort(right, house).ToList<Vector2>();

            IEnumerable<Vector2> merge_result = Merge(left, right, house).AsEnumerable<Vector2>();
            return merge_result;
        }

        public static List<Vector2> Merge(List<Vector2> left, List<Vector2> right, Vector2 house) 
        {
            var result = new List<Vector2>();

            while (left.Count() > 0 && right.Count() > 0)
            {
                if (Vector2.Distance(left.First(), house) <= Vector2.Distance(right.First(), house))
                {
                    result.Add(left.First()); 
                    left.RemoveAt(0);
                }
                else
                {
                    result.Add(right.First());
                    right.RemoveAt(0);
                }
            }

            return result;
        }
    }
}






