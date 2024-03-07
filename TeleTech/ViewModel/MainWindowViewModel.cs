using System.Windows.Media;
using System.Windows.Input;
using TeleTech.Commands;
using TeleTech.Stores;

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

        private readonly NavigationStore _navigationStore;
        private readonly AccountStore _accountStore;

        public MainWindowViewModel(NavigationStore navigationStore, AccountStore accountStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewChanged += OnCurrentViewChanged;

            _accountStore = accountStore;
            _accountStore.CurrentAccountChanged += AccountStore_CurrentAccountChanged;

            HomeCommand = new NavigationCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(), accountStore);
            UsersCommand = new NavigationCommand<UsersViewModel>(navigationStore, () => new UsersViewModel(), accountStore);
            TariffCommand = new NavigationCommand<TariffViewModel>(navigationStore, () => new TariffViewModel(), accountStore);
            SettingsCommand = new NavigationCommand<SettingsViewModel>(navigationStore, () => new SettingsViewModel(), accountStore);



            
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
