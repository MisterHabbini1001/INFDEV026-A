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
        public List<Vector2> MergeSort<Vector2>(List<Vector2> unsortedList) 
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
                    Console.WriteLine("i is equal to " + i);
                    if (i > unsortedList.Count)
                    {
                        left[i] = unsortedList[i]; // Gives an error here
                    }
                }

                for (int i = middle; i < unsortedList.Count; i++)   // middle = 25   unsortedList.Count = 50
                {
                    Console.WriteLine("jsjhdfgsjhgd is equal to " + i);
                    if (i > unsortedList.Count)
                    {
                        right[i - middle] = unsortedList[i];
                    }
                    // i = 25 gaat goed
                    // i = 26 gaat FOUT FOUT FOUT FOUT FOUT
                }

                left = MergeSort(left);
                right = MergeSort(right);
            }

            return Merge<Vector2>(left, right);
        }

        public List<Vector2> Merge<Vector2>(List<Vector2> left, List<Vector2> right) 
        {
            List<Vector2> result = new List<Vector2>(left.Count + right.Count);

            int currentElement = 0;
            Microsoft.Xna.Framework.Vector2 vector1;
            vector1.X = 0;
            vector1.Y = 0;

            //Microsoft.Xna.Framework.Vector2 left_vector = result.ElementAt(0).;
            IEnumerable<Vector2> left_list = left.AsEnumerable<Vector2>();
            IEnumerable<Vector2> right_list = right.AsEnumerable<Vector2>();
            //Console.WriteLine(left_list.ElementAt(0));

            //Microsoft.Xna.Framework.Vector2 left_list = left.ConvertAll<Vector2, Microsoft.Xna.Framework.Vector2>

            while (left.Count > 0 || right.Count > 0)
            {
                if (left.Count > 0 && right.Count > 0)
                {
                    if (left[0].Equals(right[0]))           // NEEDS TO BE FIXED
                    // if (left[0].CompareTo(right[0]) < 0)
                    // if (vector1.Equals(vector2))
                    // if (left[0].Equals(right[0]))
                    // if (left[0].Equals(right[0]) < 0)
                    // if (left[0].Equals(right[0]) < vector1)
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

/*
public class MergeSortAlgorithm
    {
        public T[] MergeSort<T>(T[] unsortedList) where T : System.IComparable<T>
        {
            T[] left, right;
            int middle = unsortedList.Count / 2;

            left = new T[middle];
            right = new T[unsortedList.Count - middle];

            if (unsortedList.Count <= 1)
                return unsortedList;

            for (int i = 0; i < middle; i++)
            {
                left[i] = unsortedList[i];
            }

            for (int i = middle; i < unsortedList.Count; i++)
            {
                right[i - middle] = unsortedList[i];
            }

            left = MergeSort(left);

            right = MergeSort(right);


            return Merge<T>(left, right);
        }

        private T[] Merge<T>(T[] left, T[] right) where T : System.IComparable<T>
        {
            T[] result = new T[left.Count + right.Count];

            int currentElement = 0;

            while (left.Count > 0 || right.Count > 0)
            {
                if (left.Count > 0 && right.Count > 0)
                {
                    if (left[0].CompareTo(right[0]) < 0)
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
*/

/*

*/
