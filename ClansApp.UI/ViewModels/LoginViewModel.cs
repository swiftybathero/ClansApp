using ClansApp.UI.Extensions;
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
        private string _apiKey;
        public string ApiKey
        {
            get { return _apiKey; }
            set { SetProperty(ref _apiKey, value); }
        }

        public ICommand LoginCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand<object>((o) => Messenger.Default.Send(new LoginMessage(this, ApiKey)), (o) => !string.IsNullOrEmpty(ApiKey));
        }
    }
}
