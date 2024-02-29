using System.Windows;
using TeleTech.Stores;
using TeleTech.ViewModel;

namespace TeleTech.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NavigationStore navigationStore = new NavigationStore();
        MainWindow()
        {
            DataContext = new MainWindowViewModel(navigationStore);
            navigationStore.CurrentView = new LogInViewModel(navigationStore);
        }
    }

}