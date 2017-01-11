using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntryPoint
{
    public static class FloydWarshallAlgorithm
    {
        public static IEnumerable<IEnumerable<Tuple<Vector2, Vector2>>> DeterminationOfRoads(Vector2 startingBuilding, List<Vector2> destinationBuildings, List<Tuple<Vector2, Vector2>> roads)
        {
            FloydWarshallGraph fw_graph = new FloydWarshallGraph();                                                // Graph is empty
            fw_graph = InsertGraph(fw_graph, roads, startingBuilding, destinationBuildings); // Graph gets created here
            fw_graph.DisplayGraph();                                                     // Graph gets displayed here on the console

            return fw_graph.ShortestPaths(startingBuilding, destinationBuildings); // Shortest path is determined between startingBuilding and destinationBuilding: is then stored in resultListBA variable
        }

        static FloydWarshallGraph InsertGraph(FloydWarshallGraph graph, List<Tuple<Vector2, Vector2>> roadslist, Vector2 startPoint, List<Vector2> endPoints)
        {
            graph.AddNode(startPoint); // Adds startingBuilding as FIRST NODE to the Graph

            foreach (var road in roadslist)
            {
                graph.AddNode(road.Item1);                                                            // Item1 is STARTING POINT of ROAD
                graph.AddNode(road.Item2);                                                            // Item2 is END POINT of ROAD
                graph.AddRoad(road.Item1, road.Item2, (int)Vector2.Distance(road.Item1, road.Item2)); // Calculates the distance between the 2 points. Adds road to the Graph
            }

            foreach (var endpoint in endPoints)
            {
               graph.AddNode(endpoint); // Adds destinationBuildings as LAST NODE to the Graph
            }

            return graph;
        }
    }

    class FloydWarshallGraph
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
            foreach (var vertex in vertices) // For each dictionary in the vertices dictionary
            {
                Console.WriteLine("Current vertex in the floyd warhsall graph with vector2 value: " + vertex.Key); // Display current Vector2 in vertices
                foreach (var vertex_neighbor in vertex.Value) // Goes through all the neighbors for current Vector2
                {
                    Console.WriteLine("Neighbor vertex: " + vertex_neighbor.Key + " has length: " + vertex_neighbor.Value + " between current vertex: " + vertex.Key); // Display current neighbor with corresponding distance
                }
            }
        }

        public List<List<Tuple<Vector2, Vector2>>> ShortestPaths(Vector2 startPoint, List<Vector2> endPoints)
        {
            List<List<Tuple<Vector2, Vector2>>> reversed_paths = new List<List<Tuple<Vector2, Vector2>>>(); 
            return reversed_paths;
        }
    }
}

