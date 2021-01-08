using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TraveloreFE.ViewModel.Commands
{
    class DeleteTravellistCommand : ICommand
    {
        TravellistViewModel _viewModel;
        public event EventHandler CanExecuteChanged;

        public DeleteTravellistCommand(TravellistViewModel tvm)
        {
            _viewModel = tvm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object tl)
        {

            await _viewModel.DeleteSelectedTravellist(tl.GetHashCode());
        }

    }
}
