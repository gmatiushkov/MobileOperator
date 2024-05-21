using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileOperator.Structures
{
    public class AvlTreeNode<T> where T : IComparable
    {
        public T Value { get; set; }
        public AvlTreeNode<T> Left { get; set; }
        public AvlTreeNode<T> Right { get; set; }
        public int Height { get; set; }

        public AvlTreeNode(T value)
        {
            Value = value;
            Height = 1;
        }
    }

    public class AVLTree<T> where T : IComparable
    {
        private AvlTreeNode<T> root;

        // Add methods for inserting, deleting, and balancing the tree
        // Add methods for in-order traversal
    }
}
