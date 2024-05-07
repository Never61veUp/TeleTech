
using TeleTech.Model;

using System.Collections.Generic;
using System.Linq;




namespace TeleTech.ViewModel
{
    internal class HomeViewModel : ViewModelBase
    {
        private ArmContext _armContext = new();


        
        
        public HomeViewModel()
        {
            //var tariffCounts = _armContext.Sims
            //.GroupBy(sim => sim.Tariff)
            //.ToDictionary(group => group.Key, group => group.Count());

            //var tariffs = _armContext.Tariffs.ToDictionary(t => t.Id, t => t.TariffName);

            //// Преобразуем элементы словаря tariffCounts, заменяя номер тарифа на его название
            //var countsList = tariffCounts
            //    .Select(kvp => new KeyValuePair<string, int>(tariffs.ContainsKey(kvp.Key) ? tariffs[kvp.Key] : kvp.Key.ToString(), kvp.Value))
            //    .ToArray();
            //// Преобразуем результаты запроса в массив целых чисел


            
        }
    }
}
