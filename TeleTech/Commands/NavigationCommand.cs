using TeleTech.Stores;
using TeleTech.ViewModel;

namespace TeleTech.Commands
{
    internal class NavigationCommand<TView> : CommandBase
        where TView : ViewModelBase
    {

        private readonly NavigationStore _navigationStore;
        private readonly Func<TView> _createView;
        private readonly AccountStore _accountStore;

        public NavigationCommand(NavigationStore navigationStore, Func<TView> createView, AccountStore accountStore)
        {

            _navigationStore = navigationStore;
            _createView = createView;
            _accountStore = accountStore;
        }
        public override void Execute(object? parameter)
        {
            if (_accountStore.IsLoggedIn)
            {
                _navigationStore.CurrentView = _createView();
            }
            
        }
    }
}
