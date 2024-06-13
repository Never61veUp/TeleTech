using System.Diagnostics;
using TeleTech.Model;

namespace TeleTech.Commands.Tests
{
    [TestClass()]
    public class EditUserCommandTests
    {
        private ArmContext armContext = new ArmContext();
        private User _userWithChanges;
        private int _idUser;
        private User _users;
        [TestMethod()]

        public void ExecuteTest()
        {
            _userWithChanges = new User
            {
                Name = "Test",
                Surname = "Test",
                Patronymic = "Test",

                Birthday = new DateOnly(2022, 1, 6),
                Address = "Test",
                PassportId = 987654,
                PassportIssueDate = new DateOnly(2022, 1, 6),
                PlaceOfPassportIssue = "Test",
                AccountStatus = 1
            };
            _idUser = armContext.Users.Where(x => x.PassportId == 987654).FirstOrDefault().Id;
            EditUserCommand editUserCommand = new(_userWithChanges, _idUser);
            editUserCommand.Execute(_userWithChanges);



        }
        [TestMethod()]
        public void ExecuteTest2()
        {
            var _userWithChanges = new User
            {
                Name = "Test",
                Surname = "Test",
                Patronymic = "Test",
                Birthday = new DateOnly(2022, 1, 6),
                Address = "Test",
                PassportId = 987654,
                PassportIssueDate = new DateOnly(2022, 1, 6),
                PlaceOfPassportIssue = "Test",
                AccountStatus = 1
            };

            var userToUpdate = armContext.Users.Where(x => x.PassportId == 987654).FirstOrDefault();
            var _idUser = userToUpdate?.Id;
            if (_idUser == null)
            {
                throw new Exception("Пользователь с данным PassportId не найден");
            }

            var editUserCommand = new EditUserCommand(_userWithChanges, (int)_idUser);


            editUserCommand.Execute(_userWithChanges);


            var updatedUser = armContext.Users.Where(x => x.Id == _idUser).FirstOrDefault();
            if (updatedUser == null)
            {
                throw new Exception("Обновленный пользователь не найден");
            }

            Debug.Assert(updatedUser.Name == _userWithChanges.Name);
            Debug.Assert(updatedUser.Surname == _userWithChanges.Surname);
            Debug.Assert(updatedUser.Patronymic == _userWithChanges.Patronymic);
            Debug.Assert(updatedUser.Birthday == _userWithChanges.Birthday);
            Debug.Assert(updatedUser.Address == _userWithChanges.Address);
            Debug.Assert(updatedUser.PassportId == _userWithChanges.PassportId);
            Debug.Assert(updatedUser.PassportIssueDate == _userWithChanges.PassportIssueDate);
            Debug.Assert(updatedUser.PlaceOfPassportIssue == _userWithChanges.PlaceOfPassportIssue);
            Debug.Assert(updatedUser.AccountStatus == _userWithChanges.AccountStatus);
        }
        [TestMethod]
        public void TestSuccessfulUpdate()
        {
            var _userWithChanges = new User
            {
                Name = "UpdatedTest",
                Surname = "UpdatedTest",
                Patronymic = "UpdatedTest",
                Birthday = new DateOnly(1990, 1, 1),
                Address = "UpdatedTest",
                PassportId = 987654,
                PassportIssueDate = new DateOnly(2010, 5, 20),
                PlaceOfPassportIssue = "UpdatedTest",
                AccountStatus = 2
            };

            var userToUpdate = armContext.Users.Where(x => x.PassportId == 987654).FirstOrDefault();
            var _idUser = userToUpdate?.Id;
            if (_idUser == null)
            {
                throw new Exception("Пользователь с данным PassportId не найден");
            }

            var editUserCommand = new EditUserCommand(_userWithChanges, (int)_idUser);

            // Act
            editUserCommand.Execute(_userWithChanges);

            // Assert
            var updatedUser = armContext.Users.Where(x => x.Id == _idUser).FirstOrDefault();
            if (updatedUser == null)
            {
                throw new Exception("Обновленный пользователь не найден");
            }

            Debug.Assert(updatedUser.Name == _userWithChanges.Name);
            Debug.Assert(updatedUser.Surname == _userWithChanges.Surname);
            Debug.Assert(updatedUser.Patronymic == _userWithChanges.Patronymic);
            Debug.Assert(updatedUser.Birthday == _userWithChanges.Birthday);
            Debug.Assert(updatedUser.Address == _userWithChanges.Address);
            Debug.Assert(updatedUser.PassportId == _userWithChanges.PassportId);
            Debug.Assert(updatedUser.PassportIssueDate == _userWithChanges.PassportIssueDate);
            Debug.Assert(updatedUser.PlaceOfPassportIssue == _userWithChanges.PlaceOfPassportIssue);
            Debug.Assert(updatedUser.AccountStatus == _userWithChanges.AccountStatus);
        }
        

     }
}