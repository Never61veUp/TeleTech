using System.Windows;
using TeleTech.Model;

namespace TeleTech.Commands
{
    internal class RemoveTariffCommand : CommandBase
    {
        private ArmContext _armContext;
        private Tariff _tariff;

        private int _selectedTariffId;
        public RemoveTariffCommand()
        {
            _armContext = new ArmContext();
        }
        public override void Execute(object? parameter)
        {
            _selectedTariffId = (int)parameter;
            var Result = MessageBox.Show(
        "Вы уверены, что хотите удалить тариф",
        "Сообщение",
        MessageBoxButton.YesNo,
        MessageBoxImage.Information);
            if (Result == MessageBoxResult.Yes)
            {
                _tariff = _armContext.Tariffs.Where(x=> x.Id == _selectedTariffId).FirstOrDefault();
                _armContext.Remove(_tariff);
                _armContext.SaveChanges();
                if (_armContext.SaveChanges() == 0)
                    MessageBox.Show("Умпешно");
            }

        }
    }
}
