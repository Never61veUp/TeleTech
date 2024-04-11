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

        
        public UsersViewModel(NavigationStore navigationStore, AccountStore accountStore, MainWindowViewModel 
            mainWindowViewModel)
        {
            _navigationStore = navigationStore;
            _mainWindowViewModel = mainWindowViewModel;

            UpdateUsersDataGrid();

            EditUserCommand = new ShowDialogCommand<EditUserViewModel>(_navigationStore, () => new EditUserViewModel(SelectedUserId),
                _mainWindowViewModel);

            CountUsers = $"{armContext.Users.Count()} пользователей";
        }

        public static ICommand AddNewClientCommand { get; set; }
        public static ICommand EditUserCommand { get; set; }
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
                                 Tariff = subS != null ? subS.Tariff : null,
                                 BgColor = UserExtended.ChooseColor(user.Name[0]),
                                 Birthday = user.Birthday,
                                 Address = user.Address,
                             }).ToList();


            if (!String.IsNullOrEmpty(FilterText))
            {
                UsersWithSIMs = UsersWithSIMs.Where(x => x.PassportId.ToString().Contains(FilterText) ||
                x.Surname.ToLower().Contains(FilterText)).ToList();
            }


        }


    }
}
