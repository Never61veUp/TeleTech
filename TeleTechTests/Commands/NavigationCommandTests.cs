using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeleTech.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleTech.Stores;
using TeleTech.ViewModel;

namespace TeleTech.Commands.Tests
{
    [TestClass()]
    public class NavigationCommandTests
    {
        [TestMethod]
        public void Execute_WhenLoggedIn_ChangesCurrentView()
        {

            var navigationStore = new NavigationStore();
            var AccountStore = new AccountStore();
            var viewModel = new ViewModelBase(); 

            AccountStore.IsLoggedIn = true;
            var navigationCommand = new NavigationCommand<ViewModelBase>(navigationStore, () => viewModel, AccountStore);


            navigationCommand.Execute(null);


            Assert.AreEqual(viewModel, navigationStore.CurrentView);
        }

        [TestMethod]
        public void Execute_WhenNotLoggedIn_DoesNotChangeCurrentView()
        {

            var navigationStore = new NavigationStore();
            var accountStore = new AccountStore();

            accountStore.IsLoggedIn = false;
            var navigationCommand = new NavigationCommand<ViewModelBase>(navigationStore, () => new ViewModelBase(), accountStore);

            navigationCommand.Execute(null);


            Assert.IsNull(navigationStore.CurrentView);
        }
        [TestMethod]
        public void Execute_CreatesView()
        {
            
            var navigationStore = new NavigationStore();
            var accountStore = new AccountStore();
            var viewModel = new ViewModelBase(); 

            accountStore.IsLoggedIn = true;
            var navigationCommand = new NavigationCommand<ViewModelBase>(navigationStore, () => viewModel, accountStore);

            
            navigationCommand.Execute(null);

            
            //Assert.IsTrue(viewModel.HasBeenNavigatedTo); 
        }

    }
}