using System;
using System.Windows.Input;

namespace Products.Views
{
    public class Command : ICommand
    {
        private Action<object> method;

        private Func<object, bool> canexecute;

        public Command(Action<object> method, Func<object, bool> canexecute = null)
        {
            this.method = method;
            this.canexecute = canexecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return canexecute == null || canexecute(parameter);
        }

        public void Execute(object parameter)
        {
            method(parameter);
        }
    }
}