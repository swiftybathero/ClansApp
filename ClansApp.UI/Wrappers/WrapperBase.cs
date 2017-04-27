using ClansApp.UI.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClansApp.UI.Wrappers
{
    /// <summary>
    /// Wrapper base class for POCO model classes - allows POCO's to use INotifyPropertyChanged interface
    /// </summary>
    /// <typeparam name="T">
    /// Type of POCO class
    /// </typeparam>
    public abstract class WrapperBase<T> : Observable
    {
        public T Model { get; private set;}

        public WrapperBase(T model)
        {
            Model = model;
        }

        /// <summary>
        /// Standard setter wrapper
        /// </summary>
        protected void SetProperty<TValue>(TValue value, [CallerMemberName] string propertyName = null)
        {
            var propertyInfo = Model.GetType().GetProperty(propertyName);
            var propertyValue = propertyInfo.GetValue(Model);

            if (Equals(value, propertyValue))
            {
                return;
            }
            propertyInfo.SetValue(Model, value);
            OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// Getter wrapper
        /// </summary>
        /// <returns>
        /// Property value, collected with reflection
        /// </returns>
        protected TValue GetProperty<TValue>([CallerMemberName] string propertyName = null)
        {
            var propertyInfo = Model.GetType().GetProperty(propertyName);
            var propertyValue = propertyInfo.GetValue(Model);

            return (TValue)propertyValue;
        }
    }
}
