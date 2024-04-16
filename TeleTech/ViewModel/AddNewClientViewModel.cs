using System.Windows.Input;
using TeleTech.Commands;
using TeleTech.Model;
using TeleTech.Stores;

namespace TeleTech.ViewModel
{
    class AddNewClientViewModel : ViewModelBase
    {
        public ICommand AddUserCommand { get; set; }
        public ICommand GetAgreementCommand { get; set; }

        private AccountStore _accountStore;


        public User users { get; set; } = new User();





        public ArmContext Context = new ArmContext();
        public List<Sim> SimList { get; }
        public Sim SelectedSimCard { get; set; } = new Sim();

        public AddNewClientViewModel(AccountStore accountStore)
        {
            _accountStore = accountStore;

            GetAgreementCommand = new GetAgreementCommand(users);
            SimList = Context.Sims.Where(x => x.IsStock == true).ToList();

            AddUserCommand = new AddUserCommand(users, SelectedSimCard);
        }


    }
}
