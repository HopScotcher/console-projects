using System;

namespace ContactManager
{
    class Program
    {

        abstract class Contact
        {
            private string name{get;set;}
            private int phoneNo{get;set;}

            public abstract void GetDetails()
            {
                
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("--WELCOME TO SIMPLE CONTACT MANAGER---");


        }
    }
}