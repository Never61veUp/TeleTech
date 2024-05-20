using System.Windows;
using TeleTech.Model;
using TeleTech.Stores;

namespace TeleTech.Commands
{
    internal class AddUserCommand : CommandBase
    {
        private readonly ArmContext _armContext = new();

        private UserExtended _newUser;
        EmployeeExtended _employee;
        public AddUserCommand()
        {
            _employee = AccountStore.CurrentAccount;
        }
        public override void Execute(object? parameter)
        {
            _newUser = parameter as UserExtended;
            var sim = _armContext.Sims.Where(x => x.SimcardNumber == _newUser.SimCardNumber).FirstOrDefault();

            User newUser = new User()
            {
                Name = _newUser.Name,
                Surname = _newUser.Surname,
                Patronymic = _newUser.Patronymic,
                Birthday = _newUser.Birthday,
                Address = _newUser.Address,
                PassportIssueDate = _newUser.PassportIssueDate,
                PlaceOfPassportIssue = _newUser.PlaceOfPassportIssue,
                PassportId = _newUser.PassportId,
                AccountStatus = 1
            };
            Simissuance newSimissuance = new Simissuance()
            {
                PassportNumber = _newUser.PassportId,
                SimcardNumber = (int)_newUser.SimCardNumber,
                IssueDate = DateOnly.FromDateTime(DateTime.Today),
                ExpiryDate = DateOnly.FromDateTime(DateTime.Today.AddYears(6)),
                EmployeeId = AccountStore.CurrentAccount.UserId
            };
            try
            {
                _armContext.Users.Add(newUser);
                _armContext.SaveChanges();
                if (_armContext.SaveChanges() == 0)
                {
                    _armContext.Simissuances.Add(newSimissuance);
                    _armContext.SaveChanges();
                    if (_armContext.SaveChanges() == 0)
                    {
                        sim.IsStock = false;
                        _armContext.SaveChanges();
                        MessageBox.Show("Регистрация успешна", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        _armContext.Users.Remove(newUser);
                        _armContext.SaveChanges(true);
                        if (_armContext.SaveChanges() == 0)
                        {
                            MessageBox.Show("Регистрация не успешна");
                        }
                        else
                        {
                            MessageBox.Show("Регистрация все сломала");
                        }
                    }

                }
            }
            catch
            {
                throw new Exception("Add New User Exception");
            }




        }
        
    }
}
