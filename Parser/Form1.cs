using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            System.Net.WebClient wc = new System.Net.WebClient();
            string Responce = wc.DownloadString("https://www.belapb.by");
            string Rate = System.Text.RegularExpressions.Regex.Match(Responce, @"href=""/rus/nalichnye-kursy-valut/vse-kursy-po-gorodu/\?bank=2&ratesBlock=1&more=3&CITY_ID=24811&DATE_RATE_DAY=07&DATE_RATE_MONTH=07&DATE_RATE_YEAR=2022&VAL1=USD&SORT=FROM"">([0-9]+\.[0-9]+)</td>").Groups[1].Value;
            
            return Rate;
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
