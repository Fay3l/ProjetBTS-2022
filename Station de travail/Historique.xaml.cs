using HandyControl.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Documents;
using System.Threading;
using System.Windows.Media.Imaging;
using System.IO;

namespace Station_de_travail
{
    /// <summary>
    /// Logique d'interaction pour Historique.xaml
    /// </summary>
    public partial class Window_Historique : Window
    {
        List<Data_Class> datamodels = new List<Data_Class>();
        List<Data_Class> mod = new List<Data_Class>();
        public Window_Historique()
        {
            InitializeComponent();
            ConfigHelper.Instance.SetLang("fr");
            try
            {

                if((datamodels = DateHistorique().Result) != null)
                {   
                    DataGridHistorique.ItemsSource = datamodels;
                    var allCO = datamodels.Where(x => x.sensor == "CO").ToList();
                    DataGridCOHistorique.ItemsSource = allCO;
                    var allCO2 = datamodels.Where(x => x.sensor == "CO2").ToList();
                    DataGridCO2Historique.ItemsSource = allCO2;
                    var allluminosite = datamodels.Where(x => x.sensor == "LUX").ToList();
                    DataGridluminositeHistorique.ItemsSource = allluminosite;
                    var alltemperature = datamodels.Where(x => x.sensor == "TC").ToList();
                    DataGridtemperatureHistorique.ItemsSource = alltemperature;
                    var allhumiditesol = datamodels.Where(x => x.sensor == "LP").ToList();
                    DataGridhumiditeausolHistorique.ItemsSource = allhumiditesol;
                    var allhumiditerelative = datamodels.Where(x => x.sensor == "HUM").ToList();
                    DataGridhumiditerelativeHistorique.ItemsSource = allhumiditerelative;
                    var allpression = datamodels.Where(x => x.sensor == "PRES").ToList();
                    DataGridpressionHistorique.ItemsSource = allpression;
                }             
            }
            catch 
            {
                System.Windows.Forms.MessageBox.Show("Erreur, aucune connection à la BDD");
            }

        }

        private static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        private void RetourButton_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.DateStart = DateStart.Text;
            Properties.Settings.Default.DateEnd = DateEnd.Text;
            Properties.Settings.Default.Save();
            this.Close();
        }

