using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TeleTech.Model;
using TeleTech.Services;

namespace TeleTech.ViewModel
{
    internal class LogInViewModel : ViewModelBase
    {
        ArmContext armContext = new ArmContext();
        List<Employee> employees;
        private int _employeeCode;
        public int EmployeeCode
        {
            get
            {
                return _employeeCode;
            }
            set
            {
                _employeeCode = value;OnPropertyChanged(nameof(EmployeeCode));
            }
        }
        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value; OnPropertyChanged(nameof(Password));
            }
        }
        public ICommand SingIn { get; set; }
        private void SingInButton(object obj)
        {
            if (Convert.ToString(EmployeeCode) != String.Empty &&
                Password != String.Empty &&
                !String.IsNullOrWhiteSpace(Convert.ToString(EmployeeCode)) &&
                !String.IsNullOrWhiteSpace(Password))
            {
                int countRecord = employees.Where(x => x.EmployeeCode == EmployeeCode && x.Password == Password).Count();
                if (countRecord == 1)
                {
                    MessageBox.Show("ds");
                    MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
                    mainWindowViewModel.OpenHomePageMethod(mainWindowViewModel.Page);

                }
            }
            else
            {
                MessageBox.Show("Данные не введены");

            }



        }
        public LogInViewModel()
        {
            employees = armContext.Employees.ToList();
            SingIn = new RelayCommand(SingInButton);
            

        }

    }
}
