using Microsoft.EntityFrameworkCore;
using System.Windows.Input;
using TeleTech.Commands;
using TeleTech.Model;

namespace TeleTech.ViewModel
{
    class SimCarViewModel : ViewModelBase
    {
        
        private readonly ArmContext _armContext = new ArmContext();
        private string _filterText;
        private List<Sim> sims;

        public SimCarViewModel()
        {
            AddNewSimCommand = new AddNewSimCommand();
            Sims = _armContext.Sims.ToList();

        }

        public ICommand AddNewSimCommand { get; set; }
        public List<Sim> Sims { get => sims; 
            private set
            {
                sims = value;
                OnPropertyChanged(nameof(Sims));
            }
        }
        public string FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                OnPropertyChanged(nameof(FilterText));
                UpdateUsersDataGrid();
            }
        }

        private void UpdateUsersDataGrid()
        {

            Sims = _armContext.Sims.ToList();
            


            if (!String.IsNullOrEmpty(FilterText))
            {
                Sims = Sims.Where(x => x.SimcardNumber.ToString().Contains(FilterText) ||
                x.UserPassport.ToString().ToLower().Contains(FilterText) || x.TariffName.Contains(FilterText)).ToList();
            }

           

        }
    }

}
