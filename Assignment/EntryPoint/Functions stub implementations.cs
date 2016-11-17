using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntryPoint
{
    class Functions_stub_implementations
    {
        /*
         private static IEnumerable<Vector2> SortSpecialBuildingsByDistance(Vector2 house, IEnumerable<Vector2> specialBuildings) // EXERCISE 1 - Sorting  EXERCISE 1 - Sorting  EXERCISE 1 - Sorting  !!!!!!
        {
      return specialBuildings.OrderBy(v => Vector2.Distance(v, house));
    }

    private static IEnumerable<IEnumerable<Vector2>> FindSpecialBuildingsWithinDistanceFromHouse( // EXERCISE 2 - Trees  EXERCISE 2 - Trees  EXERCISE 2 - Trees  EXERCISE 2 - Trees !!!!!!!!
      IEnumerable<Vector2> specialBuildings, 
      IEnumerable<Tuple<Vector2, float>> housesAndDistances)
    {
      return
          from h in housesAndDistances
          select
            from s in specialBuildings
            where Vector2.Distance(h.Item1, s) <= h.Item2
            select s;
    }

    private static IEnumerable<Tuple<Vector2, Vector2>> FindRoute(Vector2 startingBuilding, // EXERCISE 3 - Graphs  Option 1: Dijkstra  EXERCISE 3 - Graphs  Option 1: Dijkstra  EXERCISE 3 - Graphs  Option 1: Dijkstra !!!!!!!!!
      Vector2 destinationBuilding, IEnumerable<Tuple<Vector2, Vector2>> roads)
    {
      var startingRoad = roads.Where(x => x.Item1.Equals(startingBuilding)).First();
      List<Tuple<Vector2, Vector2>> fakeBestPath = new List<Tuple<Vector2, Vector2>>() { startingRoad };
      var prevRoad = startingRoad;
      for (int i = 0; i < 30; i++)
      {
        prevRoad = (roads.Where(x => x.Item1.Equals(prevRoad.Item2)).OrderBy(x => Vector2.Distance(x.Item2, destinationBuilding)).First());
        fakeBestPath.Add(prevRoad);
      }
      return fakeBestPath;
    }

    private static IEnumerable<IEnumerable<Tuple<Vector2, Vector2>>> FindRoutesToAll(Vector2 startingBuilding, // EXERCISE 3 - Graphs  Option 2: Floyd-Marshall  EXERCISE 3 - Graphs  Option 2: Floyd-Marshall  EXERCISE 3 - Graphs  Option 2: Floyd-Marshall !!!!!!!!!
      IEnumerable<Vector2> destinationBuildings, IEnumerable<Tuple<Vector2, Vector2>> roads)
    {
      List<List<Tuple<Vector2, Vector2>>> result = new List<List<Tuple<Vector2, Vector2>>>();
      foreach (var d in destinationBuildings)
      {
        var startingRoad = roads.Where(x => x.Item1.Equals(startingBuilding)).First();
        List<Tuple<Vector2, Vector2>> fakeBestPath = new List<Tuple<Vector2, Vector2>>() { startingRoad };
        var prevRoad = startingRoad;
        for (int i = 0; i < 30; i++)
        {
          prevRoad = (roads.Where(x => x.Item1.Equals(prevRoad.Item2)).OrderBy(x => Vector2.Distance(x.Item2, d)).First());
          fakeBestPath.Add(prevRoad);
        }
        result.Add(fakeBestPath);
      }
      return result;
    }
        */
    }
}

/*
  private static IEnumerable<Vector2> SortSpecialBuildingsByDistance(Vector2 house, IEnumerable<Vector2> specialBuildings) // EXERCISE 1 - Sorting  EXERCISE 1 - Sorting  EXERCISE 1 - Sorting  !!!!!!
    {
       List<Vector2> sorted_list = specialBuildings.ToList<Vector2>();
       //Console.WriteLine(sorted_list.Count()); // List contains 50 elements
       int list_length = sorted_list.Count();  // list_length = 50  for 1st iteration
       int list_half_length = list_length / 2; // list_half_length = 25 for 1st iteration

       List<Vector2> first_half_list = sorted_list.GetRange(0, list_half_length);                   // (0, 25 - 1) = (0, 24) for 1st iteration
       IEnumerable<Vector2> converted_first_half_list = first_half_list.AsEnumerable<Vector2>();

       List<Vector2> second_half_list = sorted_list.GetRange(list_half_length, list_half_length);        // (25, 50 - 1) = (25, 49) for 1st iteration   count is number of elements in range
       IEnumerable<Vector2> converted_second_half_list = second_half_list.AsEnumerable<Vector2>();

       SortSpecialBuildingsByDistance(house, converted_first_half_list);
       SortSpecialBuildingsByDistance(house, converted_second_half_list);

       return specialBuildings.OrderBy(v => Vector2.Distance(v, house));
    }
*/

/*
    private static IEnumerable<Vector2> SortSpecialBuildingsByDistance(Vector2 house, IEnumerable<Vector2> specialBuildings) // EXERCISE 1 - Sorting  EXERCISE 1 - Sorting  EXERCISE 1 - Sorting  !!!!!!
    {
       //List<Vector2> sorted_list = specialBuildings.ToList<Vector2>();
       //int list_length = sorted_list.Count();  // list_length = 50  for 1st iteration
       //int list_half_length = list_length / 2; // list_half_length = 25 for 1st iteration

       //List<Vector2> first_half_list = sorted_list.GetRange(0, sorted_list.Count() / 2);                   // (0, 25 - 1) = (0, 24) for 1st iteration
       //List<Vector2> second_half_list = sorted_list.GetRange(sorted_list.Count() / 2, sorted_list.Count() / 2);        // (25, 50 - 1) = (25, 49) for 1st iteration   count is number of elements in range

       //SortSpecialBuildingsByDistance(house, first_half_list.AsEnumerable<Vector2>());
       //SortSpecialBuildingsByDistance(house, sorted_list.GetRange(0, sorted_list.Count() / 2).AsEnumerable<Vector2>()); //
       //SortSpecialBuildingsByDistance(house, sorted_list.GetRange(sorted_list.Count() / 2, sorted_list.Count() / 2).AsEnumerable<Vector2>());

       SortSpecialBuildingsByDistance(house, specialBuildings.ToList<Vector2>().GetRange(0, specialBuildings.ToList<Vector2>().Count() / 2).AsEnumerable<Vector2>()); //
       SortSpecialBuildingsByDistance(house, specialBuildings.ToList<Vector2>().GetRange(specialBuildings.ToList<Vector2>().Count() / 2, specialBuildings.ToList<Vector2>().Count() / 2).AsEnumerable<Vector2>());

       return specialBuildings.OrderBy(v => Vector2.Distance(v, house));

       // STEP 1: Converteren naar List<T>
       // STEP 2: Maak Sort functie aan   die zichzelf aanroopt  recursive    Checken of list 1 element heeft vooraf, anders infinite recursive zooi
       // STEP 3: Maak Merge functie aan  die zichzelf NIET aanroept want statisch   Krijgt 2 lijsten binnen   Wordt aangeroepen door Sort


    }
*/

// MERGESORTALGORITHMS

/*
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

            return Merge<Vector2>(left, right);
        }

        public List<Vector2> Merge<Vector2>(List<Vector2> left, List<Vector2> right) 
        {
            List<Vector2> result = new List<Vector2>(left.Count + right.Count);

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
}
*/
