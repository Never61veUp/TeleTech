using TeleTech.Stores;
using TeleTech.ViewModel;

namespace TeleTech.Commands
{
    internal class ShowDialogCommand<TView> : CommandBase
        where TView : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TView> _createDialog;
        public ShowDialogCommand(NavigationStore navigationStore, Func<TView> createDialog)
        {
            _navigationStore = navigationStore;
            _createDialog = createDialog;
        }
        public override void Execute(object? parameter)
        {

            _navigationStore.CurrentDialog = _createDialog();

        }
    }
}
