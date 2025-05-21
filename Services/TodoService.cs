using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Interfaces;
using TodoApp.Models;

namespace TodoApp.Services
{
    /// <summary>
    /// Implementation of ITodoService that manages todo items.
    /// This implementation uses in-memory storage.
    /// </summary>
    public class TodoService : ITodoService
    {
        private readonly List<TodoItem> _todos;

        public TodoService()
        {
            _todos = new List<TodoItem>();
            // Add some sample data
            SeedSampleData();
        }

        public Task<IEnumerable<TodoItem>> GetAllTodosAsync()
        {
            return Task.FromResult(_todos.AsEnumerable());
        }

        public Task<TodoItem?> GetTodoByIdAsync(Guid id)
        {
            return Task.FromResult(_todos.FirstOrDefault(t => t.Id == id));
        }

        public Task<TodoItem> CreateTodoAsync(TodoItem todo)
        {
            if (todo == null)
                throw new ArgumentNullException(nameof(todo));

            if (string.IsNullOrWhiteSpace(todo.Title))
                throw new ArgumentException("Title cannot be empty", nameof(todo));

            // Create a new todo with a new ID to ensure uniqueness
            var newTodo = new TodoItem
            {
                Id = Guid.NewGuid(),
                Title = todo.Title,
                Description = todo.Description ?? string.Empty,
                IsCompleted = todo.IsCompleted,
                CreatedAt = DateTime.UtcNow,
                CompletedAt = todo.IsCompleted ? DateTime.UtcNow : null,
                Priority = todo.Priority
            };

            _todos.Add(newTodo);
            return Task.FromResult(newTodo);
        }

        public Task<TodoItem> UpdateTodoAsync(TodoItem todo)
        {
            if (todo == null)
                throw new ArgumentNullException(nameof(todo));

            var existingTodo = _todos.FirstOrDefault(t => t.Id == todo.Id);
            if (existingTodo == null)
                throw new KeyNotFoundException($"Todo item with ID {todo.Id} not found");

            var index = _todos.IndexOf(existingTodo);
            _todos[index] = todo;
            
            return Task.FromResult(todo);
        }

        public Task<bool> DeleteTodoAsync(Guid id)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
                return Task.FromResult(false);

            _todos.Remove(todo);
            return Task.FromResult(true);
        }

        public Task<bool> ToggleTodoStatusAsync(Guid id)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
                return Task.FromResult(false);

            todo.IsCompleted = !todo.IsCompleted;
            todo.CompletedAt = todo.IsCompleted ? DateTime.UtcNow : null;
            
            return Task.FromResult(true);
        }

        private void SeedSampleData()
        {
            _todos.Add(new TodoItem
            {
                Title = "Learn Avalonia UI",
                Description = "Master XAML UI development for cross-platform desktop apps",
                Priority = 2,
                CreatedAt = DateTime.UtcNow.AddDays(-3)
            });

            _todos.Add(new TodoItem
            {
                Title = "Implement MVVM architecture",
                Description = "Apply clean separation of concerns with Model-View-ViewModel pattern",
                Priority = 1,
                CreatedAt = DateTime.UtcNow.AddDays(-2)
            });

            _todos.Add(new TodoItem
            {
                Title = "Implement clean code practices",
                Description = "Apply SOLID principles for better maintainability and extensibility",
                Priority = 0,
                CreatedAt = DateTime.UtcNow.AddDays(-1)
            });
        }
    }
} 