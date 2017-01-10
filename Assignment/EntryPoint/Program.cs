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

    private static IEnumerable<Vector2> SortSpecialBuildingsByDistance(Vector2 house, IEnumerable<Vector2> specialBuildings) // EXERCISE 1 - Sorting  
    {
       return MergeSortAlgorithm.MergeSort(specialBuildings.ToList<Vector2>(), house); // Calls Mergesort function in static MergeSortAlgorithm class
    }

    private static IEnumerable<IEnumerable<Vector2>> FindSpecialBuildingsWithinDistanceFromHouse(IEnumerable<Vector2> specialBuildings, IEnumerable<Tuple<Vector2, float>> housesAndDistances) // EXERCISE 2 - Trees  
    {
       return BinaryTreeAlgorithm.CreateInceptionList(specialBuildings.ToList<Vector2>(), housesAndDistances.ToList<Tuple<Vector2, float>>()); // Calls CreateInceptionList function in static BinaryTreeAlgorithm class
    }

    private static IEnumerable<Tuple<Vector2, Vector2>> FindRoute(Vector2 startingBuilding, Vector2 destinationBuilding, IEnumerable<Tuple<Vector2, Vector2>> roads) // EXERCISE 3 - Graphs  Option 1: Dijkstra
    {
       return DijkstraAlgorithm.RoadDetermination(startingBuilding, destinationBuilding, roads.ToList<Tuple<Vector2, Vector2>>()); // Calls RoadDetermination function in static DijkstraAlgorithm class
    }

    private static IEnumerable<IEnumerable<Tuple<Vector2, Vector2>>> FindRoutesToAll(Vector2 startingBuilding, IEnumerable<Vector2> destinationBuildings, IEnumerable<Tuple<Vector2, Vector2>> roads) // EXERCISE 3 - Graphs - Option 2: Floyd-Warshall
    {
       return FloydWarshallAlgorithm.DeterminationOfRoads(startingBuilding, destinationBuildings.ToList<Vector2>(), roads.ToList<Tuple<Vector2, Vector2>>()); // Calls DeterminationOfRoads function in static FloydWarshallAlgorithm class
    }
  }
#endif
}







