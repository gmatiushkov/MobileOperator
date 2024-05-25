/*using System;
using MobileOperator.Models;
using MobileOperator.Structures;

namespace MobileOperator.Helpers
{
    class HashTableTests
    {
        static void Main(string[] args)
        {
            // Создание хеш-таблицы
            HashTable hashTable = new HashTable();

            // Тестовые данные
            SimCard sim1 = new SimCard { SimNumber = "123-4567890", Tariff = "Basic", ReleaseYear = 2022, IsAvailable = true };
            SimCard sim2 = new SimCard { SimNumber = "124-4567890", Tariff = "Premium", ReleaseYear = 2021, IsAvailable = false };
            SimCard sim3 = new SimCard { SimNumber = "125-4567890", Tariff = "Standard", ReleaseYear = 2020, IsAvailable = true };

            // Добавление SIM-карт в хеш-таблицу
            hashTable.Put(sim1);
            hashTable.Put(sim2);
            hashTable.Put(sim3);

            // Проверка добавления и извлечения SIM-карт по номеру
            Console.WriteLine("Testing GetSimCardByNumber:");
            SimCard retrievedSim1 = hashTable.GetSimCardByNumber("123-4567890");
            Console.WriteLine(retrievedSim1 != null ? $"Found SIM Card: {retrievedSim1.SimNumber}" : "SIM Card not found");

            SimCard retrievedSim2 = hashTable.GetSimCardByNumber("124-4567890");
            Console.WriteLine(retrievedSim2 != null ? $"Found SIM Card: {retrievedSim2.SimNumber}" : "SIM Card not found");

            SimCard retrievedSim3 = hashTable.GetSimCardByNumber("125-4567890");
            Console.WriteLine(retrievedSim3 != null ? $"Found SIM Card: {retrievedSim3.SimNumber}" : "SIM Card not found");

            // Поиск SIM-карт по тарифу
            Console.WriteLine("\nTesting SearchByTariff:");
            var basicTariffSims = hashTable.SearchByTariff("Basic");
            Console.WriteLine($"SIM Cards with 'Basic' tariff: {basicTariffSims.Count}");

            var premiumTariffSims = hashTable.SearchByTariff("Premium");
            Console.WriteLine($"SIM Cards with 'Premium' tariff: {premiumTariffSims.Count}");

            // Удаление SIM-карты по номеру
            Console.WriteLine("\nTesting RemoveSimCardByNumber:");
            SimCard removedSim = hashTable.RemoveSimCardByNumber("123-4567890");
            Console.WriteLine(removedSim != null ? $"Removed SIM Card: {removedSim.SimNumber}" : "SIM Card not found");

            // Проверка, что SIM-карта действительно удалена
            SimCard shouldBeNull = hashTable.GetSimCardByNumber("123-4567890");
            Console.WriteLine(shouldBeNull == null ? "SIM Card successfully removed" : "SIM Card still exists");

            // Проверка получения всех SIM-карт
            Console.WriteLine("\nTesting GetAllSimCards:");
            var allSimCards = hashTable.GetAllSimCards();
            Console.WriteLine($"Total SIM Cards in table: {allSimCards.Count}");

            // Очистка хеш-таблицы
            Console.WriteLine("\nTesting Clear:");
            hashTable.Clear();
            Console.WriteLine(hashTable.IsEmpty() ? "HashTable is empty" : "HashTable is not empty");
        }
    }
}
*/

using System;
using System.Collections;
using System.Collections.Generic;
using MobileOperator.Models;
using MobileOperator.Structures;

namespace MobileOperator.Tests
{
    public class HashTableTests
    {
        /*public static void Main(string[] args)
        {
            RunTests();
        }*/

        public static void RunTests()
        {
            TestAddSimCards();
            TestGetSimCardByNumber();
            TestSearchByTariff();
            TestRemoveSimCardByNumber();
            TestClearHashTable();

            HashTable hashTable2 = new HashTable();
            SimCard simCard4 = new SimCard { SimNumber = "000-0000000", Tariff = "Basic", ReleaseYear = 2004, IsAvailable = true };
            SimCard simCard5 = new SimCard { SimNumber = "666-1234567", Tariff = "Premium", ReleaseYear = 2024, IsAvailable = true };
            hashTable2.Put(simCard4);
            hashTable2.Put(simCard5);
            Console.WriteLine("Добавленные SIM-карты:");
            foreach (var simCard in hashTable2.GetAllSimCards())
            {
                Console.WriteLine($"Number: {simCard.SimNumber}, Tariff: {simCard.Tariff}, Release Year: {simCard.ReleaseYear}, Available: {simCard.IsAvailable}");
            }
        }

