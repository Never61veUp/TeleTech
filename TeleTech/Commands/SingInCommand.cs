using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TeleTech.Stores;
using TeleTech.ViewModel;
using TeleTech.Model;
using Account = TeleTech.Model.Account;

namespace TeleTech.Commands
{
    internal class SingInCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly AccountStore _accountStore;
        private readonly SingInViewModel _singInViewModel;
        public SingInCommand(NavigationStore navigationStore, AccountStore accountStore, SingInViewModel singInViewModel)
        {
            _navigationStore = navigationStore;
            _accountStore = accountStore;
            _singInViewModel = singInViewModel;
            
        }
        public override void Execute(object? parameter)
        {

            
            if (_singInViewModel.EmployeeCode.ToString() != String.Empty &&
                _singInViewModel.Password != String.Empty &&
                !String.IsNullOrWhiteSpace(_singInViewModel.EmployeeCode.ToString()) &&
                !String.IsNullOrWhiteSpace(_singInViewModel.Password))
            {
                int countRecord = _singInViewModel.employeesList.Where(x => x.EmployeeCode == _singInViewModel.EmployeeCode 
                && x.Password == _singInViewModel.Password).Count();
                if (countRecord == 1)
                {
                    _navigationStore.CurrentView = new HomeViewModel();
                    Account? account = new(_singInViewModel.EmployeeCode)
                    {
                        EmployeeCode = _singInViewModel.EmployeeCode,

                    };
                    _accountStore.CurrentAccount = account;
                    MessageBox.Show("Успешно");
                }
                else
                    MessageBox.Show("Неверные данные", "Save Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Данные не введены", "Save Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }
    }
}
