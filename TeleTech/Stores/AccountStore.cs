using TeleTech.Model;

namespace TeleTech.Stores
{
    internal class AccountStore
    {
        private Account _currentAccount;
        public Account CurrentAccount
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
