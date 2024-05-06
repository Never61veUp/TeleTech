using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeleTech.Commands;
using TeleTech.Model;

namespace TeleTech.ViewModel
{
    internal class AddTariffViewModel : ViewModelBase
    {
        private readonly ArmContext _armContext;

     
        private Tariff _tariff = new Tariff();
        public AddTariffViewModel()
        {
            _armContext = new ArmContext();
            
            AddTariffCommand = new AddTariffCommand();
        }
        public Tariff Tariff
        {
            get => _tariff;
            set
            {
                _tariff = value;
                OnPropertyChanged(nameof(Tariff));
            }
        }
        public ICommand AddTariffCommand { get; set; }
    }
}
