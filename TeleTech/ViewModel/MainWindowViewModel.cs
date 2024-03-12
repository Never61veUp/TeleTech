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
        #endregion
        
        public string? UserName => _accountStore.CurrentAccount?.FirstName;
        public string? UserGeoStatus => _accountStore.CurrentAccount?.Location;
        public char? UserCharacter => _accountStore.CurrentAccount?.Character;
        public Brush? UserAvatarColor => _accountStore.CurrentAccount?.BgColor;

        public ViewModelBase? CurrentView => _navigationStore.CurrentView;

        public ViewModelBase? CurrentDialog => _navigationStore.CurrentDialog;

        private bool _visibility = true;
        public bool Visibility
        {
            get
            {
                return _visibility;
            }
            set
            {
                _visibility = value;
                OnPropertyChanged(nameof(Visibility));
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

            HomeCommand = new NavigationCommand<HomeViewModel>(_navigationStore, () => new HomeViewModel(), accountStore);
            UsersCommand = new NavigationCommand<UsersViewModel>(_navigationStore, () => new UsersViewModel(), accountStore);
            TariffCommand = new NavigationCommand<TariffViewModel>(_navigationStore, () => new TariffViewModel(), accountStore);
            SettingsCommand = new NavigationCommand<SettingsViewModel>(_navigationStore, () => new SettingsViewModel(), accountStore);
            //SettingsCommand = new ShowDialogCommand<AddNewClientViewModel>(_navigationStore, () => new AddNewClientViewModel());
            UsersViewModel.AddNewClientCommand = new ShowDialogCommand<AddNewClientViewModel>(_navigationStore, () => new AddNewClientViewModel());



        }

        private void OnCurrentDialogChanged()
        {
            OnPropertyChanged(nameof(CurrentDialog));
            Visibility = false;
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
