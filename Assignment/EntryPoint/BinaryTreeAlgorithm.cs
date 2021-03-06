﻿using Microsoft.Xna.Framework;
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
            List<Vector2> new_median_list = ListWithMedianFirst(specialBuildings); // Changes the list so that the vector with the average length becomes the first element of the list
                                                                                   // This is to make sure that the binary tree becomes more balanced and that searching takes less time

            foreach (Vector2 special_building in new_median_list) // foreach (Vector2 special_building in specialBuildings)
            {
                binary_tree.Insert(special_building);                     // Inserts the current building into the Binary Tree
            }

            binary_tree.Count();   // Prints total amount of nodes in the binary tree BEFORE DELETION
            //binary_tree.Delete(specialBuildings.ElementAt(9));
            //binary_tree.Count(); // Prints total amount of nodes in the binary tree AFTER DELETION

            // Only comment out 1 of the 3 traversal functions below (so choose very very wisely :p)

            //binary_tree.InOrderTraversal(); // Entire binary tree on the console (by performing IN ORDER TRAVERSAL on it)
            binary_tree.PreOrderTraversal(); // Entire binary tree on the console (by performing PRE ORDER TRAVERSAL on it)
            //binary_tree.PostOrderTraversal(); // Entire binary tree on the console (by performing POST ORDER TRAVERSAL on it)

            List<List<Vector2>> inception_list = new List<List<Vector2>>(); // Final result that must be returned at the end of the function
            List<Vector2> inter_result = new List<Vector2>();               // Intermediate result that must be returned at the end of each inner loop

            foreach (var current in houseandDistances) // Loops through each Tuple<Vector2, float> element of houseandDistances
            {
                foreach (Vector2 special_building in new_median_list) // Loops through Vector2 element of specialBuildings // foreach (Vector2 special_building in specialBuildings)
                {
                    bool tree_search_result = binary_tree.Search(special_building); // Searches the current special building in the binary tree. Returns true if found and false if not found

                    if (tree_search_result) // Enter only when current special building has been found in the binary tree
                    {
                        if (Vector2.Distance(current.Item1, special_building) <= current.Item2) // Add current special building to the inter result list ONLY when it is in range of the current house
                        {
                            inter_result.Add(special_building);
                        }
                    }
                }

                inception_list.Add(inter_result);
            }

            return inception_list.AsEnumerable<IEnumerable<Vector2>>();
        } 
        
     static List<Vector2> ListWithMedianFirst(List<Vector2> specialBuildings)
     {
            List<Vector2> median_list = specialBuildings;
            List<float> sb_lengths = new List<float>(); // List that holds the lengths for all special buildings

            foreach (Vector2 sb in specialBuildings)
            {
                sb_lengths.Add(sb.Length());
            }

            float sb_total_length = 0;   // Variable that holds the total length of all the special building vectors
            float sb_average_length = 0; // Variable that holds the average length of the special building vectors

            foreach (float sb_length in sb_lengths)
            {
                sb_total_length += sb_length; // Adding the current special building length to the total length of the special buildings
            }

            sb_average_length = sb_total_length / sb_lengths.Count(); // Computes the average length of special building vectors ---> 60,03199 is average
            float median_length = 0; // Median vector2 length that must be the first element of median_list

            Dictionary<float, float> dif_lengths = new Dictionary<float, float>(); // Dictionary that holds the differences between the average vector2 length and all the special building vector lengths

            foreach (float sb_length in sb_lengths)
            {
                float difference = Math.Abs(sb_average_length - sb_length); // Math.Abs method is used to make sure that no negative lengths are given back

                if (!dif_lengths.ContainsKey(sb_length)) 
                {
                    dif_lengths[sb_length] = difference; // Puts the difference as value for the key, which is the current
                }
            }

            var keyAndValue = dif_lengths.OrderBy(kvp => kvp.Value).First(); // Selects the length with the smallest difference to the sb_average_length
            median_length = keyAndValue.Key; // Length with the smallest difference to the sb_average_length is stored as median_length

            foreach (Vector2 sb in median_list)
            {
               if(sb.Length() == median_length)
               {
                 Vector2 first_item = median_list.ElementAt(0); // First element of the specialBuildings list
                 Vector2 item_to_move = sb;

                 median_list.Remove(item_to_move); // Removes item_to_move from the list first
                 median_list.RemoveAt(0);          // Removes first element of the specialBuildings list

                 median_list.Insert(0, item_to_move); // 
                 median_list.Add(first_item);

                 break; // Foreach loop has no use any longer: might as well break it
               }
            }

            return median_list;
        }                    
    }

    class BinaryTree 
    {
        Node root;
        int count; // Amo

        public BinaryTree() // Class constructor: root is empty in the beginning (because it obviously needs to grow first :p)
        {
          root = null;
          count = 0;
        }

        public void Count() // Amount of nodes in binary tree on to the console
        {
           Console.WriteLine("Total amount of nodes in binary tree: " + count);
        }

        public bool IsRootEmpty() // Function that checks if the root is empty: true if root is null, false otherwise
        {
          return root == null; 
        }

        public void Insert(Vector2 d) // Inserts node with given value in the binary tree
        {
            if (IsRootEmpty()) // Checks if root is empty
            {
              root = new Node(d); // Create new root node when empty
            }

            else
            {
              root.AddNodeWithData(ref root, d); // Insert new node with value WHEN ROOT IS NOT EMPTY
            }

            count++; // Increments total node amount in binary tree by 1.
        }

        public bool Search(Vector2 sb_distance) // Searches for special building inside binary tree: returns true if found, false if not found
        {
          return root.Search(root, sb_distance);
        }

        public void Delete(Vector2 d) // Deletes node with given value in the binary tree
        {
          root.Delete(root, d);
          count--; // Decrements total node amount in binary tree by 1.
        }

        public void InOrderTraversal() // Function that performs in order traversal on the binary tree
                                       // Has the following order: left subtree ---> root ---> right subtree
                                       // In respect to the BST (= Binary Search Tree) property, vector2.lengths will be printed on the console in increasing order
        {
            if (!IsRootEmpty()) // First checks if root is not empty. Otherwise, traversing through the tree is pretty f*cking useless
            {
              root.InOrderTraversal(root);
            }
        }

        public void PreOrderTraversal() // Function that performs pre order traversal on the binary tree
                                        // Has the following order: root ---> left subtree ---> right subtree
        {
            if (!IsRootEmpty()) // First checks if root is not empty. Otherwise, traversing through the tree is pretty f*cking useless
            {
                root.PreOrderTraversal(root);
            }
        }

        public void PostOrderTraversal() // Function that performs post order traversal on the binary tree
                                         // Has the following order: left subtree ---> right subtree ---> root
        {
            if (!IsRootEmpty()) // First checks if root is not empty. Otherwise, traversing through the tree is pretty f*cking useless
            {
                root.PostOrderTraversal(root);
            }
        }
    }

    class Node 
    {
        Vector2 sb_vector; // Vector for special building
        Node rightLeaf;
        Node leftLeaf;

        public Node(Vector2 value)
        {
            sb_vector = value;
            rightLeaf = null;
            leftLeaf = null;
        }

        public void AddNodeWithData(ref Node node, Vector2 data) // Inserts vector data for the current node
        {
            if (node == null) // Checks if the node is empty
            {
              node = new Node(data); // Create a new node with the value for data
            } 

            else if (node.sb_vector.Length() >= data.Length()) // When the node vector is further away from point (0,0) then the data vector (= BST property). Also applies when they are the same vector   
            {
               AddNodeWithData(ref node.leftLeaf, data); // Recursive function call to go the left subtree
            }  
             
            else if (node.sb_vector.Length() < data.Length())  // When the data vector is further away from point (0,0) then the node vector (= BST property) 
            {
              AddNodeWithData(ref node.rightLeaf, data); // Recursive function call to go the right subtree
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

        public void Delete(Node node, Vector2 deleter) // Deletes node with value deleter
        {
            // FULL IMPLEMENTATION FOR DELETING NODES OF BINARY TREE SHOULD GO HERE :PPPPPP
            if(node == null)
            {
                return;
            }

            else if(node.sb_vector == deleter && node.leftLeaf == null && node.rightLeaf == null) // When node to be deleted is leaf (with no RIGHT or LEFT child nodes to speak of what so ever)
            {
                node = null;
                return;
            }

            else if (node.sb_vector == deleter && node.leftLeaf != null && node.rightLeaf == null) // When node to be deleted has only LEFT child node
            {
                node = node.leftLeaf;
                node.leftLeaf = null;
                return;
            }

            else if (node.sb_vector == deleter && node.leftLeaf == null && node.rightLeaf != null) // When node to be deleted has only RIGHT child node
            {
                node = node.rightLeaf;
                node.rightLeaf = null;
                return;
            }

            else if (node.sb_vector == deleter && node.leftLeaf != null && node.rightLeaf != null) // When node to be deleted has LEFT and RIGHT child nodes (in other words: prepare to enter a world of pain.... recursive pain that is :p)
            {
                // NEEDS REAL IMPLEMENTATION
                return;
            }
        }

        public void InOrderTraversal(Node n) // Function of node that performs in order traversal on the binary tree (= left subtree ---> root ---> right subtree)
        {
            Console.WriteLine("In order traversal I am now :p");
            if (n == null) // When there is nothing
            {
              Console.WriteLine("IOT current node is NULL :("); 
              return;
            }

            InOrderTraversal(n.leftLeaf);
            Console.WriteLine("Current node with vector2 value: " + n.sb_vector + "which has a length of: " + n.sb_vector.Length()); // Prints the value of vector2 with length on the console
            InOrderTraversal(n.rightLeaf);
        }

        public void PreOrderTraversal(Node n) // Function of node that performs in order traversal on the binary tree (= root ---> left subtree ---> right subtree)
        {
            Console.WriteLine("Pre order traversal I am now :p");
            if (n == null) // When there is nothing
            {
                Console.WriteLine("PROT current node is NULL :(");
                return;
            }

            Console.WriteLine("Current node with vector2 value: " + n.sb_vector + "which has a length of: " + n.sb_vector.Length()); // Prints the value of vector2 with length on the console
            PreOrderTraversal(n.leftLeaf);
            PreOrderTraversal(n.rightLeaf);
        }

        public void PostOrderTraversal(Node n) // Function of node that performs post order traversal on the binary tree (= left subtree ---> right subtree ---> root)
        {
            Console.WriteLine("Post order traversal I am now :p");
            if (n == null) // When there is nothing
            {
                Console.WriteLine("POOT current node is NULL :(");
                return;
            }

            PreOrderTraversal(n.leftLeaf);
            PreOrderTraversal(n.rightLeaf);
            Console.WriteLine("Current node with vector2 value: " + n.sb_vector + "which has a length of: " + n.sb_vector.Length()); // Prints the value of vector2 with length on the console
        }
    }
}

/*
BINARY SEARCH TREE - DELETION

We want to remove the node with value 𝑣
4 cases:

Deleting a leaf  we can simply remove it from the tree (easy!)
Deleting a node with one LEFT child  remove the node and replace it with its LEFT child
Deleting a node with one RIGHT child  remove the node and replace it with its RIGHT child

Deleting a node with two children  more complicated recursive procedure
call the node to be deleted 𝑛 but do not delete 𝑛
choose either its in-order successor node or its in-order predecessor node, 𝑟
copy the value of 𝑟 to 𝑛, then recursively call delete on 𝑟 until reaching one of the first two cases

*/




