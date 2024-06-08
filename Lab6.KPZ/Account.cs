using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace ClassLibrary1
{
    public interface IObserver
    {
        void Update(string message);
    }

    public interface ISubject
    {
        void RegisterObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        void NotifyObservers(string message);
    }

    public class Account : ISubject
    {
        private List<IObserver> observers = new List<IObserver>();

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
                Port = 587, // Порт 587 - це стандартний порт для шифрованого (TLS) з'єднання
                Credentials = new NetworkCredential(email, password),
                EnableSsl = true, // Увімкнути шифрування (SSL/TLS)
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
            };
        }

        public override string ToString()
        {
            return ($"Номер рахунку:{Card_number}\nПінкод:{Pin_code}\nІм'я:{Name}\nПрізвище:{Surname}\nБаланс:{Balance}");
        }

        public void RegisterObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers(string message)
        {
            foreach (var observer in observers)
            {
                observer.Update(message);
            }
        }

        public void Withdraw(int sum)
        {
            if (sum <= _sum)
            {
                _sum -= sum;
                Balance -= sum;
                NotifyObservers($"Сума {sum} знята з рахунку");
                smtpClient.Send("vt221_kmr@student.ztu.edu.ua", $"{email}", "Гроші знято", $"Сума {sum} грн. знята з рахунку!!!");
            }
            else
            {
                NotifyObservers("Недостатньо грошей на рахунку");
            }
        }
    }
}