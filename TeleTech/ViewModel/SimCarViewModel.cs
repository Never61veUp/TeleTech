using System.Windows.Input;
using TeleTech.Commands;
using TeleTech.Model;

namespace TeleTech.ViewModel
{
    class SimCarViewModel : ViewModelBase
    {
        private Sim _sim = new Sim();
        private readonly ArmContext armContext = new ArmContext();

        public SimCarViewModel()
        {
            AddNewSimCommand = new AddNewSimCommand();
            Tariff = armContext.Tariffs.ToList();
        }

        public ICommand AddNewSimCommand { get; set; }
        public Sim Sim { get { return _sim; } set { _sim = value; } }
        public List<Tariff> Tariff { get; }
    }
}