        public async Task<List<Data_Class>> DateHistorique()
        {
            string jsonText = null;
            List<Data_Class> model2 = new List<Data_Class>();
            string adresseip = Properties.Settings.Default.AdresseIp;
            string port = Properties.Settings.Default.Port;
            string id = Properties.Settings.Default.Id;
            SHA256 sha256Hash = SHA256.Create();
            var password = GetHash(sha256Hash, Properties.Settings.Default.Password);
            try
            {
                HttpClient client = new HttpClient();
                //if(DateEnd.Text == string.Empty)
                //{
                //    client.BaseAddress = new Uri("http://" + adresseip + ":" + port + "/historique.php?username=" + id + "&password=" + password + "&dateStart=" + DateTime.Parse(DateStart.Text).ToString("yyyy-MM-dd") + "&dateEnd=" + DateTime.Parse(DateStart.Text).ToString("yyyy-MM-dd") + "");
                //}
                    
                client.BaseAddress = new Uri("http://" + adresseip + ":" + port + "/historique.php?username=" + id + "&password=" + password + "&dateStart=" + DateTime.Parse(DateStart.Text).ToString("yyyy-MM-dd") + "&dateEnd=" + DateTime.Parse(DateEnd.Text).ToString("yyyy-MM-dd") + "");
                HttpResponseMessage reponse = await client.GetAsync(client.BaseAddress).ConfigureAwait(false);
                if (reponse != null)
                {
                    jsonText = await reponse.Content.ReadAsStringAsync();
                    model2 = JsonConvert.DeserializeObject<List<Data_Class>>(jsonText);
                    
                   
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("erreur");
            }
            return model2;
        }

        public async Task<List<Data_Class>> DateCourbes()
        {
            string jsonText = null;
            List<Data_Class> mod = new List<Data_Class>();
            string adresseip = Properties.Settings.Default.AdresseIp;
            string port = Properties.Settings.Default.Port;
            string id = Properties.Settings.Default.Id;
            SHA256 sha256Hash = SHA256.Create();
            var password = GetHash(sha256Hash, Properties.Settings.Default.Password);
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://" + adresseip + ":" + port + "/historique.php?username=" + id + "&password=" + password + "&dateStart=" + DateTime.Parse(Properties.Settings.Default.DateStart).ToString("yyyy-MM-dd") + "&dateEnd=" + DateTime.Parse(Properties.Settings.Default.DateEnd).ToString("yyyy-MM-dd") + "");
                HttpResponseMessage reponse = await client.GetAsync(client.BaseAddress).ConfigureAwait(false);
                if (reponse != null)
                {
                    jsonText = await reponse.Content.ReadAsStringAsync();
                    mod = JsonConvert.DeserializeObject<List<Data_Class>>(jsonText);
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("erreur");
            }
            return mod;
        }

        private void DateStart_CalendarClosed(object sender, RoutedEventArgs e)
        {
            
            
            if (this.DateEnd.Text != null)
            {
                try
                {
                    mod = DateHistorique().Result;
                    DataGridHistorique.ItemsSource = mod;
                    var allCO = mod.Where(x => x.sensor == "CO").ToList();
                    DataGridCOHistorique.ItemsSource = allCO;
                    var allCO2 = mod.Where(x => x.sensor == "CO2").ToList();
                    DataGridCO2Historique.ItemsSource = allCO2;
                    var allluminosite = mod.Where(x => x.sensor == "LUX").ToList();
                    DataGridluminositeHistorique.ItemsSource = allluminosite;
                    var alltemperature = mod.Where(x => x.sensor == "TC").ToList();
                    DataGridtemperatureHistorique.ItemsSource = alltemperature;
                    var allhumiditesol = mod.Where(x => x.sensor == "LP").ToList();
                    DataGridhumiditeausolHistorique.ItemsSource = allhumiditesol;
                    var allhumiditerelative = mod.Where(x => x.sensor == "HUM").ToList();
                    DataGridhumiditerelativeHistorique.ItemsSource = allhumiditerelative;
                    var allpression = mod.Where(x => x.sensor == "PRES").ToList();
                    DataGridpressionHistorique.ItemsSource = allpression;

                }
                catch(Exception ex)
                {
                    ex.Message.ToString();
                }
            }
            else
            {
                mod = DateHistorique().Result;
                DataGridHistorique.ItemsSource = mod;
                if (mod == null)
                {
                    System.Windows.MessageBox.Show("Aucune information sur cette date");
                };
            }
            
        }

        private void DateEnd_CalendarClosed(object sender, RoutedEventArgs e)
        {
            if (this.DateStart.Text != null)
            {
                try
                {
                    
                    mod = DateHistorique().Result;
                    DataGridHistorique.ItemsSource = mod;
                    var allCO = mod.Where(x => x.sensor == "CO").ToList();
                    DataGridCOHistorique.ItemsSource = allCO;
                    var allCO2 = mod.Where(x => x.sensor == "CO2").ToList();
                    DataGridCO2Historique.ItemsSource = allCO2;
                    var allluminosite = mod.Where(x => x.sensor == "LUX").ToList();
                    DataGridluminositeHistorique.ItemsSource = allluminosite;
                    var alltemperature = mod.Where(x => x.sensor == "TC").ToList();
                    DataGridtemperatureHistorique.ItemsSource = alltemperature;
                    var allhumiditesol = mod.Where(x => x.sensor == "LP").ToList();
                    DataGridhumiditeausolHistorique.ItemsSource = allhumiditesol;
                    var allhumiditerelative = mod.Where(x => x.sensor == "HUM").ToList();
                    DataGridhumiditerelativeHistorique.ItemsSource = allhumiditerelative;
                    var allpression = mod.Where(x => x.sensor == "PRES").ToList();
                    DataGridpressionHistorique.ItemsSource = allpression;
                   
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
        }

        private void ImprimerHistoriqueButton_Click(object sender, RoutedEventArgs e)
        {
            List<Data_Class> mod1 = new List<Data_Class>();
            mod1 = DateHistorique().Result;
            System.Windows.Controls.PrintDialog print = new System.Windows.Controls.PrintDialog();
            var select =(Tab.SelectedItem as TabItem);
            FlowDocument document = new FlowDocument();
            List list = new List();
            document.PageWidth = 500;
            
            //figure.TextAlignment = TextAlignment.Center;
           
            foreach (var Item in mod1)
            {
                Paragraph p = new Paragraph(new Run("\tCapteur:" + Item.sensor + "\tBoitier:" + Item.id_wasp + "\tValeur:" + Item.value + "\tDate:" + Item.timestamp + " "));
                p.FontSize = 12;
                list.ListItems.Add(new ListItem(p));
                document.Blocks.Add(list);
                
            }
           
            if (print.ShowDialog() == true)
            {
                IDocumentPaginatorSource idocument = document;
                print.PrintDocument(idocument.DocumentPaginator, "Historique");
            }
               
            
        }

       
    }

   
}
