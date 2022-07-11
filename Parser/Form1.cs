using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parser
{
    public partial class Form1 : Form
    {
        public string _Alfabank { get; set; }
        public string _Belarus { get; set; }
        public string _ReshenieBank { get; set; }
        public string _DabrabBank { get; set; }
        public string _BelapbBank { get; set; }

        static int a = 0;
        
        


        private const string Value = "USD_in";
        public string USDinBelarusbank = "";

        async void BelarusBank()
        {
            
            await Task.Run(() =>
            {
                var request = new GetRequest("https://belarusbank.by/api/kursExchange?city=Брест");
                request.Run();

                var responce = request.Responce;

                JArray jsonArray = JArray.Parse(responce);
                dynamic data = JObject.Parse(jsonArray[0].ToString());
                _Belarus = Convert.ToString(data.USD_in);

                

            });
            BelBank.Text = _Belarus;


        }
        public Form1()
        {
            
            InitializeComponent();
            AlfaBank();
            BelarusBank();
            BankReshenie();
            DabrabytBank();
            Belapb();

        }
          

        public double[] arr { get; set; }
        
        
     

        async public void AlfaBank()
        {
            
            await Task.Run(() =>
            {
                IWebDriver webDriver = new ChromeDriver();
                webDriver.Url = "https://www.alfabank.by/";
                var parsed = webDriver.FindElement(By.CssSelector(@"#app > div > div:nth-child(4) > section.section.no-margin > div > div.currency > div.currency__wrapper > div > div.v-data-table.currency-table.text-right.theme--light > div > table > tbody > tr:nth-child(1) > td:nth-child(3) > span:nth-child(2)")).GetAttribute("textContent");
                webDriver.Quit();
                _Alfabank = parsed.ToString();

            });
            ABank.Text = _Alfabank;
        }

        async public void DabrabytBank()
        {

            await Task.Run(() =>
            {
                System.Net.WebClient wc = new System.Net.WebClient();
                string Responce = wc.DownloadString("https://bankdabrabyt.by");
                _DabrabBank = System.Text.RegularExpressions.Regex.Match(Responce, @"</td><td class=""rate-table__sale"">([0-9]+\.[0-9]+)").Groups[1].Value;
            });

            DabrBank.Text = _DabrabBank;
        }

        async public void BankReshenie()
        {
            string Rate = "";
            await Task.Run(() =>
            {
                IWebDriver webDriver = new ChromeDriver();
                webDriver.Url = "https://rbank.by/";
                var parsed = webDriver.FindElement(By.CssSelector(@"body > div.wrapper > main > div:nth-child(3) > div > div > div > div.currency-block > div.currency-block__body > div.currency-block__table > div > div > div.currency-table__body-list.js-list-1.active > div:nth-child(3) > span.down")).GetAttribute("textContent");
                webDriver.Quit();
                Rate = parsed.ToString();

            });
            SolutionBank.Text = Rate;
        }
        

        async public void Belapb()
        {
            IWebDriver webDriver = new ChromeDriver();
            webDriver.Url = "https://www.belapb.by";

            await Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        webDriver.FindElement(By.XPath(@"//*[@id=""aj_tab2""]/table/tbody/tr[2]/td[2]/a")).GetCssValue("a");
                    }
                    catch (Exception)
                    {
                        Task.Delay(1000);
                        break;

                    }
                }

                var parsed = webDriver.FindElement(By.CssSelector(@"#aj_tab2 > table > tbody > tr:nth-child(2) > td:nth-child(2) > a")).GetAttribute("textContent");
                _BelapbBank = parsed.ToString();
                webDriver.Quit();
            });

            BelagrBank.Text = _BelapbBank; 
           
            
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            ABank.Visible = true;

        }

    

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
