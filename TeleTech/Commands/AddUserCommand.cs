using System.Windows;
using TeleTech.Model;
using TeleTech.ViewModel;

namespace TeleTech.Commands
{
    internal class AddUserCommand : CommandBase
    {
        public ArmContext armContext = new ArmContext();
        private readonly AddNewClientViewModel _addNewClientViewModel;
        public override void Execute(object? parameter)
        {
            var sim = armContext.Sims.Where(s => s.SimcardNumber == _addNewClientViewModel.SimCardId).FirstOrDefault();

            User newUser = new User()
            {
                Name = _addNewClientViewModel.Name,
                Surname = _addNewClientViewModel.SurName,
                Patronymic = _addNewClientViewModel.Patronymic,
                Birthday = _addNewClientViewModel.Birthday,
                Address = _addNewClientViewModel.Address,
                PassportIssueDate = _addNewClientViewModel.PassportIssueDate,
                PlaceOfPassportIssue = _addNewClientViewModel.PlaceOfPassportIssue,
                PassportId = _addNewClientViewModel.PassportId,
            };
            Simissuance newSimissuance = new Simissuance()
            {
                PassportNumber = _addNewClientViewModel.PassportId,
                SimcardNumber = _addNewClientViewModel.SimCardId,
                IssueDate = DateOnly.FromDateTime(DateTime.Today),
                ExpiryDate = DateOnly.FromDateTime(DateTime.Today.AddYears(6)),
                PassportNumberNavigation = newUser,
                SimcardNumberNavigation = sim




            };
            try
            {
                armContext.Users.Add(newUser);
                armContext.SaveChanges();
                if (armContext.SaveChanges() == 0)
                {
                    armContext.Simissuances.Add(newSimissuance);
                    armContext.SaveChanges();
                    if (armContext.SaveChanges() == 0)
                    {
                        sim.IsStock = false;
                        armContext.SaveChanges();
                        MessageBox.Show("Регистрация успешна", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        armContext.Users.Remove(newUser);
                        armContext.SaveChanges(true);
                        if (armContext.SaveChanges() == 0)
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
        public AddUserCommand(AddNewClientViewModel addNewClientViewModel)
        {
            _addNewClientViewModel = addNewClientViewModel;


        }
    }
}
