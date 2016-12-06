using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntryPoint
{
    class Functions_stub_implementations
    {
        /*
         private static IEnumerable<Vector2> SortSpecialBuildingsByDistance(Vector2 house, IEnumerable<Vector2> specialBuildings) // EXERCISE 1 - Sorting  EXERCISE 1 - Sorting  EXERCISE 1 - Sorting  !!!!!!
        {
      return specialBuildings.OrderBy(v => Vector2.Distance(v, house));
    }

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
        */
    }
}

/*
  private static IEnumerable<Vector2> SortSpecialBuildingsByDistance(Vector2 house, IEnumerable<Vector2> specialBuildings) // EXERCISE 1 - Sorting  EXERCISE 1 - Sorting  EXERCISE 1 - Sorting  !!!!!!
    {
       List<Vector2> sorted_list = specialBuildings.ToList<Vector2>();
       //Console.WriteLine(sorted_list.Count()); // List contains 50 elements
       int list_length = sorted_list.Count();  // list_length = 50  for 1st iteration
       int list_half_length = list_length / 2; // list_half_length = 25 for 1st iteration

       List<Vector2> first_half_list = sorted_list.GetRange(0, list_half_length);                   // (0, 25 - 1) = (0, 24) for 1st iteration
       IEnumerable<Vector2> converted_first_half_list = first_half_list.AsEnumerable<Vector2>();

       List<Vector2> second_half_list = sorted_list.GetRange(list_half_length, list_half_length);        // (25, 50 - 1) = (25, 49) for 1st iteration   count is number of elements in range
       IEnumerable<Vector2> converted_second_half_list = second_half_list.AsEnumerable<Vector2>();

       SortSpecialBuildingsByDistance(house, converted_first_half_list);
       SortSpecialBuildingsByDistance(house, converted_second_half_list);

       return specialBuildings.OrderBy(v => Vector2.Distance(v, house));
    }
*/

/*
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

       // STEP 1: Converteren naar List<T>
       // STEP 2: Maak Sort functie aan   die zichzelf aanroopt  recursive    Checken of list 1 element heeft vooraf, anders infinite recursive zooi
       // STEP 3: Maak Merge functie aan  die zichzelf NIET aanroept want statisch   Krijgt 2 lijsten binnen   Wordt aangeroepen door Sort


    }
*/

/*
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Is meant for exercise 2 - Trees

namespace EntryPoint
{
    public class BinaryTreeAlgorithm
    {
        public static IEnumerable<IEnumerable<Vector2>> InsertIntoBinaryTree(List<Vector2> specialBuildings, List<Tuple<Vector2, float>> houseandDistances)
        {
            BinaryTree b = new BinaryTree();
                      
            for (int i = 0; i < specialBuildings.Count(); i++)
            {
                b.Insert(specialBuildings.ElementAt(i));
            }

            // b.Display(); // Displays all elements in Binary tree
            
            List<List<Vector2>> test_return = MakeListOfListOfPositions(b, specialBuildings, houseandDistances);
            IEnumerable<IEnumerable<Vector2>> list_of_list_of_positions = test_return.AsEnumerable<IEnumerable<Vector2>>();

            return list_of_list_of_positions;
        }
       
        public static List<List<Vector2>> MakeListOfListOfPositions(BinaryTree bin_tree, List<Vector2> specialBuildings, List<Tuple<Vector2, float>> houseandDistances) // Error lies in this function
        {
           List<List<Vector2>> inception_list = new List<List<Vector2>>(); // Final result that should be returned
           List<Vector2> inter_result = new List<Vector2>();               // List that must be returned at the end of inner loop

           for (int j = 0; j < houseandDistances.Count(); j++)
           {
              for (int k = 0; k < specialBuildings.Count(); k++)
              {
                   bool result = bin_tree.Search(specialBuildings.ElementAt(k));

                   if (result)
                   { 
                       if (Vector2.Distance(houseandDistances.ElementAt(j).Item1, specialBuildings.ElementAt(k)) <= houseandDistances.ElementAt(j).Item2)
                       {
                           inter_result.Add(specialBuildings.ElementAt(k));
                       }
                   }                                     
              }

              inception_list.Add(inter_result);
              //inter_result.RemoveRange(0, inter_result.Count());
          }

          return inception_list;
       }        
    }

    public class BinaryTree // Binaaaaaaaaaaaarrrrrrrrryyyy tree       // Change int to Vector2
    {
        private Node root; // Root node of the binary tree
        private int count; // Amount of nodes inside the binary tree

        public BinaryTree()
        {
            root = null;
            count = 0;
        }

        public bool IsEmpty() // Checks if BinaryTree is empty by checking if root is null
        {
            return root == null;
        }

        public void Insert(Vector2 d) // Inserts a new node inside the binary tree with a certain value (= d) for Vector2
        {
            if (IsEmpty()) // If root node is equal to null (root node is empty)
            {
                root = new Node(d); // Creates new root node with value for d
            }

            else // If root node is not empty
            {
                root.InsertData(ref root, d); // root refers to root variable on line 31 - Inserts in root new vector2 for value
            }

            count++; // Increments total amount of nodes in binary tree by 1
        }

        public bool Search(Vector2 sb_distance) // Searches for node with certain value (= s) for Vector2
        {
            return root.Search(root, sb_distance);
        }

        public bool IsLeaf()
        {
            if (!IsEmpty())
            {
                return root.IsLeaf(ref root);
            }

            return true;
        }

        public void Display()
        {
            if (!IsEmpty())
                root.Display(root);
        }

        public int Count()
        {
            return count;
        }
    }

    public class Node // Noooooooooooooooooooode class
    {
        public Vector2 sb_vector; // Vector2 for Special building value inside Node
        public Node rightLeaf;
        public Node leftLeaf;

        public Node(Vector2 value)
        {
            sb_vector = value;
            rightLeaf = null;
            leftLeaf = null;
        }

        public bool IsLeaf(ref Node node)
        {
            return (node.rightLeaf == null && node.leftLeaf == null);
        }

        public void InsertData(ref Node node, Vector2 data) // data is new value for special building vector     ERROS MIGHT LAY HERE!!!!!!!!!!!!!!!!!!!!
        {
            if (node == null)                                                    
            {
                node = new Node(data);
            }

            else if (node.sb_vector.Length() >= data.Length())  // Makes a LEFTLEAF node 
            {
                InsertData(ref node.leftLeaf, data);
            }

            else if (node.sb_vector.Length() < data.Length())  // Makes a RIGHTLEAF node 
            {
                InsertData(ref node.rightLeaf, data);
            }       
        }

        public bool Search(Node node, Vector2 seeker) // hamd = house and maximum distance
        {
            if (node == null)
            {
                return false;
            }

            else if (node.sb_vector == seeker)
            {
                return true;
            }

            else if (node.sb_vector.Length() > seeker.Length())
            {
                return Search(node.leftLeaf, seeker);
            }

            else if (node.sb_vector.Length() < seeker.Length())
            {
              return Search(node.rightLeaf, seeker);
            }

            return false; 
        }

        public void Display(Node n)
        {
            if (n == null)
                return;

            Display(n.leftLeaf);
            Console.WriteLine(n.sb_vector);
            Display(n.rightLeaf);
        }
    }
}




*/
