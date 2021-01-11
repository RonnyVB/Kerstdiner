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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Kerstdiner_Models;
using Kerstdiner_Dal;

namespace Kerstdiner_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //Globale variabelen
        List<Gerecht> mijnGerechten = null;
        List<Gerecht> mijnHoofdgerechten = null;
        List<Gerecht> mijnNagerechten = null;
        FileOperations file = new FileOperations();
        List<DinerReservatie> lijstReservaties = new List<DinerReservatie>();
        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            rbDiner.IsChecked = true;
            mijnGerechten = file.BestandLezen("Gerechten.txt");
            mijnHoofdgerechten = Splitsen("hoofdgerecht", mijnGerechten);
            mijnNagerechten = Splitsen("nagerecht", mijnGerechten);
            cmbHoofdgerechten.ItemsSource = mijnHoofdgerechten;
            cmbNagerechten.ItemsSource = mijnNagerechten;
        }

        private void rbKerstdiner_Checked(object sender, RoutedEventArgs e)
        {
            lblNagerecht.Visibility = Visibility.Visible;
            cmbNagerechten.Visibility = Visibility.Visible;
        }

        private void rbDiner_Checked(object sender, RoutedEventArgs e)
        {
            lblNagerecht.Visibility = Visibility.Hidden;
            cmbNagerechten.Visibility = Visibility.Hidden;
        }        

        private void btnSluiten_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void btnBoeken_Click(object sender, RoutedEventArgs e)
        {
            string foutmelding = "";
            int aantal=0;
            DinerReservatie reservatie = null;

            if (cmbHoofdgerechten.SelectedIndex==-1)
            {
                foutmelding += "Selecteer een hoofdgerecht!" + Environment.NewLine;
            }
            if (int.TryParse(txtAantal.Text,out aantal)==false)
            {
                foutmelding += "Aantal personen moet een numerieke waarde zijn!"+Environment.NewLine;
            }
            if (cmbNagerechten.SelectedIndex==-1 && rbKerstdiner.IsChecked==true)
            {
                foutmelding += "Selecteer een nagerecht!";
            }

            try
            {
                if (foutmelding != "")
                {
                    MessageBox.Show(foutmelding, "Fouten", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (rbDiner.IsChecked == true)
                    {
                        if (cmbHoofdgerechten.SelectedItem is Gerecht hg)
                        {
                            reservatie = new DinerReservatie(txtNaam.Text, aantal, hg.Benaming, hg.Prijs);
                        }
                    }
                    else
                    {
                        if (cmbHoofdgerechten.SelectedItem is Gerecht hg)
                        {
                            if (cmbNagerechten.SelectedItem is Gerecht ng)
                            {
                                reservatie = new KerstdinerReservatie(txtNaam.Text, aantal, hg.Benaming, hg.Prijs, ng.Benaming, ng.Prijs);
                            }
                        }                        
                    }
                    if (!lijstReservaties.Contains(reservatie))
                    {
                        lijstReservaties.Add(reservatie);
                        if (lijstReservaties.Count == 5)
                        {
                            btnBoeken.IsEnabled = false;
                            MessageBox.Show("Er worden geen reservaties meer aanvaard. Restaurant zit vol!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("U heeft reeds geboekt! Wijzigingen zijn niet toegelaten!");
                    }
                }
            }
            catch (CustomException ex)
            {
                file.FoutLoggen(ex);
                MessageBox.Show(ex.Message);
            }

            
        }


        //Methoden
        private List<Gerecht> Splitsen(string Type, List<Gerecht> lijstGerechten)
        {
            List<Gerecht> lijstRetour = new List<Gerecht>();
            foreach (var item in lijstGerechten)
            {
                if (item.Type == Type)
                {
                    Gerecht g = new Gerecht(item.Type, item.Benaming, item.Prijs);
                    lijstRetour.Add(g);
                }
            }
            return lijstRetour;
        }

        private void btnToonReservaties_Click(object sender, RoutedEventArgs e)
        {
            string reservaties="";
            for (int i = 0; i < lijstReservaties.Count; i++)
            {
                reservaties += lijstReservaties[i].ToString() + Environment.NewLine;
            }
            lblReservaties.Content = reservaties;
        }
    }
}
