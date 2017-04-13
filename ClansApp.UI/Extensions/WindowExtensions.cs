using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClansApp.UI.Extensions
{
    public static class WindowExtensions
    {
        public static readonly DependencyProperty WindowStartupLocationProperty = DependencyProperty.RegisterAttached("WindowStartupLocation",
                                                                                    typeof(WindowStartupLocation),
                                                                                    typeof(WindowExtensions),
                                                                                    new UIPropertyMetadata(WindowStartupLocation.Manual, ChangeWindowStartupLocation));

        private static void ChangeWindowStartupLocation(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var window = d as Window;
            if (window != null)
            {
                window.WindowStartupLocation = GetWindowStartupLocation(d);
            }
        }

        public static WindowStartupLocation GetWindowStartupLocation(DependencyObject window)
        {
            return (WindowStartupLocation)window.GetValue(WindowStartupLocationProperty);
        }
        
        public static void SetWindowStartupLocation(DependencyObject window, WindowStartupLocation windowStartupLocation)
        {
            window.SetValue(WindowStartupLocationProperty, windowStartupLocation);
        }
    }
}
