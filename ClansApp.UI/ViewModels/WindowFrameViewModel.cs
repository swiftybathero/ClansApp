using ClansApp.UI.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ClansApp.UI.ViewModels
{
    public class WindowFrameViewModel
    {
        public ICommand CloseWindowCommand { get; set; }
        public ICommand MinimizeWindowCommand { get; set; }
        public ICommand MoveWindowCommand { get; set; }
        public ICommand MaximizeWindowCommand { get; set; }
        public ICommand RestoreWindowCommand { get; set; }

        public WindowFrameViewModel()
        {
            CloseWindowCommand = new RelayCommand<object>((o) => (o as Window).Close() /*App.Current.Shutdown()*/);
            MoveWindowCommand = new RelayCommand<object>((o) => (o as Window).DragMove());
            MinimizeWindowCommand = new RelayCommand<object>((o) => (o as Window).WindowState = WindowState.Minimized);
            MaximizeWindowCommand = new RelayCommand<object>((o) => (o as Window).WindowState = WindowState.Maximized);
            RestoreWindowCommand = new RelayCommand<object>((o) => (o as Window).WindowState = WindowState.Normal);
        }
    }
}
