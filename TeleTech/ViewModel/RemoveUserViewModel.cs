using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TeleTech.Commands;
using TeleTech.Model;

namespace TeleTech.ViewModel
{
    internal class RemoveUserViewModel : ViewModelBase
    {
        private readonly ArmContext armContext = new();
        private readonly int _idUser;
        private int _selectedAccountStatus = 2;
        //заменить на бд
        private List<AccountStatus> _accountStatusList;

        public RemoveUserViewModel(int idUser)
        {
            _idUser = idUser;

            AccountStatusList = armContext.AccountStatuses.ToList();
            SelectedAccountStatus = (int)armContext.Users.Where(x => x.Id == idUser).FirstOrDefault().AccountStatus;
            
            SaveAccountStatusChangesCommand = new SaveAccountStatusChangesCommand(_idUser);
            
        }

        public ICommand SaveAccountStatusChangesCommand { get; set; }
        public List<AccountStatus> AccountStatusList { get => _accountStatusList; set => _accountStatusList = value; }

        public int SelectedAccountStatus { get => _selectedAccountStatus;
            set { _selectedAccountStatus = value;
                OnPropertyChanged(nameof(SelectedAccountStatus)); 
                 } }

        
    }
}
