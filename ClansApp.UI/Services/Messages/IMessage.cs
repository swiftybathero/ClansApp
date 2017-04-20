using ClansApp.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClansApp.UI.Services.Messages
{
    public interface IMessage<T>
    {
        ViewModelBase Sender { get; }
        T Value { get; }
    }
}
