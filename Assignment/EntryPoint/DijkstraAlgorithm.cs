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
            Graph graph = new Graph();
            graph = insertGraph(graph, roads, startingBuilding, destinationBuilding);

            List<Tuple<Vector2, Vector2>> resultListBA = new List<Tuple<Vector2, Vector2>>();
            resultListBA = graph.ShortestPath(startingBuilding, destinationBuilding);

            List<Tuple<Vector2, Vector2>> resultListAB = new List<Tuple<Vector2, Vector2>>();

            while (resultListBA.Count > 0)
            {
                resultListAB.Add(resultListBA[resultListBA.Count - 1]);
                resultListBA.RemoveAt(resultListBA.Count - 1);
            }

            return resultListAB;
        } 

        private static List<Tuple<Vector2, Vector2>> createPaths(List<Vector2> nodes)
        {
            List<Tuple<Vector2, Vector2>> result = new List<Tuple<Vector2, Vector2>>();

            for (int i = 0; i < nodes.Count(); i++) { result.Add(new Tuple<Vector2, Vector2>(nodes[i], nodes[i + 1])); }

            return result;
        } 

        private static Graph insertGraph(Graph graph, List<Tuple<Vector2, Vector2>> roadslist, Vector2 startPoint, Vector2 endPoint)
        {
            graph.AddNode(startPoint);

            for (int i = 0; i < roadslist.Count(); i++)
            {
                Vector2 firstpoint = roadslist[i].Item1;
                Vector2 secondpoint = roadslist[i].Item2;
                graph.AddNode(firstpoint);
                graph.AddNode(secondpoint);
            }

            graph.AddNode(endPoint);

            for (int i = 0; i < roadslist.Count(); i++)
            {
                Vector2 firstVector = roadslist[i].Item1;
                Vector2 secondVector = roadslist[i].Item2;
                int distance = calculateDistance(firstVector, secondVector);

                graph.AddRoad(firstVector, secondVector, distance);

            }

            return graph;
        } 

        private static int calculateDistance(Vector2 beginpoint, Vector2 endpoint)
        {
            int result;
            float floatResult;
            floatResult = Vector2.Distance(beginpoint, endpoint);
            result = (int)floatResult;

            return result;
        } 
    } 

    class Graph
    {
        Dictionary<Vector2, Dictionary<Vector2, int>> vertices = new Dictionary<Vector2, Dictionary<Vector2, int>>();

        public void AddNode(Vector2 location)
        {
            if (!vertices.ContainsKey(location))
            {
                Dictionary<Vector2, int> emptyRoadsList = new Dictionary<Vector2, int>();
                vertices[location] = emptyRoadsList;
            }
        }

        public void AddRoad(Vector2 roadpoint1, Vector2 roadpoint2, int length)
        {
            if (!vertices[roadpoint1].ContainsKey(roadpoint2)) { vertices[roadpoint1].Add(roadpoint2, length); }
            if (!vertices[roadpoint2].ContainsKey(roadpoint1)) { vertices[roadpoint2].Add(roadpoint1, length); }
        }

        public Dictionary<Vector2, Dictionary<Vector2, int>> getVertices() { return vertices; }

        public List<Tuple<Vector2, Vector2>> ShortestPath(Vector2 startPoint, Vector2 endPoint)
        {
            var previous = new Dictionary<Vector2, Vector2>();
            var distances = new Dictionary<Vector2, int>();
            var nodes = new List<Vector2>();

            List<Tuple<Vector2, Vector2>> path = null;

            foreach (var node in vertices)
            {
                if (node.Key == startPoint) { distances[node.Key] = 0; }
                else { distances[node.Key] = int.MaxValue; }

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

                if (distances[smallest] == int.MaxValue) { break; }

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


