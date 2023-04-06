using System;
using System.Windows.Input;

namespace ModuleCameraSettings.Infrastructures
{
    /// <summary>
    /// An ICommand whose delegates can be attached for Execute and CanExecute.
    /// </summary>
    public class DelegateCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool>? _canExecute;

        public event EventHandler? CanExecuteChanged;

        public DelegateCommand(Action action, Func<bool>? canExecute = null)
        {
            _execute = action;
            _canExecute = canExecute;
        }

        public void Execute(object? parameter) => _execute();

        public bool CanExecute(object? parameter) => (_canExecute is null) || _canExecute();

        public void UpdateCanExecute() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
