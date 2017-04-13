using ClansApp.UI.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ClansApp.UI.ViewModels
{
    public class WindowFrameViewModel : ViewModelBase
    {
        public ViewModelBase CurrentViewModel { get; set; }

        private WindowState _customWindowState;
        public WindowState CustomWindowState
        {
            get
            {
                return _customWindowState;
            }
            set
            {
                _customWindowState = value;
                OnPropertyChanged(nameof(CustomWindowState));
            }
        }

        public ICommand CloseWindowCommand { get; set; }
        public ICommand MinimizeWindowCommand { get; set; }
        /// <summary>
        /// Currently unused - was used by Interaction.Trigger from System.Windows.Interactivity
        /// </summary>
        public ICommand MoveWindowCommand { get; set; }
        public ICommand MaximizeWindowCommand { get; set; }
        public ICommand RestoreWindowCommand { get; set; }

        public WindowFrameViewModel()
        {
            CloseWindowCommand = new RelayCommand<object>((o) => (o as Window).Close() /*App.Current.Shutdown()*/);
            //MoveWindowCommand = new RelayCommand<object>((o) => (o as Window).DragMove());
            MinimizeWindowCommand = new RelayCommand<object>((o) => CustomWindowState = WindowState.Minimized);
            MaximizeWindowCommand = new RelayCommand<object>((o) => CustomWindowState = WindowState.Maximized);
            RestoreWindowCommand = new RelayCommand<object>((o) => CustomWindowState = WindowState.Normal);

            CurrentViewModel = new LoginViewModel();
        }
    }
}
