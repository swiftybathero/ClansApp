using ClansApp.UI.Services.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClansApp.UI.Services
{
    public interface IMessenger
    {
        void Register<T>(Action<T> action);
        void Unregister<T>(Action<T> action);
        void Send<T>(T message);
    }
}
