using System.Windows.Input;
using TeleTech.Commands;
using TeleTech.Model;

namespace TeleTech.ViewModel
{
    internal class TariffViewModel : ViewModelBase
    {
        private readonly ArmContext armContext = new ArmContext();

        
        private static int _selectedTariffId;

        public TariffViewModel()
        {

            TariffList = armContext.Tariffs.ToList();
            RemoveTariffCommand = new RemoveTariffCommand();
            


        }

        #region ICommand
        public static ICommand OpenEditTariffDialogWindowCommand { get; set; }
        public ICommand RemoveTariffCommand { get; set; }
        public static ICommand AddTariffDIalogWindowShowCommand { get; set; }
        #endregion

        public List<Tariff> TariffList { get; set; }
        
        public int SelectedTariffId { 
            get => _selectedTariffId;
            set
            {
                _selectedTariffId = value;
                OnPropertyChanged(nameof(SelectedTariffId));
            }
        }
    }
}
