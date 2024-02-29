using System.Windows.Input;
using TeleTech.Commands;
using TeleTech.Model;
using TeleTech.Stores;

namespace TeleTech.ViewModel
{
    internal class LogInViewModel : ViewModelBase
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





        //if (Convert.ToString(EmployeeCode) != String.Empty &&
        //    Password != String.Empty &&
        //    !String.IsNullOrWhiteSpace(Convert.ToString(EmployeeCode)) &&
        //    !String.IsNullOrWhiteSpace(Password))
        //{
        //    int countRecord = employees.Where(x => x.EmployeeCode == EmployeeCode && x.Password == Password).Count();
        //    if (countRecord == 1)
        //    {
        //        MessageBox.Show("ds");
        //        MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
        //        mainWindowViewModel.OpenHomePageMethod(mainWindowViewModel.Page);

        //    }
        //}
        //else
        //{
        //    MessageBox.Show("Данные не введены");

        //}




        public LogInViewModel(NavigationStore navigationStore)
        {
            //employees = armContext.Employees.ToList();
            SingInCommand = new SingInCommand(navigationStore);


        }

    }
}
