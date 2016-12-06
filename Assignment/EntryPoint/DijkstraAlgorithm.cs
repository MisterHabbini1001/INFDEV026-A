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
            Tuple<Vector2, Vector2> starting_road;
            List<Tuple<Vector2, Vector2>> road_result = new List<Tuple<Vector2, Vector2>>() { };

            for (int i = 0; i < roads.Count(); i++)
            {
                if(roads.ElementAt(i).Item1 == startingBuilding)
                {
                    starting_road = new Tuple<Vector2, Vector2>(roads.ElementAt(i).Item1, roads.ElementAt(i).Item2);
                    road_result.Add(starting_road);
                }
            }

            List<Vertex> vertices = new List<Vertex>();
            Console.WriteLine("Length of vertices length BEFORE = " + vertices.Count());

            for (int j = 0; j < roads.Count(); j++)
            {
               Vertex adder_zero_one;
               Vertex adder_zero_two;

               if (j == 0)
               {
                 adder_zero_one = new Vertex(roads.ElementAt(j).Item1);
                 adder_zero_two = new Vertex(roads.ElementAt(j).Item2);
                 vertices.Add(adder_zero_one);
                 vertices.Add(adder_zero_two);
               }

               else
               {
                 for (int k = 0; k < vertices.Count(); k++)
                 {
                   if(roads.ElementAt(j).Item1 != vertices.ElementAt(k).vector)
                   {
                      adder_zero_one = new Vertex(roads.ElementAt(j).Item1);
                      vertices.Add(adder_zero_one);
                   }

                  if (roads.ElementAt(j).Item2 != vertices.ElementAt(k).vector)
                  {
                      adder_zero_two = new Vertex(roads.ElementAt(j).Item2);
                      vertices.Add(adder_zero_two);
                  }
                }
              }
            }

            Console.WriteLine("Length of vertices length AFTER = " + vertices.Count());

            IEnumerable<Tuple<Vector2, Vector2>> on_the_road_again = road_result.AsEnumerable<Tuple<Vector2, Vector2>>();
            return on_the_road_again;
        }
    }

    public class Vertex  // Hoek van graph
    {
        public Vector2 vector;

        public Vertex(Vector2 vector)
        {
            this.vector = vector;
        }
    }

    public class Edge
    {
        public Vertex vertex1;
        public Vertex vertex2;
        public float distance;

        public Edge(Vertex vertex1, Vertex vertex2, float distance)
        {
            this.vertex1 = vertex1;
            this.vertex2 = vertex2;
            this.distance = distance;
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
