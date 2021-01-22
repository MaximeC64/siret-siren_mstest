using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Text;

namespace UnitTestProjectAppium
{
    [TestClass]
    public class UnitTest1
    {
        protected const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723/wd/hub";
        private const string WpfAppId = @"C:\\Users\\maxim\\Documents\\CODE\\siren-siret_project\\siret-siren_mstest\\WpfAppSirenSiret\\bin\\Debug\\WpfAppSirenSiret.exe";

        protected static WindowsDriver<WindowsElement> session;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            if (session == null)
            {
                var appiumOptions = new AppiumOptions();
                appiumOptions.AddAdditionalCapability("platformName", "windows");
                appiumOptions.AddAdditionalCapability("app", WpfAppId);
                appiumOptions.AddAdditionalCapability("deviceName", "WindowsPC");
                session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appiumOptions);
            }
        }

        [TestMethod]
        public void CheckAValidSIREN()     
        {
            var txtName = session.FindElementByAccessibilityId("txtValue");
            txtName.SendKeys(
                Keys.NumberPad8 + 
                Keys.NumberPad0 + 
                Keys.NumberPad0 + 
                Keys.NumberPad4 + 
                Keys.NumberPad0 + 
                Keys.NumberPad3 + 
                Keys.NumberPad2 + 
                Keys.NumberPad2 +
                Keys.NumberPad2
                );
            session.FindElementByAccessibilityId("checkValueButton").Click();
            var txtResult = session.FindElementByAccessibilityId("txtResult");
            Assert.AreEqual(txtResult.Text, $"Le SIREN est correct. Il comprend bien 9 chiffres et est bien un multiple de 10.");
        }

        [TestMethod]
        public void CheckAnInvalidSIREN()
        {
            var txtName = session.FindElementByAccessibilityId("txtValue");
            txtName.Clear();
            txtName.SendKeys(
                Keys.NumberPad8 +
                Keys.NumberPad0 +
                Keys.NumberPad0 +
                Keys.NumberPad4 +
                Keys.NumberPad0 +
                Keys.NumberPad3 +
                Keys.NumberPad2 +
                Keys.NumberPad2 +
                Keys.NumberPad3
                );
            session.FindElementByAccessibilityId("checkValueButton").Click();
            var txtResult = session.FindElementByAccessibilityId("txtResult");
            Assert.AreEqual(txtResult.Text, $"Erreur : Le SIREN : {txtName.Text}, est incorrect.");
        }
    }
}
