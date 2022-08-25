
using ModernWpf;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows;

namespace Station_de_travail
{
    /// <summary>
    /// Logique d'interaction pour Paramètre.xaml
    /// </summary>
    public partial class Window_Paramètre : Window
    {


       

        public Window_Paramètre()
        {
            InitializeComponent();
            try
            {
                if (Properties.Settings.Default.ThemeMode == "Dark")
                {
                    Toggle.IsOn = true;

                }
                else
                {
                    Toggle.IsOn = false;
                }
                ObservableCollection<IPaddress_Class> ipaddress = new ObservableCollection<IPaddress_Class>();
                IPaddress_Class ip_text = new IPaddress_Class(Properties.Settings.Default.AdresseIp, Properties.Settings.Default.Port);
                ipaddress.Add(ip_text);
                DataContext = ipaddress;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            Properties.Settings.Default.AdresseIp = AdresseIp_Text.Text;
            Properties.Settings.Default.Port = Port_Text.Text;
            Properties.Settings.Default.Id = Identification_Text.Text;
            Properties.Settings.Default.Password = Password_Text.Password;
            Properties.Settings.Default.Save();
            
            this.Close();
        }

        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            OnClosing(new CancelEventArgs());
                  
            this.Close();
        }

        private void Port_Text_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text); 
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            e.Cancel = true;
        }

        private void Toggle_Toggled(object sender, RoutedEventArgs e)
        {
            
            if(Toggle.IsOn)
            {
                ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
                HandyControl.Themes.ThemeManager.Current.ApplicationTheme = HandyControl.Themes.ApplicationTheme.Dark; 

                Properties.Settings.Default.ThemeMode = Toggle.OnContent.ToString();
                
                Properties.Settings.Default.Save();
            }
            else
            {
                ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
                HandyControl.Themes.ThemeManager.Current.ApplicationTheme = HandyControl.Themes.ApplicationTheme.Light;


                Properties.Settings.Default.ThemeMode = Toggle.OffContent.ToString();
                
                Properties.Settings.Default.Save();
            }
        }

        private void AdresseIp_Text_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            // @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$"
            Regex regex = new Regex(@"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
