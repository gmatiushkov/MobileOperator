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
        public static AVLTree avlTree = new AVLTree();
        public static CircularLinkedList circularLinkedList = new CircularLinkedList();

        static void Main(string[] args)
        {
            // Добавление тестовых данных
            AddTestClients();
            AddTestSimCards();

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
            Console.Write("Введите номер паспорта (формат NNNN-NNNNNN): ");
            string passportNumber = Console.ReadLine();
            Console.Write("Введите место и дату выдачи паспорта: ");
            string issuePlaceAndDate = Console.ReadLine();
            Console.Write("Введите ФИО: ");
            string fullName = Console.ReadLine();
            Console.Write("Введите год рождения: ");
            int birthYear = int.Parse(Console.ReadLine());
            Console.Write("Введите адрес: ");
            string address = Console.ReadLine();

            Client client = new Client
            {
                PassportNumber = passportNumber,
                IssuePlaceAndDate = issuePlaceAndDate,
                FullName = fullName,
                BirthYear = birthYear,
                Address = address
            };

            avlTree.Add(client);
            Console.WriteLine("Клиент успешно зарегистрирован.");
        }

        static void RemoveClient()
        {
            Console.Write("Введите номер паспорта клиента для удаления: ");
            string passportNumber = Console.ReadLine();

            Client client = avlTree.SearchByPassportNumber(passportNumber);
            if (client != null)
            {
                List<string> simNumbers = circularLinkedList.RemoveByPassportNumber(passportNumber);
                foreach (var simNumber in simNumbers)
                {
                    SimCard simCard = hashTable.GetSimCardByNumber(simNumber);
                    if (simCard != null)
                    {
                        simCard.IsAvailable = true;
                    }
                }
                avlTree.Remove(client);
                Console.WriteLine($"Клиент с номером паспорта {passportNumber} успешно удалён.");
            }
            else
            {
                Console.WriteLine($"Клиент с номером паспорта {passportNumber} не найден.");
            }
        }




        static void ViewAllClients()
        {
            var clients = avlTree.GetAllClients();
            if (clients.Count > 0)
            {
                Console.WriteLine("Список всех клиентов:");
                foreach (var client in clients)
                {
                    Console.WriteLine($"Паспорт: {client.PassportNumber}, ФИО: {client.FullName}, Адрес: {client.Address}");
                }
            }
            else
            {
                Console.WriteLine("Нет зарегистрированных клиентов.");
            }
        }

        static void ClearAllClients()
        {
            avlTree.Clear();
            Console.WriteLine("Все данные о клиентах успешно удалены.");
        }

        static void SearchClientByPassportNumber()
        {
            Console.Write("Введите номер паспорта для поиска: ");
            string passportNumber = Console.ReadLine();

            Client client = avlTree.SearchByPassportNumber(passportNumber);
            if (client != null)
            {
                Console.WriteLine($"Найденный клиент: Паспорт: {client.PassportNumber}, ФИО: {client.FullName}, Адрес: {client.Address}, Место и дата выдачи: {client.IssuePlaceAndDate}, Год рождения: {client.BirthYear}");
            }
            else
            {
                Console.WriteLine($"Клиент с номером паспорта {passportNumber} не найден.");
            }
        }

        static void SearchClientsByFullName()
        {
            Console.Write("Введите фрагмент ФИО или адреса для поиска: ");
            string fragment = Console.ReadLine();

            var clients = avlTree.SearchByPartOfFullNameOrAddress(fragment);
            if (clients.Count > 0)
            {
                Console.WriteLine("Найденные клиенты:");
                foreach (var client in clients)
                {
                    Console.WriteLine($"Паспорт: {client.PassportNumber}, ФИО: {client.FullName}, Адрес: {client.Address}");
                }
            }
            else
            {
                Console.WriteLine("Клиенты с заданным фрагментом не найдены.");
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

        static void RegisterSimCardIssue()
        {
            Console.Write("Введите номер паспорта: ");
            string passportNumber = Console.ReadLine();
            Console.Write("Введите номер SIM-карты: ");
            string simNumber = Console.ReadLine();
            Console.Write("Введите дату выдачи: ");
            string issueDate = Console.ReadLine();
            Console.Write("Введите дату окончания действия: ");
            string expiryDate = Console.ReadLine();

            // Проверка существования клиента в AVL-дереве
            Client client = avlTree.SearchByPassportNumber(passportNumber);
            if (client == null)
            {
                Console.WriteLine("Клиент с таким номером паспорта не найден.");
                return;
            }

            // Проверка наличия SIM-карты
            SimCard simCard = hashTable.GetSimCardByNumber(simNumber);
            if (simCard == null || !simCard.IsAvailable)
            {
                Console.WriteLine("SIM-карта недоступна для выдачи.");
                return;
            }

            SimCardIssue issue = new SimCardIssue
            {
                PassportNumber = passportNumber,
                SimNumber = simNumber,
                IssueDate = issueDate,
                ExpiryDate = expiryDate
            };

            circularLinkedList.Add(issue);
            simCard.IsAvailable = false;
            Console.WriteLine("Регистрация выдачи SIM-карты успешно выполнена.");
        }

        static void RegisterSimCardReturn()
        {
            Console.Write("Введите номер паспорта: ");
            string passportNumber = Console.ReadLine();
            Console.Write("Введите номер SIM-карты: ");
            string simNumber = Console.ReadLine();

            bool result = circularLinkedList.Remove(passportNumber, simNumber);
            if (result)
            {
                SimCard simCard = hashTable.GetSimCardByNumber(simNumber);
                if (simCard != null)
                {
                    simCard.IsAvailable = true;
                }
                Console.WriteLine("Регистрация возврата SIM-карты успешно выполнена.");
            }
            else
            {
                Console.WriteLine("Не удалось найти запись о выдаче данной SIM-карты.");
            }
        }


        static void ViewAllSimCardIssues()
        {
            var issues = circularLinkedList.GetAll();
            if (issues.Count > 0)
            {
                Console.WriteLine("Список всех выдач/возвратов SIM-карт:");
                foreach (var issue in issues)
                {
                    Console.WriteLine($"Паспорт: {issue.PassportNumber}, SIM-карта: {issue.SimNumber}, Дата выдачи: {issue.IssueDate}, Дата окончания действия: {issue.ExpiryDate}");
                }
            }
            else
            {
                Console.WriteLine("Нет записей о выдаче SIM-карт.");
            }
        }

        static void AddTestClients()
        {
            var clients = new List<Client>
    {
        new Client
        {
            PassportNumber = "1234-567890",
            IssuePlaceAndDate = "Москва, 01.01.2000",
            FullName = "Иванов Иван Иванович",
            BirthYear = 1980,
            Address = "ул. Пушкина, д. 10"
        },
        new Client
        {
            PassportNumber = "2345-678901",
            IssuePlaceAndDate = "Санкт-Петербург, 02.02.2001",
            FullName = "Петров Петр Петрович",
            BirthYear = 1985,
            Address = "пр. Ленина, д. 20"
        },
        new Client
        {
            PassportNumber = "3456-789012",
            IssuePlaceAndDate = "Новосибирск, 03.03.2002",
            FullName = "Сидоров Сидор Сидорович",
            BirthYear = 1990,
            Address = "ул. Советская, д. 30"
        }
    };

            foreach (var client in clients)
            {
                avlTree.Add(client);
            }

            Console.WriteLine("Тестовые клиенты добавлены.");
        }

        static void AddTestSimCards()
        {
            var simCards = new List<SimCard>
    {
        new SimCard
        {
            SimNumber = "111-1111111",
            Tariff = "Basic",
            ReleaseYear = 2020,
            IsAvailable = true
        },
        new SimCard
        {
            SimNumber = "222-2222222",
            Tariff = "Basic",
            ReleaseYear = 2021,
            IsAvailable = true
        },
        new SimCard
        {
            SimNumber = "333-3333333",
            Tariff = "Premium",
            ReleaseYear = 2022,
            IsAvailable = true
        }
    };

            foreach (var simCard in simCards)
            {
                hashTable.Put(simCard);
            }

            Console.WriteLine("Тестовые SIM-карты добавлены.");
        }

    }
}
