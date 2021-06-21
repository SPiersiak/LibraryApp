using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryApp.Mobile.ViewModels;
namespace LibraryApp.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_if_labelIsEmpty_test1()
        {
            string login = "";
            string password = "sad";
            bool resultExpected = false;
            LoginViewModel viewModel = new LoginViewModel();

            var result = viewModel.ValidState(login, password);

            Assert.AreEqual(resultExpected, result);
        }
        [TestMethod]
        public void Test_if_labelIsEmpty_test2()
        {
            string login = "asdas";
            string password = "sad";
            bool resultExpected = true;
            LoginViewModel viewModel = new LoginViewModel();

            var result = viewModel.ValidState(login, password);

            Assert.AreEqual(resultExpected, result);
        }
        [TestMethod]
        public void Test_if_labelIsEmpty_test3()
        {
            string login = "assa";
            string password = "";
            bool resultExpected = false;
            LoginViewModel viewModel = new LoginViewModel();

            var result = viewModel.ValidState(login, password);

            Assert.AreEqual(resultExpected, result);
        }
    }
}
