using System.Windows.Input;
using TeleTech.Commands;
using TeleTech.Model;

namespace TeleTech.ViewModel
{
    class SimCarViewModel : ViewModelBase
    {

        private readonly ArmContext _armContext = new ArmContext();
        private string _filterText;
        private List<Sim> _sims;
        private List<Tariff> _tariff;

        private Sim _sim = new Sim();

        public SimCarViewModel()
        {
            AddNewSimCommand = new AddNewSimCommand();
            Sims = _armContext.Sims.ToList();
            Tariff = _armContext.Tariffs.ToList();

        }

        public ICommand AddNewSimCommand { get; set; }
        public List<Sim> Sims
        {
            get => _sims;
            private set
            {
                _sims = value;
                OnPropertyChanged(nameof(Sims));
            }
        }
        public List<Tariff> Tariff
        {
            get => _tariff;
            set
            {
                _tariff = value;
                OnPropertyChanged(nameof(Tariff));
            }
        }

        public Sim Sim
        {
            get => _sim;
            set
            {
                _sim = value;
                OnPropertyChanged(nameof(Sim));
            }
        }
            
        public string FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                OnPropertyChanged(nameof(FilterText));
                UpdateSimDataGrid();
            }
        }

        private void UpdateSimDataGrid()
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
