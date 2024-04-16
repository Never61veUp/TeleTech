using StringCheckLibrary;
using System.Windows;
using TeleTech.Model;

namespace TeleTech.Commands
{
    class AddNewSimCommand : CommandBase
    {
        private ArmContext armContext = new ArmContext();
        private Sim _sim;
        private CheckStringClass checkStringClass = new CheckStringClass();

        public override void Execute(object? parameter)
        {
            _sim = (Sim)parameter;
            if (checkStringClass.SimCardNumberCheck(_sim.SimcardNumber))
            {
                _sim.IsStock = true;
                _sim.IssueYear = DateOnly.FromDateTime(DateTime.Today);
                //TODO: Проверка тарифа
                using (var transaction = armContext.Database.BeginTransaction())
                {
                    try
                    {
                        armContext.Add(_sim);
                        armContext.SaveChanges();
                        if (armContext.SaveChanges() == 0)
                        {
                            MessageBox.Show("Успешное изменеие.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        transaction.Commit();



                    }
                    catch (Exception ex)
                    {
                        // Если произошло исключение, откатываем транзакцию
                        transaction.Rollback();
                        MessageBox.Show($"Произошло исключение: {ex.Message}");
                    }
                }
            }

        }



    }
}
