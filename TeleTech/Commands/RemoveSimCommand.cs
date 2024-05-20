using System.Windows;
using TeleTech.Model;

namespace TeleTech.Commands
{
    internal class RemoveSimCommand : CommandBase
    {
        private readonly ArmContext _armContext = new ArmContext();
        public override void Execute(object? parameter)
        {
            _armContext.Remove(parameter);
            _armContext.SaveChanges();
            if (_armContext.SaveChanges() == 0)
                MessageBox.Show("Успешно");

        }
    }
}
