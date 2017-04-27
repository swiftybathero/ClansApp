using ClansApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClansApp.UI.Wrappers
{
    public class MemberWrapper : WrapperBase<Member>
    {
        public string Name
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }
        public string Role
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }
        public int ExpLevel
        {
            get { return GetProperty<int>(); }
            set { SetProperty(value); }
        }
        public int Trophies
        {
            get { return GetProperty<int>(); }
            set { SetProperty(value); }
        }
        public int ClanRank
        {
            get { return GetProperty<int>(); }
            set { SetProperty(value); }
        }
        public int Donations
        {
            get { return GetProperty<int>(); }
            set { SetProperty(value); }
        }
        public int DonationsReceived
        {
            get { return GetProperty<int>(); }
            set { SetProperty(value); }
        }

        public MemberWrapper(Member member) : base(member) { }
    }
}
