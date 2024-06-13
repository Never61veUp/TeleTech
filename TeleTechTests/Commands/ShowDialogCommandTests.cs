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
    public class ShowDialogCommandTests
    {
        [TestMethod()]

        public void Execute_SetsCurrentDialogAndSetsAppActiveToFalse()
        {
            var navigationStore = new NavigationStore();
            var accountStore = new AccountStore();
            var mainWindowViewModel = new MainWindowViewModel(navigationStore,accountStore);
            var viewModel = new ViewModelBase(); 

            var showDialogCommand = new ShowDialogCommand<ViewModelBase>(navigationStore, () => viewModel, mainWindowViewModel);


            showDialogCommand.Execute(null);


            Assert.AreEqual(viewModel, navigationStore.CurrentDialog);
            Assert.IsFalse(mainWindowViewModel.IsAppActive);
        }

        [TestMethod]
        public void Execute_WhenDialogIsNull_SetsCurrentDialogAndSetsAppActiveToTrue()
        {

            var navigationStore = new NavigationStore();
            var accountStore = new AccountStore();
            var mainWindowViewModel = new MainWindowViewModel(navigationStore, accountStore);

            var showDialogCommand = new ShowDialogCommand<ViewModelBase>(navigationStore, () => null, mainWindowViewModel);


            showDialogCommand.Execute(null);

            Assert.IsNull(navigationStore.CurrentDialog);
            Assert.IsTrue(mainWindowViewModel.IsAppActive);
        }
    }
}