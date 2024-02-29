using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleTech.Stores;
using TeleTech.ViewModel;

namespace TeleTech.Commands
{
    internal class TariffCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        public TariffCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }
        public override void Execute(object? parameter)
        {
            _navigationStore.CurrentView = new TariffViewModel();

        }
    }
}
