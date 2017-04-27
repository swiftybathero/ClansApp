using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ClansApp.Data.DataProviders.REST
{
    public class ClansDataProvider : IClansDataProvider
    {
        /// <summary>
        /// HttpClient should be instantiated only once because of performance,
        /// it's not readonly because of lazy loading of Clans class instance in Default property
        /// </summary>
        private static HttpClient _httpClient = new HttpClient();

        /// <summary>
        /// Singleton instance of ClansDataProvider
        /// </summary>
        private static IClansDataProvider _dataProvider;

        private string _apiKey;
        public string APIKey
        {
            get
            {
                return _apiKey;
            }
            set
            {
                _apiKey = value;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
            }
        }

        public static IClansDataProvider Default
        {
            get
            {
                if (_dataProvider == null)
                {
                    _dataProvider = new ClansDataProvider();
                }
                return _dataProvider;
            }
        }

        private ClansDataProvider()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IResult<T>> GetDataAsync<T>(Uri requestUri)
        {
            var response = await _httpClient.GetAsync(requestUri);

            T responseValue = default(T);
            if (response.IsSuccessStatusCode)
            {
                responseValue = await response.Content.ReadAsAsync<T>();
            }

            return new ResponseResult<T>(response.IsSuccessStatusCode, response.ReasonPhrase, responseValue);
        }
    }
}
