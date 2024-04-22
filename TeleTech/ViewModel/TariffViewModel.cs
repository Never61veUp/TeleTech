using System.Windows.Input;
using TeleTech.Commands;
using TeleTech.Model;

namespace TeleTech.ViewModel
{
    internal class TariffViewModel : ViewModelBase
    {
        private readonly ArmContext armContext = new ArmContext();

        User user;
        private static int selectedTariffId;

        public TariffViewModel()
        {

            TariffList = armContext.Tariffs.ToList();


        }

        public static ICommand OpenEditTariffDialogWindowCommand { get; set; }
        public List<Tariff> TariffList { get; set; }
        public static int SelectedTariffId { 
            get => selectedTariffId; 
            set => selectedTariffId = value; }
    }
}
