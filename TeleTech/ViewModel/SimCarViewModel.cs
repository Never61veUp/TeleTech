using System.Windows.Input;
using TeleTech.Commands;
using TeleTech.Model;

namespace TeleTech.ViewModel
{
    class SimCarViewModel : ViewModelBase
    {
        private Sim _sim = new Sim();

        public SimCarViewModel()
        {
            AddNewSimCommand = new AddNewSimCommand();
        }

        public ICommand AddNewSimCommand { get; set; }
        public Sim Sim { get { return _sim; } set { _sim = value; } }
    }
}
