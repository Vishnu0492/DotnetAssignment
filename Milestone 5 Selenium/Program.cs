using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Milestone_5_Selenium
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();
            try
            {
                //Navigate to URL
                driver.Navigate().GoToUrl("https://example.com/login");
                driver.Manage().Window.Maximize();
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                //locate username and password field

                IWebElement UsernameField = wait.Until(d => d.FindElement(By.XPath("//input[@name='username']")));
                IWebElement PasswordField = wait.Until(d => d.FindElement(By.XPath("//input[@name='password']")));
                IWebElement LoginButton = wait.Until(d => d.FindElement(By.XPath("//button[@type='submit']")));
                //Input Credential
                UsernameField.SendKeys("testuser");
                PasswordField.SendKeys("password123");
                LoginButton.Click();
                //wait for the dashboard to load
                wait.Until(d => d.Url.Contains("dashboard"));
                string currentUrl = driver.Url;
                if (currentUrl.Equals("https://example.com/dashboard"))
                {
                    Console.WriteLine($"Login Successfull. Current Url:{currentUrl}");
                }
                else
                {
                    Console.WriteLine($"Login failed. Current Url:{currentUrl}");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            { driver.Quit(); }
        }
    }
}