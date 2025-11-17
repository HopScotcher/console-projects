
using System;

namespace GuessGame
{
    class Program
    {  
        static void Main(string[] args)
        {

            Random random = new Random();

            int n = random.Next(1, 10);

            Console.WriteLine(n);

            Console.WriteLine("guess the secret number");
            string guessStr = Console.ReadLine();
            int guessInt = int.Parse(guessStr);

            int guessCount = 5;

            Console.WriteLine($"My guess is {guessInt}");

            while(guessInt != n && guessCount != 0)
            {   
                guessCount--;
                string hint = guessInt > n? "go lower": "go higher";

                if(guessCount == 0)
                {
                    Console.WriteLine("You are out of guesses");
                    return;
                }
                else
                {
                    Console.WriteLine($"{hint} ({guessCount} guesses left!)");
                }

                guessStr = Console.ReadLine();
                guessInt = int.Parse(guessStr);  

            } 
            Console.WriteLine("you guessed right");          
        }
    }
}






// GEMINI'S IMPLEMENTATION

// using System;
// using System.IO;

// namespace GuessingGameManager
// {
//     class Program
//     {
         
//         private const string HighScoreFile = "highscore.txt";
        
//         private static readonly Random random = new Random();
//         private const int MaxNumber = 100;

//         static void Main(string[] args)
//         {
//             Console.Title = "The Guessing Game Manager";
//             Console.WriteLine("--- Welcome to The Guessing Game ---");
//             Console.WriteLine($"Try to guess the secret number (1 to {MaxNumber})!");
            
//             // C# Methods: Load the best score from the file at startup.
//             int currentHighScore = LoadHighScore();
//             Console.WriteLine($"Current All-Time Best Score (Lowest Guesses): {currentHighScore}");
            
//             // C# While Loop: Main application loop to allow multiple games.
//             while (true)
//             {
//                 int guessesTaken = PlayGame();
                
//                 // C# Logic (If...Else): Check if a new record was set.
//                 if (guessesTaken > 0 && guessesTaken < currentHighScore)
//                 {
//                     currentHighScore = guessesTaken;
//                     Console.WriteLine("\n*** CONGRATULATIONS! NEW HIGH SCORE! ***");
//                     // C# Methods: Save the new record to the file.
//                     SaveHighScore(currentHighScore);
//                 }
                
//                 Console.WriteLine("\n-------------------------------------------");
//                 Console.Write("Press Enter to play again, or type 'exit' to quit: ");
//                 if (Console.ReadLine().ToLower() == "exit")
//                 {
//                     break;
//                 }
//             }
//         }

//         /// <summary>
//         /// Manages the core game loop for a single game.
//         /// </summary>
//         /// <returns>The number of guesses taken, or -1 if the user quits.</returns>
//         static int PlayGame()
//         {
//             // Generate a random number between 1 and MaxNumber (inclusive).
//             int secretNumber = random.Next(1, MaxNumber + 1);
//             int guessCount = 0;
//             int guess = 0;

//             Console.WriteLine("\n--- NEW GAME STARTED ---");

//             // C# While Loop: Loop continues until the user guesses correctly.
//             while (guess != secretNumber)
//             {
//                 guessCount++;
//                 Console.Write($"Guess #{guessCount}: Enter your number: ");
//                 string input = Console.ReadLine();
                
//                 // C# Break/Continue: Allows the user to exit the game mid-session.
//                 if (input.ToLower() == "exit")
//                 {
//                     Console.WriteLine("Game abandoned.");
//                     return -1;
//                 }

//                 // C# Type Casting: Input validation using TryParse.
//                 // C# Logic: If TryParse is NOT successful (!):
//                 if (!int.TryParse(input, out guess))
//                 {
//                     Console.WriteLine("Invalid input. Please enter a whole number.");
//                     guessCount--; // Don't count invalid input as a guess
//                     continue;    // Skip to the next iteration of the while loop
//                 }
                
//                 // C# If...Else: Comparison logic
//                 if (guess < secretNumber)
//                 {
//                     Console.WriteLine("Too low! Try again.");
//                 }
//                 else if (guess > secretNumber)
//                 {
//                     Console.WriteLine("Too high! Try again.");
//                 }
//             }

//             // Game over
//             Console.WriteLine($"\n*** Correct! The secret number was {secretNumber} ***");
//             Console.WriteLine($"You guessed it in {guessCount} attempts.");
//             return guessCount;
//         }

//         /// <summary>
//         /// C# Files & Exceptions: Reads the high score from the file.
//         /// </summary>
//         static int LoadHighScore()
//         {
//             // C# Exceptions: The file reading is placed in a try-catch block
//             try
//             {
//                 // C# Files: Read all text (which should be a single number).
//                 string scoreText = File.ReadAllText(HighScoreFile);
                
//                 // C# Type Casting: Convert the stored string back to an integer.
//                 if (int.TryParse(scoreText, out int score) && score > 0)
//                 {
//                     return score;
//                 }
//                 // If the file content is invalid (e.g., empty or corrupt), return a very high score.
//                 return int.MaxValue; 
//             }
//             catch (FileNotFoundException)
//             {
//                 // C# Exceptions: If the file doesn't exist, this is the safest default score.
//                 // We use int.MaxValue (the largest possible int) so the user's first score
//                 // will always be a new record.
//                 return int.MaxValue; 
//             }
//             catch (Exception ex)
//             {
//                 // Catch any other unexpected file reading errors
//                 Console.WriteLine($"[ERROR] Could not load high score: {ex.Message}");
//                 return int.MaxValue;
//             }
//         }

//         /// <summary>
//         /// C# Files & Exceptions: Saves the new best score to the file.
//         /// </summary>
//         static void SaveHighScore(int newScore)
//         {
//             try
//             {
//                 // C# Files: Overwrite the existing content with the new, single score string.
//                 File.WriteAllText(HighScoreFile, newScore.ToString());
//                 Console.WriteLine($"[System] Successfully saved new high score of {newScore}.");
//             }
//             catch (Exception ex)
//             {
//                 // Catch any write errors (e.g., file locked, disk full)
//                 Console.WriteLine($"[ERROR] Failed to save high score: {ex.Message}");
//             }
//         }
//     }
// }