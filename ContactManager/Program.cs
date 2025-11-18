// OOP (Inheritance & Polymorphism) ---> Define an abstract class Contact and derive two specific classes from it (e.g., FamilyContact, WorkContact).
// Collections --->	Use a List<Contact> to store all contacts. This demonstrates polymorphism in collections.
// Methods --->	Implement an abstract method in Contact, like GetDetails(), and override it in the derived classes to show specific information (e.g., WorkContact includes a Department).
// User Input & Logic --->	Use a main menu and a switch statement for commands like "Add," "View All," and "Search."
// LINQ --->	Use the .Where() method to quickly search the List<Contact> by a partial name match.


// name phone email rship

using System;

namespace ContactManager
{
    class Program
    {
        abstract class Contact
        {
            public string Name{get; set;}
            public string Phone{get;set;}
            public string Email{get;set;}
            // private string Relationship{get;set;}
            public Contact(string name, string phone, string email)
            {
                this.Name = name;
                this.Phone = phone;
                this.Email = email;
            }

            public abstract void GetDetails();
        }


        class FamilyContact: Contact
        {
            public string Relationship{get; set;}
            public FamilyContact(string name, string phone, string email, string relationship): base(name, phone, email)
            {
             this.Relationship = relationship;   
            }

            public override void GetDetails()
            {

                Console.WriteLine($"Name: {Name}\n Phone: {Phone}\n Email: {Email}\n Relationship: {Relationship}");
            }
        }


        class WorkContact: Contact
        {
            private string Department{get; set;}

            public WorkContact(string name, string phone, string email, string department): base(name, phone, email)
            {
                this.Department = department;
            }

            public override void GetDetails()
            {
                Console.WriteLine($"Name: {Name}\n Phone: {Phone}\n Email: {Email}\n Department: {Department}");
            }
        }

        private static List<Contact> myContacts = new List<Contact>();


        static void InitializeContacts()
        {
            myContacts.Add(new FamilyContact("Alice Smith", "555-0001", "alice@home.com", "Sister"));
            myContacts.Add(new WorkContact("Kis Becer", "555-0002", "beker@work.com", "Accounting"));
        }

        static void ViewAllContacts()
        {
            if(myContacts == null || myContacts.Count() == 0) Console.WriteLine("Empty contact list");
            foreach(var item in myContacts)
            {
                item.GetDetails();
            }
        }

        static void AddContact()
        {   
            Console.WriteLine("Enter name");
            string name = Console.ReadLine();

            Console.WriteLine("Enter phone number");
            string phone = Console.ReadLine();

            Console.WriteLine("Enter email");
            string email = Console.ReadLine();

            Console.WriteLine("Enter Work contact (W) or Family Contact (F)");
            string category = Console.ReadLine();

            Contact newContact = null;

            switch (category.ToUpper())
            {
                case "W":
                    Console.WriteLine("Enter Department");
                    string department = Console.ReadLine();
                    newContact = new WorkContact(name, phone, email, department);
                    break;

                case "F":
                    Console.WriteLine("Enter relationship");
                    string relationship = Console.ReadLine();
                    newContact = new WorkContact(name, phone, email, relationship);

                    break;

                default:
                    Console.WriteLine("You entered an invalid option. Try again");
                    return;
                
            }


            myContacts.Add(newContact);
 

        }

        static void Main(string[] args)
        {
            
            Console.WriteLine("--WELCOME TO SIMPLE CONTACT MANAGER---");

             Console.WriteLine("\n--- Menu ---");
            Console.WriteLine("1. Add New Contact");
            Console.WriteLine("2. View All Contacts");
            // Console.WriteLine("3. Search by Name");
            Console.WriteLine("4. Exit");


            while (true)
            {
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddContact();
                        break;

                    case "2":
                        ViewAllContacts();
                        break;

                    case "4":
                    Console.WriteLine("Exiting the app...");
                    return;

                    default:
                        Console.WriteLine("Select a valid option");
                        break;
                }
            }

            

        }
    }
}





// GEMINI'S IMPLEMENTATION


// using System;
// using System.Collections.Generic;
// using System.Linq; // Required for LINQ methods like .Where() and .ToList()

// namespace SimpleContactManager
// {
//     // C# Abstraction: Defines the universal blueprint for a Contact.
//     public abstract class Contact
//     {
//         // C# Properties: Encapsulated data common to ALL contacts.
//         public string Name { get; set; }
//         public string Phone { get; set; }
//         public string Email { get; set; }

//         // C# Constructor: Base constructor used to initialize common data.
//         public Contact(string name, string phone, string email)
//         {
//             this.Name = name;
//             this.Phone = phone;
//             this.Email = email;
//         }

//         // C# Polymorphism: Abstract method requires implementation in all derived classes.
//         // It enforces the rule: "Every contact MUST know how to display its own specific details."
//         public abstract void DisplayDetails();
//     }

//     // C# Inheritance: Adds specific data (Relationship) and implements the abstract behavior.
//     public class FamilyContact : Contact
//     {
//         public string Relationship { get; set; }

//         public FamilyContact(string name, string phone, string email, string relationship)
//             : base(name, phone, email) // Calls the base Contact constructor
//         {
//             this.Relationship = relationship;
//         }

//         // C# Polymorphism: The Family-specific implementation.
//         public override void DisplayDetails()
//         {
//             Console.WriteLine($"[TYPE: Family] Name: {Name}");
//             Console.WriteLine($"|-- Phone: {Phone}");
//             Console.WriteLine($"|-- Email: {Email}");
//             Console.WriteLine($"|-- Relationship: {Relationship}");
//         }
//     }

