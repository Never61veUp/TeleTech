using System.Windows;
using System.Windows.Input;
using TeleTech.Commands;
using TeleTech.Model;
using TeleTech.Stores;

namespace TeleTech.ViewModel
{
    class UsersViewModel : ViewModelBase
    {

        private readonly ArmContext armContext = new ArmContext();

        private readonly NavigationStore _navigationStore;
        //TODO: Убрать мейнвиндов
        private readonly MainWindowViewModel _mainWindowViewModel;

        private int _selectedUserId;
        private string _filterText;
        private List<UserExtended> _usersWithSIMs;
        private bool[] _activeUserType = { true, false, false };


        public UsersViewModel(NavigationStore navigationStore, AccountStore accountStore, MainWindowViewModel
            mainWindowViewModel)
        {
            _navigationStore = navigationStore;
            _mainWindowViewModel = mainWindowViewModel;

            UpdateUsersDataGrid();

            EditUserCommand = new ShowDialogCommand<EditUserViewModel>(_navigationStore, () => new EditUserViewModel(SelectedUserId),
                _mainWindowViewModel);
            RemoveUserCommand = new ShowDialogCommand<RemoveUserViewModel>(_navigationStore, () => new RemoveUserViewModel(SelectedUserId),
                _mainWindowViewModel);

            CountUsers = $"{armContext.Users.Count()} пользователей";
        }

        public static ICommand AddNewClientCommand { get; set; }
        public static ICommand EditUserCommand { get; set; }
        public ICommand RemoveUserCommand { get; set; }
        public string CountUsers { get; set; }
        public int SelectedUserId
        {
            get => _selectedUserId;
            set
            {
                _selectedUserId = value;
                OnPropertyChanged(nameof(SelectedUserId));
            }
        }
        public string FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                OnPropertyChanged(nameof(FilterText));
                UpdateUsersDataGrid();
            }
        }

        public List<UserExtended> UsersWithSIMs
        {
            get => _usersWithSIMs;
            private set
            {
                _usersWithSIMs = value;
                OnPropertyChanged(nameof(UsersWithSIMs));
            }
        }

        public bool[] ActiveUserType
        {
            get
            {
                return _activeUserType;
            }
            set
            {
                _activeUserType = value;
                OnPropertyChanged(nameof(ActiveUserType));
                UpdateUsersDataGrid();
                MessageBox.Show(ActiveUserType.ToString());
            }
        }


        private void UpdateUsersDataGrid()
        {

            UsersWithSIMs = (from user in armContext.Users
                             join simIssuance in armContext.Simissuances on user.PassportId equals simIssuance.PassportNumber
                             into temp
                             from subSi in temp.DefaultIfEmpty()
                             join s in armContext.Sims on subSi.SimcardNumber equals s.SimcardNumber into temp2
                             from subS in temp2.DefaultIfEmpty()
                             select new UserExtended
                             {
                                 Name = $"{user.Surname} {user.Name[0]}.{user.Patronymic[0]}.",
                                 Surname = user.Surname,
                                 Patronymic = user.Patronymic,
                                 Character = user.Name[0],
                                 Id = user.Id,
                                 PassportId = user.PassportId,
                                 SimCardNumber = subSi != null ? subSi.SimcardNumber : null,
                                 Tariff = subS != null ? armContext.Tariffs.Where(x => x.Id == subS.Tariff).FirstOrDefault().TariffName.ToString() : null,
                                 BgColor = UserExtended.ChooseColor(user.Name[0]),
                                 Birthday = user.Birthday,
                                 Address = user.Address,
                                 AccountStatus = user.AccountStatus
                             }).ToList();

            for (int i = 0; i < ActiveUserType.Count(); i++)
            {
                if (ActiveUserType[i] == true)
                {
                    UsersWithSIMs = UsersWithSIMs.Where(x => x.AccountStatus == i).ToList();
                }

            }

            if (!String.IsNullOrEmpty(FilterText))
            {
                UsersWithSIMs = UsersWithSIMs.Where(x => x.PassportId.ToString().Contains(FilterText) ||
                x.Surname.ToLower().Contains(FilterText)).ToList();
            }
            


        }


    }
}
