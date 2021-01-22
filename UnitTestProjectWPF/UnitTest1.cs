using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WpfAppSirenSiret.ViewModels;

namespace UnitTestProjectWPF
{
    [TestClass]
    public class UnitTest1
    {
        [DataTestMethod]
        [DataRow("800403222")]
        [DataRow("80040322200014")]
        [DataRow("217103746")]
        public void SirenSiretCheckValide(string value)
        {
            var viewModel = new MainWindowViewModel();
            bool result = viewModel.SirenSiretCheck(value);
            Assert.IsTrue(result, $"{value} is not a SIREN or a SIRET");
        }

        [DataTestMethod]
        [DataRow("800403223")]
        [DataRow("80040322200015")]
        public void SirenSiretCheckInvalide(string value)
        {
            var viewModel = new MainWindowViewModel();
            bool result = viewModel.SirenSiretCheck(value);
            Assert.IsFalse(result, $"{value} is a SIREN or a SIRET");
        }
    }
}
