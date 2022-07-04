using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
        public string BelarusBank()//did not work
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            string Responce = wc.DownloadString("https://belarusbank.by");
            string Rate = System.Text.RegularExpressions.Regex.Match(Responce, @"<td\sclass=""currency-table__cell-value"">\n\s([0-9]+\.[0-9]+)").Groups[1].Value;
            
            return Rate;
        }

        public string Belapb()
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            string Responce = wc.DownloadString("https://www.belapb.by");
            string Rate = System.Text.RegularExpressions.Regex.Match(Responce, @"href=""/rus/nalichnye-kursy-valut/vse-kursy-po-gorodu/\?bank=2&ratesBlock=1&more=3&CITY_ID=24811&DATE_RATE_DAY=04&DATE_RATE_MONTH=07&DATE_RATE_YEAR=2022&VAL1=USD&SORT=FROM"">([0-9]+\.[0-9]+)</td>").Groups[1].Value;
            
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
    }
}
