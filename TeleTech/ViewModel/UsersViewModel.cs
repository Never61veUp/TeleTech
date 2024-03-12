using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using TeleTech.Commands;
using TeleTech.Model;
using TeleTech.Stores;
using TeleTech.View;

namespace TeleTech.ViewModel
{
    internal class UsersViewModel : ViewModelBase
    {
        public static ICommand AddNewClientCommand {  get; set; }

        public ArmContext armContext = new ArmContext();

        

        private List<Simissuance> simissuances;
        private List<Sim> sims;
        private List<User> users;
        private NavigationStore _navigationStore = new NavigationStore();

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
                                    Name = $"{u.Surname} {u.Name[0]}.{u.Patronymic[0]}." ,
                                    Character = u.Name[0],
                                    Id = u.Id,
                                    PassportId = u.PassportId,
                                    SimCardNumber = si.SimcardNumber,
                                    Tariff = s.Tariff,
                                    BgColor = UserExtended.ChooseColor(u.Name[0]),
                                    Birthday = u.Birthday,
                                    Address = u.Address,
                                    

                                    
                                }).ToList();
            //legacy :)
            //var userSims = from user in users
            //               join sim in simissuances on user.PassportId equals sim.PassportNumber
            //               select new UserExtended { Name = user.Name, SimCardNumber = sim.SimcardNumber, PassportId = user.PassportId, Id = user.Id, Tariff = sim.};
            
            UsersWithSIMs = combinedData.ToList();
            
            CountUsers = $"{armContext.Users.Count()} пользователей";
        }
        
    }
}
