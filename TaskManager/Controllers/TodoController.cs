using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class TodoController
    {
        private List<Todo> todos;
        private int nextId;
        public TodoController()
        {
            todos = new List<Todo>();
            nextId = 1;
        }

        public Todo GetTodoById(int id)
        {
            return todos.FirstOrDefault(t => t.Id == id);
        }

        public Todo AddTodo(string title, string description, DateTime dueDate, int priority)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title cannot be empty");
            }

            if (dueDate.Date < DateTime.Now.Date)
            {
                throw new ArgumentException("Todo date cannot be in the past");
            }

            if (priority < 1 || priority > 5)
            {
                throw new ArgumentException("Priority must be between 1 and 5");
            }

            if (todos.Any(t => t.Title.Equals(title, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException("Todo title already exists");
            }


            var newTodo = new Todo
            {
                Id = nextId++,
                Title = title,
                Description = description,
                DueDate = dueDate,
                Priority = priority,
                IsCompleted = false
            };

            todos.Add(newTodo);

            return newTodo;
        }


        public List<Todo> GetAllTodos()
        {
            return new List<Todo>(todos);
        }

        public Todo UpdateTodo(int id, string title, string description, DateTime dueDate, int priority)
        {
            var updatedTodo = GetTodoById(id);

            if (updatedTodo == null)
            {
                throw new Exception($"todo with id {id}not found");
            }


            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title cannot be empty");
            }

            if (dueDate.Date < DateTime.Now.Date)
            {
                throw new ArgumentException("Date cannot be in the past");
            }

            if (priority < 1 || priority > 5)
            {
                throw new ArgumentException("Priority is not within range (1-5)");
            }

            if (todos.Any(t => t.Id != id && t.Title.Equals(title, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException("This todo title already exist");
            }

            updatedTodo.Title = title;
            updatedTodo.Description = description;
            updatedTodo.DueDate = dueDate;
            updatedTodo.Priority = priority;


            return updatedTodo;
        }

        public Todo ToggleTodoCompletion(int id)
        {
            var todo = GetTodoById(id);

            if (todo == null)
            {
                throw new ArgumentException("Todo is empty");
            }

            todo.IsCompleted = !todo.IsCompleted;

            return todo;
        }

        public bool DeleteTodo(int id)
        {
            var todoToDelete = GetTodoById(id);
            if (todoToDelete == null)
            {
                throw new ArgumentException("Todo does not exist");
            }

            return todos.Remove(todoToDelete);
        }

        public int GetTodoCount()
        {
            return todos.Count();
        }

        public int GetCompletedTodoCount()
        {
            return todos.Count(t => t.IsCompleted);
        }

        public List<Todo> GetTodosByStatus(bool isCompleted)
        {
            return todos.Where(t => t.IsCompleted == isCompleted).ToList();
        }
    }
}