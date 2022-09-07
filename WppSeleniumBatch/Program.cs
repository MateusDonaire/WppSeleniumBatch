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

                var watch = new System.Diagnostics.Stopwatch();

                watch.Start();
                h.WatingToLoadWhatsApp(driver);
                watch.Stop();

                string timeWatchLoadWhatsApp = h.ToFormatWatchTime(watch);

                var groupsList = new List<string>();
                groupsList.Add(EnumHelper.FirstGroup);
                groupsList.Add(EnumHelper.SecondGroup);

                watch.Restart();
                foreach (var group in groupsList)
                {
                    h.SelectGroup(driver, group);
                    h.SendImage(driver, group);
                }
                watch.Stop();

                string timeWatchSendImages = h.ToFormatWatchTime(watch);

                h.SelectGroup(driver, EnumHelper.MyGroup);
                h.SendTimeWatch(driver, EnumHelper.MyGroup, timeWatchLoadWhatsApp, timeWatchSendImages);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Read();
            }
            finally
            {
                driver.Quit();
            }
        }


    }
}
