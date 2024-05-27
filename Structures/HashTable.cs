using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MobileOperator.Models;

namespace MobileOperator.Structures
{
    public class HashNode
    {
        public string Key { get; set; }
        public SimCard SimCard { get; set; }

        public HashNode(SimCard simCard)
        {
            Key = simCard.SimNumber;
            SimCard = simCard;
        }
    }

    public class HashTable
    {
        private static int capacity = 500;
        public HashNode[] Table;

        public HashTable()
        {
            Table = new HashNode[capacity];
        }

        public static int HashFunction1(string key)
        {
            int hash = 0;
            foreach (char c in key)
            {
                hash += c * c;
            }
            return hash % capacity;
        }

        public static int HashFunction2(string key)
        {
            int hash = 0;
            foreach (char c in key)
            {
                hash = (hash * 31 + c) % capacity;
            }
            return hash;
        }

        public bool IsEmpty()
        {
            foreach (HashNode node in Table)
            {
                if (node != null)
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsFull()
        {
            for (int i = 0; i < capacity; i++)
            {
                if (Table[i] == null)
                {
                    return false;
                }
            }
            return true;
        }

        private void ResizeAndRehash()
        {
            int newCapacity = capacity * 2;
            HashNode[] newTable = new HashNode[newCapacity];

            foreach (HashNode node in Table)
            {
                if (node != null)
                {
                    int hashCode = HashFunction1(node.Key);
                    if (newTable[hashCode] == null)
                    {
                        newTable[hashCode] = node;
                    }
                    else
                    {
                        int secondHashCode = HashFunction2(node.Key);
                        int i = 1;
                        while (newTable[(hashCode + i * secondHashCode) % newCapacity] != null)
                        {
                            i++;
                        }
                        newTable[(hashCode + i * secondHashCode) % newCapacity] = node;
                    }
                }
            }

            Table = newTable;
            capacity = newCapacity;
        }

        public ObservableCollection<SimCard> GetAllSimCards()
        {
            ObservableCollection<SimCard> simCards = new ObservableCollection<SimCard>();

            for (int i = 0; i < capacity; i++)
            {
                if (Table[i] != null)
                {
                    simCards.Add(Table[i].SimCard);
                }
            }

            return simCards;
        }

        public void Put(SimCard simCard)
        {
            if (IsFull())
            {
                ResizeAndRehash();
            }

            HashNode newNode = new HashNode(simCard);
            int hashCode = HashFunction1(newNode.Key);

            if (Table[hashCode] == null)
            {
                Table[hashCode] = newNode;
            }
            else
            {
                int secondHashCode = HashFunction2(newNode.Key);
                int i = 1;
                while (Table[(hashCode + i * secondHashCode) % capacity] != null)
                {
                    i++;
                }
                Table[(hashCode + i * secondHashCode) % capacity] = newNode;
            }
        }

        public void Clear()
        {
            for (int i = 0; i < capacity; i++)
            {
                Table[i] = null;
            }
        }

        public SimCard GetSimCardByNumber(string key)
        {
            int hashCode1 = HashFunction1(key);
            int hashCode2 = HashFunction2(key);

            int i = 0;
            while (true)
            {
                int index = (hashCode1 + i * hashCode2) % capacity;
                if (Table[index] != null && Table[index].Key == key)
                {
                    return Table[index].SimCard;
                }
                else if (Table[index] == null)
                {
                    return null;
                }
                i++;
            }
        }

        public List<SimCard> SearchByTariff(string tariff)
        {
            List<SimCard> simCards = new List<SimCard>();
            ObservableCollection<SimCard> collection = GetAllSimCards();
            foreach (var simCard in collection)
            {
                if (simCard.Tariff.Equals(tariff, StringComparison.OrdinalIgnoreCase))
                {
                    simCards.Add(simCard);
                }
            }
            return simCards;
        }

        public SimCard RemoveSimCardByNumber(string key)
        {
            int hashCode1 = HashFunction1(key);
            int hashCode2 = HashFunction2(key);

            int i = 0;
            while (i < capacity)
            {
                int index = (hashCode1 + i * hashCode2) % capacity;
                if (Table[index] != null && Table[index].Key == key)
                {
                    SimCard removedSimCard = Table[index].SimCard;
                    Table[index] = null;
                    return removedSimCard;
                }
                else if (Table[index] == null)
                {
                    break;
                }
                i++;
            }

            return null;
        }
    }
}


