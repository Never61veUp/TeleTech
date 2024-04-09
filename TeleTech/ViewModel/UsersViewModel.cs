using System.Windows.Input;
using TeleTech.Commands;
using TeleTech.Model;
using TeleTech.Stores;

namespace TeleTech.ViewModel
{
    internal class UsersViewModel : ViewModelBase
    {
        public static ICommand AddNewClientCommand { get; set; }
        public static ICommand EditUserCommand { get; set; }

        public ArmContext armContext = new ArmContext();



        private List<Simissuance> simissuances;
        private List<Sim> sims;
        private List<User> users;
        private NavigationStore _navigationStore;
        private AccountStore _accountStore;
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


        public List<UserExtended> UsersWithSIMs { get; set; }

        //TODO: Убрать майнвбю модел

        public UsersViewModel(NavigationStore navigationStore, AccountStore accountStore, MainWindowViewModel mainWindowViewModel)
        {

            _navigationStore = navigationStore;
            _accountStore = accountStore;
            _mainWindowViewModel = mainWindowViewModel;
            EditUserCommand = new ShowDialogCommand<EditUserViewModel>(_navigationStore, () => new EditUserViewModel(activeUserId), _mainWindowViewModel);
            users = armContext.Users.ToList();
            simissuances = armContext.Simissuances.ToList();
            sims = armContext.Sims.ToList();

            var combinedData = (from user in armContext.Users
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

            UsersWithSIMs = combinedData.ToList();

            CountUsers = $"{armContext.Users.Count()} пользователей";
        }

    }
}
