using System.Windows;
using TeleTech.Model;
using TeleTech.ViewModel;

namespace TeleTech.Commands
{
    internal class AddUserCommand : CommandBase
    {
        public ArmContext armContext = new ArmContext();
        private User _newUser;
        private Sim _selectedSimCard;

        public override void Execute(object? parameter)
        {
            var sim = armContext.Sims.Where(s => s.SimcardNumber == _selectedSimCard.SimcardNumber).FirstOrDefault();

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
                AccountStatus = 0
            };
            Simissuance newSimissuance = new Simissuance()
            {
                PassportNumber = _newUser.PassportId,
                SimcardNumber = _selectedSimCard.SimcardNumber,
                IssueDate = DateOnly.FromDateTime(DateTime.Today),
                ExpiryDate = DateOnly.FromDateTime(DateTime.Today.AddYears(6)),
                




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
        public AddUserCommand(User newUser, Sim selectedSimCard)
        {
            _newUser = newUser;
            _selectedSimCard = selectedSimCard;


        }
    }
}
