using ClassLibrary1;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

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
