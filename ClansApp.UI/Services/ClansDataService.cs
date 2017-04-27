using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClansApp.UI.Wrappers;
using ClansApp.Data.DataProviders.REST;
using ClansApp.Data.Models;
using System.Windows;

namespace ClansApp.UI.Services
{
    public class ClansDataService : IClansDataService
    {
        private IClansDataProvider _clansDataProvider = ClansDataProvider.Default;
        private const string ClansDataGetUrl = "https://api.clashofclans.com/v1/clans/%P89V02CG";

        public string APIKey
        {
            get { return _clansDataProvider.APIKey; }
            set { _clansDataProvider.APIKey = value; }
        }

        public ClansDataService() { }

        public ClansDataService(string apiKey)
        {
            APIKey = apiKey;
        }

        public async Task<ObservableCollection<MemberWrapper>> GetAllMembersAsync()
        {
            var memberCollection = new ObservableCollection<MemberWrapper>();
            var dataResult = await _clansDataProvider.GetDataAsync<Clan>(new Uri(ClansDataGetUrl));

            if (dataResult.IsSuccessStatusCode)
            {
                foreach (var member in dataResult.Value.MemberList)
                {
                    memberCollection.Add(new MemberWrapper(member));
                }
            }
            else
            {
                MessageBox.Show(dataResult.Message);
            }
            return memberCollection;
        }
    }
}
