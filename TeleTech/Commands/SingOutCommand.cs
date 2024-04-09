using TeleTech.Stores;
using TeleTech.ViewModel;

namespace TeleTech.Commands
{
    internal class SingOutCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly AccountStore _accountStore;
        public SingOutCommand(NavigationStore navigationStore, AccountStore accountStore)
        {
            _navigationStore = navigationStore;
            _accountStore = accountStore;
        }
        public override void Execute(object? parameter)
        {
            _accountStore.CurrentAccount = null;
            _navigationStore.CurrentView = new SingInViewModel(_navigationStore, _accountStore);
        }
    }
}
