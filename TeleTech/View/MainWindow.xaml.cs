using System.Windows;
using TeleTech.Stores;
using TeleTech.ViewModel;

namespace TeleTech.View
{
    // <summary>
    // Interaction logic for MainWindow.xaml
    // </summary>
    public partial class MainWindow : Window
    {
        NavigationStore navigationStore = new NavigationStore();
        AccountStore accountStore = new AccountStore();
        MainWindow()
        {
            DataContext = new MainWindowViewModel(navigationStore, accountStore);
            navigationStore.CurrentView = new SingInViewModel(navigationStore, accountStore);


        }
    }

}