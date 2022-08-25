using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Security.Cryptography;

namespace Station_de_travail
{
    /// <summary>
    /// Logique d'interaction pour Seuils.xaml
    /// </summary>
    public partial class Window_Seuils : Window
    {

        private string id = Properties.Settings.Default.Id;
        private string password = Properties.Settings.Default.Password;


        public Window_Seuils()
        { 
            InitializeComponent();
            try
            {
                ObservableCollection<Seuils_Class_PropertyChanged> seuil = new ObservableCollection<Seuils_Class_PropertyChanged>();
                List<Seuils_class> datamodels = JsonSeuils().Result;
                
                MinLUX.Text = datamodels.Where(x => x.sensor == "LUX").Select(x => x.min_valeur).Last().ToString();
                MaxLUX.Text = datamodels.Where(x => x.sensor == "LUX").Select(x => x.max_valeur).Last().ToString();
                MinCO.Text = datamodels.Where(x => x.sensor == "CO").Select(x => x.min_valeur).Last().ToString();
                MaxCO.Text = datamodels.Where(x => x.sensor == "CO").Select(x => x.max_valeur).Last().ToString();
                MinCO2.Text = datamodels.Where(x => x.sensor == "CO2").Select(x => x.min_valeur).Last().ToString();
                MaxCO2.Text = datamodels.Where(x => x.sensor == "CO2").Select(x => x.max_valeur).Last().ToString();
                MinTEM.Text = datamodels.Where(x => x.sensor == "TC").Select(x => x.min_valeur).Last().ToString();
                MaxTEM.Text = datamodels.Where(x => x.sensor == "TC").Select(x => x.max_valeur).Last().ToString();
                MinPRE.Text = datamodels.Where(x => x.sensor == "PRES").Select(x => x.min_valeur).Last().ToString();
                MaxPRE.Text = datamodels.Where(x => x.sensor == "PRES").Select(x => x.max_valeur).Last().ToString();
                MinHUMRE.Text = datamodels.Where(x => x.sensor == "HUM").Select(x => x.min_valeur).Last().ToString();
                MaxHUMRE.Text = datamodels.Where(x => x.sensor  == "HUM").Select(x => x.max_valeur).Last().ToString();
                MinHUMSOL.Text = datamodels.Where(x => x.sensor == "LP").Select(x => x.min_valeur).Last().ToString();
                MaxHUMSOL.Text = datamodels.Where(x => x.sensor == "LP").Select(x => x.max_valeur).Last().ToString();
                MinBAT.Text = datamodels.Where(x => x.sensor == "BAT").Select(x => x.min_valeur).Last().ToString();
                MaxBAT.Text = datamodels.Where(x => x.sensor == "BAT").Select(x => x.max_valeur).Last().ToString();
                 
                 
                Seuils_Class_PropertyChanged seuilCO = new Seuils_Class_PropertyChanged(MaxCO.Text,MinCO.Text, MaxCO2.Text, MinCO2.Text, MaxHUMSOL.Text, MinHUMSOL.Text, MaxHUMRE.Text, MinHUMRE.Text, MaxTEM.Text, MinTEM.Text, MaxBAT.Text, MinBAT.Text, MaxPRE.Text, MinPRE.Text,MaxLUX.Text,MinLUX.Text);
                seuil.Add(seuilCO);
                DataContext = seuil;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Aucun connection au serveur");
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

        private void ValueText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);


        }

