using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Threading;
using WindowsInput;

namespace WppSeleniumBatch
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var h = new Helper();

            ChromeOptions options = new ChromeOptions();
            options.AddArgument($@"user-data-dir={EnumHelper.PathChrome}");
            options.AddArgument($@"--profile-directory=Default");
            options.AddArgument("--start-maximized");

            var driver = new ChromeDriver(EnumHelper.PathWebDriver, options);
            try
            {

                driver.Navigate().GoToUrl(EnumHelper.Url);

                h.WatingToLoadWhatsApp(driver);

                var groupsList = new List<string>();
                groupsList.Add(EnumHelper.FirstGroup);
                groupsList.Add(EnumHelper.SecondGroup);

                foreach (var group in groupsList)
                {
                    h.SelectGroup(driver, group);
                    h.SendImage(driver, group);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Read();
            }
            finally
            {
                driver.Close();
                driver.Quit();
            }
        }

    }
}
