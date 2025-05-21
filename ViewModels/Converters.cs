using System;
using Avalonia.Data.Converters;
using Avalonia.Media;
using System.Globalization;
using Avalonia.Controls;

namespace TodoApp.ViewModels
{
    /// <summary>
    /// Converts a priority integer to an appropriate color.
    /// </summary>
    public class PriorityToColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int priority)
            {
                return priority switch
                {
                    >= 2 => Color.Parse("#E53935"),   // Vibrant Red
                    1 => Color.Parse("#FB8C00"),      // Vibrant Orange
                    _ => Color.Parse("#00C853")       // Vibrant Green
                };
            }
            return Color.Parse("#00C853"); // Default to green
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Converts a boolean value to a TextDecoration (strikethrough for completed items).
    /// </summary>
    public class BoolToTextDecorationConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value is bool completed && completed ? TextDecorations.Strikethrough : null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Converts a boolean value to a color brush (gray for completed items, black for active items).
    /// Used for text coloring.
    /// </summary>
    public class BoolToCompletedColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            bool completed = value is bool b && b;
            return completed 
                ? new SolidColorBrush(Color.Parse("#757575")) 
                : new SolidColorBrush(Color.Parse("#202124"));  // Keep text black for active items
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Converts a boolean value to a color brush for borders (light gray for all items).
    /// </summary>
    public class BoolToBorderColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return new SolidColorBrush(Color.Parse("#DDDDDD"));  // Light gray for all borders
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Converts a count value to a boolean indicating if count equals zero.
    /// </summary>
    public class CountToBoolConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int count)
                return count == 0;
            return false;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
} 