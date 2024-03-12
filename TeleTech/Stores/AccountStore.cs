using TeleTech.Model;

namespace TeleTech.Stores
{
    internal class AccountStore
    {
        private EmployeeExtended _currentAccount;
        public EmployeeExtended CurrentAccount
        {
            get => _currentAccount;
            set
            {
                _currentAccount = value;
                CurrentAccountChanged?.Invoke();
            }
        }

        public bool IsLoggedIn => CurrentAccount != null;


        public event Action? CurrentAccountChanged;

    }
}
