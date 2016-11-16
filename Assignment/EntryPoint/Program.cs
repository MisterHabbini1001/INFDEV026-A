using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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
    }

    //

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

*/

//Console.WriteLine(list_half_length);
//Console.WriteLine(sorted_list.Count()); // List contains 50 elements
/*
for(int i = 0; i < list_length; i++)
{
   Console.WriteLine(i);
   Console.WriteLine(sorted_list.ElementAt(i));
}
*/

//Console.WriteLine("Dont you cry no more");

/*
for (int i = 0; i < list_half_length - 1; i++)
{
  Console.WriteLine(i);
  Console.WriteLine(first_half_list.ElementAt(i));
}
*/

