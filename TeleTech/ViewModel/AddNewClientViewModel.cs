using Microsoft.Identity.Client.NativeInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeleTech.Commands;
using TeleTech.Model;
using TeleTech.Stores;

namespace TeleTech.ViewModel
{
    class AddNewClientViewModel : ViewModelBase
    {
        public static ICommand Close { get; set; }
        public ICommand AddUserCommand { get; set; }
        public ICommand GetAgreementCommand { get; set; }

        private AccountStore _accountStore;
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






        public ArmContext Context = new ArmContext();
        public List<Sim> SimList { get; }
        public AddNewClientViewModel(AccountStore accountStore)
        {
            _accountStore = accountStore;
            GetAgreementCommand = new GetAgreementCommand(this, _accountStore);
            SimList = Context.Sims.Where(x => x.IsStock == true).ToList();
            AddUserCommand = new AddUserCommand(this);
        }

    }
}
