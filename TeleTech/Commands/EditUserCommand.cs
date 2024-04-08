using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeleTech.Model;
using TeleTech.ViewModel;

namespace TeleTech.Commands
{
    internal class EditUserCommand : CommandBase
    {
        private ArmContext armContext = new ArmContext();
        private EditUserViewModel _editUserViewModel;
        public override void Execute(object? parameter)
        {
            _editUserViewModel.users.Name = _editUserViewModel.Name;
            _editUserViewModel.users.Surname = _editUserViewModel.SurName;
            _editUserViewModel.users.Patronymic = _editUserViewModel.Patronymic;
            _editUserViewModel.users.Birthday = _editUserViewModel.Birthday;
            _editUserViewModel.users.Address = _editUserViewModel.Address;
            _editUserViewModel.users.PassportIssueDate = _editUserViewModel.PassportIssueDate;
            _editUserViewModel.users.PlaceOfPassportIssue = _editUserViewModel.PlaceOfPassportIssue;
            _editUserViewModel.armContext.SaveChanges();   
            if(armContext.SaveChanges() == 0)
            {
                MessageBox.Show("d");
            }
        }
        public EditUserCommand(EditUserViewModel editUserViewModel)
        {
            _editUserViewModel = editUserViewModel;
        }
    }
}
