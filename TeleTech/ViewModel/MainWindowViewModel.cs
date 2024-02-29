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
        public string UserAvatar { get; set; }
        public string UserName { get; set; }
        public string UserGeoStatus { get; set; }



        public ViewModelBase CurrentView => _navigationStore.CurrentView;

        private readonly NavigationStore _navigationStore;
        public MainWindowViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewChanged += OnCurrentViewChanged;

            HomeCommand = new NavigationCommand<HomeViewModel>(navigationStore, () => new HomeViewModel());
            UsersCommand = new NavigationCommand<UsersViewModel>(navigationStore, () => new UsersViewModel());
            TariffCommand = new NavigationCommand<TariffViewModel>(navigationStore, () => new TariffViewModel());
            SettingsCommand = new NavigationCommand<SettingsViewModel>(navigationStore, () => new SettingsViewModel());
            UserAvatar = "/Assets/Icons/ava1.png";
            UserName = "Евгений Картов";
            UserGeoStatus = "Москва, Россия";

        }

        private void OnCurrentViewChanged()
        {
            OnPropertyChanged(nameof(CurrentView));
        }
    }
}
