using System;

namespace TodoApp.Models
{
    /// <summary>
    /// Model representing a todo item.
    /// </summary>
    public class TodoItem
    {
        /// <summary>
        /// Unique identifier for the todo item.
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Title of the todo item.
        /// </summary>
        public string Title { get; set; } = string.Empty;
        
        /// <summary>
        /// Optional description of the todo item.
        /// </summary>
        public string Description { get; set; } = string.Empty;
        
        /// <summary>
        /// Whether the todo item is completed.
        /// </summary>
        public bool IsCompleted { get; set; }
        
        /// <summary>
        /// When the todo item was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }
        
        /// <summary>
        /// When the todo item was completed, if applicable.
        /// </summary>
        public DateTime? CompletedAt { get; set; }
        
        /// <summary>
        /// Priority level of the todo item (0=Low, 1=Medium, 2=High).
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Creates a new todo item with default values.
        /// </summary>
        public TodoItem()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            IsCompleted = false;
            Priority = 0;
        }
    }
} 