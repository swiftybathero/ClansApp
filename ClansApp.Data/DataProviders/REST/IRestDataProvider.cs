using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClansApp.Data.DataProviders.REST
{
    public interface IRestDataProvider
    {
        Task<IResult<T>> GetDataAsync<T>(Uri requestAddress);
    }
}
