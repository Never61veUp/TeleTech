using System.Windows.Input;
using TeleTech.Commands;
using TeleTech.Model;

namespace TeleTech.ViewModel
{
    class AddNewClientViewModel : ViewModelBase
    {
        private readonly ArmContext _armContext = new ArmContext();
        public AddNewClientViewModel()
        {
            SimList = _armContext.Sims.Where(x => x.IsStock == true).ToList();

            AddUserCommand = new AddUserCommand();
            GetAgreementCommand = new GetAgreementCommand(NewUser);

        }
        #region ICOMMAND
        public ICommand AddUserCommand { get; set; }
        public ICommand GetAgreementCommand { get; set; }
        #endregion

        public UserExtended NewUser { get; set; } = new UserExtended();

        public List<Sim> SimList { get; }
    }
}
