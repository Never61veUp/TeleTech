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
        List<Sim> sims;
        List<User> users;

        public string CountUsers { get; set; }

        public List<UserExtended> UsersWithSIMs { get; set; }
        

        public UsersViewModel()
        {
            users = armContext.Users.ToList();
            simissuances = armContext.Simissuances.ToList();
            sims = armContext.Sims.ToList();
            
            var combinedData = (from u in armContext.Users
                                join si in armContext.Simissuances on u.PassportId equals si.PassportNumber
                                join s in armContext.Sims on si.SimcardNumber equals s.SimcardNumber
                                select new UserExtended
                                {
                                    Name = u.Name,
                                    Id = u.Id,
                                    PassportId = u.PassportId,
                                    SimCardNumber = si.SimcardNumber,
                                    Tariff = s.Tariff
                                }).ToList();
            //var userSims = from user in users
            //               join sim in simissuances on user.PassportId equals sim.PassportNumber
            //               select new UserExtended { Name = user.Name, SimCardNumber = sim.SimcardNumber, PassportId = user.PassportId, Id = user.Id, Tariff = sim.};

            UsersWithSIMs = combinedData.ToList();





            CountUsers = $"{armContext.Users.Count()} пользователей";
        }
    }
}
