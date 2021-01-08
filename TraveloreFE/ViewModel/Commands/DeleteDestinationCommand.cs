using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TraveloreFE.ViewModel.Commands
{
    class DeleteDestinationCommand : ICommand
    {
        ItineraryViewModel _viewModel;
        public event EventHandler CanExecuteChanged;

        public DeleteDestinationCommand(ItineraryViewModel ivm)
        {
            _viewModel = ivm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object dest)
        {
            await _viewModel.DeleteSelectedDestination(dest.GetHashCode());
        }
    }
}
