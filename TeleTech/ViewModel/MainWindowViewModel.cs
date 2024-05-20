using System.Windows;
using System.Windows.Input;
using TeleTech.Commands;
using TeleTech.Model;
using TeleTech.Stores;

namespace TeleTech.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {

        private bool _isAppActive = true;
        private bool _isLeftBarVisible;
        private Visibility _isDialogOpen = Visibility.Collapsed;

        private readonly NavigationStore _navigationStore;
        private readonly AccountStore _accountStore;
        private readonly TariffViewModel _tariffViewModel;

        public MainWindowViewModel(NavigationStore navigationStore, AccountStore accountStore)
        {
            _tariffViewModel = new TariffViewModel();

            _navigationStore = navigationStore;
            _navigationStore.CurrentViewChanged += OnCurrentViewChanged;

            _navigationStore.CurrentDialogChanged += OnCurrentDialogChanged;

            _accountStore = accountStore;
            AccountStore.CurrentAccountChanged += AccountStore_CurrentAccountChanged;
            TariffViewModel.OpenEditTariffDialogWindowCommand = new ShowDialogCommand<EditTariffViewModel>
                (_navigationStore, () => new EditTariffViewModel(_tariffViewModel.SelectedTariffId), this);
            HomeCommand = new NavigationCommand<HomeViewModel>(_navigationStore, () => new HomeViewModel(), _accountStore);
            UsersCommand = new NavigationCommand<UsersViewModel>(_navigationStore, () => new UsersViewModel(_navigationStore
               ,this), _accountStore);
            TariffCommand = new NavigationCommand<TariffViewModel>(_navigationStore, () => new TariffViewModel(), _accountStore);
            //SettingsCommand = new NavigationCommand<SettingsViewModel>(_navigationStore, () => new SettingsViewModel(),
            //    _accountStore);
            OpenSimViewCommand = new NavigationCommand<SimCarViewModel>(_navigationStore, () => new SimCarViewModel(),
    _accountStore);
            SingOutCommand = new SingOutCommand(_navigationStore, _accountStore);

            UsersViewModel.AddNewClientCommand = new ShowDialogCommand<AddNewClientViewModel>(_navigationStore,
                () => new AddNewClientViewModel(), this);
            CloseDialogWindowCommand = new ShowDialogCommand<AddNewClientViewModel>(_navigationStore, () => null, this);
            TariffViewModel.AddTariffDIalogWindowShowCommand = new ShowDialogCommand<AddTariffViewModel>(_navigationStore,()=> new AddTariffViewModel(),this);


        }
        #region ICommand
        public ICommand HomeCommand { get; }
        public ICommand UsersCommand { get; }
        public ICommand TariffCommand { get; }
        public ICommand OpenSimViewCommand { get; }
        public ICommand SingOutCommand { get; }
        public ICommand CloseDialogWindowCommand { get; }
        #endregion

        public EmployeeExtended CurrentEmployee => AccountStore.CurrentAccount;

        public ViewModelBase? CurrentView => _navigationStore.CurrentView;
        public ViewModelBase? CurrentDialog => _navigationStore.CurrentDialog;

        public bool IsAppActive
        {
            get { return _isAppActive; }
            set
            {
                _isAppActive = value;
                OnPropertyChanged(nameof(IsAppActive));
            }
        }
        public bool IsLeftBarVisible
        {
            get { return _isLeftBarVisible; }
            set
            {
                _isLeftBarVisible = value;
                OnPropertyChanged(nameof(IsLeftBarVisible));
            }
        }
        public Visibility IsDialogOpen
        {
            get { return _isDialogOpen; }
            set
            {
                _isDialogOpen = value;
                OnPropertyChanged(nameof(IsDialogOpen));
            }
        }

        private void OnCurrentDialogChanged()
        {
            if (_navigationStore.CurrentDialog != null)
            {
                IsAppActive = false;
                IsDialogOpen = Visibility.Visible;

            }
            else
            {
                IsAppActive = true;
                IsDialogOpen = Visibility.Collapsed;
            }


            OnPropertyChanged(nameof(CurrentDialog));

        }

        private void AccountStore_CurrentAccountChanged()
        {
            OnPropertyChanged(nameof(CurrentEmployee));
            if (AccountStore.CurrentAccount != null)
                IsLeftBarVisible = true;

            else
                IsLeftBarVisible = false;
        }

        private void OnCurrentViewChanged()
        {
            OnPropertyChanged(nameof(CurrentView));
        }
    }
}
