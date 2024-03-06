using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


        List<Simissuance> simissuances;
        List<User> users;

        public string CountUsers { get; set; }
        public List<UserExtended> UsersWithSIMs {  get; set; }


        public UsersViewModel()
        {
            users = armContext.Users.ToList();
            simissuances = armContext.Simissuances.ToList();

            var userSims = from user in users
                           join sim in simissuances on user.PassportId equals sim.PassportNumber
                           select new UserExtended { Name = user.Name, SimCardNumber = sim.SimcardNumber, PassportId = user.PassportId, Id = user. };

            UsersWithSIMs = userSims.ToList();





            CountUsers = $"{armContext.Users.Count()} пользователей";
        }
    }
}
