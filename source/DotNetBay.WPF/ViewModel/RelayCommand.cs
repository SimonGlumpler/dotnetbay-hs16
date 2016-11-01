using System;
using System.Windows.Input;

namespace DotNetBay.WPF.ViewModel
{
    public class RelayCommand<TCommandParameter> : ICommand
    {
        #region Members
        readonly Func<TCommandParameter, bool> canExecute;
        readonly Action<TCommandParameter> execute;
        #endregion

        #region Constructors
        public RelayCommand(Action<TCommandParameter> execute) : this(execute, null) { }

        public RelayCommand(Action<TCommandParameter> execute, Func<TCommandParameter, bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            this.execute = execute;
            this.canExecute = canExecute;
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (canExecute != null)
                    CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (canExecute != null)
                    CommandManager.RequerySuggested -= value;
            }
        }
        
        public bool CanExecute(object parameter)
        {
            return canExecute?.Invoke((TCommandParameter) parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            execute((TCommandParameter) parameter);
        }
        #endregion
    }

    //[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "This basically the same class, but with no TypeArguments")]
    public class RelayCommand : RelayCommand<object>
    {
        public RelayCommand(Action executeAction, Func<bool> canExecute)
            : base(o => executeAction(), o => canExecute())
        {
        }

        public RelayCommand(Action executeAction)
            : base(o => executeAction(), null)
        {
        }
    }
}
