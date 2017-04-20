using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClansApp.UI.ViewModels;

namespace ClansApp.UI.Services.Messages
{
    public class LoginMessage : IMessage<string>
    {
        private ViewModelBase _sender;
        public ViewModelBase Sender => _sender;

        private string _value;
        public string Value => _value;

        public LoginMessage(ViewModelBase sender, string value)
        {
            _sender = sender;
            _value = value;
        }
    }
}
