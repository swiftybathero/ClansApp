using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClansApp.UI.Extensions
{
    public class RelayCommand<T> : ICommand
    {
        private Action<T> _executeCommand;
        private Predicate<T> _canExecuteCommand; 

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<T> command) : this(command, null)
        {
        }
        public RelayCommand(Action<T> command, Predicate<T> canExecuteCommand)
        {
            _executeCommand = command ?? throw new NullReferenceException("Command cannot be null");
            _canExecuteCommand = canExecuteCommand;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecuteCommand != null)
            {
                return _canExecuteCommand((T)parameter);
            }
            return true;
        }

        public void Execute(object parameter)
        {
            _executeCommand((T)parameter);
        }
    }
}
