using TeleTech.Model;

namespace TeleTech.Stores
{
    internal class AccountStore
    {
        private static AccountStore _instance;
        public static AccountStore Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AccountStore();
                }
                return _instance;
            }
        }
        private static EmployeeExtended _currentAccount;
        public static EmployeeExtended CurrentAccount
        {
            get => _currentAccount;
            set
            {
                _currentAccount = value;
                CurrentAccountChanged?.Invoke();
            }
        }

        public bool IsLoggedIn => CurrentAccount != null;


        public static event Action? CurrentAccountChanged;

    }
}
