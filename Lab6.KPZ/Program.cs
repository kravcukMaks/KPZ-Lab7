using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using ClassLibrary1;

namespace CSLight
{
    class Program : IObserver
    {
        static void Main(string[] args)
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)
                        System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            Console.Title = "Лабораторна робота №6";

            AccountFactory factory = new StandardAccountFactory();
            Account accountone = factory.CreateAccount(4441321567, 2606, "Максим", "Кравчук", 6200, "vt221_kmr@student.ztu.edu.ua", "863380");
            Account accountwo = factory.CreateAccount(8768573642, 8523, "Олександр", "Стадник", 2100, "kmr@student.ztu.edu.ua", "");

            Account[] accounts = new Account[] { accountone, accountwo };

            Bank bank_one = new Bank("ПриватБанк", "Cash Dispensers");
            Bank bank_two = new Bank("ЕкоБанк", "Cash Acceptors");
            Bank bank_three = new Bank("ОщадБанк", "Specialized ATMs");

            AutomatedTellerMachine atm_one = new AutomatedTellerMachine("ПриватБанк", "Cash Dispensers", 500.0, "Київська 37");
            AutomatedTellerMachine atm_two = new AutomatedTellerMachine("ЕкоБанк", "Cash Acceptors", 500000.0, "Шевченківська 13");
            AutomatedTellerMachine atm_three = new AutomatedTellerMachine("ОщадБанк", "Specialized ATMs", 30000.0, "Житомирська 55");

            int atmChoice;
            bool option = true;
            Account currentAccount = null;
            AutomatedTellerMachine currentATM = null;
            Program program = new Program();

            do
            {
                Console.BackgroundColor = ConsoleColor.DarkMagenta;
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Оберіть банкомат:");
                Console.WriteLine("1 - ПриватБанк");
                Console.WriteLine("2 - ЕкоБанк");
                Console.WriteLine("3 - ОщадБанк");
                Console.WriteLine("0 - Вийти");

                Console.Write("\nОберіть пункт: ");
                Console.ResetColor();

                if (int.TryParse(Console.ReadLine(), out atmChoice))
                {
                    switch (atmChoice)
                    {
                        case 1:
                            currentATM = atm_one;
                            break;
                        case 2:
                            currentATM = atm_two;
                            break;
                        case 3:
                            currentATM = atm_three;
                            break;
                        case 0:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Такого банкомату не існує.");
                            continue;
                    }

                    Console.Clear();
                    do
                    {
                        Console.WriteLine("Введіть номер картки:");
                        string cardPattern = @"^\d{10}$";
                        string enteredCardNumber = Console.ReadLine();

                        if (Regex.IsMatch(enteredCardNumber, cardPattern))
                        {
                            Console.WriteLine("Введіть пін-код до картки:");
                            int enteredPinCode;

                            if (int.TryParse(Console.ReadLine(), out enteredPinCode) && enteredPinCode >= 1000)
                            {
                                bool accountFound = false;
                                for (int i = 0; i < accounts.Length; i++)
                                {
                                    if (enteredCardNumber == accounts[i].Card_number.ToString() && enteredPinCode == accounts[i].Pin_code)
                                    {
                                        Show_Massage("Доступ дозволено");
                                        currentAccount = accounts[i];
                                        currentAccount.RegisterObserver(program);
                                        option = false;
                                        accountFound = true;
                                        break;
                                    }
                                }

                                if (!accountFound)
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.WriteLine("Номер картки або пін-код невірні!");
                                    Console.ResetColor();
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("Введіть коректний пін-код (мінімум 4 цифри)!");
                                Console.ResetColor();
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Введіть коректний номер картки (10 цифр)!");
                            Console.ResetColor();
                        }
                    } while (option);
                    option = true;
                    bool exit = false;
                    do
                    {
                        Console.BackgroundColor = ConsoleColor.Cyan;
                        Console.Clear();

                        Console.WriteLine("Інформація про банкомат:");
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        currentATM.ShowInfo();
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine();
                        Console.WriteLine("Оберіть опцію:");
                        Console.WriteLine("1 - Перегляд балансу");
                        Console.WriteLine("2 - Зняття коштів");
                        Console.WriteLine("3 - Перекидання грошей на рахунок картки");
                        Console.WriteLine("4 - Перерахування коштів на рахунок картки із заданим номером");
                        Console.WriteLine("0 - Вийти");
                        Console.Write("\nОберіть пункт: ");
                        Console.ResetColor();

                        if (int.TryParse(Console.ReadLine(), out int menuChoice))
                        {
                            IATMOperationStrategy strategy = null;
                            switch (menuChoice)
                            {
                                case 1:
                                    strategy = new BalanceInquiryStrategy();
                                    break;
                                case 2:
                                    strategy = new WithdrawStrategy();
                                    break;
                                case 3:
                                    // Implement DepositStrategy
                                    break;
                                case 4:
                                    // Implement TransferStrategy
                                    break;
                                case 0:
                                    exit = true;
                                    break;
                                default:
                                    Show_Massage("Такої опції немає.");
                                    break;
                            }

                            if (strategy != null && currentAccount != null)
                            {
                                strategy.Execute(currentAccount, currentATM, 0); // Amount is 0 for balance inquiry
                            }
                        }
                        else
                        {
                            Show_Massage("Введіть коректний номер опції.");
                        }

                        if (!exit)
                        {
                            Show_Massage("\nНатисніть будь-яку клавішу, щоб продовжити...");
                            Console.ReadKey();
                        }

                    } while (!exit);
                }
                else
                {
                    Show_Massage("Введіть коректний номер банкомату.");
                }
            } while (true);
        }

        static void Show_Massage(string message)
        {
            Console.WriteLine(message);
        }

        public void Update(string message)
        {
            Show_Massage(message);
        }
    }
}