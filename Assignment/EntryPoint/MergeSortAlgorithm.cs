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
        public List<Vector2> MergeSort<Vector2>(List<Vector2> unsortedList, Vector2 house) 
        {
            List<Vector2> left, right;
            int middle = unsortedList.Count / 2;

            left = new List<Vector2>(middle);
            right = new List<Vector2>(unsortedList.Count - middle);

            if (unsortedList.Count <= 1)
            {
                return unsortedList;
            }

            else
            {
                for (int i = 0; i < middle; i++)
                {
                    if (i > unsortedList.Count)
                    {
                        left[i] = unsortedList[i]; 
                    }
                }

                for (int i = middle; i < unsortedList.Count; i++)   
                {
                    if (i > unsortedList.Count)
                    {
                        right[i - middle] = unsortedList[i];
                    }
                }

                left = MergeSort(left, house);
                right = MergeSort(right, house);
            }

            return Merge<Vector2>(left, right);
        }

        public List<Vector2> Merge<Vector2>(List<Vector2> left, List<Vector2> right) 
        {
            List<Vector2> result = new List<Vector2>(left.Count + right.Count);
            int currentElement = 0;
            // Do something with Vector2.Distance here

            while (left.Count > 0 || right.Count > 0)
            {
                if (left.Count > 0 && right.Count > 0)
                {
                    if (left[0].Equals(right[0]))           
                    {
                        result[currentElement] = left[0];
                        left = left.Skip(1).ToList();
                        currentElement++;
                    }
                    else
                    {
                        result[currentElement] = right[0];
                        right = right.Skip(1).ToList();
                        currentElement++;
                    }
                }
                else if (left.Count > 0)
                {
                    result[currentElement] = left[0];
                    left = left.Skip(1).ToList();
                    currentElement++;
                }
                else if (right.Count > 0)
                {
                    result[currentElement] = right[0];
                    right = right.Skip(1).ToList();
                    currentElement++;
                }
            }

            return result;
        }
    }
}


