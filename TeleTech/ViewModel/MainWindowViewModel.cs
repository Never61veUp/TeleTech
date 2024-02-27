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
        public ICommand OpenUsersPage {  get; set; }
        private object _page;
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
        public object Page { get { return _page; } set { _page = value;OnPropertyChanged(); } }
        private void Home(object obj) => Page = new UsersViewModel();
        public MainWindowViewModel()
        {
            OpenUsersPage = new RelayCommand(Home);
            UserAvatar = "/Assets/Icons/ava1.png";
            UserName = "Евгений Картов";
            UserGeoStatus = "Москва, Россия";
            Page = new HomeViewModel();
        }
    }
}