        private async void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
             HttpClient client = new HttpClient();
            try
            {
                if (this.MinCO.Text != null && this.MaxCO.Text != null)
                {
                    SHA256 sha256Hash = SHA256.Create();
                    if (this.MinCO.Text == "0")
                    {
                        HttpResponseMessage reponse1 = await client.GetAsync("http://172.16.40.251:8080/set_seuils.php?capteur=CO&mini='" + this.MinCO.Text + "'&maxi=" + this.MaxCO.Text + "&username=" + id + "&password=" + GetHash(sha256Hash, password) + "");
                    }
                    else
                    {
                        HttpResponseMessage reponse = await client.GetAsync("http://172.16.40.251:8080/set_seuils.php?capteur=CO&mini=" + this.MinCO.Text + "&maxi=" + this.MaxCO.Text + "&username=" + id + "&password=" + GetHash(sha256Hash, password) + "");
                    }


                }
                if (this.MinCO2.Text != null && this.MaxCO2.Text != null)
                {
                    SHA256 sha256Hash = SHA256.Create();
                    if (this.MinCO2.Text == "0")
                    {
                        HttpResponseMessage reponse1 = await client.GetAsync("http://172.16.40.251:8080/set_seuils.php?capteur=CO2&mini='" + this.MinCO2.Text + "'&maxi=" + this.MaxCO2.Text + "&username=" + id + "&password=" + GetHash(sha256Hash, password) + "");
                    }
                    else
                    {
                        HttpResponseMessage reponse = await client.GetAsync("http://172.16.40.251:8080/set_seuils.php?capteur=CO2&mini=" + this.MinCO2.Text + "&maxi=" + this.MaxCO2.Text + "&username=" + id + "&password=" + GetHash(sha256Hash, password) + "");
                    }
                    
                }
                if (this.MinLUX.Text != null && this.MaxLUX.Text != null)
                {
                    SHA256 sha256Hash = SHA256.Create();
                    if (this.MinLUX.Text == "0")
                    {
                        HttpResponseMessage reponse1 = await client.GetAsync("http://172.16.40.251:8080/set_seuils.php?capteur=LUX&mini='" + this.MinLUX.Text + "'&maxi=" + this.MaxLUX.Text + "&username=" + id + "&password=" + GetHash(sha256Hash, password) + "");
                    }
                    else
                    {
                        HttpResponseMessage reponse = await client.GetAsync("http://172.16.40.251:8080/set_seuils.php?capteur=LUX&mini=" + this.MinLUX.Text + "&maxi=" + this.MaxLUX.Text + "&username=" + id + "&password=" + GetHash(sha256Hash, password) + "");
                    }
                    
                        
                }
                if (this.MinPRE.Text != null && this.MaxPRE.Text != null)
                {
                    SHA256 sha256Hash = SHA256.Create();
                    if (this.MinPRE.Text == "0")
                    {
                        HttpResponseMessage reponse1 = await client.GetAsync("http://172.16.40.251:8080/set_seuils.php?capteur=PRES&mini='" + this.MinPRE.Text + "'&maxi=" + this.MaxPRE.Text + "&username=" + id + "&password=" + GetHash(sha256Hash, password) + "");
                    }
                    else
                    {
                        HttpResponseMessage reponse = await client.GetAsync("http://172.16.40.251:8080/set_seuils.php?capteur=PRES&mini=" + this.MinPRE.Text + "&maxi=" + this.MaxPRE.Text + "&username=" + id + "&password=" + GetHash(sha256Hash, password) + "");
                    }
                    
                       
                    

                }
                if (this.MinTEM.Text != null && this.MaxTEM.Text != null)
                {
                    SHA256 sha256Hash = SHA256.Create();
                    if (this.MinTEM.Text == "0")
                    {
                        HttpResponseMessage reponse1 = await client.GetAsync("http://172.16.40.251:8080/set_seuils.php?capteur=TC&mini='" + this.MinTEM.Text + "'&maxi=" + this.MaxTEM.Text + "&username=" + id + "&password=" + GetHash(sha256Hash, password) + "");
                    }
                    else
                    {
                        HttpResponseMessage reponse = await client.GetAsync("http://172.16.40.251:8080/set_seuils.php?capteur=TC&mini=" + this.MinTEM.Text + "&maxi=" + this.MaxTEM.Text + "&username=" + id + "&password=" + GetHash(sha256Hash, password) + "");
                    }
                   
                       
                }
                if (this.MinHUMRE.Text != null && this.MaxHUMRE.Text != null)
                {
                    SHA256 sha256Hash = SHA256.Create();
                    if (this.MinHUMRE.Text == "0")
                    {
                        HttpResponseMessage reponse1 = await client.GetAsync("http://172.16.40.251:8080/set_seuils.php?capteur=HUM&mini='" + this.MinHUMRE.Text + "'&maxi=" + this.MaxHUMRE.Text + "&username=" + id + "&password=" + GetHash(sha256Hash, password) + "");
                    }
                    else
                    {
                        HttpResponseMessage reponse = await client.GetAsync("http://172.16.40.251:8080/set_seuils.php?capteur=HUM&mini=" + this.MinHUMRE.Text + "&maxi=" + this.MaxHUMRE.Text + "&username=" + id + "&password=" + GetHash(sha256Hash, password) + "");
                    }
                   
                       
                }
                if (this.MinHUMSOL.Text != null && this.MaxHUMSOL.Text != null)
                {
                    SHA256 sha256Hash = SHA256.Create();
                    if (this.MinHUMSOL.Text == "0")
                    {
                        HttpResponseMessage reponse1 = await client.GetAsync("http://172.16.40.251:8080/set_seuils.php?capteur=LP&mini='" + this.MinHUMSOL.Text + "'&maxi=" + this.MaxHUMSOL.Text + "&username=" + id + "&password=" + GetHash(sha256Hash, password) + "");
                    }
                    else
                    {
                        HttpResponseMessage reponse = await client.GetAsync("http://172.16.40.251:8080/set_seuils.php?capteur=LP&mini=" + this.MinHUMSOL.Text + "&maxi=" + this.MaxHUMSOL.Text + "&username=" + id + "&password=" + GetHash(sha256Hash, password) + "");
                    }
                   
                       
                }
                if (this.MinBAT.Text != null && this.MaxBAT.Text != null)
                {
                    SHA256 sha256Hash = SHA256.Create();
                    if (this.MinBAT.Text == "0")
                    {
                        HttpResponseMessage reponse1 = await client.GetAsync("http://172.16.40.251:8080/set_seuils.php?capteur=BAT&mini='" + this.MinBAT.Text + "'&maxi=" + this.MaxBAT.Text + "&username=" + id + "&password=" + GetHash(sha256Hash, password) + "");
                    }
                    else
                    {
                        HttpResponseMessage reponse = await client.GetAsync("http://172.16.40.251:8080/set_seuils.php?capteur=BAT&mini=" + this.MinBAT.Text + "&maxi=" + this.MaxBAT.Text + "&username=" + id + "&password=" + GetHash(sha256Hash, password) + "");
                    }
                    
                }
                MessageBox.Show("Mise à jour effectué");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            
            this.Close();
        }

        public static async Task<List<Seuils_class>> JsonSeuils()
        {
            string jsonText = null;
            List<Seuils_class> model1 = new List<Seuils_class>();
            try
            {
                string adresseip = Properties.Settings.Default.AdresseIp;
                string port = Properties.Settings.Default.Port;
                string id = Properties.Settings.Default.Id;
                string password = Properties.Settings.Default.Password;
                HttpClient client = new HttpClient();
                SHA256 sha256Hash = SHA256.Create();
                client.BaseAddress = new Uri("http://"+adresseip+":"+port+"/set_seuils.php?username="+id+"&password="+GetHash(sha256Hash,password)+"");
                HttpResponseMessage reponse = await client.GetAsync(client.BaseAddress).ConfigureAwait(false);
                if (reponse != null)
                {
                    jsonText = await reponse.Content.ReadAsStringAsync();
                    model1 = JsonConvert.DeserializeObject<List<Seuils_class>>(jsonText); 
                }
            }
            catch (Newtonsoft.Json.JsonException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            return model1;
        }


    }
}
