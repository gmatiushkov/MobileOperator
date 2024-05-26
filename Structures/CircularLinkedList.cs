using System;
using System.Collections.Generic;
using MobileOperator.Models;

namespace MobileOperator.Structures
{
    public class CircularLinkedListNode
    {
        public SimCardIssue Data { get; set; }
        public CircularLinkedListNode Next { get; set; }

        public CircularLinkedListNode(SimCardIssue data)
        {
            Data = data;
            Next = this; // Изначально узел указывает на себя, создавая цикл.
        }
    }

    public class CircularLinkedList
    {
        private CircularLinkedListNode head;

        public void Add(SimCardIssue data)
        {
            var newNode = new CircularLinkedListNode(data);
            if (head == null)
            {
                head = newNode;
            }
            else
            {
                CircularLinkedListNode current = head;
                while (current.Next != head)
                {
                    current = current.Next;
                }
                current.Next = newNode;
                newNode.Next = head;
                SortList();
            }
        }

        public bool Remove(string passportNumber, string simNumber)
        {
            if (head == null) return false;

            CircularLinkedListNode current = head;
            CircularLinkedListNode previous = null;
            do
            {
                if (current.Data.PassportNumber == passportNumber && current.Data.SimNumber == simNumber)
                {
                    if (previous != null)
                    {
                        previous.Next = current.Next;
                    }
                    else
                    {
                        if (current.Next == head)
                        {
                            head = null;
                        }
                        else
                        {
                            CircularLinkedListNode temp = head;
                            while (temp.Next != head)
                            {
                                temp = temp.Next;
                            }
                            head = current.Next;
                            temp.Next = head;
                        }
                    }
                    return true;
                }
                previous = current;
                current = current.Next;
            } while (current != head);

            return false;
        }

        public List<SimCardIssue> GetAll()
        {
            List<SimCardIssue> issues = new List<SimCardIssue>();
            if (head == null) return issues;

            CircularLinkedListNode current = head;
            do
            {
                issues.Add(current.Data);
                current = current.Next;
            } while (current != head);

            return issues;
        }

        public SimCardIssue SearchByPassportAndSimNumber(string passportNumber, string simNumber)
        {
            if (head == null) return null;

            CircularLinkedListNode current = head;
            do
            {
                if (current.Data.PassportNumber == passportNumber && current.Data.SimNumber == simNumber)
                {
                    return current.Data;
                }
                current = current.Next;
            } while (current != head);

            return null;
        }

        public List<string> RemoveByPassportNumber(string passportNumber)
        {
            List<string> simNumbers = new List<string>();
            if (head == null) return simNumbers;

            CircularLinkedListNode current = head;
            CircularLinkedListNode previous = null;

            do
            {
                if (current.Data.PassportNumber == passportNumber)
                {
                    simNumbers.Add(current.Data.SimNumber);

                    if (current == head)
                    {
                        // Find the last node
                        CircularLinkedListNode last = head;
                        while (last.Next != head)
                        {
                            last = last.Next;
                        }

                        // Move head to next node
                        head = head.Next;
                        last.Next = head;

                        current = head;
                    }
                    else
                    {
                        previous.Next = current.Next;
                        current = previous.Next;
                    }
                }
                else
                {
                    previous = current;
                    current = current.Next;
                }
            } while (current != head);

            // Check if the last node needs to be removed
            if (head != null && head.Data.PassportNumber == passportNumber)
            {
                simNumbers.Add(head.Data.SimNumber);
                head = null;
            }

            return simNumbers;
        }




        private void SortList()
        {
            if (head == null || head.Next == head) return;

            // Temporarily break the cycle
            CircularLinkedListNode last = head;
            while (last.Next != head)
            {
                last = last.Next;
            }
            last.Next = null;

            // Sort the list
            head = MergeSort(head);

            // Restore the cycle
            CircularLinkedListNode current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = head;
        }

        private CircularLinkedListNode MergeSort(CircularLinkedListNode node)
        {
            if (node == null || node.Next == null)
            {
                return node;
            }

            CircularLinkedListNode middle = GetMiddle(node);
            CircularLinkedListNode nextOfMiddle = middle.Next;
            middle.Next = null;

            CircularLinkedListNode left = MergeSort(node);
            CircularLinkedListNode right = MergeSort(nextOfMiddle);

            return SortedMerge(left, right);
        }

        private CircularLinkedListNode SortedMerge(CircularLinkedListNode left, CircularLinkedListNode right)
        {
            if (left == null) return right;
            if (right == null) return left;

            CircularLinkedListNode result;

            if (string.Compare(left.Data.SimNumber, right.Data.SimNumber, StringComparison.Ordinal) <= 0)
            {
                result = left;
                result.Next = SortedMerge(left.Next, right);
            }
            else
            {
                result = right;
                result.Next = SortedMerge(left, right.Next);
            }

            return result;
        }

        private CircularLinkedListNode GetMiddle(CircularLinkedListNode node)
        {
            if (node == null) return node;

            CircularLinkedListNode slow = node;
            CircularLinkedListNode fast = node;

            while (fast.Next != null && fast.Next.Next != null)
            {
                slow = slow.Next;
                fast = fast.Next.Next;
            }

            return slow;
        }

    }
}
