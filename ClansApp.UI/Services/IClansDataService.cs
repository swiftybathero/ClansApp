using ClansApp.UI.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClansApp.UI.Services
{
    public interface IClansDataService
    {
        string APIKey { get; set; }
        Task<ObservableCollection<MemberWrapper>> GetAllMembersAsync();
    }
}
