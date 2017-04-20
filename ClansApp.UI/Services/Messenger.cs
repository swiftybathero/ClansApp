using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClansApp.UI.Services
{
    /// <summary>
    /// Simple messenger class, for ViewModels communication
    /// </summary>
    public class Messenger : IMessenger
    {
        private static Dictionary<Type, List<object>> _subscribers = new Dictionary<Type, List<object>>();

        private static IMessenger _messenger;
        public static IMessenger Default
        {
            get
            {
                if (_messenger == null)
                {
                    _messenger = new Messenger();
                }
                return _messenger;
            }
        }

        private Messenger() { }

        /// <summary>
        /// Register a listener for a T type of message
        /// </summary>
        /// <typeparam name="T">Type of expected object</typeparam>
        /// <param name="action">Action to be invoked when object is sent</param>
        public void Register<T>(Action<T> action)
        {
            if (_subscribers.ContainsKey(typeof(T)))
            {
                var actions = _subscribers[typeof(T)];
                actions.Add(action);
            }
            else
            {
                var actions = new List<object>() { action };
                _subscribers.Add(typeof(T), actions);
            }
        }

        /// <summary>
        /// Unregister from listening for messages
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        public void Unregister<T>(Action<T> action)
        {
            if (_subscribers.ContainsKey(typeof(T)))
            {
                var actions = _subscribers[typeof(T)];
                actions.Remove(action);

                if (actions.Count == 0)
                {
                    _subscribers.Remove(typeof(T));
                }
            }
        }

        /// <summary>
        /// Send a message to all subscribers
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        public void Send<T>(T message)
        {
            if (_subscribers.ContainsKey(typeof(T)))
            {
                foreach (Action<T> action in _subscribers[typeof(T)])
                {
                    action.Invoke(message);
                }
            }
        }
    }
}
