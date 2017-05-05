using ClansApp.UI.Extensions;
using ClansApp.UI.Serializers;
using ClansApp.UI.Services;
using ClansApp.UI.Services.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClansApp.UI.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private const string ButtonLoginText = "LOGIN";
        private const string ButtonDataFetchingText = "LOADING ...";

        private string _apiKey;
        public string ApiKey
        {
            get { return _apiKey; }
            set { SetProperty(ref _apiKey, value); }
        }

        private string _loginButtonText;
        public string LoginButtonText
        {
            get { return _loginButtonText; }
            set { SetProperty(ref _loginButtonText, value); }
        }

        private bool _loginInProcess;
        public bool LoginInProcess
        {
            get { return _loginInProcess; }
            set { SetProperty(ref _loginInProcess, value); }
        }

        public ICommand LoginCommand { get; set; }

        private ISettingsSerializer _settings;

        private LoginViewModel()
        {
            LoginCommand = new RelayCommand<object>(OnLogin, OnCanLogin);
            LoginButtonText = ButtonLoginText;
        }
        public LoginViewModel(ISettingsSerializer settings) : this()
        {
            _settings = settings;
            LoadSettings();

        }
        private void LoadSettings()
        {
            ApiKey = _settings.LoadAPIKey();
        }

        private void OnLogin(object obj)
        {
            var loginMessage = new LoginMessage(this, ApiKey);
            loginMessage.LoginStartedCallBackEvent += OnLoginStartedCallBack;
            loginMessage.LoginFinishedCallBackEvent += OnLoginFinishedCallBack;

            Messenger.Default.Send(loginMessage);
        }

        private void OnLoginStartedCallBack()
        {
            LoginInProcess = true;
            LoginButtonText = ButtonDataFetchingText;
            SaveSettings();
        }

        private void SaveSettings()
        {
            _settings.SaveAPIKey(ApiKey);
        }

        private void OnLoginFinishedCallBack()
        {
            LoginInProcess = false;
            LoginButtonText = ButtonLoginText;
        }

        private bool OnCanLogin(object obj)
        {
            return !string.IsNullOrEmpty(ApiKey) && !LoginInProcess;
        }
    }
}
