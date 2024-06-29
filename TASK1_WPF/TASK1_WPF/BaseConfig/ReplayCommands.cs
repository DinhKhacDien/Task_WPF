using System.Windows.Input;

namespace TASK1_WPF.BaseConfig
{
    public class ReplayCommands : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly Action<object> _excute; // tham chieu toi phuong thuc tra ve kieu void


        private readonly Predicate<object> _canExcute; // tham chieu toi phuong thuc co kieu tra ve la bool
        public ReplayCommands(Action<object> Excute, Predicate<object> canExcute)
        {
            this._canExcute = canExcute;
            this._excute = Excute;
        }
        public bool CanExecute(object? parameter)
        {
            return _canExcute(parameter);
        }

        public void Execute(object? parameter)
        {
            _excute(parameter);
        }
    }
}
