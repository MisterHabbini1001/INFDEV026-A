using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntryPoint
{
    public class BinaryTreeAlgorithm
    {
        public static IEnumerable<IEnumerable<Vector2>> InsertIntoBinaryTree(List<Vector2> specialBuildings, List<Tuple<Vector2, float>> houseandDistances)
        {
            BinaryTree b = new BinaryTree();
                      
            for (int i = 0; i < specialBuildings.Count(); i++) { b.Insert(specialBuildings.ElementAt(i)); }
            
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
              //inter_result.RemoveRange(0, inter_result.Count());   Fixed the exercise: no unit tests whatso ever
          }

          return inception_list;
       }        
    }

    public class BinaryTree 
    {
        private Node root; 
        private int count; 

        public BinaryTree()
        {
            root = null;
            count = 0;
        }

        public bool IsEmpty() { return root == null; }

        public void Insert(Vector2 d) 
        {
            if (IsEmpty()) { root = new Node(d); }
            else           { root.InsertData(ref root, d); }

            count++; 
        }

        public bool Search(Vector2 sb_distance) { return root.Search(root, sb_distance); }
    }

    public class Node 
    {
        public Vector2 sb_vector; 
        public Node rightLeaf;
        public Node leftLeaf;

        public Node(Vector2 value)
        {
            sb_vector = value;
            rightLeaf = null;
            leftLeaf = null;
        }

        public void InsertData(ref Node node, Vector2 data) 
        {
            if (node == null) { node = new Node(data); }

            else if (node.sb_vector.Length() >= data.Length()) { InsertData(ref node.leftLeaf, data); }
            else if (node.sb_vector.Length() < data.Length())  { InsertData(ref node.rightLeaf, data); }       
        }

        public bool Search(Node node, Vector2 seeker) 
        {
            if (node == null) { return false; }

            else if (node.sb_vector == seeker) { return true; }
            else if (node.sb_vector.Length() > seeker.Length()) { return Search(node.leftLeaf, seeker); }
            else if (node.sb_vector.Length() < seeker.Length()) { return Search(node.rightLeaf, seeker); }

            return false; 
        }
    }
}



