using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TodoApp.Helpers;
using TodoApp.Interfaces;
using TodoApp.Models;

namespace TodoApp.ViewModels
{
    /// <summary>
    /// Main view model that handles todo operations following MVVM pattern.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly ITodoService _todoService;
        private ObservableCollection<TodoItem> _todos = new();
        private string _newTodoTitle = string.Empty;
        private string _newTodoDescription = string.Empty;
        private bool _isLoading;

        public MainViewModel(ITodoService todoService)
        {
            _todoService = todoService ?? throw new ArgumentNullException(nameof(todoService));
            
            // Initialize commands
            AddTodoCommand = new RelayCommand(async () => await AddTodo(), CanAddTodo);
            DeleteTodoCommand = new RelayCommand<TodoItem>(async (todo) => await DeleteTodo(todo));
            ToggleTodoCommand = new RelayCommand<TodoItem>(async (todo) => await ToggleTodo(todo));
            
            // Load todos
            LoadTodos();
        }

        #region Properties

        public ObservableCollection<TodoItem> Todos
        {
            get => _todos;
            set => SetProperty(ref _todos, value);
        }

        public string NewTodoTitle
        {
            get => _newTodoTitle;
            set 
            {
                if (SetProperty(ref _newTodoTitle, value))
                {
                    // The RelayCommand will handle requerying automatically
                    ((RelayCommand)AddTodoCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public string NewTodoDescription
        {
            get => _newTodoDescription;
            set => SetProperty(ref _newTodoDescription, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        #endregion

        #region Commands

        public ICommand AddTodoCommand { get; }
        public ICommand DeleteTodoCommand { get; }
        public ICommand ToggleTodoCommand { get; }

        #endregion

        #region Private Methods

        private bool CanAddTodo() => !string.IsNullOrWhiteSpace(NewTodoTitle);

        private async void LoadTodos()
        {
            IsLoading = true;
            try
            {
                var todos = await _todoService.GetAllTodosAsync();
                Todos.Clear();
                foreach (var todo in todos)
                {
                    Todos.Add(todo);
                }
            }
            catch (Exception ex)
            {
                // In a real app, we would log this and handle it properly
                System.Diagnostics.Debug.WriteLine($"Error loading todos: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task AddTodo()
        {
            if (!CanAddTodo())
                return;

            var newTodo = new TodoItem
            {
                Title = NewTodoTitle,
                Description = NewTodoDescription
            };

            try
            {
                var addedTodo = await _todoService.CreateTodoAsync(newTodo);
                Todos.Add(addedTodo);

                // Clear input fields
                NewTodoTitle = string.Empty;
                NewTodoDescription = string.Empty;
            }
            catch (Exception ex)
            {
                // In a real app, we would log this and handle it properly
                System.Diagnostics.Debug.WriteLine($"Error adding todo: {ex.Message}");
            }
        }

        private async Task DeleteTodo(TodoItem? todo)
        {
            if (todo == null) return;

            try
            {
                if (await _todoService.DeleteTodoAsync(todo.Id))
                {
                    Todos.Remove(todo);
                }
            }
            catch (Exception ex)
            {
                // In a real app, we would log this and handle it properly
                System.Diagnostics.Debug.WriteLine($"Error deleting todo: {ex.Message}");
            }
        }

        private async Task ToggleTodo(TodoItem? todo)
        {
            if (todo == null) return;

            try
            {
                if (await _todoService.ToggleTodoStatusAsync(todo.Id))
                {
                    todo.IsCompleted = !todo.IsCompleted;
                    todo.CompletedAt = todo.IsCompleted ? DateTime.UtcNow : null;
                }
            }
            catch (Exception ex)
            {
                // In a real app, we would log this and handle it properly
                System.Diagnostics.Debug.WriteLine($"Error toggling todo status: {ex.Message}");
            }
        }

        #endregion
    }
} 