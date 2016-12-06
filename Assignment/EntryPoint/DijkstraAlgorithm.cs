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
            List<Tuple<Vector2, Vector2>> road_result = new List<Tuple<Vector2, Vector2>>();

            Tuple<Vector2, Vector2> starting_road;
            Tuple<Vector2, Vector2> destination_road;

            for (int i = 0; i < roads.Count(); i++)
            {
                if(roads.ElementAt(i).Item1 == startingBuilding)
                {
                    starting_road = new Tuple<Vector2, Vector2>(roads.ElementAt(i).Item1, roads.ElementAt(i).Item2);
                    road_result.Add(starting_road);
                }

                if (roads.ElementAt(i).Item2 == destinationBuilding)
                {
                    destination_road = new Tuple<Vector2, Vector2>(roads.ElementAt(i).Item1, roads.ElementAt(i).Item2);
                }
            }

            IEnumerable<Tuple<Vector2, Vector2>> on_the_road_again = road_result.AsEnumerable<Tuple<Vector2, Vector2>>();
            return on_the_road_again;
        }
    }

    public class Vertex  // Hoek van graph
    {
        public Vector2 vector;
        public bool is_visited;

        public Vertex(Vector2 vector)
        {
            this.vector = vector;
            this.is_visited = false;
        }
    }

    public class Edge
    {
        public Vertex vertex1;
        public Vertex vertex2;
        public float distance;

        public Edge(Vertex vertex1, Vertex vertex2)
        {
            this.vertex1 = vertex1;
            this.vertex2 = vertex2;
            this.distance = Vector2.Distance(this.vertex1.vector, this.vertex2.vector);
        }
    }

    public class DiGraph
    {
        public List<Vertex> vertices;
        public List<Edge> edges;

        public DiGraph()
        {
            vertices = new List<Vertex>();
            edges = new List<Edge>();
        }

        public void ExecuteDijkstra()
        {
          
        }
    }

    public class AdjacencyMatrix
    {
        public DiGraph directional_graph;

        public AdjacencyMatrix()
        {
            directional_graph = new DiGraph();
        }

        public void Create()
        {

        }
    }
}

/*
GOAL: Build adjacency matrix using starting point and endpoint of road sections
*/
