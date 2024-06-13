using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Drawing;
using System.Windows.Media;
using TeleTech.Model;
using TeleTech.Stores;
using System.Timers;

namespace TeleTech.ViewModel
{
    internal class HomeViewModel : ViewModelBase
    {
        private readonly ArmContext _armContext = new();
        private DateTime _date;
        private System.Timers.Timer _timer;
        private string _currentTime;

        public HomeViewModel()
        {
            _timer = new System.Timers.Timer(1000); // Обновление каждую секунду
            _timer.Elapsed += OnTimerElapsed;
            _timer.Start();

            Simissuances = _armContext.Simissuances.ToList();
            Simissuances = Simissuances.OrderByDescending(x => x.IssueDate).ToList();
            CreateDiagram();
            CreateGraph();
            CreateMiniGraph();
            
        }

        private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            CurrentTime = DateTime.Now.ToString("HH:mm:ss\ndd.MM.yyyy");
        }

        public string CurrentTime {
            get => _currentTime;
            set
            {
                if (_currentTime != value)
                {
                    _currentTime = value;
                    OnPropertyChanged();
                }
            }
        }


        public List<Simissuance> Simissuances { get; set; } = new List<Simissuance>();
        private void CreateMiniGraph()
        {
            
            DateOnly date = DateOnly.FromDateTime(DateTime.Today.AddDays(-7));
            for(int i = 7; i >0; i--)
            {
                date = date.AddDays(1);
                
                tariff3.Add(_armContext.Simissuances.Where(x=>x.IssueDate == date).Count());
                tariff2.Add(_armContext.Simissuances.Where(x=>x.IssueDate == date&& x.EmployeeId == AccountStore.CurrentAccount.UserId).Count());
            }
            date = DateOnly.FromDateTime(DateTime.Today);
            for(int i = 6; i >=0; i--)
             {
                
                Labels1[i] = date.ToString();
                date = date.AddDays(-1);
            }
            SeriesCollection1 =
            [
                new ColumnSeries
                {
                    Title = "Все",
                    Values = tariff3
                },
                //adding series will update and animate the chart automatically
                new ColumnSeries
                {
                    Title = AccountStore.CurrentAccount.FirstName,
                    Values = tariff2
                },
            ];

            //also adding values updates and animates the chart automatically
            

            
            Formatter = value => value.ToString("N");
        }

        public SeriesCollection SeriesCollection1 { get; set; }
        public string[] Labels1 { get; set; } = new string[7];
        public Func<double, string> Formatter { get; set; }
        private ChartValues<int> tariff2 = new ChartValues<int>();
        private ChartValues<int> tariff3 = new ChartValues<int>();

        #region TopBarProperties
        public string CountOfUser => $"Пользователей:\n{_armContext.Users.Count()}";
        public string CountOfSims => $"Симкарт в наличии:\n{_armContext.Sims.Where(x=>x.IsStock == true).Count()}";

        public string CountOfSalesPerMonth => $"Продаж за месяц:\n{_armContext.Simissuances.Where(x=> x.IssueDate >= StartOfMonth()).Count()}";
        #endregion
        //Diagram
        public SeriesCollection SeriesCollection { get; set; }

        public List<TariffData> TariffDataList { get; set; }

        //Graph
        public SeriesCollection SeriesCollectionG { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        private ChartValues<int> tariff1 = new ChartValues<int>();
        

        private void CreateDiagram()
        {
            var tariffData = (from tariff in _armContext.Tariffs
                              join sim in _armContext.Sims on tariff.Id equals sim.Tariff into gj
                              select new
                              {
                                  TariffName = tariff.TariffName,
                                  Count = gj.Count()
                              }).ToList();

            TariffDataList = tariffData.Select(x => new TariffData { TariffName = x.TariffName, Count = x.Count }).ToList();


            SeriesCollection = new SeriesCollection();

            foreach (var item in TariffDataList)
            {
                SeriesCollection.Add(new PieSeries
                {
                    Title = item.TariffName,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(item.Count) },
                    DataLabels = true,
                    //Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("White")
                });
            }
        }
        public void CreateGraph()
        {
            DateTime currentDate = DateTime.Now;

            SeriesCollectionG = new SeriesCollection();

            Labels = new[] { currentDate.AddMonths(-2).ToString("MMMM"),
    currentDate.AddMonths(-1).ToString("MMMM"),
    currentDate.ToString("MMMM") };

            YFormatter = value => value.ToString();
            var data = _armContext.Simissuances.ToList();

            var a = _armContext.Tariffs;
            List<Simissuance> simissuances = new List<Simissuance>();
            for (int j = 0; j < a.Count(); j++)
            {
                for (int i = 2; i >= 0; i--)
                {
                    DateOnly startDate = DateOnly.FromDateTime(DateTime.Now).AddMonths(-i);
                    DateOnly finishDate = startDate.AddMonths(-1);


                    tariff1.Add(_armContext.Simissuances
    .Where(si => si.SimcardNumberNavigation.Tariff == j + 1 && si.IssueDate <= startDate && si.IssueDate >= finishDate).Count());
                }

                var lineSeries = new LineSeries
                {
                    Title = a.Where(x => x.Id == j + 1).FirstOrDefault().TariffName,
                    Values = tariff1,
                    PointGeometrySize = 10
                };
                tariff1 = new ChartValues<int>();
                SeriesCollectionG.Add(lineSeries);


            }
        }
        private DateOnly StartOfMonth()
        {
            DateTime currentDate = DateTime.Today;
            DateTime startOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            DateOnly startOfMonthDateOnly = new DateOnly(startOfMonth.Year, startOfMonth.Month, startOfMonth.Day);
            return startOfMonthDateOnly;
        }


        public class TariffData
        {
            public string TariffName { get; set; }
            public int Count { get; set; }
        }



    }
}
