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

            var new_left = left.AsEnumerable<Vector2>();
            var new_right = right.AsEnumerable<Vector2>();

            new_left = MergeSort(left, house);
            new_right = MergeSort(right, house);
            
            return Merge(new_left, new_right, house);
        }

        public static IEnumerable<Vector2> Merge(IEnumerable<Vector2> left, IEnumerable<Vector2> right, Vector2 house) 
        {
            var result = new List<Vector2>();
            int left_count = left.Count();
            int right_count = right.Count();

            while (left.Count() > 0 && right.Count() > 0)
            {
                if (Vector2.Distance(left.First(), house) <= Vector2.Distance(right.First(), house))
                {
                    MoveValueToResult(left.ToList<Vector2>(), result);
                }
                else
                {
                    MoveValueToResult(right.ToList<Vector2>(), result);
                }
                             
                while (left.Count() > 0)
                {
                    MoveValueToResult(left.ToList<Vector2>(), result);
                }
                while (right.Count() > 0)
                {
                    MoveValueToResult(right.ToList<Vector2>(), result);
                }
            }

            return result.AsEnumerable<Vector2>();
        }

        public static void MoveValueToResult(List<Vector2> list, List<Vector2> result)
        {
            result.Add(list.First()); //Gives OutOfMemoryException here !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            list.RemoveAt(0);
        }
    }
}

/*
private static List<Vector2> Merge(List<Vector2> left, List<Vector2> right, Vector2 house) 
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
*/

/*
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

            var new_left = left.AsEnumerable<Vector2>();
            var new_right = right.AsEnumerable<Vector2>();

            new_left = MergeSort(left, house);
            new_right = MergeSort(right, house);
            
            return Merge(new_left, new_right, house);
        }

        public static IEnumerable<Vector2> Merge(IEnumerable<Vector2> left, IEnumerable<Vector2> right, Vector2 house) 
        {
            var result = new List<Vector2>();
            int left_count = left.Count();

            while (left.Count() > 0 && right.Count() > 0)
            {
                if (Vector2.Distance(left.First(), house) <= Vector2.Distance(right.First(), house))
                {
                    MoveValueToResult(left.ToList<Vector2>(), result);
                }
                else
                {
                    MoveValueToResult(right.ToList<Vector2>(), result);
                }
                             
                while (left.Count() > 0)
                {
                    MoveValueToResult(left.ToList<Vector2>(), result);
                }
                while (right.Count() > 0)
                {
                    MoveValueToResult(right.ToList<Vector2>(), result);
                }
            }

            return result.AsEnumerable<Vector2>();
        }

        public static void MoveValueToResult(List<Vector2> list, List<Vector2> result)
        {
            result.Add(list.First()); //Gives OutOfMemoryException here !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            list.RemoveAt(0);
        }
    }
}
*/



