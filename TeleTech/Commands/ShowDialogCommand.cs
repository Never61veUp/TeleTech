using TeleTech.Stores;
using TeleTech.ViewModel;

namespace TeleTech.Commands
{
    internal class ShowDialogCommand<TView> : CommandBase
        where TView : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TView> _createDialog;
        private readonly MainWindowViewModel _mainWindowViewModel;
        public ShowDialogCommand(NavigationStore navigationStore, Func<TView> createDialog, MainWindowViewModel mainWindowViewModel)
        {
            _navigationStore = navigationStore;
            _createDialog = createDialog;
            _mainWindowViewModel = mainWindowViewModel;
        }
        public override void Execute(object? parameter)
        {
            //if(parameter != null)
            //{
            //    _navigationStore.CurrentDialog = _createDialog(parameter);
            //    if (_createDialog() != null)
            //        _mainWindowViewModel.IsAppActive = false;
            //    else
            //        _mainWindowViewModel.IsAppActive = true;
            ////}
            //else
            //{
            _navigationStore.CurrentDialog = _createDialog();
            if (_createDialog() != null)
                _mainWindowViewModel.IsAppActive = false;
            else
                _mainWindowViewModel.IsAppActive = true;
            //}


        }
        
    }
}
