using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MobileOperator.Models;

namespace MobileOperator.Structures
{
    public class Node
    {
        public Client Data;
        public Node Left, Right;
        public int Height;

        public Node(Client item)
        {
            Data = item;
            Left = Right = null;
            Height = 1;
        }
    }

    public class AVLTree
    {
        private Node _root;

        private int Height(Node node)
        {
            return node == null ? -1 : node.Height;
        }

        private int Max(int a, int b)
        {
            return a > b ? a : b;
        }

        private Node RightRotate(Node y)
        {
            Node x = y.Left;
            Node T2 = x.Right;

            x.Right = y;
            y.Left = T2;

            y.Height = Max(Height(y.Left), Height(y.Right)) + 1;
            x.Height = Max(Height(x.Left), Height(x.Right)) + 1;

            return x;
        }

        private Node LeftRotate(Node x)
        {
            Node y = x.Right;
            Node T2 = y.Left;

            y.Left = x;
            x.Right = T2;

            x.Height = Max(Height(x.Left), Height(x.Right)) + 1;
            y.Height = Max(Height(y.Left), Height(y.Right)) + 1;

            return y;
        }

        private int GetBalance(Node node)
        {
            return node == null ? 0 : Height(node.Left) - Height(node.Right);
        }

        private Node Insert(Node node, Client data)
        {
            if (node == null)
                return new Node(data);

            if (string.Compare(data.PassportNumber, node.Data.PassportNumber) < 0)
                node.Left = Insert(node.Left, data);
            else if (string.Compare(data.PassportNumber, node.Data.PassportNumber) > 0)
                node.Right = Insert(node.Right, data);
            else
                return node;

            node.Height = 1 + Max(Height(node.Left), Height(node.Right));

            int balance = GetBalance(node);

            if (balance > 1 && string.Compare(data.PassportNumber, node.Left.Data.PassportNumber) < 0)
                return RightRotate(node);

            if (balance < -1 && string.Compare(data.PassportNumber, node.Right.Data.PassportNumber) > 0)
                return LeftRotate(node);

            if (balance > 1 && string.Compare(data.PassportNumber, node.Left.Data.PassportNumber) > 0)
            {
                node.Left = LeftRotate(node.Left);
                return RightRotate(node);
            }

            if (balance < -1 && string.Compare(data.PassportNumber, node.Right.Data.PassportNumber) < 0)
            {
                node.Right = RightRotate(node.Right);
                return LeftRotate(node);
            }

            return node;
        }

        private Node MinValueNode(Node node)
        {
            Node current = node;

            while (current.Left != null)
                current = current.Left;

            return current;
        }

        private Node Delete(Node root, Client data)
        {
            if (root == null)
                return root;

            if (string.Compare(data.PassportNumber, root.Data.PassportNumber) < 0)
                root.Left = Delete(root.Left, data);
            else if (string.Compare(data.PassportNumber, root.Data.PassportNumber) > 0)
                root.Right = Delete(root.Right, data);
            else
            {
                if (root.Left == null || root.Right == null)
                {
                    Node temp = null;
                    if (temp == root.Left)
                        temp = root.Right;
                    else
                        temp = root.Left;

                    if (temp == null)
                    {
                        temp = root;
                        root = null;
                    }
                    else
                        root = temp;
                }
                else
                {
                    Node temp = MinValueNode(root.Right);
                    root.Data = temp.Data;
                    root.Right = Delete(root.Right, temp.Data);
                }
            }

            if (root == null)
                return root;

            root.Height = 1 + Max(Height(root.Left), Height(root.Right));

            int balance = GetBalance(root);

            if (balance > 1 && GetBalance(root.Left) >= 0)
                return RightRotate(root);

            if (balance > 1 && GetBalance(root.Left) < 0)
            {
                root.Left = LeftRotate(root.Left);
                return RightRotate(root);
            }

            if (balance < -1 && GetBalance(root.Right) <= 0)
                return LeftRotate(root);

            if (balance < -1 && GetBalance(root.Right) > 0)
            {
                root.Right = RightRotate(root.Right);
                return LeftRotate(root);
            }

            return root;
        }

        private Node Search(Node root, Client data)
        {
            if (root == null || string.Compare(data.PassportNumber, root.Data.PassportNumber) == 0)
                return root;

            if (string.Compare(data.PassportNumber, root.Data.PassportNumber) < 0)
                return Search(root.Left, data);

            return Search(root.Right, data);
        }

        private void PreOrder(Node root, List<Client> resultList)
        {
            if (root != null)
            {
                resultList.Add(root.Data);
                PreOrder(root.Left, resultList);
                PreOrder(root.Right, resultList);
            }
        }

        private List<Client> PreOrderTraversal()
        {
            List<Client> result = new List<Client>();
            PreOrder(_root, result);
            return result;
        }

        public void Add(Client data)
        {
            _root = Insert(_root, data);
        }

        public void Remove(Client data)
        {
            _root = Delete(_root, data);
        }

        public Client SearchByPassportNumber(string passportNumber)
        {
            if (_root == null)
                return null;

            Client searchClient = new Client
            {
                PassportNumber = passportNumber
            };

            Node found = Search(_root, searchClient);
            return found?.Data;
        }

        public List<Client> SearchByPartOfFullNameOrAddress(string fragment)
        {
            List<Client> clients = new List<Client>();
            List<Client> allClients = PreOrderTraversal();
            foreach (var client in allClients)
            {
                if ((client.FullName != null && client.FullName.IndexOf(fragment, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (client.Address != null && client.Address.IndexOf(fragment, StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    clients.Add(client);
                }
            }
            return clients;
        }

        public ObservableCollection<Client> GetAllClients()
        {
            ObservableCollection<Client> clients = new ObservableCollection<Client>();
            List<Client> allClients = PreOrderTraversal();

            foreach (var client in allClients)
            {
                clients.Add(client);
            }

            return clients;
        }

        public void Clear()
        {
            _root = null;
        }
    }
}
