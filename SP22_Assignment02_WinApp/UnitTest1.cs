using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace SP22_Assignment02_WinApp
{
    [TestClass]
    public class UnitTest1
    {
        public static WindowsDriver<WindowsElement> driver;

        #region Setups and Cleanups
        public TestContext instance;
        public TestContext TestContext
        {
            set { instance = value; }
            get { return instance; }
        }


        [TestInitialize()]
        public void TestInit()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");
            options.AddAdditionalCapability("deviceName", "WindowsPC");
            driver = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [TestCleanup()]
        public void TestCleanUp()
        {
            if (driver != null)
            {
                driver.Quit();
                driver = null;
            }
        }

        #endregion
        private string GetCalculatorResultText()
        {
            return driver.FindElementByAccessibilityId("CalculatorResults").Text.Replace("Display is", string.Empty).Trim();
        }

        [TestMethod]
        public void Addition()
        {
            driver.FindElementByName("Five").Click();
            driver.FindElementByName("Plus").Click();
            driver.FindElementByName("Seven").Click();
            driver.FindElementByName("Equals").Click();
            var calculatorResult = GetCalculatorResultText();
            Assert.AreEqual("12", calculatorResult);
        }

        [TestMethod]
        public void Division()
        {
            driver.FindElementByAccessibilityId("num8Button").Click();
            driver.FindElementByAccessibilityId("num8Button").Click();
            driver.FindElementByAccessibilityId("divideButton").Click();
            driver.FindElementByAccessibilityId("num1Button").Click();
            driver.FindElementByAccessibilityId("num1Button").Click();
            driver.FindElementByAccessibilityId("equalButton").Click();
            Assert.AreEqual("8", GetCalculatorResultText());
        }

        [TestMethod]
        public void Multiplication()
        {
            driver.FindElementByXPath("//Button[@Name='Nine']").Click();
            driver.FindElementByXPath("//Button[@Name='Multiply by']").Click();
            driver.FindElementByXPath("//Button[@Name='Nine']").Click();
            driver.FindElementByXPath("//Button[@Name='Equals']").Click();
            Assert.AreEqual("81", GetCalculatorResultText());
        }

        [TestMethod]
        public void Subtraction()
        {
            driver.FindElementByAccessibilityId("num9Button").Click();
            driver.FindElementByAccessibilityId("minusButton").Click();
            driver.FindElementByAccessibilityId("num1Button").Click();
            driver.FindElementByAccessibilityId("equalButton").Click();
            Assert.AreEqual("8", GetCalculatorResultText());
        }

        [TestMethod]
        [DataRow("One", "Plus", "Seven", "8")]
        [DataRow("Nine", "Minus", "One", "8")]
        [DataRow("Eight", "Divide by", "Eight", "1")]
        public void Calculation_DataDriven(string input1, string operation, string input2, string expectedResult)
        {
            driver.FindElementByName(input1).Click();
            driver.FindElementByName(operation).Click();
            driver.FindElementByName(input2).Click();
            driver.FindElementByName("Equals").Click();
            Assert.AreEqual(expectedResult, GetCalculatorResultText());
        }


    }
}
