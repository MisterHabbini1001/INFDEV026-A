using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntryPoint
{
    public static class BinaryTreeAlgorithm
    {
        public static IEnumerable<IEnumerable<Vector2>> CreateInceptionList(List<Vector2> specialBuildings, List<Tuple<Vector2, float>> houseandDistances) 
        {
            BinaryTree binary_tree = new BinaryTree();  // Creates Binary Tree with an empty root

            foreach (Vector2 special_building in specialBuildings)
            {
                binary_tree.Insert(special_building);                     // Inserts the current building into the Binary Tree
            }

            List<List<Vector2>> inception_list = new List<List<Vector2>>(); // Final result that must be returned at the end of the function
            List<Vector2> inter_result = new List<Vector2>();               // Intermediate result that must be returned at the end of each inner loop

            foreach (Tuple<Vector2, float> current in houseandDistances) 
            {
                foreach (Vector2 special_building in specialBuildings) 
                {
                    bool tree_search_result = binary_tree.Search(special_building);            // Searches the current special building in the binary tree. Returns true if found and false if not found

                    if (tree_search_result) // Enter only when current special building has been found in the binary tree
                    {
                        if (Vector2.Distance(current.Item1, special_building) <= current.Item2) // Add current special building to the inter result list ONLY when it is in range of the current house
                        {
                            inter_result.Add(special_building);
                        }
                    }
                }

                inception_list.Add(inter_result);
                //inter_result.RemoveRange(0, inter_result.Count());   Fixed the exercise: no unit tests whatso ever
            }

            return inception_list.AsEnumerable<IEnumerable<Vector2>>();
        }      
    }

    class BinaryTree 
    {
        Node root; 

        public BinaryTree() // Class constructor: root is empty in the beginning (because it obviously needs to grow first :p)
        {
          root = null;
        }

        public bool IsEmpty() // Function that checks if the root is empty: true if root is null, false otherwise
        {
          return root == null; 
        }

        public void Insert(Vector2 d) // Inserts node with given value in the binary tree
        {
            if (IsEmpty()) // Checks if root is empty
            {
              root = new Node(d); // Create new root node when empty
            }

            else
            {
              root.InsertData(ref root, d); // Insert new node with value WHEN ROOT IS NOT EMPTY
            }
        }

        public bool Search(Vector2 sb_distance) // Searches for special building inside binary tree: returns true if found, false if not found
        {
          return root.Search(root, sb_distance);
        }
    }

    class Node 
    {
        Vector2 sb_vector; 
        Node rightLeaf;
        Node leftLeaf;

        public Node(Vector2 value)
        {
            sb_vector = value;
            rightLeaf = null;
            leftLeaf = null;
        }

        public void InsertData(ref Node node, Vector2 data) // Inserts vector data for the current node
        {
            if (node == null) // Checks if the node is empty
            {
              node = new Node(data); // Create a new node with the value for data
            } 

            else if (node.sb_vector.Length() >= data.Length()) // When the node vector is further away from point (0,0) then the data vector (= BST property). Also applies when they are the same vector   
            {
               InsertData(ref node.leftLeaf, data); // Recursive function call to go the left subtree
            }  
             
            else if (node.sb_vector.Length() < data.Length())  // When the data vector is further away from point (0,0) then the node vector (= BST property) 
            {
              InsertData(ref node.rightLeaf, data); // Recursive function call to go the right subtree
            }     
        }

        public bool Search(Node node, Vector2 seeker) // Searches for node with value for seeker
        {
            if (node == null) // Checks if the node is empty: returns false when node does not exist, skips the block when root does exist
            {
              return false;
            } 

            else if (node.sb_vector == seeker)  // Checks if the value for seeker MATCHES with the value in the current node
            {
              return true;
            }                                             

            else if (node.sb_vector.Length() > seeker.Length()) // When the node vector is further away from point (0,0) then the seeker vector (= BST property).   
            {
              return Search(node.leftLeaf, seeker); // Recursive function call to go the left subtree
            }   

            else if (node.sb_vector.Length() < seeker.Length()) // When the seeker vector is further away from point (0,0) then the node vector (= BST property) 
            {
              return Search(node.rightLeaf, seeker); // Recursive function call to go the right subtree
            }   

            return false; // If the value for seeker is not found in the tree
        }
    }
}




