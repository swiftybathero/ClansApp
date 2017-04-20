using ClansApp.UI.Extensions;
using ClansApp.UI.Services;
using ClansApp.UI.Services.Messages;
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
            get { return _customWindowState; }
            set { SetProperty(ref _customWindowState, value); }
        }

        private ResizeMode _customResizeMode;
        public ResizeMode CustomResizeMode
        {
            get { return _customResizeMode; }
            set { SetProperty(ref _customResizeMode, value); }
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
            CustomResizeMode = ResizeMode.CanResizeWithGrip;

            CloseWindowCommand = new RelayCommand<object>((o) => (o as Window).Close() /*App.Current.Shutdown()*/);
            //MoveWindowCommand = new RelayCommand<object>((o) => (o as Window).DragMove());
            MinimizeWindowCommand = new RelayCommand<object>((o) => CustomWindowState = WindowState.Minimized);
            MaximizeWindowCommand = new RelayCommand<object>((o) => CustomWindowState = WindowState.Maximized);
            RestoreWindowCommand = new RelayCommand<object>((o) => CustomWindowState = WindowState.Normal);

            CurrentViewModel = new LoginViewModel();

            PropertyChanged += WindowFrameViewModel_PropertyChanged;

            Messenger.Default.Register<LoginMessage>(OnLogin);
        }

        private void OnLogin(LoginMessage message)
        {
            // TODO: handle login message
            MessageBox.Show($"Sender: {message.Sender.ToString()}, API KEY: {message.Value}", ToString());
        }

        private void WindowFrameViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // temporary fix of window fullscreen screen 'margin' problem
            // problem still exists, when maximizing window by dragging it to top of the screen
            if (e.PropertyName == nameof(CustomWindowState))
            {
                switch (CustomWindowState)
                {
                    case WindowState.Maximized:
                        CustomResizeMode = ResizeMode.NoResize;
                        break;
                    default:
                        CustomResizeMode = ResizeMode.CanResizeWithGrip;
                        break;
                }
            }
        }
    }
}
