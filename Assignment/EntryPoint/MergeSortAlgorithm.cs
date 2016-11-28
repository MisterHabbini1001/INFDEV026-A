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
                return unsortedList.AsEnumerable<Vector2>(); // Converts unsortedList to IEnumerable first
            }

            else
            {
                var left = new List<Vector2>();  // 1st half of unsortedList
                var right = new List<Vector2>(); // 2nd half of unsortedList

                for (int i = 0; i < unsortedList.Count; i++)
                {
                    if (i % 2 > 0) // Checks if modulus of i is bigger than 0. This means condition is only true when i is uneven, since modulus for uneven numbers is 1
                    {
                        left.Add(unsortedList.ElementAt(i)); // When i is UNEVEN  
                    }
                    else
                    {
                        right.Add(unsortedList.ElementAt(i)); // When i is EVEN   
                    }
                }

                left = MergeSort(left, house).ToList<Vector2>(); // Converts to List first for Merge function
                right = MergeSort(right, house).ToList<Vector2>(); // Converts to List first for Merge function 

                return Merge(left, right, house); // Returns result of Merge function
            }
        }

        private static List<Vector2> Merge(List<Vector2> left, List<Vector2> right, Vector2 house) 
        {
            var result = new List<Vector2>(); // List that is returned at the end of function

            while (left.Count() > 0 && right.Count() > 0) // Goes to this code block when both LEFT and RIGHT list contain NO ELEMENTS
            {
                if (Vector2.Distance(left.First(), house) <= Vector2.Distance(right.First(), house)) // Compares distances of 1ST ELEMENT of both LEFT and RIGHT lists
                {
                    result.Add(left.First()); // Adds the 1st ELEMENT of left list to result list
                    left.RemoveAt(0);         // Removes element of left list at index 0
                }
                else
                {
                    result.Add(right.First()); // Adds the 1st ELEMENT of right list to result list
                    right.RemoveAt(0);         // Removes element of right list at index 0
                }
            }

            while (left.Count > 0) // Goes to this code block when RIGHT list contains NO ELEMENTS
            {
                result.Add(left.First()); // Adds the 1st ELEMENT of left list to result list
                left.RemoveAt(0);         // Removes element of left list at index 0
            }

            while (right.Count > 0) // Goes to this code block when LEFT list contains NO ELEMENTS
            {
                result.Add(right.First()); // Adds the 1st ELEMENT of right list to result list
                right.RemoveAt(0);         // Removes element of left list at index 0
            }

            return result; // Returns the final list
        }
    }
}








