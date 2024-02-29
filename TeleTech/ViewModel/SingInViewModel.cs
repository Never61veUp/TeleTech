using System.Windows.Input;
using TeleTech.Commands;
using TeleTech.Model;
using TeleTech.Stores;

namespace TeleTech.ViewModel
{
    internal class SingInViewModel : ViewModelBase
    {
        ArmContext armContext = new ArmContext();
        List<Employee> employees;
        private int _employeeCode;
        public int EmployeeCode
        {
            get => _employeeCode;
            set
            {
                _employeeCode = value;
                OnPropertyChanged(nameof(EmployeeCode));
            }
        }
        private string _password;
        public string Password
        {
            get => _password;

            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public ICommand SingInCommand { get; }


        







        public SingInViewModel(NavigationStore navigationStore)
        {
            employees = armContext.Employees.ToList();
            
        }

    }
}
