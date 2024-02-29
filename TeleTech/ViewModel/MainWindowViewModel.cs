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

            HomeCommand = new HomeCommand(navigationStore);
            UsersCommand = new UsersCommand(navigationStore);
            TariffCommand = new TariffCommand(navigationStore);
            SettingsCommand = new SettingsCommand(navigationStore);
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
