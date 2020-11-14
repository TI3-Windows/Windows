using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;

namespace TraveloreFE.ViewModel.Commands
{
    public class AddTaskCommand : ICommand
    {
        TaskViewModel taskViewModel;

        public AddTaskCommand(TaskViewModel tvm)
        {
            this.taskViewModel = tvm;
        }

        public event EventHandler CanExecuteChanged;


        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            await taskViewModel.AddTask();
        }
    }
}
