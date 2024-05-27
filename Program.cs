using System;
using System.Collections.Generic;
using System.Linq;
using MobileOperator.Models;
using MobileOperator.Helpers;
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
            string passportNumber = null;
            while (true)
            {
                Console.Write("Введите номер паспорта (формат NNNN-NNNNNN): ");
                passportNumber = Console.ReadLine();

                // Проверка на корректность формата номера паспорта
                if (!DataValidator.ValidatePassportNumber(passportNumber))
                {
                    Console.WriteLine("Неверный формат, повторите ввод.");
                    continue;
                }

                // Проверка на существование клиента с таким же номером паспорта
                if (avlTree.SearchByPassportNumber(passportNumber) != null)
                {
                    Console.WriteLine("Клиент с таким номером паспорта уже существует. Пожалуйста, введите другой номер.");
                    continue;
                }

                // Если формат корректен и такого номера паспорта нет, выходим из цикла
                break;
            }

            Console.Write("Введите место и дату выдачи паспорта: ");
            string issuePlaceAndDate = Console.ReadLine();
            while (!DataValidator.ValidateAddress(issuePlaceAndDate))
            {
                Console.WriteLine("Неверный формат, повторите ввод.");
                Console.Write("Введите место и дату выдачи паспорта: ");
                issuePlaceAndDate = Console.ReadLine();
            }

            Console.Write("Введите ФИО: ");
            string fullName = Console.ReadLine();
            while (!DataValidator.ValidateFullName(fullName))
            {
                Console.WriteLine("Неверный формат, повторите ввод.");
                Console.Write("Введите ФИО: ");
                fullName = Console.ReadLine();
            }

            Console.Write("Введите год рождения: ");
            int birthYear = DataParser.ParseYearOfBirth(Console.ReadLine());
            while (birthYear == -1)
            {
                Console.WriteLine("Неверный формат, повторите ввод.");
                Console.Write("Введите год рождения: ");
                birthYear = DataParser.ParseYearOfBirth(Console.ReadLine());
            }

            Console.Write("Введите адрес: ");
            string address = Console.ReadLine();
            while (!DataValidator.ValidateAddress(address))
            {
                Console.WriteLine("Неверный формат, повторите ввод.");
                Console.Write("Введите адрес: ");
                address = Console.ReadLine();
            }

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
            string passportNumber;

            while (true)
            {
                // Вывод списка всех клиентов перед вводом номера паспорта для удаления
                var allClients = avlTree.GetAllClients();
                if (!allClients.Any())
                {
                    Console.WriteLine("Нет зарегистрированных клиентов.");
                    return;
                }

                Console.WriteLine("Список всех клиентов:");
                foreach (var cl in allClients)
                {
                    Console.WriteLine($"Паспорт: {cl.PassportNumber}, ФИО: {cl.FullName}, Адрес: {cl.Address}");
                }

                Console.Write("Введите номер паспорта клиента для удаления: ");
                passportNumber = Console.ReadLine();

                // Проверка на корректность формата номера паспорта
                if (!DataValidator.ValidatePassportNumber(passportNumber))
                {
                    Console.WriteLine("Неверный формат, повторите ввод.");
                    continue;
                }

                break; // Формат корректен, выходим из цикла
            }

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
            string passportNumber;

            Console.Write("Введите номер паспорта для поиска: ");
            passportNumber = Console.ReadLine();

            // Проверка на корректность формата номера паспорта
            while (!DataValidator.ValidatePassportNumber(passportNumber))
            {
                Console.WriteLine("Неверный формат, повторите ввод.");
                Console.Write("Введите номер паспорта для поиска: ");
                passportNumber = Console.ReadLine();
            }

            Client client = avlTree.SearchByPassportNumber(passportNumber);
            if (client != null)
            {
                Console.WriteLine($"Найденный клиент: Паспорт: {client.PassportNumber}, ФИО: {client.FullName}, Адрес: {client.Address}, Место и дата выдачи: {client.IssuePlaceAndDate}, Год рождения: {client.BirthYear}");

                // Поиск всех SIM-карт, выданных клиенту
                List<SimCardIssue> simCardIssues = circularLinkedList.GetAll().FindAll(issue => issue.PassportNumber == passportNumber);
                if (simCardIssues.Count > 0)
                {
                    Console.WriteLine("Выданные SIM-карты:");
                    foreach (var issue in simCardIssues)
                    {
                        Console.WriteLine($"SIM-карта: {issue.SimNumber}, Дата выдачи: {issue.IssueDate}, Дата окончания действия: {issue.ExpiryDate}");
                    }
                }
                else
                {
                    Console.WriteLine("Нет выданных SIM-карт.");
                }
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
            string simNumber;
            while (true)
            {
                Console.Write("Введите номер SIM-карты (формат NNN-NNNNNNN): ");
                simNumber = Console.ReadLine();
                // Проверка на корректность формата номера SIM-карты
                if (!DataValidator.ValidateSimNumber(simNumber))
                {
                    Console.WriteLine("Неверный формат, повторите ввод.");
                    continue;
                }
                // Проверка на существование SIM-карты с таким же номером
                if (hashTable.GetSimCardByNumber(simNumber) != null)
                {
                    Console.WriteLine("SIM-карта с таким номером уже существует. Пожалуйста, введите другой номер.");
                    continue;
                }
                // Если формат корректен и такой номер SIM-карты не существует, выходим из цикла
                break;
            }

            string tariff;
            Console.Write("Введите тариф: ");
            tariff = Console.ReadLine();
            while (!DataValidator.ValidateTariff(tariff))
            {
                Console.WriteLine("Неверный формат, повторите ввод.");
                Console.Write("Введите тариф: ");
                tariff = Console.ReadLine();
            }

            int releaseYear;
            Console.Write("Введите год выпуска: ");
            string releaseYearInput = Console.ReadLine();
            while (!int.TryParse(releaseYearInput, out releaseYear) || releaseYear < 1900 || releaseYear > DateTime.Now.Year)
            {
                Console.WriteLine("Неверный формат, повторите ввод.");
                Console.Write("Введите год выпуска: ");
                releaseYearInput = Console.ReadLine();
            }

            bool isAvailable = true;

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
            string simNumber;

            while (true)
            {
                // Вывод списка всех SIM-карт перед вводом номера для удаления
                var allSimCards = hashTable.GetAllSimCards();
                if (!allSimCards.Any())
                {
                    Console.WriteLine("Нет доступных SIM-карт.");
                    return;
                }

                Console.WriteLine("Список всех SIM-карт:");
                foreach (var simCard in allSimCards)
                {
                    Console.WriteLine($"Номер: {simCard.SimNumber}, Тариф: {simCard.Tariff}, Год выпуска: {simCard.ReleaseYear}, Доступна: {simCard.IsAvailable}");
                }

                Console.Write("Введите номер SIM-карты для удаления: ");
                simNumber = Console.ReadLine();
                if (!DataValidator.ValidateSimNumber(simNumber))
                {
                    Console.WriteLine("Неверный формат, повторите ввод.");
                    continue;
                }

                break; // Формат корректен, выходим из цикла
            }

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
            string simNumber;
            Console.Write("Введите номер SIM-карты для поиска: ");
            simNumber = Console.ReadLine();
            while (!DataValidator.ValidateSimNumber(simNumber))
            {
                Console.WriteLine("Неверный формат номера SIM-карты. Повторите ввод.");
                Console.Write("Введите номер SIM-карты для поиска: ");
                simNumber = Console.ReadLine();
            }

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
            string tariff;
            Console.Write("Введите тариф для поиска SIM-карт: ");
            tariff = Console.ReadLine();
            while (!DataValidator.ValidateTariff(tariff))
            {
                Console.WriteLine("Неверный формат тарифа. Повторите ввод.");
                Console.Write("Введите тариф для поиска SIM-карт: ");
                tariff = Console.ReadLine();
            }

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
            // Проверка на наличие доступных SIM-карт
            var availableSimCards = hashTable.GetAllSimCards().Where(simCard => simCard.IsAvailable).ToList();
            if (!availableSimCards.Any())
            {
                Console.WriteLine("Нет доступных SIM-карт для выдачи.");
                return;
            }

            // Проверка на наличие зарегистрированных клиентов
            var allClients = avlTree.GetAllClients();
            if (!allClients.Any())
            {
                Console.WriteLine("Нет зарегистрированных клиентов.");
                return;
            }

            string passportNumber;
            while (true)
            {
                // Вывод списка всех клиентов перед вводом номера паспорта
                Console.WriteLine("Список всех клиентов:");
                foreach (var client in allClients)
                {
                    Console.WriteLine($"Паспорт: {client.PassportNumber}, ФИО: {client.FullName}, Адрес: {client.Address}");
                }

                Console.Write("Введите номер паспорта: ");
                passportNumber = Console.ReadLine();
                if (!DataValidator.ValidatePassportNumber(passportNumber))
                {
                    Console.WriteLine("Неверный формат номера паспорта. Повторите ввод.");
                    continue;
                }

                // Проверка существования клиента в AVL-дереве
                if (avlTree.SearchByPassportNumber(passportNumber) == null)
                {
                    Console.WriteLine("Клиент с таким номером паспорта не найден. Повторите ввод.");
                    continue;
                }

                break; // Формат корректен и клиент существует
            }

            string simNumber;
            SimCard selectedSimCard;
            while (true)
            {
                // Вывод списка всех доступных SIM-карт перед вводом номера SIM-карты
                Console.WriteLine("Список доступных SIM-карт:");
                foreach (var sim in availableSimCards)
                {
                    Console.WriteLine($"Номер: {sim.SimNumber}, Тариф: {sim.Tariff}, Год выпуска: {sim.ReleaseYear}");
                }

                Console.Write("Введите номер SIM-карты: ");
                simNumber = Console.ReadLine();
                if (!DataValidator.ValidateSimNumber(simNumber))
                {
                    Console.WriteLine("Неверный формат номера SIM-карты. Повторите ввод.");
                    continue;
                }

                // Проверка наличия SIM-карты
                selectedSimCard = hashTable.GetSimCardByNumber(simNumber);
                if (selectedSimCard == null || !selectedSimCard.IsAvailable)
                {
                    Console.WriteLine("SIM-карта недоступна для выдачи. Повторите ввод.");
                    continue;
                }

                break; // Формат корректен и SIM-карта доступна
            }

            string issueDate;
            Console.Write("Введите дату выдачи (дд.мм.гггг): ");
            issueDate = Console.ReadLine();
            while (!DataValidator.ValidateDate(issueDate))
            {
                Console.WriteLine("Неверный формат даты. Повторите ввод.");
                Console.Write("Введите дату выдачи (дд.мм.гггг): ");
                issueDate = Console.ReadLine();
            }

            string expiryDate;
            DateTime issueDateParsed = DataParser.ParseDate(issueDate);
            DateTime expiryDateParsed;
            Console.Write("Введите дату окончания действия (дд.мм.гггг): ");
            expiryDate = Console.ReadLine();
            while (!DataValidator.ValidateDate(expiryDate) || (expiryDateParsed = DataParser.ParseDate(expiryDate)) <= issueDateParsed)
            {
                if (!DataValidator.ValidateDate(expiryDate))
                {
                    Console.WriteLine("Неверный формат даты. Повторите ввод.");
                }
                else
                {
                    Console.WriteLine("Дата окончания действия не может быть раньше или равна дате выдачи. Повторите ввод.");
                }
                Console.Write("Введите дату окончания действия (дд.мм.гггг): ");
                expiryDate = Console.ReadLine();
            }

            SimCardIssue issue = new SimCardIssue
            {
                PassportNumber = passportNumber,
                SimNumber = simNumber,
                IssueDate = issueDate,
                ExpiryDate = expiryDate
            };

            circularLinkedList.Add(issue);

            // Обновление статуса SIM-карты
            SimCard issuedSimCard = hashTable.GetSimCardByNumber(simNumber);
            issuedSimCard.IsAvailable = false;

            Console.WriteLine("Регистрация выдачи SIM-карты успешно выполнена.");
        }


        static void RegisterSimCardReturn()
        {
            // Проверка на наличие записей о выдаче SIM-карт
            var allSimCardIssues = circularLinkedList.GetAll();
            if (!allSimCardIssues.Any())
            {
                Console.WriteLine("Нет записей о выдаче SIM-карт.");
                return;
            }

            string passportNumber;
            while (true)
            {
                // Вывод списка клиентов, у кого есть выданные SIM-карты
                var clientsWithSimCards = allSimCardIssues.Select(issue => issue.PassportNumber).Distinct().ToList();
                Console.WriteLine("Клиенты с выданными SIM-картами:");
                foreach (var passport in clientsWithSimCards)
                {
                    var client = avlTree.SearchByPassportNumber(passport);
                    if (client != null)
                    {
                        Console.WriteLine($"Паспорт: {client.PassportNumber}, ФИО: {client.FullName}, Адрес: {client.Address}");
                    }
                }

                Console.Write("Введите номер паспорта: ");
                passportNumber = Console.ReadLine();
                if (!DataValidator.ValidatePassportNumber(passportNumber))
                {
                    Console.WriteLine("Неверный формат номера паспорта. Повторите ввод.");
                    continue;
                }

                // Проверка существования клиента и наличия у него выданных SIM-карт
                if (avlTree.SearchByPassportNumber(passportNumber) == null || !clientsWithSimCards.Contains(passportNumber))
                {
                    Console.WriteLine("Клиент с таким номером паспорта не найден или у него нет выданных SIM-карт. Повторите ввод.");
                    continue;
                }

                break;
            }

            string simNumber;
            SimCard simCard;
            while (true)
            {
                // Вывод списка выданных SIM-карт для выбранного клиента
                var simCardsIssuedToClient = allSimCardIssues.Where(issue => issue.PassportNumber == passportNumber).ToList();
                Console.WriteLine("Выданные SIM-карты для клиента:");
                foreach (var issue in simCardsIssuedToClient)
                {
                    var sim = hashTable.GetSimCardByNumber(issue.SimNumber);
                    if (sim != null)
                    {
                        Console.WriteLine($"Номер: {sim.SimNumber}, Тариф: {sim.Tariff}, Год выпуска: {sim.ReleaseYear}");
                    }
                }

                Console.Write("Введите номер SIM-карты: ");
                simNumber = Console.ReadLine();
                if (!DataValidator.ValidateSimNumber(simNumber))
                {
                    Console.WriteLine("Неверный формат номера SIM-карты. Повторите ввод.");
                    continue;
                }

                // Проверка наличия SIM-карты и ее выдачи выбранному клиенту
                simCard = hashTable.GetSimCardByNumber(simNumber);
                if (simCard == null || !simCardsIssuedToClient.Any(issue => issue.SimNumber == simNumber))
                {
                    Console.WriteLine("SIM-карта не найдена или не выдана данному клиенту. Повторите ввод.");
                    continue;
                }

                break;
            }

            bool result = circularLinkedList.Remove(passportNumber, simNumber);
            if (result)
            {
                simCard.IsAvailable = true;
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
                Console.WriteLine("Список всех выдач SIM-карт:");
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
            PassportNumber = "5678-123456",
            IssuePlaceAndDate = "Отделение УФМС России по Свердловской области в г. Екатеринбурге 15.05.2005",
            FullName = "Кузнецов Андрей Алексеевич",
            BirthYear = 1975,
            Address = "г. Екатеринбург, ул. Ленина, д. 15, кв. 10"
        },
        new Client
        {
            PassportNumber = "6789-234567",
            IssuePlaceAndDate = "Отделение УМВД России по Республике Татарстан в г. Казани 20.07.2006",
            FullName = "Смирнов Алексей Сергеевич",
            BirthYear = 1982,
            Address = "г. Казань, ул. Гагарина, д. 25, кв. 5"
        },
        new Client
        {
            PassportNumber = "7890-345678",
            IssuePlaceAndDate = "Отделение УФМС России по Нижегородской области в г. Нижнем Новгороде 10.09.2007",
            FullName = "Михайлов Дмитрий Иванович",
            BirthYear = 1988,
            Address = "г. Нижний Новгород, ул. Ломоносова, д. 35, кв. 12"
        }
    };

            foreach (var client in clients)
            {
                avlTree.Add(client);
            }
        }

        static void AddTestSimCards()
        {
            var simCards = new List<SimCard>
    {
        new SimCard
        {
            SimNumber = "111-1111111",
            Tariff = "Basic",
            ReleaseYear = 2019,
            IsAvailable = true
        },
        new SimCard
        {
            SimNumber = "314-1592653",
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
        }
    }
}
