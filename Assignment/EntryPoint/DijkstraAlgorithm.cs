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
            graph = insertGraph(graph, roads, startingBuilding, destinationBuilding); // Graph gets created here

            List<Tuple<Vector2, Vector2>> resultListBA = new List<Tuple<Vector2, Vector2>>();
            resultListBA = graph.ShortestPath(startingBuilding, destinationBuilding);

            List<Tuple<Vector2, Vector2>> resultListAB = new List<Tuple<Vector2, Vector2>>(); // End result that should be returned

            while (resultListBA.Count > 0)
            {
                resultListAB.Add(resultListBA[resultListBA.Count - 1]); // Adds all elements from resultListBA to resultListAB. It goes from the LAST till the FIRST element of the list
                resultListBA.RemoveAt(resultListBA.Count - 1);          // To prevent an infinite loop of occuring
            }

            return resultListAB; // Returning end result
        } 

        private static Graph insertGraph(Graph graph, List<Tuple<Vector2, Vector2>> roadslist, Vector2 startPoint, Vector2 endPoint)
        {
            graph.AddNode(startPoint); // Adds startingBuilding as FIRST NODE to the graph

            for (int i = 0; i < roadslist.Count(); i++)
            {
                Vector2 firstpoint = roadslist[i].Item1;  // Item1 is STARTING POINT of ROAD
                graph.AddNode(firstpoint);

                Vector2 secondpoint = roadslist[i].Item2; // Item2 is END POINT of ROAD
                graph.AddNode(secondpoint);

                int distance = (int)Vector2.Distance(firstpoint, secondpoint); // Calculates the distance between the 2 points
                graph.AddRoad(firstpoint, secondpoint, distance);
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
            if (!vertices.ContainsKey(location)) //!false == true;
            {
                Dictionary<Vector2, int> emptyRoadsList = new Dictionary<Vector2, int>();
                vertices[location] = emptyRoadsList;
            }
        }

        public void AddRoad(Vector2 roadpoint1, Vector2 roadpoint2, int length)
        {
            if (!vertices[roadpoint1].ContainsKey(roadpoint2))
            {
              vertices[roadpoint1].Add(roadpoint2, length);
            }

            if (!vertices[roadpoint2].ContainsKey(roadpoint1))
            {
              vertices[roadpoint2].Add(roadpoint1, length);
            }
        }

        public Dictionary<Vector2, Dictionary<Vector2, int>> getVertices()
        {
          return vertices;
        }

        public List<Tuple<Vector2, Vector2>> ShortestPath(Vector2 startPoint, Vector2 endPoint)
        {
            var previous = new Dictionary<Vector2, Vector2>();
            var distances = new Dictionary<Vector2, int>();
            var nodes = new List<Vector2>();

            List<Tuple<Vector2, Vector2>> path = null;

            foreach (var node in vertices)
            {
                if (node.Key == startPoint)
                {
                  distances[node.Key] = 0;
                }

                else
                {
                  distances[node.Key] = int.MaxValue;
                }

                nodes.Add(node.Key);
            }

            while (nodes.Count != 0)
            {
                nodes.Sort((x, y) => distances[x] - distances[y]);
                var smallest = nodes[0];
                nodes.Remove(smallest);

                if (smallest == endPoint)
                {
                    path = new List<Tuple<Vector2, Vector2>>();

                    while (previous.ContainsKey(smallest))
                    {
                        Tuple<Vector2, Vector2> pair;
                        pair = new Tuple<Vector2, Vector2>(smallest, previous[smallest]);
                        path.Add(pair);
                        smallest = previous[smallest];
                    }

                    break;
                }

                if (distances[smallest] == int.MaxValue)
                {
                  break;
                }

                foreach (var neighbor in vertices[smallest])
                {
                    var totalDistance = distances[smallest] + neighbor.Value;

                    if (totalDistance < distances[neighbor.Key])
                    {
                        distances[neighbor.Key] = totalDistance;
                        previous[neighbor.Key] = smallest;
                    }
                }
            }

            return path;
        }
    }
}




