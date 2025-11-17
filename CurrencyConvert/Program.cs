// Data Structures ---	Use a Dictionary<string, decimal> to store the currency code (string) and the exchange rate (decimal) for fast lookups.
// Data Types	---Use the decimal type for all money amounts and exchange rates to ensure perfect precision.
// Logic	--Use a switch statement to select the target currency based on the user's input code (e.g., typing "EUR").
// String Formatting---	Use string interpolation and the C (Currency) format specifier to display the final result.
// Loops---	Use a loop to allow multiple conversions without restarting the program.
// Exception Handling	---Use a try-catch block to handle a KeyNotFoundException if the user enters a currency code that doesn't exist in your dictionary.




using System;

namespace CurrencyConvert
{
    class Program
    {

        static void Converter(decimal amount, string currency)
        {
            Dictionary<string, decimal> currencyDic = new Dictionary <string, decimal> {{"EUR", 0.0067m}, {"USD", 0.0077m},{"YEN", 1.2m} };

            decimal rate = currencyDic[currency];

            Console.WriteLine($"Ksh. {amount} = {currency} {amount*rate}");
             
        }


        static void Main(string[] args)
        {
            while (true)
            {

            Console.WriteLine("-----CURRENCY CONVERTER -- CONVERT KSH TO ANY CURRENCY----");
            
            Console.WriteLine("Enter the amount to convert");
            string amountStr = Console.ReadLine();

            decimal amount = decimal.Parse(amountStr);
            
            Console.WriteLine("Enter target currency below:");
            Console.WriteLine("1: EUR\n 2: USD\n 3: YEN\n");

            string selectedCurrency = Console.ReadLine();
                
            switch (selectedCurrency)
            {
                case "1":
                    Converter(amount, "EUR");
                    break;
                case "2":
                    Converter(amount, "USD");
                    break;

                case "3":
                    Converter(amount, "YEN");
                    break;
                case "4":
                    Console.WriteLine("Exiting the app...");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Please select a valid input from below");
                    continue;
                
            }
            }

        }
    }
}


// GEMINI'S IMPLEMENTATION


// using System;
// using System.Collections.Generic;

// namespace ConsoleCurrencyConverter
// {
//     class Program
//     {
//         // C# Data Structures: Dictionary for fast lookup of exchange rates.
//         // TKey is the currency code (string), TValue is the rate (decimal).
//         private static readonly Dictionary<string, decimal> ExchangeRates = new Dictionary<string, decimal>
//         {
//             // Base currency is 1 USD.
//             { "EUR", 0.93m },   // Euro
//             { "JPY", 151.75m }, // Japanese Yen
//             { "GBP", 0.81m },   // British Pound
//             { "CAD", 1.36m },   // Canadian Dollar
//             { "USD", 1.00m }    // Identity mapping (for clarity)
//         };

//         static void Main(string[] args)
//         {
//             Console.Title = "Simple Currency Converter (USD Base)";
//             Console.WriteLine("---- Simple Currency Converter ----");
//             Console.WriteLine("Base Currency: USD (US Dollar)");
//             Console.WriteLine("Available Currencies: EUR, JPY, GBP, CAD");
            
//             // C# While Loop: Keeps the converter running.
//             while (true)
//             {
//                 // 1. Get Amount to Convert
//                 decimal amountUSD = GetAmountFromUser();

//                 // Check for exit condition signaled by GetAmountFromUser()
//                 if (amountUSD == -1.0m)
//                 {
//                     break;
//                 }

//                 // 2. Get Target Currency Code
//                 Console.Write("Enter the target currency code (e.g., EUR, JPY) or 'exit': ");
//                 string targetCode = Console.ReadLine()?.ToUpper() ?? "";

//                 if (targetCode == "EXIT")
//                 {
//                     break;
//                 }

//                 // 3. Perform Conversion and Handle Exceptions
//                 ConvertAndDisplay(amountUSD, targetCode);

//                 Console.WriteLine("\n-------------------------------------------");
//             }

//             Console.WriteLine("Application shutting down. Goodbye!");
//         }

//         /// <summary>
//         /// Prompts the user for a numeric amount and validates the input.
//         /// </summary>
//         /// <returns>The validated decimal amount, or -1.0m to signal exit.</returns>
//         static decimal GetAmountFromUser()
//         {
//             while (true)
//             {
//                 Console.Write("Enter amount in USD or type 'exit': ");
//                 string input = Console.ReadLine();

//                 if (input?.ToLower() == "exit")
//                 {
//                     return -1.0m;
//                 }

//                 // C# Data Types & Type Casting: Safely converts string input to decimal.
//                 // Uses 'm' suffix for literal decimal types.
//                 if (decimal.TryParse(input, out decimal amount) && amount >= 0)
//                 {
//                     return amount;
//                 }
//                 else
//                 {
//                     Console.WriteLine("[ERROR] Invalid amount. Please enter a valid positive number.");
//                 }
//             }
//         }

//         /// <summary>
//         /// Attempts to convert the currency and displays the result.
//         /// </summary>
//         static void ConvertAndDisplay(decimal amountUSD, string targetCode)
//         {
//             // C# Exceptions: We use a try-catch block specifically for KeyNotFoundException.
//             try
//             {
//                 // C# Logic: Retrieves the rate from the Dictionary.
//                 // If the key (targetCode) doesn't exist, this line throws a KeyNotFoundException.
//                 decimal rate = ExchangeRates[targetCode];

//                 // C# Methods & Operators: Performs the calculation using decimal precision.
//                 decimal convertedAmount = ConvertCurrency(amountUSD, rate);

//                 // C# String Formatting: Uses the Currency (C) specifier for clean output.
//                 Console.WriteLine($"\n--- Conversion Result ---");
//                 Console.WriteLine($"{amountUSD:C} USD is equal to {convertedAmount:C} {targetCode}");
//                 Console.WriteLine($"Exchange Rate: 1 USD = {rate:N4} {targetCode}");
//             }
//             // C# Exceptions: Catch block runs ONLY if the Dictionary lookup fails.
//             catch (KeyNotFoundException)
//             {
//                 Console.WriteLine($"[ERROR] Currency code '{targetCode}' is not supported.");
//                 Console.WriteLine("Please choose from: EUR, JPY, GBP, CAD.");
//             }
//             // Catch any other unexpected error.
//             catch (Exception ex)
//             {
//                 Console.WriteLine($"[FATAL ERROR] An unexpected error occurred: {ex.Message}");
//             }
//         }

//         /// <summary>
//         /// C# Methods: Calculation logic is encapsulated in a separate method (SRP).
//         /// </summary>
//         static decimal ConvertCurrency(decimal amount, decimal rate)
//         {
//             return amount * rate;
//         }
//     }
// }