        public static void TestAddSimCards()
        {
            HashTable hashTable = new HashTable();

            SimCard simCard1 = new SimCard { SimNumber = "123-4567890", Tariff = "Basic", ReleaseYear = 2020, IsAvailable = true };
            SimCard simCard2 = new SimCard { SimNumber = "124-4567891", Tariff = "Premium", ReleaseYear = 2021, IsAvailable = true };
            SimCard simCard3 = new SimCard { SimNumber = "125-4567892", Tariff = "Standard", ReleaseYear = 2022, IsAvailable = true };

            hashTable.Put(simCard1);
            hashTable.Put(simCard2);
            hashTable.Put(simCard3);

            Console.WriteLine("Добавленные SIM-карты:");
            foreach (var simCard in hashTable.GetAllSimCards())
            {
                Console.WriteLine($"Number: {simCard.SimNumber}, Tariff: {simCard.Tariff}, Release Year: {simCard.ReleaseYear}, Available: {simCard.IsAvailable}");
            }
        }

        public static void TestGetSimCardByNumber()
        {
            HashTable hashTable = new HashTable();

            SimCard simCard1 = new SimCard { SimNumber = "123-4567890", Tariff = "Basic", ReleaseYear = 2020, IsAvailable = true };
            SimCard simCard2 = new SimCard { SimNumber = "124-4567891", Tariff = "Premium", ReleaseYear = 2021, IsAvailable = true };

            hashTable.Put(simCard1);
            hashTable.Put(simCard2);

            SimCard foundSimCard = hashTable.GetSimCardByNumber("124-4567891");
            Console.WriteLine($"\nНайдена SIM-карта: Number: {foundSimCard?.SimNumber}, Tariff: {foundSimCard?.Tariff}, Release Year: {foundSimCard?.ReleaseYear}, Available: {foundSimCard?.IsAvailable}");
        }

        public static void TestSearchByTariff()
        {
            HashTable hashTable = new HashTable();

            SimCard simCard1 = new SimCard { SimNumber = "123-4567890", Tariff = "Basic", ReleaseYear = 2020, IsAvailable = true };
            SimCard simCard2 = new SimCard { SimNumber = "124-4567891", Tariff = "Premium", ReleaseYear = 2021, IsAvailable = true };
            SimCard simCard3 = new SimCard { SimNumber = "126-4567893", Tariff = "Premium", ReleaseYear = 2021, IsAvailable = true };

            hashTable.Put(simCard1);
            hashTable.Put(simCard2);
            hashTable.Put(simCard3);

            List<SimCard> premiumSimCards = hashTable.SearchByTariff("Premium");
            Console.WriteLine("\nSIM-карты с тарифом 'Premium':");
            foreach (var simCard in premiumSimCards)
            {
                Console.WriteLine($"Number: {simCard.SimNumber}, Tariff: {simCard.Tariff}, Release Year: {simCard.ReleaseYear}, Available: {simCard.IsAvailable}");
            }
        }

        public static void TestRemoveSimCardByNumber()
        {
            HashTable hashTable = new HashTable();

            SimCard simCard1 = new SimCard { SimNumber = "123-4567890", Tariff = "Basic", ReleaseYear = 2020, IsAvailable = true };
            SimCard simCard2 = new SimCard { SimNumber = "125-4567892", Tariff = "Standard", ReleaseYear = 2022, IsAvailable = true };

            hashTable.Put(simCard1);
            hashTable.Put(simCard2);

            SimCard removedSimCard = hashTable.RemoveSimCardByNumber("125-4567892");
            Console.WriteLine($"\nУдалена SIM-карта: Number: {removedSimCard?.SimNumber}, Tariff: {removedSimCard?.Tariff}, Release Year: {removedSimCard?.ReleaseYear}, Available: {removedSimCard?.IsAvailable}");

            Console.WriteLine("\nОставшиеся SIM-карты:");
            foreach (var simCard in hashTable.GetAllSimCards())
            {
                Console.WriteLine($"Number: {simCard.SimNumber}, Tariff: {simCard.Tariff}, Release Year: {simCard.ReleaseYear}, Available: {simCard.IsAvailable}");
            }
        }

        public static void TestClearHashTable()
        {
            HashTable hashTable = new HashTable();

            SimCard simCard1 = new SimCard { SimNumber = "123-4567890", Tariff = "Basic", ReleaseYear = 2020, IsAvailable = true };
            SimCard simCard2 = new SimCard { SimNumber = "124-4567891", Tariff = "Premium", ReleaseYear = 2021, IsAvailable = true };

            hashTable.Put(simCard1);
            hashTable.Put(simCard2);

            hashTable.Clear();
            Console.WriteLine($"\nХэш-таблица пуста: {hashTable.IsEmpty()}");
        }
    }
}
