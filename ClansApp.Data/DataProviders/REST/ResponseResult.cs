using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClansApp.Data.DataProviders.REST
{
    public class ResponseResult<T> : IResult<T>
    {
        public bool IsSuccessStatusCode { get; private set; }
        public string Message { get; private set; }
        public T Value { get; private set; }

        private ResponseResult() { }
        public ResponseResult(bool isSuccessStatusCode, string message, T value)
        {
            IsSuccessStatusCode = isSuccessStatusCode;
            Message = message;
            Value = value;
        }
    }
}
