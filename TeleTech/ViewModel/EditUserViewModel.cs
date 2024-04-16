using System.Windows.Input;
using TeleTech.Commands;
using TeleTech.Model;

namespace TeleTech.ViewModel
{
    internal class EditUserViewModel : ViewModelBase
    {
        public ICommand SaveUserEditChanges { get; }
        public ICommand GetAgreementCommand { get; }
        public ArmContext armContext = new ArmContext();
        public User users { get; set; }
        private int _idUser;

        public List<Sim> sims { get; set; }
        private List<Simissuance> simissuances;


        public EditUserViewModel(int idUser)
        {
            _idUser = idUser;



            users = armContext.Users.Where(x => x.Id == _idUser).FirstOrDefault();
            SaveUserEditChanges = new EditUserCommand(users, _idUser);
            GetAgreementCommand = new GetAgreementCommand(users);



        }
    }
}
