using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeleTech.Services;

namespace TeleTech.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        #region ICommand
        public ICommand OpenHomePageCommand { get; set; }
        public ICommand OpenUsersPageCommand {  get; set; }
        public ICommand OpenTariffPageCommand { get; set; }
        public ICommand OpenSettingsPageCommand { get; set; }
        #endregion
        private object _page;
        public object Page { 
            get 
            { 
                return _page; 
            } 
            set 
            { 
                _page = value; OnPropertyChanged("Page"); 
            } 
        }
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
        
        private void OpenHomePageMethod(object obj) => Page = new HomeViewModel();
        private void OpenUsersPageMethod(object obj) => Page = new UsersViewModel();
        private void OpenTariffPageMethod(object obj) => Page = new TariffViewModel();
        private void OpenSettingsPageMethod(object obj) => Page = new SettingsViewModel();
        public MainWindowViewModel()
        {
            
            OpenHomePageCommand = new RelayCommand(OpenHomePageMethod);
            OpenUsersPageCommand = new RelayCommand(OpenUsersPageMethod);
            OpenTariffPageCommand = new RelayCommand(OpenTariffPageMethod);
            OpenSettingsPageCommand = new RelayCommand(OpenSettingsPageMethod);
            UserAvatar = "/Assets/Icons/ava1.png";
            UserName = "Евгений Картов";
            UserGeoStatus = "Москва, Россия";
            Page = new HomeViewModel();
        }
    }
}
