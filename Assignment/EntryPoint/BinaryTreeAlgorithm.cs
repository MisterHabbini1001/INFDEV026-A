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
                Console.WriteLine("Inserting into binary tree: specialBuilding at position " + i + " with value : " + specialBuildings.ElementAt(i));
                b.Insert(specialBuildings.ElementAt(i));
            }

            b.Display();
            
            List<List<Vector2>> test_return = MakeListOfListOfPositions(b, specialBuildings, houseandDistances);
            Console.WriteLine("INSERT_INTO_BINARY_TREE : Amount of elements in test_return " + test_return.Count());

            IEnumerable<IEnumerable<Vector2>> list_of_list_of_positions = test_return.AsEnumerable<IEnumerable<Vector2>>();
            Console.WriteLine("INSERT_INTO_BINARY_TREE : Amount of elements in list_of_list_of_positions " + list_of_list_of_positions.Count());

            return list_of_list_of_positions;
        }
       
        public static List<List<Vector2>> MakeListOfListOfPositions(BinaryTree bin_tree, List<Vector2> specialBuildings, List<Tuple<Vector2, float>> houseandDistances)
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

                Console.WriteLine("Amount of elements in inter_result BEFORE " + inter_result.Count());
                inception_list.Add(inter_result);
                inter_result.RemoveRange(0, inter_result.Count());

                Console.WriteLine("Amount of elements in inter_result AFTER " + inter_result.Count());   // Always returns 0
            }

            Console.WriteLine("Amount of elements in inception_list_result " + inception_list.Count());
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



