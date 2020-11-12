using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TraveloreFE.ViewModel.Commands
{
    public class AddTaskCommand
    {
        private TaskViewModel taskViewModel;

        public AddTaskCommand(TaskViewModel tvm)
        {
            this.taskViewModel = tvm;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        /*public void Execute(object parameter)
        {
           
        }*/
    }
}
