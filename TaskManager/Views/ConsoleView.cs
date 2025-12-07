using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Controllers;
using TaskManager.Models;

namespace TaskManager.Views
{
    public class ConsoleView
    {
        private readonly TodoController controller;

        public ConsoleView(TodoController controller)
        {
            this.controller = controller;
        }

        /// <summary>
        /// Main application loop - displays menu and handles user choices
        /// </summary>
        public void Run()
        {
            bool running = true;

            while (running)
            {
                DisplayMainMenu();
                var choice = GetUserChoice();

                switch (choice)
                {
                    case "1":
                        AddTodoView();
                        break;
                    case "2":
                        ViewAllTodosView();
                        break;
                    case "3":
                        ViewTodosByStatusView();
                        break;
                    case "4":
                        ViewTodoDetailsView();
                        break;
                    case "5":
                        UpdateTodoView();
                        break;
                    case "6":
                        ToggleTodoCompletionView();
                        break;
                    case "7":
                        DeleteTodoView();
                        break;
                    case "8":
                        ViewStatisticsView();
                        break;
                    case "9":
                        running = false;
                        Console.WriteLine("\nGoodbye! Thanks for using Todo Manager.");
                        break;
                    default:
                        Console.WriteLine("\n❌ Invalid choice. Please try again.");
                        break;
                }

                if (running)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        private void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════╗");
            Console.WriteLine("║      TODO MANAGEMENT SYSTEM        ║");
            Console.WriteLine("╚════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine("1. Add New Todo");
            Console.WriteLine("2. View All Todos");
            Console.WriteLine("3. View Todos by Status");
            Console.WriteLine("4. View Todo Details");
            Console.WriteLine("5. Update Todo");
            Console.WriteLine("6. Toggle Todo Completion");
            Console.WriteLine("7. Delete Todo");
            Console.WriteLine("8. View Statistics");
            Console.WriteLine("9. Exit");
            Console.WriteLine();
        }

        private string GetUserChoice()
        {
            Console.Write("Enter your choice: ");
            return Console.ReadLine()?.Trim() ?? "";
        }

        private void AddTodoView()
        {
            Console.Clear();
            Console.WriteLine("═══ ADD NEW TODO ═══\n");

            try
            {
                Console.Write("Title: ");
                string title = Console.ReadLine()?.Trim() ?? "";

                Console.Write("Description: ");
                string description = Console.ReadLine()?.Trim() ?? "";

                DateTime dueDate = GetDateInput("Due Date (yyyy-MM-dd): ");
                int priority = GetPriorityInput();

                var todo = controller.AddTodo(title, description, dueDate, priority);

                Console.WriteLine($"\n✅ Todo created successfully!");
                Console.WriteLine($"   ID: {todo.Id}");
                Console.WriteLine($"   Title: {todo.Title}");
                Console.WriteLine($"   Due: {todo.DueDate:yyyy-MM-dd}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\n❌ Validation Error: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"\n❌ Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Unexpected error: {ex.Message}");
            }
        }

        private void ViewAllTodosView()
        {
            Console.Clear();
            Console.WriteLine("═══ ALL TODOS ═══\n");

            var todos = controller.GetAllTodos();

            if (todos.Count == 0)
            {
                Console.WriteLine("No todos found.");
                return;
            }

            DisplayTodoTable(todos);
        }

        private void ViewTodosByStatusView()
        {
            Console.Clear();
            Console.WriteLine("═══ VIEW TODOS BY STATUS ═══\n");
            Console.WriteLine("1. Completed Todos");
            Console.WriteLine("2. Incomplete Todos");
            Console.Write("\nChoice: ");

            string choice = Console.ReadLine()?.Trim() ?? "";

            if (choice == "1")
            {
                var todos = controller.GetTodosByStatus(true);
                Console.WriteLine("\n--- COMPLETED TODOS ---\n");
                DisplayTodoTable(todos);
            }
            else if (choice == "2")
            {
                var todos = controller.GetTodosByStatus(false);
                Console.WriteLine("\n--- INCOMPLETE TODOS ---\n");
                DisplayTodoTable(todos);
            }
            else
            {
                Console.WriteLine("\n❌ Invalid choice.");
            }
        }

        private void ViewTodoDetailsView()
        {
            Console.Clear();
            Console.WriteLine("═══ TODO DETAILS ═══\n");

            int id = GetTodoIdInput();
            var todo = controller.GetTodoById(id);

            if (todo == null)
            {
                Console.WriteLine($"\n❌ Todo with ID {id} not found.");
                return;
            }

            DisplayTodoDetails(todo);
        }

        private void UpdateTodoView()
        {
            Console.Clear();
            Console.WriteLine("═══ UPDATE TODO ═══\n");

            int id = GetTodoIdInput();
            var todo = controller.GetTodoById(id);

            if (todo == null)
            {
                Console.WriteLine($"\n❌ Todo with ID {id} not found.");
                return;
            }

            Console.WriteLine("\nCurrent todo details:");
            DisplayTodoDetails(todo);

            Console.WriteLine("\n--- Enter new details ---");

            try
            {
                Console.Write("New Title: ");
                string title = Console.ReadLine()?.Trim() ?? "";

                Console.Write("New Description: ");
                string description = Console.ReadLine()?.Trim() ?? "";

                DateTime dueDate = GetDateInput("New Due Date (yyyy-MM-dd): ");
                int priority = GetPriorityInput();

                var updatedTodo = controller.UpdateTodo(id, title, description, dueDate, priority);

                Console.WriteLine("\n✅ Todo updated successfully!");
                DisplayTodoDetails(updatedTodo);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\n❌ Validation Error: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"\n❌ Error: {ex.Message}");
            }
        }

        private void ToggleTodoCompletionView()
        {
            Console.Clear();
            Console.WriteLine("═══ TOGGLE TODO COMPLETION ═══\n");

            int id = GetTodoIdInput();

            try
            {
                var todo = controller.GetTodoById(id);
                if (todo == null)
                {
                    Console.WriteLine($"\n❌ Todo with ID {id} not found.");
                    return;
                }

                bool previousStatus = todo.IsCompleted;
                controller.ToggleTodoCompletion(id);

                Console.WriteLine($"\n✅ Todo status changed!");
                Console.WriteLine($"   {todo.Title}");
                Console.WriteLine($"   Status: {(previousStatus ? "Complete" : "Incomplete")} → {(todo.IsCompleted ? "Complete" : "Incomplete")}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\n❌ Error: {ex.Message}");
            }
        }

        private void DeleteTodoView()
        {
            Console.Clear();
            Console.WriteLine("═══ DELETE TODO ═══\n");

            int id = GetTodoIdInput();
            var todo = controller.GetTodoById(id);

            if (todo == null)
            {
                Console.WriteLine($"\n Todo with ID {id} not found.");
                return;
            }

            Console.WriteLine("\nTodo to delete:");
            DisplayTodoDetails(todo);

            Console.Write("\nAre you sure? (y/n): ");
            string confirm = Console.ReadLine()?.Trim().ToLower() ?? "";

            if (confirm == "y" || confirm == "yes")
            {
                try
                {
                    controller.DeleteTodo(id);
                    Console.WriteLine("\n Todo deleted successfully!");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"\n Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("\n Deletion cancelled.");
            }
        }

        private void ViewStatisticsView()
        {
            Console.Clear();
            Console.WriteLine("═══ TODO STATISTICS ═══\n");

            int total = controller.GetTodoCount();
            int completed = controller.GetCompletedTodoCount();
            int incomplete = total - completed;

            Console.WriteLine($"Total Todos:      {total}");
            Console.WriteLine($"Completed:        {completed}");
            Console.WriteLine($"Incomplete:       {incomplete}");

            if (total > 0)
            {
                double completionRate = (double)completed / total * 100;
                Console.WriteLine($"Completion Rate:  {completionRate:F1}%");
            }
        }

        // Helper methods for display
        private void DisplayTodoTable(List<Todo> todos)
        {
            if (todos.Count == 0)
            {
                Console.WriteLine("No todos to display.");
                return;
            }

            Console.WriteLine($"{"ID",-5} {"Title",-25} {"Due Date",-12} {"Priority",-10} {"Status",-12}");
            Console.WriteLine(new string('-', 70));

            foreach (var todo in todos.OrderBy(t => t.DueDate))
            {
                string status = todo.IsCompleted ? "✓ Complete" : "○ Incomplete";
                string title = todo.Title.Length > 24 ? todo.Title.Substring(0, 21) + "..." : todo.Title;

                Console.WriteLine($"{todo.Id,-5} {title,-25} {todo.DueDate:yyyy-MM-dd}  {todo.Priority,-10} {status,-12}");
            }

            Console.WriteLine($"\nTotal: {todos.Count} todo(s)");
        }

        private void DisplayTodoDetails(Todo todo)
        {
            Console.WriteLine($"\nID:          {todo.Id}");
            Console.WriteLine($"Title:       {todo.Title}");
            Console.WriteLine($"Description: {todo.Description}");
            Console.WriteLine($"Due Date:    {todo.DueDate:yyyy-MM-dd}");
            Console.WriteLine($"Priority:    {todo.Priority}");
            Console.WriteLine($"Status:      {(todo.IsCompleted ? "✓ Complete" : "○ Incomplete")}");
        }

        // Helper methods for input
        private int GetTodoIdInput()
        {
            while (true)
            {
                Console.Write("Enter Todo ID: ");
                string input = Console.ReadLine()?.Trim() ?? "";

                if (int.TryParse(input, out int id) && id > 0)
                {
                    return id;
                }

                Console.WriteLine("❌ Invalid ID. Please enter a positive number.");
            }
        }

        private DateTime GetDateInput(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine()?.Trim() ?? "";

                if (DateTime.TryParse(input, out DateTime date))
                {
                    return date;
                }

                Console.WriteLine("❌ Invalid date format. Please use yyyy-MM-dd.");
            }
        }

        private int GetPriorityInput()
        {
            while (true)
            {
                Console.Write("Priority (1-5): ");
                string input = Console.ReadLine()?.Trim() ?? "";

                if (int.TryParse(input, out int priority) && priority >= 1 && priority <= 5)
                {
                    return priority;
                }

                Console.WriteLine("❌ Invalid priority. Please enter a number between 1 and 5.");
            }
        }
    }
}