//     // C# Inheritance: Adds different specific data (Department) and implements the abstract behavior.
//     public class WorkContact : Contact
//     {
//         public string Department { get; set; }

//         public WorkContact(string name, string phone, string email, string department)
//             : base(name, phone, email) // Calls the base Contact constructor
//         {
//             this.Department = department;
//         }

//         // C# Polymorphism: The Work-specific implementation.
//         public override void DisplayDetails()
//         {
//             Console.WriteLine($"[TYPE: Work] Name: {Name}");
//             Console.WriteLine($"|-- Phone: {Phone}");
//             Console.WriteLine($"|-- Email: {Email}");
//             Console.WriteLine($"|-- Department: {Department}");
//         }
//     }

//     class Program
//     {
//         // C# Collections: The List<Contact> is the central data container.
//         // It holds derived types (FamilyContact, WorkContact) as the base Contact type.
//         private static List<Contact> contactList = new List<Contact>();

//         static void Main(string[] args)
//         {
//             InitializeData();

//             Console.Title = "Simple Contact Manager";
//             Console.WriteLine("--- Simple Contact Manager (OOP Demo) ---");
            
//             // C# While Loop: Main menu loop.
//             while (true)
//             {
//                 DisplayMenu();
//                 string choice = Console.ReadLine();

//                 switch (choice)
//                 {
//                     case "1":
//                         AddContact();
//                         break;
//                     case "2":
//                         ViewAllContacts();
//                         break;
//                     case "3":
//                         SearchContacts();
//                         break;
//                     case "4":
//                         Console.WriteLine("Exiting application. Goodbye!");
//                         return;
//                     default:
//                         Console.WriteLine("Invalid choice. Please select 1, 2, 3, or 4.");
//                         break;
//                 }
//                 Console.WriteLine("\n-------------------------------------------");
//             }
//         }

//         static void DisplayMenu()
//         {
//             Console.WriteLine("\n--- Menu ---");
//             Console.WriteLine("1. Add New Contact");
//             Console.WriteLine("2. View All Contacts");
//             Console.WriteLine("3. Search by Name");
//             Console.WriteLine("4. Exit");
//             Console.Write("Enter your choice: ");
//         }

//         // C# Methods: Initializes the list with various contact types.
//         static void InitializeData()
//         {
//             contactList.Add(new FamilyContact("Alice Smith", "555-0001", "alice@home.com", "Sister"));
//             contactList.Add(new WorkContact("Bob Johnson", "555-0002", "bob@corp.com", "Engineering"));
//             contactList.Add(new FamilyContact("Charlie Brown", "555-0003", "charlie@home.com", "Brother"));
//         }

//         // C# Methods: Handles user input for creating a new contact.
//         static void AddContact()
//         {
//             Console.WriteLine("\n--- Add New Contact ---");
//             Console.Write("Enter Name: ");
//             string name = Console.ReadLine();
//             Console.Write("Enter Phone: ");
//             string phone = Console.ReadLine();
//             Console.Write("Enter Email: ");
//             string email = Console.ReadLine();

//             Console.Write("Type (F for Family, W for Work): ");
//             string type = Console.ReadLine()?.ToUpper();

//             Contact newContact = null;

//             // C# Switch: Instantiates the correct derived class.
//             switch (type)
//             {
//                 case "F":
//                     Console.Write("Enter Relationship: ");
//                     string relationship = Console.ReadLine();
//                     newContact = new FamilyContact(name, phone, email, relationship);
//                     break;
//                 case "W":
//                     Console.Write("Enter Department: ");
//                     string department = Console.ReadLine();
//                     newContact = new WorkContact(name, phone, email, department);
//                     break;
//                 default:
//                     Console.WriteLine("[ERROR] Invalid contact type. Contact not added.");
//                     return;
//             }

//             // C# Collections: Add the new object to the List<Contact>.
//             contactList.Add(newContact);
//             Console.WriteLine($"[SUCCESS] Contact '{name}' added as a {newContact.GetType().Name}.");
//         }

//         // C# Methods: Views all contacts in the list.
//         static void ViewAllContacts()
//         {
//             if (contactList.Count == 0)
//             {
//                 Console.WriteLine("The contact list is empty.");
//                 return;
//             }
            
//             Console.WriteLine($"\n--- All Contacts ({contactList.Count}) ---");
            
//             // C# Loops: The core logic of polymorphism.
//             foreach (var contact in contactList)
//             {
//                 // The runtime decides which specific DisplayDetails() to call:
//                 // If 'contact' is a FamilyContact, it calls the Family version.
//                 // If 'contact' is a WorkContact, it calls the Work version.
//                 contact.DisplayDetails(); 
//                 Console.WriteLine("---");
//             }
//         }

//         // C# Methods: Searches the list using LINQ.
//         static void SearchContacts()
//         {
//             Console.Write("\nEnter search term (partial name): ");
//             string searchTerm = Console.ReadLine();

//             if (string.IsNullOrWhiteSpace(searchTerm)) return;

//             // C# LINQ: Use .Where() to filter the list.
//             var results = contactList
//                 // Check if the Name contains the searchTerm, ignoring case.
//                 .Where(c => c.Name.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
//                 .ToList(); // Execute the query and put results into a new List.
            
//             if (results.Any())
//             {
//                 Console.WriteLine($"\n--- Found {results.Count} Result(s) ---");
//                 foreach (var contact in results)
//                 {
//                     contact.DisplayDetails();
//                     Console.WriteLine("---");
//                 }
//             }
//             else
//             {
//                 Console.WriteLine($"No contacts found matching '{searchTerm}'.");
//             }
//         }
//     }
// }