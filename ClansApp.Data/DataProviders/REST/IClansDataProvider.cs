using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClansApp.Data.DataProviders.REST
{
    public interface IClansDataProvider : IRestDataProvider
    {
        string APIKey { get; set; }
    }
}
