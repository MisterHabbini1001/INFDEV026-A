using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntryPoint
{
    public static class DijkstraAlgorithm
    {
        public static IEnumerable<Tuple<Vector2, Vector2>> RoadDetermination(Vector2 startingBuilding, Vector2 destinationBuilding, List<Tuple<Vector2, Vector2>> roads) 
        {
            Graph graph = new Graph();                                                // Graph is empty
            graph = InsertGraph(graph, roads, startingBuilding, destinationBuilding); // Graph gets created here

            List<Tuple<Vector2, Vector2>> resultListBA = new List<Tuple<Vector2, Vector2>>();
            resultListBA = graph.ShortestPath(startingBuilding, destinationBuilding); // Shortest path is determined between startingBuilding and destinationBuilding: is then stored in resultListBA variable

            List<Tuple<Vector2, Vector2>> resultListAB = new List<Tuple<Vector2, Vector2>>(); // End result that should be returned
            while (resultListBA.Count > 0)
            {
                resultListAB.Add(resultListBA[resultListBA.Count - 1]); // Adds all elements from resultListBA to resultListAB. It goes from the LAST till the FIRST element of the list
                resultListBA.RemoveAt(resultListBA.Count - 1);          // To prevent an infinite loop of occuring
            }

            return resultListAB; // Returning end result
        } 

        private static Graph InsertGraph(Graph graph, List<Tuple<Vector2, Vector2>> roadslist, Vector2 startPoint, Vector2 endPoint)
        {
            graph.AddNode(startPoint); // Adds startingBuilding as FIRST NODE to the graph

            foreach (var road in roadslist)
            {
                graph.AddNode(road.Item1); // Item1 is STARTING POINT of ROAD
                graph.AddNode(road.Item2); // Item2 is END POINT of ROAD
                graph.AddRoad(road.Item1, road.Item2, (int)Vector2.Distance(road.Item1, road.Item2)); // Calculates the distance between the 2 points. Adds road to the graph
            }

            graph.AddNode(endPoint); // Adds destinationBuilding as LAST NODE to the graph
            return graph;
        } 
    }

    class Graph
    {
        Dictionary<Vector2, Dictionary<Vector2, int>> vertices = new Dictionary<Vector2, Dictionary<Vector2, int>>();

        public void AddNode(Vector2 location)
        {
            if (!vertices.ContainsKey(location)) // Checks if the vertices dictionary contains a key with the value given for location. 
                                                 // If that is not the case (= false), then it converts to true, which means the if block is entered
            {
                Dictionary<Vector2, int> emptyRoadsList = new Dictionary<Vector2, int>();
                vertices[location] = emptyRoadsList; // In the dictionary with the key for location, the value for emptyRoadsList
            }
        }

        public void AddRoad(Vector2 roadpoint1, Vector2 roadpoint2, int length_of_road)
        {
            if (!vertices[roadpoint1].ContainsKey(roadpoint2)) // Checks if the value in the vertices dictionary with the roadpoint1 key contains a key with the value for roadPoint2
                                                               // If that is not the case (= false), then it converts to true, which means the if block is entered
            {
               vertices[roadpoint1].Add(roadpoint2, length_of_road); // Adds a new dictionary (= new value for key roadpoint1) with vector2 = roadPoint2 and int = length_of_road to the vertices dictionary
            }

            if (!vertices[roadpoint2].ContainsKey(roadpoint1)) // Checks if the value in the vertices dictionary with the roadpoint2 key contains a key with the value for roadPoint1
                                                               // If that is not the case (= false), then it converts to true, which means the if block is entered
            {
               vertices[roadpoint2].Add(roadpoint1, length_of_road); // Adds a new dictionary (= new value for key roadpoint2) with vector1 = roadPoint2 and int = length_of_road to the vertices dictionary
            }
        }

        public List<Tuple<Vector2, Vector2>> ShortestPath(Vector2 startPoint, Vector2 endPoint)
        {
            Dictionary<Vector2, Vector2> previous = new Dictionary<Vector2, Vector2>();
            Dictionary<Vector2, int> distances = new Dictionary<Vector2, int>();
            List<Vector2> nodes = new List<Vector2>();

            List<Tuple<Vector2, Vector2>> path = null; // Resulting shortest path is empty in the beginning (stating the obvious here). 
                                                       // Is returned by the ShortestPath function

            foreach (var node in vertices)
            {
                if (node.Key == startPoint) // startPoint = Vector2 for the house.  Checks if it matches with any of the keys in the vertices dictionary
                {
                  distances[node.Key] = 0; // Its key in the distances dictionary will be set to 0
                }

                else
                {
                  distances[node.Key] = int.MaxValue; // Key in distances dictionary if set to the MaxValue that the int variable type 
                                                      // This is done in order to (somehow) achieve infinity
                }

                nodes.Add(node.Key); // Adds the current Vector2 of vertices to the nodes list
            }

            while (nodes.Count > 0)
            {
                nodes.Sort((x, y) => distances[x] - distances[y]); // Substracting int values for x and y keys of distances dictionary. x and y are both vectors. Comparison is distance
                var smallest = nodes[0]; // Selects the first element of the nodes list
                nodes.Remove(smallest); // Removes the smallest node from the nodes list. In other words, the 1st element of the list will always be removed

                if (smallest == endPoint)  // Checks f the smallest road is equal to the end road
                {
                    path = new List<Tuple<Vector2, Vector2>>(); // Path variable is instantiated

                    while (previous.ContainsKey(smallest))
                    {
                        Tuple<Vector2, Vector2> pair;
                        pair = new Tuple<Vector2, Vector2>(smallest, previous[smallest]);
                        path.Add(pair);
                        smallest = previous[smallest];
                    }

                    break; // Breaks the while loop with (previous.ContainsKey(smallest)). 
                }

                if (distances[smallest] == int.MaxValue)
                {
                  break; // Breaks the big while loop (with nodes.Count > 0) After that, the value for the path variable will be returned
                }

                foreach (var neighbor in vertices[smallest]) // Goes through each Dictionary value for the key (with value smallest) in vertices dictionary
                {
                    var totalDistance = distances[smallest] + neighbor.Value; // Calculate distance through each unvisited neightbor

                    if (totalDistance < distances[neighbor.Key]) // Update the neighbor's distance if smaller
                    {
                        distances[neighbor.Key] = totalDistance; // Update the neighbor's distance if smaller
                        previous[neighbor.Key] = smallest;
                    }
                }
           }

           return path;
        }
    }
}

/*
MAIN STEPS OF DIJKSTRA ALGORITHM:

1 = Pick the unvisited vertex with the lowest distance
2 = Calculate the distance through it to each unvisited neighbor
3 = Update the neighbor's distance if smaller
4 = Mark as visited when done with neighbors

*/




