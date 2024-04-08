using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleTech.Model;

namespace TeleTech.ViewModel
{
    internal class EditUserViewModel
    {
        public ArmContext armContext = new ArmContext();
        private int _idUser;
        private List<User> users;

        #region ПОЛЯ ДОБАВЛЕНИЯ
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Patronymic { get; set; }
        public DateOnly Birthday { get; set; }
        public string Addres { get; set; }

        public DateOnly PassportIssueDate { get; set; }
        public string PlaceOfPassportIssue { get; set; }
        public int PassportId { get; set; }

        public int SimCardId { get; set; }


        #endregion

        public EditUserViewModel(int idUser)
        {
            _idUser = idUser;
            users = armContext.Users.Where(x => x.Id == _idUser).ToList();

        }
    }
}
