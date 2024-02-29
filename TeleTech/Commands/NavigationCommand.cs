using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleTech.Stores;
using TeleTech.ViewModel;

namespace TeleTech.Commands
{
    internal class NavigationCommand<TView> : CommandBase
        where TView : ViewModelBase
    {

        private readonly NavigationStore _navigationStore;
        private readonly Func<TView> _createView;

        public NavigationCommand(NavigationStore navigationStore, Func<TView> createView)
        {
            _navigationStore = navigationStore;
            _createView = createView;
        }
        public override void Execute(object? parameter)
        {
            _navigationStore.CurrentView = _createView();

        }
    }
}
