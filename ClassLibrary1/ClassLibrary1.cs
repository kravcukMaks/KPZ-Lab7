using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;


namespace ClassLibrary1
{
    public class Account
    {
        public delegate void AccountStateHandler(string messege);
        AccountStateHandler _del;

        protected long card_number;
        protected int pin_code;
        protected string name;
        protected string surname;
        protected double balance;
        private double _sum;
        protected string email;
        protected string password;
        SmtpClient smtpClient;

        public long Card_number
        {
            get => card_number;
            set => card_number = value;
        }
        public int Pin_code
        {
            get => pin_code;
            set => pin_code = value;
        }
        public string Name
        {
            get => name;
            set => name = value;
        }

        public string Surname
        {
            get => surname;
            set => surname = value;
        }

        public double Balance
        {
            get => balance;
            set => balance = value;
        }
        public string Email
        {
            get => email;
            set => email = value;
        }
        public string Password
        {
            get => password;
            set => password = value;
        }
        public Account(long card_number, int pin_code, string name, string surname, double balance, string email, string password)
        {
            Card_number = card_number;
            Pin_code = pin_code;
            Name = name;
            Surname = surname;
            Balance = balance;
            _sum = balance;
            this.email = email;
            this.password = password;
            this.smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(email, password),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

        }
        public override string ToString()
        {
            return ($"Номер рахунку:{Card_number}\nПінкод:{Pin_code}\nІм'я:{Name}\nПрізвище:{Surname}\nБаланс:{Balance}");
        }

        public void RegisterHandler(AccountStateHandler del)
        {
            _del = del;
        }

        public void Withdraw(int sum)
        {
            if (sum <= _sum)
            {
                _sum -= sum;
                Balance -= sum;
                if (_del != null)
                    _del($"Сумма {sum} снята со счета");
                smtpClient.Send("vt221_kmr@student.ztu.edu.ua", $"{email}", "Гроші були зняті з рахунку", $"Сумма {sum} снята со счета");
            }
            else
            {
                if (_del != null)
                    _del("Недостаточно денег на счете");
            }
        }
    }
}


namespace ClassLibrary1
{
    public class Bank
    {
        protected string name_of_the_bank;
        protected string list_of_ATMs;


        public string Name_Of_The_Bank
        {
            get => name_of_the_bank;
            set => name_of_the_bank = value;
        }
        public string List_Of_ATMS
        {
            get => list_of_ATMs;
            set => list_of_ATMs = value;
        }

        public Bank(string name_of_the_bank, string list_of_ATMs)
        {
            Name_Of_The_Bank = name_of_the_bank;
            List_Of_ATMS = list_of_ATMs;

        }

    }
}

namespace ClassLibrary1
{
    public class AutomatedTellerMachine : Bank
    {
        protected double money;
        protected string atm_ID;
        public double Money
        {
            get => money;
            set => money = value;
        }
        public string ATM_ID
        {
            get => atm_ID;
            set => atm_ID = value;
        }
        public AutomatedTellerMachine(string name_of_the_bank, string list_of_ATMs, double money, string atm_ID) : base(name_of_the_bank, list_of_ATMs)
        {
            Money = money;
            ATM_ID = atm_ID;
        }
        public void ShowInfo()
        {
            Console.WriteLine($"Назва банку:{Name_Of_The_Bank}\nСписок банкоматів:{List_Of_ATMS}\nКількість грошей у банкоматі:{Money}\nАдреса:{ATM_ID}");
        }
        public void WithdrawMoney(double amount)
        {
            if (amount <= Money)
            {
                Money -= amount;
            }
        }
    }
}
