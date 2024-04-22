using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TeleTech.Model;

namespace TeleTech.Commands
{
    class SaveTariffChangesCommand : CommandBase
    {
        private Tariff _tariff;
        private Tariff _parameter;
        private ArmContext _armContext;

        
        public SaveTariffChangesCommand()
        {
            _armContext = new ArmContext();
            
        }
        public override void Execute(object? parameter)
        {
            _parameter = parameter as Tariff;
            _tariff = _armContext.Tariffs.Where(x => x.Id == _parameter.Id).FirstOrDefault();
            
            try
            {
                _tariff.QuantityMinutes = _parameter.QuantityMinutes;
                _tariff.QuantitySms = _parameter.QuantitySms;
                _tariff.QuantityGigabyte = _parameter.QuantityGigabyte;
                _tariff.TariffName = _parameter.TariffName;

                _armContext.SaveChanges();
                if (_armContext.SaveChanges() == 0)
                    MessageBox.Show("Успешно");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
