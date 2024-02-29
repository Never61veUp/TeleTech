using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeleTech.Commands;
using TeleTech.Services;
using TeleTech.Stores;

namespace TeleTech.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        

        



        #region ICommand
        public ICommand HomeCommand { get; }
        public ICommand UsersCommand {  get; }
        public ICommand TariffCommand { get; }
        public ICommand SettingsCommand { get; }
        #endregion
        
        
        private string _userAvatar;
        public string UserAvatar { 
            get
            {
                return _userAvatar;
            }
            set
            {
                _userAvatar = value;
            }
            
        }
        private string _userName;
        public string UserName { get => _userName; set => _userName = value; }

        private string _userGeoStatus;
        public string UserGeoStatus { get => _userGeoStatus; set => _userGeoStatus = value; }



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
