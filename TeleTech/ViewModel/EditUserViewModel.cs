using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeleTech.Commands;
using TeleTech.Model;

namespace TeleTech.ViewModel
{
    internal class EditUserViewModel : ViewModelBase
    {
        public ICommand SaveUserEditChanges { get; }
        public ArmContext armContext = new ArmContext();
        public User users;
        private int _idUser;
        
        public List<Sim> sims {  get; set; }
        private List<Simissuance> simissuances;

        #region ПОЛЯ ДОБАВЛЕНИЯ
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Patronymic { get; set; }
        public DateOnly Birthday { get; set; }
        public string Address { get; set; }

        public DateOnly PassportIssueDate { get; set; }
        public string PlaceOfPassportIssue { get; set; }
        public int PassportId { get; set; }

        public int SimCardId { get; set; }


        #endregion

        public EditUserViewModel(int idUser)
        {
            _idUser = idUser;
            SaveUserEditChanges = new EditUserCommand(this);
            
            users = armContext.Users.Where(x => x.Id == _idUser).FirstOrDefault();
            Name = users.Name;
            SurName = users.Surname;
            Patronymic = users.Patronymic;
            Birthday = users.Birthday;
            Address = users.Address;
            PassportIssueDate = users.PassportIssueDate;
            PlaceOfPassportIssue = users.PlaceOfPassportIssue;
            PassportId = users.PassportId;
            SimCardId = armContext.Simissuances.Where(x => x.PassportNumber == users.PassportId).FirstOrDefault().SimcardNumber;


        }
    }
}
