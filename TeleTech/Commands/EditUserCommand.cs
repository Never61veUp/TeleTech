using System.Windows;
using TeleTech.Model;

namespace TeleTech.Commands
{
    public class EditUserCommand : CommandBase
    {
        private ArmContext armContext = new ArmContext();
        private User _userWithChanges;
        private int _idUser;
        private User _users;
        public override void Execute(object? parameter)
        {
            
            using (var transaction = armContext.Database.BeginTransaction())
            {
                try
                {
                    _users = armContext.Users.Where(x => x.Id == _idUser).FirstOrDefault();

                    var Simissuances = armContext.Simissuances.Where(x => x.PassportNumber == _users.PassportId).ToList();

                    armContext.Users.Remove(_users);

                    User newUser = new User
                    {
                        Name = _userWithChanges.Name,
                        Surname = _userWithChanges.Surname,
                        Patronymic = _userWithChanges.Patronymic,
                        Birthday = _userWithChanges.Birthday,
                        Address = _userWithChanges.Address,
                        PassportId = _userWithChanges.PassportId,
                        PassportIssueDate = _userWithChanges.PassportIssueDate,
                        PlaceOfPassportIssue = _userWithChanges.PlaceOfPassportIssue,
                        AccountStatus = _userWithChanges.AccountStatus
                    };


                    foreach (var item in Simissuances)
                    {
                        var simCardNumberOld = item.SimcardNumber;

                        var issueDateOld = item.IssueDate;
                        var expiryDateOld = item.ExpiryDate;

                        armContext.Simissuances.Remove(item);

                        Simissuance newSimissuance = new Simissuance
                        {
                            SimcardNumber = simCardNumberOld,
                            PassportNumber = _userWithChanges.PassportId,
                            IssueDate = issueDateOld,
                            ExpiryDate = expiryDateOld

                        };
                        armContext.Add(newSimissuance);
                    }
                    armContext.Users.Add(newUser);
                    armContext.SaveChanges();
                    if (armContext.SaveChanges() == 0)
                    {
                        MessageBox.Show("Успешное изменеие.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    transaction.Commit();



                }
                catch (Exception ex)
                {
                    // Если произошло исключение, откатываем транзакцию
                    transaction.Rollback();
                    MessageBox.Show($"Произошло исключение: {ex.Message}");
                    throw new Exception();
                }
            }





        }
        public EditUserCommand(User userWithChanges, int idUser)
        {
            _userWithChanges = userWithChanges;
            _idUser = idUser;
        }
    }
}
