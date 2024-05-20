using System.Windows.Input;
using TeleTech.Commands;
using TeleTech.Model;

namespace TeleTech.ViewModel
{
    internal class EditUserViewModel : ViewModelBase
    {

        private readonly ArmContext _armContext = new();

        private readonly int _idUser;
        public EditUserViewModel(int idUser)
        {
            _idUser = idUser;
            SelectedUser = _armContext.Users.Where(x => x.Id == _idUser).FirstOrDefault();

            SaveUserEditChanges = new EditUserCommand(SelectedUser, _idUser);
            GetAgreementCommand = new GetAgreementCommand(SelectedUser);

        }
        #region ICOMMAND
        public ICommand SaveUserEditChanges { get; }
        public ICommand GetAgreementCommand { get; }
        #endregion
        public User SelectedUser { get; set; }
    }
}
