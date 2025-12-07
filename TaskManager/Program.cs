using System;
using TaskManager.Controllers;
using TaskManager.Views;

namespace TaskManager
{
    class Program
    {
        static void Main(string[] args)
        {
            var controller = new TodoController();

            var view = new ConsoleView(controller);

            view.Run();
        }
    }
}