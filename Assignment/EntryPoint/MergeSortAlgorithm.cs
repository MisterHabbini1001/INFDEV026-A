using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntryPoint
{
    public static class MergeSortAlgorithm
    {
        public static IEnumerable<Vector2> MergeSort(List<Vector2> unsortedList, Vector2 house) 
        {
            if (unsortedList.Count <= 1) // In case list has 1 or 0 elements
            {
              return unsortedList.AsEnumerable<Vector2>();
            } 

            else
            {
                List<Vector2> left  = new List<Vector2>();  // 1st half of unsortedList
                List<Vector2> right = new List<Vector2>(); // 2nd half of unsortedList

                for (int i = 0; i < unsortedList.Count; i++)
                {
                    Vector2 current_special_building = unsortedList.ElementAt(i); // Special building in unsorted list with index i

                    if (i % 2 > 0) // If i is UNEVEN (modulus always 1)
                    {
                       left.Add(current_special_building); // Adds element to left list
                    }

                    else // If i is EVEN (modulus always 0)
                    {
                      right.Add(current_special_building); // Adds element to right list
                    } 
                }

                left  = MergeSort(left, house).ToList<Vector2>(); // Converts to List first for Merge function
                right = MergeSort(right, house).ToList<Vector2>(); // Converts to List first for Merge function 

                return Merge(left, right, house); // Returns result of Merge function
            }
        }

        private static List<Vector2> Merge(List<Vector2> left, List<Vector2> right, Vector2 house) 
        {
            List<Vector2> result = new List<Vector2>(); // List that is returned at the end of function

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











