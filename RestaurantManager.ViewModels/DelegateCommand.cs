using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RestaurantManager.ViewModels
{
    public class DelegateCommand<T> : ICommand
    {
        private readonly Func<T, bool> _CanExecuteMethod;
        private readonly Action<T> _ExecuteMethod;

        #region Constructors

        public DelegateCommand(Action<T> executeMethod)
            : this(executeMethod, null)
        {
        }

        public DelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        {
            _ExecuteMethod = executeMethod;
            _CanExecuteMethod = canExecuteMethod;
        }

        #endregion Constructors

        #region ICommand Members

        public event EventHandler CanExecuteChanged;

        bool ICommand.CanExecute(object parameter)
        {
            try
            {
                return CanExecute((T)parameter);
            }
            catch { return false; }
        }

        void ICommand.Execute(object parameter)
        {
            Execute((T)parameter);
        }

        #endregion ICommand Members

        #region Public Methods

        public bool CanExecute(T parameter)
        {
            return ((_CanExecuteMethod == null) || _CanExecuteMethod(parameter));
        }

        public void Execute(T parameter)
        {
            if (_ExecuteMethod != null)
            {
                _ExecuteMethod(parameter);
            }
        }

        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged(EventArgs.Empty);
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual void OnCanExecuteChanged(EventArgs e)
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion Protected Methods
    }
}
