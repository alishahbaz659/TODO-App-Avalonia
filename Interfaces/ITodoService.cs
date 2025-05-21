using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Models;

namespace TodoApp.Interfaces
{
    /// <summary>
    /// Service interface for managing Todo items, following the Interface Segregation Principle.
    /// </summary>
    public interface ITodoService
    {
        /// <summary>
        /// Retrieves all todo items.
        /// </summary>
        /// <returns>Collection of todos</returns>
        Task<IEnumerable<TodoItem>> GetAllTodosAsync();
        
        /// <summary>
        /// Retrieves a specific todo by its ID.
        /// </summary>
        /// <param name="id">The ID of the todo</param>
        /// <returns>The todo item or null if not found</returns>
        Task<TodoItem?> GetTodoByIdAsync(Guid id);
        
        /// <summary>
        /// Creates a new todo item.
        /// </summary>
        /// <param name="todo">The todo to create</param>
        /// <returns>The created todo</returns>
        Task<TodoItem> CreateTodoAsync(TodoItem todo);
        
        /// <summary>
        /// Updates an existing todo item.
        /// </summary>
        /// <param name="todo">The updated todo</param>
        /// <returns>The updated todo</returns>
        Task<TodoItem> UpdateTodoAsync(TodoItem todo);
        
        /// <summary>
        /// Deletes a todo by its ID.
        /// </summary>
        /// <param name="id">The ID of the todo to delete</param>
        /// <returns>True if deletion was successful</returns>
        Task<bool> DeleteTodoAsync(Guid id);
        
        /// <summary>
        /// Toggles the completion status of a todo.
        /// </summary>
        /// <param name="id">The ID of the todo</param>
        /// <returns>True if toggling was successful</returns>
        Task<bool> ToggleTodoStatusAsync(Guid id);
    }
} 