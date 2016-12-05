using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace EntryPoint
{
#if WINDOWS || LINUX
  public static class Program
  {
    [STAThread]
    static void Main()
    {
      var fullscreen = false;
      read_input:
      switch (Microsoft.VisualBasic.Interaction.InputBox("Which assignment shall run next? (1, 2, 3, 4, or q for quit)", "Choose assignment", VirtualCity.GetInitialValue()))
      {
        case "1":
          using (var game = VirtualCity.RunAssignment1(SortSpecialBuildingsByDistance, fullscreen))
            game.Run();
          break;
        case "2":
          using (var game = VirtualCity.RunAssignment2(FindSpecialBuildingsWithinDistanceFromHouse, fullscreen))
            game.Run();
          break;
        case "3":
          using (var game = VirtualCity.RunAssignment3(FindRoute, fullscreen))
            game.Run();
          break;
        case "4":
          using (var game = VirtualCity.RunAssignment4(FindRoutesToAll, fullscreen))
            game.Run();
          break;
        case "q":
          return;
      }
      goto read_input;
    }

    private static IEnumerable<Vector2> SortSpecialBuildingsByDistance(Vector2 house, IEnumerable<Vector2> specialBuildings) // EXERCISE 1 - Sorting  EXERCISE 1 - Sorting  EXERCISE 1 - Sorting  !!!!!!
    {
       return MergeSortAlgorithm.MergeSort(specialBuildings.ToList<Vector2>(), house); // Calls Mergesort function in static MergeSortAlgorithm class
    }

        //

    private static IEnumerable<IEnumerable<Vector2>> FindSpecialBuildingsWithinDistanceFromHouse( // EXERCISE 2 - Trees  EXERCISE 2 - Trees  EXERCISE 2 - Trees  EXERCISE 2 - Trees !!!!!!!!
      IEnumerable<Vector2> specialBuildings, 
      IEnumerable<Tuple<Vector2, float>> housesAndDistances)
    {
      List<Vector2> convert_specialBuildings = specialBuildings.ToList<Vector2>();                   // Conversion to list for easier looping later (in my opinion)
      List<Tuple<Vector2, float>> convert_list = housesAndDistances.ToList<Tuple<Vector2, float>>(); // Conversion to list for easier looping later (in my opinion)

      IEnumerable<IEnumerable<Vector2>> nigga_list = BinaryTreeAlgorithm.InsertIntoBinaryTree(convert_specialBuildings, convert_list);
      return nigga_list; // Calls InsertIntoBinaryTree function in static BinaryTreeAlgorithm class     
            
      // Giuseppe advies: Test elke functie apart (= oftewel unit testen).  Kijk ook of ik gewenste output krijg             
    }

    //

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

    //

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
  }
#endif
}

/*
    private static IEnumerable<Vector2> SortSpecialBuildingsByDistance(Vector2 house, IEnumerable<Vector2> specialBuildings) // EXERCISE 1 - Sorting  EXERCISE 1 - Sorting  EXERCISE 1 - Sorting  !!!!!!
    {
       MergeSortAlgorithm algorithm_stuff = new MergeSortAlgorithm();
       List<Vector2> unsorted_list = specialBuildings.ToList<Vector2>();
       algorithm_stuff.MergeSort<System.IComparable<Vector2>>(unsorted_list);
       IEnumerable<Vector2> sorted_list = algorithm_stuff.MergeSort<System.IComparable<Vector2>>(unsorted_list).AsEnumerable<Vector2>();
       return sorted_list;
       //return specialBuildings.OrderBy(v => Vector2.Distance(v, house));

       // STEP 1: Converteren naar List<T>
       // STEP 2: Maak Sort functie aan   die zichzelf aanroopt  recursive    Checken of list 1 element heeft vooraf, anders infinite recursive zooi
       // STEP 3: Maak Merge functie aan  die zichzelf NIET aanroept want statisch   Krijgt 2 lijsten binnen   Wordt aangeroepen door Sort
    }
*/



