using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using WindowsInput;

namespace WppSeleniumBatch
{
    internal class Helper
    {

        public void SendImage(ChromeDriver driver, string group)
        {
            var selectAction = driver.FindElement(By.XPath(@"//*[@id='main']/footer/div[1]/div/span[2]/div/div[1]/div[2]/div/div/span"));
            selectAction.Click();
            Thread.Sleep(500);

            var selectImage = driver.FindElement(By.XPath(@"//*[@id='main']/footer/div[1]/div/span[2]/div/div[1]/div[2]/div/span/div/div/ul/li[1]/button/span"));
            selectImage.Click();
            Thread.Sleep(1500);

            var inputSimulator = new InputSimulator();
            inputSimulator.Keyboard.TextEntry(ToFormatImagePath(group));
            Thread.Sleep(1000);
            inputSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
            Thread.Sleep(2000);

            var sendImage = driver.FindElement(By.XPath(@"//*[@id='app']/div/div/div[2]/div[2]/span/div/span/div/div/div[2]/div/div[2]/div[2]/div/div/span"));
            sendImage.Click();
            Thread.Sleep(2000);

        }

        public void SelectGroup(ChromeDriver driver, string group)
        {
            var search = driver.FindElement(By.XPath(@"//*[@id='side']/div[1]/div/div/div[2]/div/div[2]"));
            search.SendKeys(group);
            search.SendKeys(Keys.Enter);
            Thread.Sleep(500);
        }

        public void WatingToLoadWhatsApp(ChromeDriver driver)
        {
            bool loadedWhatsApp = false;
            do
            {
                try
                {
                    driver.FindElement(By.XPath(@"//*[@id='app']/div/div/div[4]/div/div/div[2]/div[1]/h1"));
                    loadedWhatsApp = true;
                }
                catch (Exception)
                {
                    loadedWhatsApp = false;
                }
            }
            while (!loadedWhatsApp);
        }

        public string ToFormatImagePath(string group)
        {
            string day = ToFormatImageName();
            string path = @$"{EnumHelper.PathImage}{group}\";
            string fullPath = path + day + ".png";
            return fullPath;
        }
        public string ToFormatImageName()
        {
            string diaFormatado;
            var dia = (DateTime.Now.DayOfYear - 9).ToString();

            if (dia.Length == 1) diaFormatado = "00" + dia;
            else if (dia.Length == 2) diaFormatado = "0" + dia;
            else diaFormatado = dia;


            return diaFormatado;
        }

        public string ToFormatWatchTime(System.Diagnostics.Stopwatch watch, int minutes = 0, int seconds = 0)
        {
            return Convert.ToInt32(watch.Elapsed.TotalMinutes) + " minutos e " + Convert.ToInt32(watch.Elapsed.TotalSeconds) + " segundos" ;
        }

        public void SendTimeWatch(ChromeDriver driver, string group, string time1, string time2)
        {
            string message =$"O Whatsapp demorou *{time1}* para abrir e *{time2}* para enviar as imagens! =D ";

            var selectImage = driver.FindElement(By.XPath(@"//*[@id='main']/footer/div[1]/div/span[2]/div/div[2]/div[1]/div/div[1]/p"));
            selectImage.SendKeys(message);
            Thread.Sleep(1500);

            var sendImage = driver.FindElement(By.XPath(@"//*[@id='main']/footer/div[1]/div/span[2]/div/div[2]/div[2]/button/span"));
            sendImage.Click();
            Thread.Sleep(2000);

        }
    }
}
