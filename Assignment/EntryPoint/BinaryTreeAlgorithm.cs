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

            List<List<Vector2>> test_return = MakeListOfListOfPositions(b, specialBuildings, houseandDistances);
            IEnumerable<IEnumerable<Vector2>> list_of_list_of_positions = test_return.AsEnumerable<IEnumerable<Vector2>>();

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
                    Vector2 v_result = bin_tree.Search(specialBuildings.ElementAt(k), houseandDistances.ElementAt(j));
                    inter_result.Add(v_result);
                }

                inception_list.Add(inter_result);
                inter_result.RemoveRange(0, inter_result.Count());
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

        public Vector2 Search(Vector2 sb_distance, Tuple<Vector2, float> house_and_max_distance) // Searches for node with certain value (= s) for Vector2
        {
            return root.SeekAndAdd(root, sb_distance, house_and_max_distance);
        }

        public bool IsLeaf()
        {
            if (!IsEmpty())
                return root.IsLeaf(ref root);

            return true;
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

        public void InsertData(ref Node node, Vector2 data) // data is new value for special building vector
        {
            if (node == null)                                                    // If there is no root node available in the binary tree
            {
                node = new Node(data);
            }
           
            else if (node.sb_vector.X <= data.X && node.sb_vector.Y >= data.Y)  // Makes a RIGHTLEAF node 
            {
                InsertData(ref node.rightLeaf, data);
            }
           
            else if (node.sb_vector.X >= data.X && node.sb_vector.Y <= data.Y)  // Makes a LEFTLEAF node 
            {
                InsertData(ref node.leftLeaf, data);
            }
       
        }

        public Vector2 SeekAndAdd(Node node, Vector2 sb_distance, Tuple<Vector2, float> hamd) // hamd = house and maximum distance
        {
            Vector2 null_result = new Vector2(0, 0); // Result with special buildings for house

            if (node == null)
            {
                return null_result;
            }

            else if (node.sb_vector == sb_distance)
            {
                if (Vector2.Distance(hamd.Item1, node.sb_vector) <= hamd.Item2)
                {
                    return node.sb_vector;
                    SeekAndAdd(node.rightLeaf, sb_distance, hamd);
                }

                else if (Vector2.Distance(hamd.Item1, node.sb_vector) > hamd.Item2)
                {
                    return SeekAndAdd(node.leftLeaf, sb_distance, hamd);
                }
            }

            return null_result;
        }
    }
}




