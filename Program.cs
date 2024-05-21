using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileOperator.Models;
using MobileOperator.Structures;
using MobileOperator.Helpers;

namespace MobileOperator
{
    public class Program
    {
        public static HashTable<string, SimCard> hashTable = new HashTable<string, SimCard>(100);
        public static AVLTree<Client> avlTree = new AVLTree<Client>();
        public static CircularLinkedList<SimCardIssue> circularList = new CircularLinkedList<SimCardIssue>();

        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в информационную систему оператора сотовой связи!");
            // Здесь предполагается инициализация из контекста базы данных, который вы реализуете
            // using (var context = new ApplicationDbContext())
            // {
            //     context.Clients.ToList().ForEach(c => { avlTree.Add(c); });
            //     context.SimCards.ToList().ForEach(s => { hashTable.Put(s.SimNumber, s); });
            //     context.SimCardIssues.ToList().ForEach(i => { circularList.Insert(i); });
            // }

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
                    case "1":
                        RegisterNewClient();
                        break;
                    case "2":
                        RemoveClient();
                        break;
                    case "3":
                        ViewAllClients();
                        break;
                    case "4":
                        ClearAllClients();
                        break;
                    case "5":
                        SearchClientByPassportNumber();
                        break;
                    case "6":
                        SearchClientsByFullName();
                        break;
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
                    case "13":
                        RegisterSimCardIssue();
                        break;
                    case "14":
                        RegisterSimCardReturn();
                        break;
                    case "15":
                        ViewAllSimCardIssues();
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

        static void RegisterNewClient()
        {
            // Логика регистрации нового клиента
        }

        static void RemoveClient()
        {
            // Логика удаления клиента
        }

        static void ViewAllClients()
        {
            // Логика просмотра всех клиентов
        }

        static void ClearAllClients()
        {
            // Логика очистки данных о клиентах
        }

        static void SearchClientByPassportNumber()
        {
            // Логика поиска клиента по номеру паспорта
        }

        static void SearchClientsByFullName()
        {
            // Логика поиска клиентов по ФИО
        }

        static void AddNewSimCard()
        {
            // Логика добавления новой SIM-карты
        }

        static void RemoveSimCard()
        {
            // Логика удаления SIM-карты
        }

        static void ViewAllSimCards()
        {
            // Логика просмотра всех SIM-карт
        }

        static void ClearAllSimCards()
        {
            // Логика очистки данных о SIM-картах
        }

        static void SearchSimCardByNumber()
        {
            // Логика поиска SIM-карты по номеру
        }

        static void SearchSimCardByTariff()
        {
            // Логика поиска SIM-карты по тарифу
        }

        static void RegisterSimCardIssue()
        {
            // Логика регистрации выдачи SIM-карты клиенту
        }

        static void RegisterSimCardReturn()
        {
            // Логика регистрации возврата SIM-карты
        }

        static void ViewAllSimCardIssues()
        {
            // Логика просмотра всех данных о выдаче SIM-карт
        }
    }
}
