using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Cryptography;
using ModernWpf;
using HandyControl.Themes;
using HandyControl.Data;
using System.Media;

namespace Station_de_travail
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Data_Class> datamodels = new List<Data_Class>();
        Window_Paramètre Para = new Window_Paramètre();
        
        DispatcherTimer timer = new DispatcherTimer();

        
        public MainWindow()
        {
            //HandyControl.Themes.ThemeManager.Current.SystemThemeChanged += ThemeResources_SystemThemeChanged;

            InitializeComponent();
            //User_Label.Content = Properties.Settings.Default.Id;
            startclock();
            Update();
            
            try
            {
                if(Properties.Settings.Default.ThemeMode == "Dark")
                {
                    ModernWpf.ThemeManager.Current.ApplicationTheme = ModernWpf.ApplicationTheme.Dark;
                    HandyControl.Themes.ThemeManager.Current.ApplicationTheme = HandyControl.Themes.ApplicationTheme.Dark;
                }
                else
                {
                    ModernWpf.ThemeManager.Current.ApplicationTheme = ModernWpf.ApplicationTheme.Light;
                    HandyControl.Themes.ThemeManager.Current.ApplicationTheme = HandyControl.Themes.ApplicationTheme.Light ;
                }
                if ((datamodels = JsonValeurs().Result) != null)
                {
                    DataContext = datamodels;
                    Seuil();
                    this.Connection.Foreground = Brushes.Green;
                    this.Connection.Text = "Connecté";
                }

            }
            catch(Exception ex)
            {
                ex.Message.ToString();
                this.Connection.Foreground = Brushes.Red;
                this.Connection.Text = "Vous n'êtes pas connecté";
            }
            
        }
        
        private void Update()
        {
            timer.Interval = TimeSpan.FromMinutes(1);
            timer.Tick += delegate (object sender, EventArgs e) {
                try
                {
                    if ((datamodels = JsonValeurs().Result) == null && datamodels == null)
                    {
                        this.Connection.Foreground = Brushes.Red;
                        this.Connection.Text = "Vous n'êtes pas connecté";
                        
                    }
                    else
                    {
                        DataContext = datamodels;
                        Seuil();
                        this.Connection.Foreground = Brushes.Green;
                        this.Connection.Text = "Connecté";

                    }
                }
                catch
                {
                    this.Connection.Foreground = Brushes.Red;
                    this.Connection.Text = "Vous n'êtes pas connecté";
                }
               
            };
            timer.Start();

        }

       

        private void startclock()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += tickevent;
            timer.Start();
        }

        private void tickevent(object sender, EventArgs e)
        {
            Time.Text  = DateTime.Now.ToString();
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

        public  async Task<List<Data_Class>> JsonValeurs()
        {
            string jsonText = null;
            string adresseip = Properties.Settings.Default.AdresseIp;
            string port = Properties.Settings.Default.Port;
            string id = Properties.Settings.Default.Id;
            var password = Properties.Settings.Default.Password;
            try
            {
                HttpClient client = new HttpClient();
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    client.BaseAddress = new Uri("http://" + adresseip + ":" + port + "/last_data_hash.php?username=" + id + "&password=" + GetHash(sha256Hash,password) + "");
                }
                    
                HttpResponseMessage reponse = await client.GetAsync(client.BaseAddress).ConfigureAwait(false);
                if (reponse != null)
                {
                    jsonText = await reponse.Content.ReadAsStringAsync();
                    datamodels = JsonConvert.DeserializeObject<List<Data_Class>>(jsonText);
                    

                }
            }
            catch (Newtonsoft.Json.JsonException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message,"Erreur JSON");
            }
                return datamodels;
        }

        

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
       
            Para.AdresseIp_Text.Text = Properties.Settings.Default.AdresseIp;
            Para.Port_Text.Text = Properties.Settings.Default.Port;
            Para.Identification_Text.Text = Properties.Settings.Default.Id;
            Para.Password_Text.Password = Properties.Settings.Default.Password;
            try
            {
                if ((bool)Para.ShowDialog())
                {
                    
                    if (Properties.Settings.Default.Id != "ChefAtelier")
                    {
                        this.HistoriqueButton.IsEnabled = false;
                        this.CourbeButton.IsEnabled = false;
                        
                        this.Seuisbutton.IsEnabled = false;
                    }
                    else
                    {
                        this.HistoriqueButton.IsEnabled = true;
                        this.CourbeButton.IsEnabled = true;
                        
                        this.Seuisbutton.IsEnabled = true;
                    }
                     
                }
                
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.StackTrace);
            }
            
        }

        private void HistoriqueButton1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            Window_Historique historique = new Window_Historique();
            historique.ShowDialog();
        }

        private void CourbeButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Window_Courbes courbes = new Window_Courbes();
            courbes.ShowDialog();
        }

     

        private void Seuisbutton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Window_Seuils seuils = new Window_Seuils();
            seuils.ShowDialog();
        }

        private void Quitterbutton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string message = "sûrs ?";
            string titredumessagebox = "Quitter l'application";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult résultat;
            résultat = System.Windows.Forms.MessageBox.Show(message, titredumessagebox, buttons,MessageBoxIcon.Question);
            if (résultat == System.Windows.Forms.DialogResult.Yes)
                this.Close();
        }

        private void Seuil()
        {
            
            SolidColorBrush brush_blue = new SolidColorBrush(Color.FromArgb(255, 82, 113, 255));
            SolidColorBrush brush_gray = new SolidColorBrush(Color.FromArgb(255, 84, 84, 84));
            foreach (var sensor in datamodels)
            {
                switch(sensor.sensor)
                {
                        case "CO": 
                        if(double.Parse(sensor.value,System.Globalization.CultureInfo.InvariantCulture) > sensor.max_valeur )
                        {
                            ValeurCO.Foreground = Brushes.Red;
                            GroupBox_CO.BorderBrush = Brushes.Red;
                        }
                        else if (double.Parse(sensor.value, System.Globalization.CultureInfo.InvariantCulture) < sensor.min_valeur)
                        {
                            ValeurCO.Foreground = brush_gray;
                            GroupBox_CO.BorderBrush = brush_gray;
                        }
                        else
                        {
                            ValeurCO.Foreground = Brushes.Black;
                            GroupBox_CO.BorderBrush = brush_blue;
                        }
                        break;
                        case "CO2":
                        if (double.Parse(sensor.value, System.Globalization.CultureInfo.InvariantCulture) > sensor.max_valeur )
                        {
                            ValeurCO2.Foreground = Brushes.Red;
                            GroupBox_CO2.BorderBrush = Brushes.Red;
                        }
                        else if (double.Parse(sensor.value, System.Globalization.CultureInfo.InvariantCulture) < sensor.min_valeur)
                        {
                            ValeurCO2.Foreground = brush_gray;
                            GroupBox_CO2.BorderBrush = brush_gray;
                        }
                        else
                        {
                            ValeurCO2.Foreground = Brushes.Black;
                            GroupBox_CO2.BorderBrush = brush_blue;
                        }
                        break;
                        case "HUM":
                        if (double.Parse(sensor.value, System.Globalization.CultureInfo.InvariantCulture) > sensor.max_valeur )
                        {
                            ValeurHumiditerelative.Foreground = Brushes.Red;
                            GroupBox_HUM.BorderBrush = Brushes.Red;
                        }
                        else if (double.Parse(sensor.value, System.Globalization.CultureInfo.InvariantCulture) < sensor.min_valeur)
                        {
                            ValeurHumiditerelative.Foreground = brush_gray;
                            GroupBox_HUM.BorderBrush = brush_gray;
                        }
                        else
                        {
                            ValeurHumiditerelative.Foreground = Brushes.Black;
                            GroupBox_HUM.BorderBrush = brush_blue;
                        }
                        break;
                        case "LUX":
                        if (double.Parse(sensor.value, System.Globalization.CultureInfo.InvariantCulture) > sensor.max_valeur )
                        {
                            ValeurLuminosite.Foreground = Brushes.Red;
                            GroupBox_LUX.BorderBrush = Brushes.Red;
                        }
                        else if (double.Parse(sensor.value, System.Globalization.CultureInfo.InvariantCulture) < sensor.min_valeur)
                        {
                            ValeurLuminosite.Foreground = brush_gray;
                            GroupBox_LUX.BorderBrush = brush_gray;
                        }
                        else
                        {
                            ValeurLuminosite.Foreground = Brushes.Black;
                            GroupBox_LUX.BorderBrush = brush_blue;
                        }
                        break;
                        case "PRES":
                        if ((double.Parse(sensor.value, System.Globalization.CultureInfo.InvariantCulture)/100) > sensor.max_valeur )
                        {
                            ValeurPression.Foreground = Brushes.Red;
                            GroupBox_Pres.BorderBrush = Brushes.Red;
                        }
                        else if ((double.Parse(sensor.value, System.Globalization.CultureInfo.InvariantCulture)/100) < sensor.min_valeur)
                        {
                            ValeurPression.Foreground = brush_gray;
                            GroupBox_Pres.BorderBrush = brush_gray;
                        }
                        else
                        {
                            ValeurPression.Foreground = Brushes.Black;
                            GroupBox_Pres.BorderBrush = brush_blue;
                        }
                        break;
                        case "TC":
                        if (double.Parse(sensor.value, System.Globalization.CultureInfo.InvariantCulture) > sensor.max_valeur )
                        {
                            ValeurTemperature.Foreground = Brushes.Red;
                            GroupBox_TC.BorderBrush = Brushes.Red;
                        }
                        else if (double.Parse(sensor.value, System.Globalization.CultureInfo.InvariantCulture) < sensor.min_valeur)
                        {
                            ValeurTemperature.Foreground = brush_gray;
                            GroupBox_TC.BorderBrush = brush_gray;
                        }
                        else
                        {
                            ValeurTemperature.Foreground = Brushes.Black;
                            GroupBox_TC.BorderBrush = brush_blue;
                        }
                        break;
                        case "LP":
                        if (double.Parse(sensor.value, System.Globalization.CultureInfo.InvariantCulture) > sensor.max_valeur)
                        {
                            ValeurHumiditesol.Foreground = Brushes.Red;
                            GroupBox_LP.BorderBrush = Brushes.Red;
                        }
                        else if (double.Parse(sensor.value, System.Globalization.CultureInfo.InvariantCulture) < sensor.min_valeur)
                        {
                            ValeurHumiditesol.Foreground = brush_gray;
                            GroupBox_LP.BorderBrush = brush_gray;
                        }
                        else
                        {
                            if (Properties.Settings.Default.ThemeMode == "Dark")
                            {
                                ValeurHumiditesol.Foreground = Brushes.White;
                                GroupBox_LP.BorderBrush = brush_blue;
                            }
                            else
                            {
                                ValeurHumiditesol.Foreground = Brushes.Black;
                                GroupBox_LP.BorderBrush = brush_blue;
                            }    
                        }
                        break;
                }
            }
        }

        private void AppBarToggleButton_Click(object sender, RoutedEventArgs e)
        {
            SoundPlayer soundplayer = new SoundPlayer(@"C:\Users\fayel\source\repos\Station de travail\Station de travail\Alarmes\alarm.wav");
            if (CO2_Alarm.IsChecked == true)
            {
                foreach(var sensor in datamodels)
                {
                    if(sensor.sensor =="CO2")
                    {
                            if (double.Parse(sensor.value, System.Globalization.CultureInfo.InvariantCulture) > sensor.max_valeur || double.Parse(sensor.value, System.Globalization.CultureInfo.InvariantCulture) < sensor.min_valeur)
                            {
                                soundplayer.PlayLooping();
                            }
                    }

                }
            }
            else
            {
                soundplayer.Stop();
            }
           
        }

        private void CO_Alarm_Click(object sender, RoutedEventArgs e)
        {
            SoundPlayer soundplayer = new SoundPlayer(@"C:\Users\fayel\source\repos\Station de travail\Station de travail\Alarmes\alarm.wav");
            if (CO_Alarm.IsChecked == true)
            {
                foreach (var sensor in datamodels)
                {
                    if (sensor.sensor == "CO")
                    {
                        if (double.Parse(sensor.value, System.Globalization.CultureInfo.InvariantCulture) > sensor.max_valeur || double.Parse(sensor.value, System.Globalization.CultureInfo.InvariantCulture) < sensor.min_valeur)
                        {
                            soundplayer.PlayLooping();
                        }
                    }

                }
            }
            else
            {
                soundplayer.Stop();
            }
        }

        private void HUMRE_Alarm_Click(object sender, RoutedEventArgs e)
        {
            SoundPlayer soundplayer = new SoundPlayer(@"C:\Users\fayel\source\repos\Station de travail\Station de travail\Alarmes\alarm.wav");
            if (HUMRE_Alarm.IsChecked == true)
            {
                foreach (var sensor in datamodels)
                {
                    if (sensor.sensor == "HUM")
                    {
                        if (double.Parse(sensor.value, System.Globalization.CultureInfo.InvariantCulture) > sensor.max_valeur || double.Parse(sensor.value, System.Globalization.CultureInfo.InvariantCulture) < sensor.min_valeur)
                        {
                            soundplayer.PlayLooping();
                        }
                    }

                }
            }
            else
            {
                soundplayer.Stop();
            }
        }

        private void PRES_Alarm_Click(object sender, RoutedEventArgs e)
        {
            SoundPlayer soundplayer = new SoundPlayer(@"C:\Users\fayel\source\repos\Station de travail\Station de travail\Alarmes\alarm.wav");
            if (PRES_Alarm.IsChecked == true)
            {
                foreach (var sensor in datamodels)
                {
                    if (sensor.sensor == "PRES")
                    {
                        if (double.Parse(sensor.value, System.Globalization.CultureInfo.InvariantCulture)/100 > sensor.max_valeur || double.Parse(sensor.value, System.Globalization.CultureInfo.InvariantCulture)/100 < sensor.min_valeur)
                        {
                            soundplayer.PlayLooping();
                        }
                    }

                }
            }
            else
            {
                soundplayer.Stop();
            }
        }

        private void LUX_Alarm_Click(object sender, RoutedEventArgs e)
        {
            SoundPlayer soundplayer = new SoundPlayer(@"C:\Users\fayel\source\repos\Station de travail\Station de travail\Alarmes\alarm.wav");
            if (LUX_Alarm.IsChecked == true)
            {
                foreach (var sensor in datamodels)
                {
                    if (sensor.sensor == "LUX")
                    {
                        if (double.Parse(sensor.value, System.Globalization.CultureInfo.InvariantCulture) > sensor.max_valeur || double.Parse(sensor.value, System.Globalization.CultureInfo.InvariantCulture) < sensor.min_valeur)
                        {
                            soundplayer.PlayLooping();
                        }
                    }

                }
            }
            else
            {
                soundplayer.Stop();
            }
        }

        private void HUMSOL_Alarm_Click(object sender, RoutedEventArgs e)
        {
            SoundPlayer soundplayer = new SoundPlayer(@"C:\Users\fayel\source\repos\Station de travail\Station de travail\Alarmes\alarm.wav");
            if (HUMSOL_Alarm.IsChecked == true)
            {
                foreach (var sensor in datamodels)
                {
                    if (sensor.sensor == "LP")
                    {
                        if (double.Parse(sensor.value, System.Globalization.CultureInfo.InvariantCulture) > sensor.max_valeur || double.Parse(sensor.value, System.Globalization.CultureInfo.InvariantCulture) < sensor.min_valeur)
                        {
                            soundplayer.PlayLooping();
                        }
                    }

                }
            }
            else
            {
                soundplayer.Stop();
            }
        }

        private void TEM_Alarm_Click(object sender, RoutedEventArgs e)
        {
            SoundPlayer soundplayer = new SoundPlayer(@"C:\Users\fayel\source\repos\Station de travail\Station de travail\Alarmes\alarm.wav");
            if (TEM_Alarm.IsChecked == true)
            {
                foreach (var sensor in datamodels)
                {
                    if (sensor.sensor == "TC")
                    {
                        if (double.Parse(sensor.value, System.Globalization.CultureInfo.InvariantCulture) > sensor.max_valeur || double.Parse(sensor.value, System.Globalization.CultureInfo.InvariantCulture) < sensor.min_valeur)
                        {
                            soundplayer.PlayLooping();
                        }
                    }

                }
            }
            else
            {
                soundplayer.Stop();
            }
        }

       
    }
   
}
