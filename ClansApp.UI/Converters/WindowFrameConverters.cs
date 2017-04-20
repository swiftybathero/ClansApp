using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace ClansApp.UI.Converters
{
    public class MaximizeRestoreVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// True - for restore button
        /// </summary>
        public bool IsReverse { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var windowState = (WindowState)value;
            if (IsReverse)
            {
                if (windowState == WindowState.Maximized)
                {
                    return Visibility.Visible;
                }
            }
            else
            {
                if (windowState == WindowState.Normal)
                {
                    return Visibility.Visible;
                }
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
