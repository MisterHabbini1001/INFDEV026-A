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
            List<Vector2> new_median_list = ListWithMedianFirst(specialBuildings); // Changes the list so that the vector with the average length becomes the first element of the list
                                                                                   // This is to make sure that the binary tree becomes more balanced and that searching takes less time

            foreach (Vector2 special_building in new_median_list) // foreach (Vector2 special_building in specialBuildings)
            {
                binary_tree.Insert(special_building);                     // Inserts the current building into the Binary Tree
            }

            binary_tree.Display(); // Displays the entire binary tree on the console

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

        public BinaryTree() // Class constructor: root is empty in the beginning (because it obviously needs to grow first :p)
        {
          root = null;
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
        }

        public bool Search(Vector2 sb_distance) // Searches for special building inside binary tree: returns true if found, false if not found
        {
          return root.Search(root, sb_distance);
        }

        public void Display()
        {
            if (!IsRootEmpty())
                root.Display(root);
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

        public void Display(Node n)
        {
            if (n == null) // When there is nothing to display
            {
              return;
            }

            Console.WriteLine(n.sb_vector);
            Display(n.leftLeaf);
            Display(n.rightLeaf);
        }
    }
}




