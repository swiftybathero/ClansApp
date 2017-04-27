using ClansApp.UI.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClansApp.UI.ViewModels;

namespace ClansApp.UI.Services.Messages
{
    public class MemberDataMessage : IMessage<ObservableCollection<MemberWrapper>>
    {
        private ViewModelBase _sender;
        public ViewModelBase Sender => _sender;

        private ObservableCollection<MemberWrapper> _memberCollection;
        public ObservableCollection<MemberWrapper> Value => _memberCollection;

        public MemberDataMessage(ViewModelBase sender, ObservableCollection<MemberWrapper> memberCollection)
        {
            _sender = sender;
            _memberCollection = memberCollection;
        }
    }
}
