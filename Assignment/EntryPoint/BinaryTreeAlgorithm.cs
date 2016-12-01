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
        public static void InsertIntoBinaryTree(List<Vector2> specialBuildings)
        {
            BinaryTree b = new BinaryTree();

            for (int i = 0; i < specialBuildings.Count(); i++)
            {
                b.Insert(specialBuildings.ElementAt(i));
            }

            b.Display();

            Console.ReadLine();
        }

        /*
        public List<List<Vector2>>(BinaryTree bin_tree)
        {
           //
        }
        */
    }

    class BinaryTree // Binaaaaaaaaaaaarrrrrrrrryyyy tree       // Change int to Vector2
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

        public bool Search(Vector2 s) // Searches for node with certain value (= s) for Vector2
        {
            return root.Search(root, s);
        }

        public bool IsLeaf()
        {
            if (!IsEmpty())
                return root.IsLeaf(ref root);

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

    class Node // Noooooooooooooooooooode class
    {
        private Vector2 sb_vector; // Vector2 for Special building value inside Node
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

        public void InsertData(ref Node node, Vector2 data)
        {
            if (node == null)
            {
                node = new Node(data);
            }

            /*
            else if (node.sb_vector < data)
            {
                InsertData(ref node.rightLeaf, data);
            }
            */

            /*
            else if (node.sb_vector > data)
            {
                InsertData(ref node.leftLeaf, data);
            }
            */
        }

        public bool Search(Node node, Vector2 s)
        {
            if (node == null)
                return false;

            if (node.sb_vector == s)
            {
                return true;
            }

            /*
            else if (node.sb_vector < s)
            {
                return Search(node.rightLeaf, s);
            }
            */

            /*
            else if (node.sb_vector > s)
            {
                return Search(node.leftLeaf, s);
            }
            */

            return false;
        }

        public void Display(Node n)
        {
            if (n == null)
                return;

            Display(n.leftLeaf);
            Console.Write(" " + n.sb_vector);
            Display(n.rightLeaf);
        }
    }
}

/*
public static class BinaryTreeAlgorithm
    {
        public static void InsertIntoBinaryTree(List<Vector2> specialBuildings)
        {
            BinaryTree b = new BinaryTree();

            for (int i = 0; i < specialBuildings.Count(); i++)
            {
                b.Insert(specialBuildings.ElementAt(i));
            }

            b.Insert(1);
            b.Insert(6);
            b.Insert(2);
            b.Insert(4);
            b.Insert(5);
            b.Insert(3);

            b.Display();

            Console.ReadLine();
        }
    }

    class BinaryTree // Binaaaaaaaaaaaarrrrrrrrryyyy tree       // Change int to Vector2
    {
        private Node root;
        private int count;

        public BinaryTree()
        {
            root = null;
            count = 0;
        }

        public bool IsEmpty()
        {
            return root == null;
        }

        public void Insert(int d)
        {
            if (IsEmpty())
            {
                root = new Node(d);
            }
            else
            {
                root.InsertData(ref root, d);
            }

            count++;
        }

        public bool Search(int s)
        {
            return root.Search(root, s);
        }

        public bool IsLeaf()
        {
            if (!IsEmpty())
                return root.IsLeaf(ref root);

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

    class Node // Noooooooooooooooooooode class
    {
        private int sb_vector;
        public Node rightLeaf;
        public Node leftLeaf;

        public Node(int value)
        {
            sb_vector = value;
            rightLeaf = null;
            leftLeaf = null;
        }

        public bool IsLeaf(ref Node node)
        {
            return (node.rightLeaf == null && node.leftLeaf == null);

        }

        public void InsertData(ref Node node, int data)
        {
            if (node == null)
            {
                node = new Node(data);

            }
            else if (node.sb_vector < data)
            {
                InsertData(ref node.rightLeaf, data);
            }

            else if (node.sb_vector > data)
            {
                InsertData(ref node.leftLeaf, data);
            }
        }

        public bool Search(Node node, int s)
        {
            if (node == null)
                return false;

            if (node.sb_vector == s)
            {
                return true;
            }
            else if (node.sb_vector < s)
            {
                return Search(node.rightLeaf, s);
            }
            else if (node.sb_vector > s)
            {
                return Search(node.leftLeaf, s);
            }

            return false;
        }

        public void Display(Node n)
        {
            if (n == null)
                return;

            Display(n.leftLeaf);
            Console.Write(" " + n.sb_vector);
            Display(n.rightLeaf);
        }
    }
*/
