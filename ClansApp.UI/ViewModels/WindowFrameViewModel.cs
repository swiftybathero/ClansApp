using ClansApp.UI.Extensions;
using ClansApp.UI.Serializers;
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
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set { SetProperty(ref _currentViewModel, value); }
        }

        private LoginViewModel _loginViewModel;
        private DataViewModel _dataViewModel;

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

        public const string MainWindowTitle = "Clash of Clans Stats";

        private string _windowTitle;
        public string WindowTitle
        {
            get { return _windowTitle; }
            set { SetProperty(ref _windowTitle, value); }
        }


        public ICommand CloseWindowCommand { get; set; }
        public ICommand MinimizeWindowCommand { get; set; }
        /// <summary>
        /// Currently unused - was used by Interaction.Trigger from System.Windows.Interactivity
        /// </summary>
        public ICommand MoveWindowCommand { get; set; }
        public ICommand MaximizeWindowCommand { get; set; }
        public ICommand RestoreWindowCommand { get; set; }

        private IClansDataService _clansDataService;

        public WindowFrameViewModel()
        {
            CustomResizeMode = ResizeMode.CanResizeWithGrip;
            WindowTitle = MainWindowTitle;

            CloseWindowCommand = new RelayCommand<object>((o) => (o as Window).Close() /*App.Current.Shutdown()*/);
            MinimizeWindowCommand = new RelayCommand<object>((o) => CustomWindowState = WindowState.Minimized);
            MaximizeWindowCommand = new RelayCommand<object>((o) => CustomWindowState = WindowState.Maximized);
            RestoreWindowCommand = new RelayCommand<object>((o) => CustomWindowState = WindowState.Normal);

            _loginViewModel = new LoginViewModel(new XmlSettingsSerializer()); // perfect place for DI
            _dataViewModel = new DataViewModel();

            _clansDataService = new ClansDataService();

            CurrentViewModel = _loginViewModel;

            PropertyChanged += WindowFrameViewModel_PropertyChanged;

            Messenger.Default.Register<LoginMessage>(OnLogin);
        }

        private async void OnLogin(LoginMessage message)
        {
            _clansDataService.APIKey = message.Value;
            message.OnLoginStarted();
            await ShowMembersData();
            message.OnLoginFinished();
        }

        private async Task ShowMembersData()
        {
            var clanMemberList = await _clansDataService.GetAllMembersAsync();

            if (clanMemberList.Count != 0)
            {
                Messenger.Default.Send(new MemberDataMessage(this, clanMemberList));
                CurrentViewModel = _dataViewModel;
            }
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
