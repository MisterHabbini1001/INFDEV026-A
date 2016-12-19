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
            graph.DisplayGraph();                                                     // Graph gets displayed here on the console

            return graph.ShortestPath(startingBuilding, destinationBuilding); // Shortest path is determined between startingBuilding and destinationBuilding: is then stored in resultListBA variable
        }

        static Graph InsertGraph(Graph graph, List<Tuple<Vector2, Vector2>> roadslist, Vector2 startPoint, Vector2 endPoint)
        {
            graph.AddNode(startPoint); // Adds startingBuilding as FIRST NODE to the Graph

            foreach (var road in roadslist)
            {
                graph.AddNode(road.Item1);                                                            // Item1 is STARTING POINT of ROAD
                graph.AddNode(road.Item2);                                                            // Item2 is END POINT of ROAD
                graph.AddRoad(road.Item1, road.Item2, (int)Vector2.Distance(road.Item1, road.Item2)); // Calculates the distance between the 2 points. Adds road to the Graph
            }

            graph.AddNode(endPoint); // Adds destinationBuilding as LAST NODE to the Graph
            return graph;
        } 
    }

    class Graph
    {
        Dictionary<Vector2, Dictionary<Vector2, int>> vertices = new Dictionary<Vector2, Dictionary<Vector2, int>>(); // Collection of vertices in the graph. Each vertice is a key, which has as value one of the vertices it is connected to

        public void AddNode(Vector2 location) // Adds a node to the graph with the value vor the location parameter
        {
            if (!vertices.ContainsKey(location)) // Checks if the vertices dictionary contains a key with the value given for location. 
                                                 // If that is not the case (= false), then it converts to true, which means the if block is entered
            {
                Dictionary<Vector2, int> emptyRoadsList = new Dictionary<Vector2, int>();
                vertices[location] = emptyRoadsList; // In the dictionary with the key for location, the value for emptyRoadsList
            }
        }

        public void AddRoad(Vector2 roadpoint1, Vector2 roadpoint2, int length_of_road) // Adds a road between two points on the map. The length between the points is also given
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

        public void DisplayGraph() // Displays the vertices in the graph on to the console
        {
          foreach(var vertex in vertices) // For each dictionary in the vertices dictionary
          {
             Console.WriteLine("Current vertex in the graph with vector2 value: " + vertex.Key); // Display current Vector2 in vertices
             foreach (var vertex_neighbor in vertex.Value) // Goes through all the neighbors for current Vector2
             {
                Console.WriteLine("Neighbor vertex: " + vertex_neighbor.Key + " has length: " + vertex_neighbor.Value + " between current vertex: " + vertex.Key); // Display current neighbor with corresponding distance
             }
          }
        }

        public List<Tuple<Vector2, Vector2>> ShortestPath(Vector2 startPoint, Vector2 endPoint)
        {
            Dictionary<Vector2, Vector2> previous_neighbors = new Dictionary<Vector2, Vector2>(); // While calculating the shortest path, this variable will store the previous neighbors of a node when it is done with them.
            Dictionary<Vector2, int> node_total_distances = new Dictionary<Vector2, int>(); // Variable that holds the shortest distance between nodes and all the other nodes in the graph
            List<Vector2> nodes = new List<Vector2>(); // List that will hold all the nodes in the graph

            List<Tuple<Vector2, Vector2>> path = null; // Resulting shortest path is empty in the beginning (stating the obvious here). 
                                                       // Is returned by the ShortestPath function

            foreach (var node in vertices)
            {
                if (node.Key == startPoint) // startPoint = Vector2 for the house.  Checks if it matches with any of the keys in the vertices dictionary
                {
                  node_total_distances[node.Key] = 0; // Its key in the node_total_distances dictionary will be set to 0
                }

                else
                {
                  node_total_distances[node.Key] = int.MaxValue; // Key in node_total_distances dictionary if set to the MaxValue that the int variable type 
                                                                 // This is done in order to (somehow) achieve infinity
                }

                nodes.Add(node.Key); // Adds the current Vector2 of vertices to the nodes list
            }

            while (nodes.Count > 0)
            {
                nodes.Sort((x, y) => node_total_distances[x] - node_total_distances[y]); // Substracting int values for x and y keys of node_total_distances dictionary. x and y are both vectors. Comparison is distance
                Vector2 smallest_node_vector = nodes[0];                                             // Selects the first element of the nodes list
                nodes.Remove(smallest_node_vector);                                                  // Removes the smallest_node_vector node from the nodes list. In other words, the 1st element of the list will always be removed

                if (smallest_node_vector == endPoint)  // Checks f the smallest_node_vector road is equal to the end road
                {
                    path = new List<Tuple<Vector2, Vector2>>(); // Path variable is instantiated

                    while (previous_neighbors.ContainsKey(smallest_node_vector)) // While loop will keep adding roads to path until all the nodes of the neighbor_nodes have been taken
                    {
                        Tuple<Vector2, Vector2> road;                                     // Creating a new road variable
                        road = new Tuple<Vector2, Vector2>(smallest_node_vector, previous_neighbors[smallest_node_vector]); // Initializing the pair variable. Values that are given are smallest_node_vector vector and its value in previous_neighbors dictionary
                        path.Add(road);                                                   // Pair of starting point and end point of road is added to the total path
                        smallest_node_vector = previous_neighbors[smallest_node_vector];                                    // Value of smallest_node_vector is set to its value in previous_neighbors dictionary. This for no infinite while loop
                    }

                    break; // Breaks the while loop with (previous_neighbors.ContainsKey(smallest_node_vector)). 
                }

                if (node_total_distances[smallest_node_vector] == int.MaxValue) // When the distance of a node is infinite, break the loop. Otherwise, an exception might be thrown
                {
                  break; // Breaks the big while loop (with nodes.Count > 0) After that, the value for the reversed_path variable will be returned
                }

                foreach (var neighbor_node in vertices[smallest_node_vector]) // Goes through each Dictionary value for the key (with value smallest_node_vector) in vertices dictionary
                {
                    int totalDistance = node_total_distances[smallest_node_vector] + neighbor_node.Value; // Calculate distance through each unvisited neightbor

                    if (totalDistance < node_total_distances[neighbor_node.Key]) // Update the neighbor_node's distance if smaller
                    {
                        node_total_distances[neighbor_node.Key] = totalDistance; // Update the neighbor_node's distance if smaller
                        previous_neighbors[neighbor_node.Key] = smallest_node_vector;       // Adds for the current neighbor_node key as value the current smallest_node_vector vector
                                                                 // For the house vector (= startingbuilding), its value will be given to its 1st neighbor_node
                    }
                }
           }

            List<Tuple<Vector2, Vector2>> reversed_path = new List<Tuple<Vector2, Vector2>>(); // End result that should be returned. Otherwise, colors of points between house and destination_building are shown wrong
            while (path.Count > 0)
            {
                reversed_path.Add(path[path.Count - 1]); // Adds all elements from resultListBA to resultListAB. It goes from the LAST till the FIRST element of the list
                path.RemoveAt(path.Count - 1);          // To prevent an infinite loop of occuring
            }

            return reversed_path;
        }
    }
}






