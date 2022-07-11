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
        private const string Value = "USD_in";
        public string USDinBelarusbank = "";
        public Form1()
        {
            
            InitializeComponent();
            var request = new GetRequest("https://belarusbank.by/api/kursExchange?city=Брест");
            request.Run();

            var responce = request.Responce;

            JArray jsonArray = JArray.Parse(responce);
            dynamic data = JObject.Parse(jsonArray[0].ToString());
            USDinBelarusbank = Convert.ToString(data.USD_in);
            

        }
          

        public double[] arr { get; set; }
        public static short a = 0;
        
     

        public string AlfaBank()
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            string Responce = wc.DownloadString("https://www.alfabank.by");
            string Rate = System.Text.RegularExpressions.Regex.Match(Responce, @"""USD"":{""BYN"":{""value"":""([0-9]+\.[0-9]+)""").Groups[1].Value;
            ;
            return Rate;
        }

        public string DabrabytBank()
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            string Responce = wc.DownloadString("https://bankdabrabyt.by");
            string Rate = System.Text.RegularExpressions.Regex.Match(Responce, @"</td><td class=""rate-table__sale"">([0-9]+\.[0-9]+)").Groups[1].Value;
            
            return Rate;
        }
        public string BankReshenie()
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            string Responce = wc.DownloadString("https://rbank.by/");
            string Rate = System.Text.RegularExpressions.Regex.Match(Responce, @"<span class=""up"">([0-9]+\.[0-9]+)</span>").Groups[1].Value;
            
            return Rate;
        }
        public string BelarusBank()
        {
            return USDinBelarusbank;
        }

        public string Belapb()
        {
            IWebDriver webDriver = new ChromeDriver();
            webDriver.Url = "https://www.belapb.by";

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

            return Convert.ToString(parsed);
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            label2.Visible = true;

            label2.Text = "Alfabank: " + AlfaBank();
            label3.Text = "ДабрабытБанк: " + DabrabytBank();
            label4.Text = "Банк Решение: " + BankReshenie();
            label5.Text = "Беларусбанк: " + BelarusBank();
            label6.Text = "Белагропромбанк: " + Belapb();
           

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
