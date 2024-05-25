using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileOperator.Models;
using MobileOperator.Structures;

namespace MobileOperator
{
    public class Program
    {
        public static HashTable hashTable = new HashTable();

        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в информационную систему оператора сотовой связи!");

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Регистрация нового клиента");
                Console.WriteLine("2. Удаление данных о клиенте");
                Console.WriteLine("3. Просмотр всех зарегистрированных клиентов");
                Console.WriteLine("4. Очистка всех данных о клиентах");
                Console.WriteLine("5. Поиск клиента по номеру паспорта");
                Console.WriteLine("6. Поиск клиентов по фрагментам ФИО");
                Console.WriteLine("7. Добавление новой SIM-карты");
                Console.WriteLine("8. Удаление данных о SIM-карте");
                Console.WriteLine("9. Просмотр всех имеющихся SIM-карт");
                Console.WriteLine("10. Очистка всех данных о SIM-картах");
                Console.WriteLine("11. Поиск SIM-карты по номеру SIM-карты");
                Console.WriteLine("12. Поиск SIM-карты по тарифу");
                Console.WriteLine("13. Регистрация выдачи SIM-карты клиенту");
                Console.WriteLine("14. Регистрация возврата SIM-карты");
                Console.WriteLine("15. Просмотр всех данных о выдаче SIM-карт");
                Console.WriteLine("0. Выход");

                Console.Write("Введите номер действия: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "7":
                        AddNewSimCard();
                        break;
                    case "8":
                        RemoveSimCard();
                        break;
                    case "9":
                        ViewAllSimCards();
                        break;
                    case "10":
                        ClearAllSimCards();
                        break;
                    case "11":
                        SearchSimCardByNumber();
                        break;
                    case "12":
                        SearchSimCardByTariff();
                        break;
                    case "0":
                        Console.WriteLine("До свидания!");
                        return;
                    default:
                        Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                        break;
                }
            }
        }

        static void AddNewSimCard()
        {
            Console.Write("Введите номер SIM-карты (формат NNN-NNNNNNN): ");
            string simNumber = Console.ReadLine();
            Console.Write("Введите тариф: ");
            string tariff = Console.ReadLine();
            Console.Write("Введите год выпуска: ");
            int releaseYear = int.Parse(Console.ReadLine());
            Console.Write("SIM-карта доступна? (да/нет): ");
            bool isAvailable = Console.ReadLine().ToLower() == "да";

            SimCard simCard = new SimCard
            {
                SimNumber = simNumber,
                Tariff = tariff,
                ReleaseYear = releaseYear,
                IsAvailable = isAvailable
            };

            hashTable.Put(simCard);
            Console.WriteLine("SIM-карта успешно добавлена.");
        }

        static void RemoveSimCard()
        {
            Console.Write("Введите номер SIM-карты для удаления: ");
            string simNumber = Console.ReadLine();

            SimCard removedSimCard = hashTable.RemoveSimCardByNumber(simNumber);
            if (removedSimCard != null)
            {
                Console.WriteLine($"SIM-карта с номером {simNumber} успешно удалена.");
            }
            else
            {
                Console.WriteLine($"SIM-карта с номером {simNumber} не найдена.");
            }
        }

        static void ViewAllSimCards()
        {
            var simCards = hashTable.GetAllSimCards();
            if (simCards.Count > 0)
            {
                Console.WriteLine("Список всех SIM-карт:");
                foreach (var simCard in simCards)
                {
                    Console.WriteLine($"Номер: {simCard.SimNumber}, Тариф: {simCard.Tariff}, Год выпуска: {simCard.ReleaseYear}, Доступна: {simCard.IsAvailable}");
                }
            }
            else
            {
                Console.WriteLine("Нет доступных SIM-карт.");
            }
        }

        static void ClearAllSimCards()
        {
            hashTable.Clear();
            Console.WriteLine("Все данные о SIM-картах успешно удалены.");
        }

        static void SearchSimCardByNumber()
        {
            Console.Write("Введите номер SIM-карты для поиска: ");
            string simNumber = Console.ReadLine();

            SimCard simCard = hashTable.GetSimCardByNumber(simNumber);
            if (simCard != null)
            {
                Console.WriteLine($"Найденная SIM-карта: Номер: {simCard.SimNumber}, Тариф: {simCard.Tariff}, Год выпуска: {simCard.ReleaseYear}, Доступна: {simCard.IsAvailable}");
            }
            else
            {
                Console.WriteLine($"SIM-карта с номером {simNumber} не найдена.");
            }
        }

        static void SearchSimCardByTariff()
        {
            Console.Write("Введите тариф для поиска SIM-карт: ");
            string tariff = Console.ReadLine();

            var simCards = hashTable.SearchByTariff(tariff);
            if (simCards.Count > 0)
            {
                Console.WriteLine($"Найденные SIM-карты с тарифом {tariff}:");
                foreach (var simCard in simCards)
                {
                    Console.WriteLine($"Номер: {simCard.SimNumber}, Год выпуска: {simCard.ReleaseYear}, Доступна: {simCard.IsAvailable}");
                }
            }
            else
            {
                Console.WriteLine($"SIM-карты с тарифом {tariff} не найдены.");
            }
        }
    }
}
