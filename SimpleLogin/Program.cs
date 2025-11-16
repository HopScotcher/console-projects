 using System;

 namespace SimpleLogin{

    public class User{
        public string Username {get;set;}
        public string Password {get; set;}
    };

 
    class Program{
        static void Main(string[] args){

        File.AppendAllText("logindb.txt", "tyler, fight123\n");
        Console.WriteLine("-----Welcome to simple login -----");
        Console.WriteLine("1. Login");
        Console.WriteLine("2. Register");

            
            string choice = Console.ReadLine();
            do
            {
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Username:");
                        string username1 = Console.ReadLine();

                        Console.WriteLine("Password:");
                        string password1 = Console.ReadLine();

                        Login(username1, password1);
                        break;

                    case "2":
                        Console.WriteLine("Username:");
                        string username2 = Console.ReadLine();

                        Console.WriteLine("Password:");
                        string password2 = Console.ReadLine();

                        Register(username2, password2);
                        break;

                    default:
                        Console.WriteLine("Select a valid choice");
                        continue;
                }
            }while(choice != "1" && choice != "2");

        }



            static void Register(string username, string password)
        {

            try
            {
                var dbContents = File.ReadAllLines("logindb.txt");


               bool userExists =  dbContents.Any(line =>
                {
                    string[] parts = line.Split(',');

                    return parts.Length >= 1 && parts[0].Equals(username, StringComparison.OrdinalIgnoreCase);
                });

                if (userExists)
                {
                    Console.WriteLine($"Username {username} is already taken");
                }
                else
                {
                    File.AppendAllText("logindb.txt", $"{username},{password}\n");
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex}");
            }
             
        }

        static void Login(string username, string password)
        {
            try
            {
                 
                    var dbContents = File.ReadAllLines("logindb.txt");

                    foreach(var pair in dbContents)
                    {
                        if (string.IsNullOrWhiteSpace(pair)) continue;
                        string[] values = pair.Split(',');

                        if (values.Length == 2)
                        {
                             string fileUsername = values[0];
                             string filePassword = values[1];

                            if(fileUsername.Equals(username, StringComparison.OrdinalIgnoreCase) && filePassword.Equals(password))
                            {
                                Console.WriteLine("logged in successfully");
                            }

                        }

                    }

                    Console.WriteLine("Username not found");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex}");
            }
        }
    }
 }



// GEMINI'S IMPLEMENTATION

// using System;
// using System.IO;
// using System.Linq; // Needed for LINQ operations like .Any()

// namespace UserAuthSystem
// {
//     // C# Classes/Objects: Defines the data model for a user session.
//     public class User
//     {
//         public string Username { get; set; }
//         public string Password { get; set; }
//         // We could add more properties here, like Email or CreatedDate
//     }

//     class Program
//     {
//         // C# Files: Define the simple "database" file
//         private const string UserDataFile = "users.csv";
//         private const char Delimiter = ',';

//         static void Main(string[] args)
//         {
//             Console.WriteLine("--- Welcome to the Auth System Demo ---");

//             // C# While Loop: Main application loop
//             while (true)
//             {
//                 Console.WriteLine("\nChoose an option:");
//                 Console.WriteLine("1. Register");
//                 Console.WriteLine("2. Login");
//                 Console.WriteLine("3. Exit");
//                 Console.Write("> ");

//                 string choice = Console.ReadLine();

//                 switch (choice)
//                 {
//                     case "1":
//                         RegisterUser();
//                         break;
//                     case "2":
//                         LoginUser();
//                         break;
//                     case "3":
//                         Console.WriteLine("Goodbye!");
//                         return;
//                     default:
//                         Console.WriteLine("Invalid choice. Please try again.");
//                         break;
//                 }
//             }
//         }

//         /// <summary>
//         /// Reads all users from the CSV file.
//         /// C# Exceptions: Handles FileNotFoundException if no users exist yet.
//         /// </summary>
//         private static string[] ReadAllUserLines()
//         {
//             // C# Files: We wrap the file reading in a try-catch block
//             try
//             {
//                 // File.ReadAllLines reads all text and returns a string array, one element per line.
//                 return File.ReadAllLines(UserDataFile);
//             }
//             catch (FileNotFoundException)
//             {
//                 // Expected scenario: the file hasn't been created yet.
//                 Console.WriteLine("[INFO] User file not found. Starting fresh database.");
//                 return new string[0]; // Return an empty array
//             }
//             catch (IOException ex)
//             {
//                 // Handle file locked or permission errors
//                 Console.WriteLine($"[ERROR] File access denied: {ex.Message}");
//                 return new string[0]; 
//             }
//         }

//         /// <summary>
//         /// Registers a new user account.
//         /// </summary>
//         static void RegisterUser()
//         {
//             Console.Write("Enter new username: ");
//             string username = Console.ReadLine();
            
//             if (IsUsernameTaken(username))
//             {
//                 Console.WriteLine("[ERROR] Username already exists. Please choose another.");
//                 return;
//             }

//             Console.Write("Enter password (INSECURE DEMO): ");
//             string password = Console.ReadLine();

//             // C# Strings: Format the data for the CSV file.
//             string newUserRecord = $"{username}{Delimiter}{password}{Environment.NewLine}";

//             try
//             {
//                 // C# Files: Append the new record to the file.
//                 File.AppendAllText(UserDataFile, newUserRecord);
//                 Console.WriteLine($"\n[SUCCESS] User '{username}' registered!");
//             }
//             catch (Exception ex)
//             {
//                 Console.WriteLine($"[FATAL] Could not write to file: {ex.Message}");
//             }
//         }
        
//         /// <summary>
//         /// Checks if a username already exists in the file.
//         /// </summary>
//         private static bool IsUsernameTaken(string username)
//         {
//             // We use LINQ's .Any() method for a quick check.
//             return ReadAllUserLines()
//                 .Any(line => 
//                 {
//                     // C# Strings: Use .Split to separate the username from the password
//                     string[] parts = line.Split(Delimiter);
//                     // Check if the first part (username) matches the input
//                     return parts.Length >= 1 && parts[0].Equals(username, StringComparison.OrdinalIgnoreCase);
//                 });
//         }

//         /// <summary>
//         /// Logs a user in by checking credentials against the file.
//         /// </summary>
//         static void LoginUser()
//         {
//             Console.Write("Enter username: ");
//             string username = Console.ReadLine();
//             Console.Write("Enter password: ");
//             string password = Console.ReadLine();

//             string[] userLines = ReadAllUserLines();
            
//             // C# Loops: Iterate through every line in the file
//             foreach (string line in userLines)
//             {
//                 if (string.IsNullOrWhiteSpace(line)) continue;

//                 // C# Strings: Split the line into username and password parts.
//                 string[] parts = line.Split(Delimiter);

//                 // C# Logic (If/Else): Check if the line has the required parts
//                 if (parts.Length == 2)
//                 {
//                     string fileUsername = parts[0];
//                     string filePassword = parts[1];

//                     // Check for a match
//                     if (fileUsername.Equals(username, StringComparison.OrdinalIgnoreCase) && filePassword.Equals(password))
//                     {
//                         Console.WriteLine($"\n[WELCOME] Login successful! Welcome, {username}.");
//                         return; // Exit the LoginUser method immediately
//                     }
//                 }
//             }

//             // If the loop finishes without finding a match:
//             Console.WriteLine("\n[FAILURE] Invalid username or password.");
//         }
//     }
// }








