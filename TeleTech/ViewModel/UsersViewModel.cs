using System.Windows.Input;
using TeleTech.Commands;
using TeleTech.Model;
using TeleTech.Stores;

namespace TeleTech.ViewModel
{
    class UsersViewModel : ViewModelBase
    {
        public static ICommand AddNewClientCommand { get; set; }
        public static ICommand EditUserCommand { get; set; }

        public ArmContext armContext = new ArmContext();



        private NavigationStore _navigationStore;
        //TODO: Убрать мейнвиндов
        private MainWindowViewModel _mainWindowViewModel;

        public string CountUsers { get; set; }
        private int _activeUserId;
        public int activeUserId
        {
            get { return _activeUserId; }
            set
            {
                _activeUserId = value;
                OnPropertyChanged(nameof(activeUserId));
            }
        }

        private string _filterText;
        private List<UserExtended> usersWithSIMs;

        public string FilterText
        {
            get { return _filterText; }
            set
            {
                _filterText = value;
                OnPropertyChanged(nameof(FilterText));
                UpdateUsersDataGrid();
            }
        }

        public List<UserExtended> UsersWithSIMs
        {
            get => usersWithSIMs; set
            {
                usersWithSIMs = value;
                OnPropertyChanged(nameof(UsersWithSIMs));
            }
        }


        public UsersViewModel(NavigationStore navigationStore, AccountStore accountStore)
        {
            _navigationStore = navigationStore;

            UpdateUsersDataGrid();

            EditUserCommand = new ShowDialogCommand<EditUserViewModel>(_navigationStore, () => new EditUserViewModel(activeUserId), _mainWindowViewModel);

            CountUsers = $"{armContext.Users.Count()} пользователей";
        }
        public void UpdateUsersDataGrid()
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
                UsersWithSIMs = UsersWithSIMs.Where(x => x.PassportId.ToString() == FilterText).ToList();

        }
    }
}
