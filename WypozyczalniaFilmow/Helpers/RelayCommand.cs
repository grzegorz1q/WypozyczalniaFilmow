using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WypozyczalniaFilmow.Helpers
{
    public class RelayCommand : ICommand
    {
        private Action _execute;
       // private  Func<bool> _canExecute;

        public event EventHandler? CanExecuteChanged;
        public RelayCommand(Action execute)
        {
            _execute = execute;
        }
        /*        public RelayCommand(Action execute, Func<bool> canExecute)
                {
                    _execute = execute;
                    _canExecute = canExecute;
                }*/

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _execute();
        }
    }
}
