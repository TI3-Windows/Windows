using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TraveloreFE.ViewModel.Commands
{
    public class DeleteTaskCommand : ICommand
    {
        TaskViewModel _viewModel;
        public event EventHandler CanExecuteChanged;

        public DeleteTaskCommand(TaskViewModel tvm)
        {
            _viewModel = tvm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object task)
        {
            
            //await _viewModel.DeleteSelectedTask();
        }
    }
}
