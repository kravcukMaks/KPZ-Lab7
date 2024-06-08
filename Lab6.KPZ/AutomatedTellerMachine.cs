using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static ClassLibrary1.Account;

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
