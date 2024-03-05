using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using TeleTech.Model;

namespace TeleTech.ViewModel
{
    internal class UsersViewModel : ViewModelBase
    {
        ArmContext armContext = new ArmContext();
        public string CountUsers { get; set; }
        public UsersViewModel()
        {
            CountUsers = $"{armContext.Users.Count()} пользователей"; 
        }
    }
}
