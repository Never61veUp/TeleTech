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
    internal class EditTariffViewModel : ViewModelBase
    {
        private readonly ArmContext _armContext;

        private int _selectedTariffId;
        private Tariff _tariff;

        public EditTariffViewModel(int selectedTariffId)
        {
            _armContext = new ArmContext();

            _selectedTariffId = selectedTariffId;
            
            Tariff = _armContext.Tariffs.Where(x=> x.Id == _selectedTariffId).FirstOrDefault();

            SaveTariffChangesCommand = new SaveTariffChangesCommand(); 
        }
        public Tariff Tariff { get => _tariff; set => _tariff = value; }
        public ICommand SaveTariffChangesCommand { get; set; }
    }
}
