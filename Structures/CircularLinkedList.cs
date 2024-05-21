using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileOperator.Structures
{
    public class CircularLinkedListNode<T>
    {
        public T Value { get; set; }
        public CircularLinkedListNode<T> Next { get; set; }

        public CircularLinkedListNode(T value)
        {
            Value = value;
        }
    }

    public class CircularLinkedList<T>
    {
        private CircularLinkedListNode<T> head;

        // Add methods for adding, removing, and traversing the list
    }
}
