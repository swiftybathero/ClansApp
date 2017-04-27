using ClansApp.UI.Services;
using ClansApp.UI.Services.Messages;
using ClansApp.UI.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClansApp.UI.ViewModels
{
    public class DataViewModel : ViewModelBase
    {
        public ObservableCollection<MemberWrapper> MemberList { get; set; }

        public DataViewModel()
        {
            Messenger.Default.Register<MemberDataMessage>(OnShowData);
        }

        private void OnShowData(MemberDataMessage memberDataMessage)
        {
            MemberList = memberDataMessage.Value;
        }
    }
}
