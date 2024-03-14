using System.Windows.Media;
using System.Windows.Input;
using TeleTech.Commands;
using TeleTech.Stores;
using TeleTech.View;

namespace TeleTech.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        #region ICommand
        public ICommand HomeCommand { get; }
        public ICommand UsersCommand { get; }
        public ICommand TariffCommand { get; }
        public ICommand SettingsCommand { get; }
        public ICommand SingOutCommand { get; }
        #endregion
        
        public string? UserName => _accountStore.CurrentAccount?.FirstName;
        public string? UserGeoStatus => _accountStore.CurrentAccount?.Location;
        public char? UserCharacter => _accountStore.CurrentAccount?.Character;
        public Brush? UserAvatarColor => _accountStore.CurrentAccount?.BgColor;

        public ViewModelBase? CurrentView => _navigationStore.CurrentView;

        public ViewModelBase? CurrentDialog => _navigationStore.CurrentDialog;

        private bool _isAppActive = true;
        public bool IsAppActive
        {
            get
            {
                return _isAppActive;
            }
            set
            {
                _isAppActive = value;
                OnPropertyChanged(nameof(IsAppActive));
            }
        }


        private readonly NavigationStore _navigationStore;
        private readonly AccountStore _accountStore;

        public MainWindowViewModel(NavigationStore navigationStore, AccountStore accountStore)
        {
            

            _navigationStore = navigationStore;
            _navigationStore.CurrentViewChanged += OnCurrentViewChanged;

            _navigationStore.CurrentDialogChanged += OnCurrentDialogChanged;

            _accountStore = accountStore;
            _accountStore.CurrentAccountChanged += AccountStore_CurrentAccountChanged;

            HomeCommand = new NavigationCommand<HomeViewModel>(_navigationStore, () => new HomeViewModel(), _accountStore);
            UsersCommand = new NavigationCommand<UsersViewModel>(_navigationStore, () => new UsersViewModel(), _accountStore);
            TariffCommand = new NavigationCommand<TariffViewModel>(_navigationStore, () => new TariffViewModel(), _accountStore);
            SettingsCommand = new NavigationCommand<SettingsViewModel>(_navigationStore, () => new SettingsViewModel(), _accountStore);
            SingOutCommand = new SingOutCommand(_navigationStore, _accountStore);
            UsersViewModel.AddNewClientCommand = new ShowDialogCommand<AddNewClientViewModel>(_navigationStore, () => new AddNewClientViewModel(), this);
            AddNewClientViewModel.Close = new ShowDialogCommand<AddNewClientViewModel>(_navigationStore, () => null, this);
        }



        private void OnCurrentDialogChanged()
        {
            OnPropertyChanged(nameof(CurrentDialog));
            OnPropertyChanged(nameof(IsAppActive));
        }

        private void AccountStore_CurrentAccountChanged()
        {
            OnPropertyChanged(nameof(UserName));
            OnPropertyChanged(nameof(UserGeoStatus));
            OnPropertyChanged(nameof(UserCharacter));
            OnPropertyChanged(nameof(UserAvatarColor));
        }

        private void OnCurrentViewChanged()
        {
            OnPropertyChanged(nameof(CurrentView));
        }
    }
}
