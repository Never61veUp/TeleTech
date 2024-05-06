using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TeleTech.Model;

namespace TeleTech.Commands
{
    internal class AddTariffCommand : CommandBase
    {
        private ArmContext _armContext;
        private Tariff _newTariff;
        public AddTariffCommand()
        {
            _armContext = new ArmContext();
        }
        public override void Execute(object? parameter)
        {
            _newTariff = parameter as Tariff;
            try
            {
                _armContext.Tariffs.Add(_newTariff);
                _armContext.SaveChanges();
                if (_armContext.SaveChanges() == 0)
                    MessageBox.Show("Умпешно");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }
    }
}
