using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeService;

namespace PrimeService.Tests
{
    [TestClass]
    public class Tests
    {
        [DataTestMethod]
        [DataRow("800403222")]
        [DataRow("80040322200014")]
        [DataRow("217103746")]
        public void SirenSiretCheckValide(string value)
        {
            var primeService = new PrimeService();
            bool result = primeService.SirenSiretIsCorrect(value);
            Assert.IsTrue(result, $"{value} is not a SIREN or a SIRET");
        }

        [DataTestMethod]
        [DataRow("800403223")]
        [DataRow("80040322200015")]
        public void SirenSiretCheckInvalide(string value)
        {
            var primeService = new PrimeService();
            bool result = primeService.SirenSiretIsCorrect(value);
            Assert.IsFalse(result, $"{value} is a SIREN or a SIRET");
        }
    }
}
