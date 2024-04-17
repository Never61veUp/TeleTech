using TeleTech.Model;

namespace TeleTech.ViewModel
{
    internal class TariffViewModel : ViewModelBase
    {
        private readonly ArmContext armContext = new ArmContext();

        public List<Tariff> TariffList {  get; set; }
        public TariffViewModel()
        {
            TariffList = armContext.Tariffs.ToList();
        }
    }
}
