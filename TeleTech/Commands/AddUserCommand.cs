using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Sim sim = armContext.Sims.FirstOrDefault(s => s.SimcardNumber == _addNewClientViewModel.SimCardId);

            User newUser = new User()
            {
                Name = _addNewClientViewModel.Name,
                Surname = _addNewClientViewModel.SurName,
                Patronymic = _addNewClientViewModel.Patronymic,
                Birthday = _addNewClientViewModel.Birthday,
                Address = _addNewClientViewModel.Addres,
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
            armContext.Users.Add(newUser);
            armContext.SaveChanges();
            armContext.Simissuances.Add(newSimissuance);
            armContext.SaveChanges();
            sim.IsStock = false;
            armContext.SaveChanges();
            if (armContext.SaveChanges() == 0)
            {

                MessageBox.Show("Регистрация успешна", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                

            }
        }
        public AddUserCommand(AddNewClientViewModel addNewClientViewModel)
        {
            _addNewClientViewModel = addNewClientViewModel;
            

        }
    }
}
