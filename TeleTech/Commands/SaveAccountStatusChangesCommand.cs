using System.Windows;
using TeleTech.Model;

namespace TeleTech.Commands
{
    internal class SaveAccountStatusChangesCommand : CommandBase
    {

        private ArmContext armContext = new ArmContext();

        private int _idUser;
        private User _currentUser;


        public SaveAccountStatusChangesCommand(int idUser)
        {
            _idUser = idUser;
            _currentUser = armContext.Users.Where(x => x.Id == _idUser).FirstOrDefault();

        }
        public override void Execute(object? parameter)
        {
            if (parameter == null || 0 > (int)parameter || (int)parameter > 2)
                throw new Exception("Роль не выбрана");
            _currentUser.AccountStatus = (int)parameter;
            armContext.SaveChanges();
            if (armContext.SaveChanges() == 0)
                MessageBox.Show("dsa");

        }
    }
}
