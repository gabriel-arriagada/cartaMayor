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

namespace CartaMayor
{
    /// <summary>
    /// Lógica de interacción para Inicio.xaml
    /// </summary>
    public partial class Inicio : Window
    {
        //VARIABLE OPCION PARA TIPOS DE JUEGO
        public static bool opcion;
        public static String juga1;
        public static String juga2;

        public Inicio()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (UvsU.IsChecked != true && UvsPC.IsChecked != true)
                MessageBox.Show("Selecciona el tipo de juego", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                if (t_jugador2.Text.Equals("") || t_jugador1.Text.Equals(""))
                    MessageBox.Show("Antes de jugar, debes ingresar el nombre de/los jugadores", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                {
                    //igualo las variables juga1 y juga2 a las cajas de texto que contienen
                    //los nombres de los jugadores
                    juga1 = t_jugador1.Text;
                    juga2 = t_jugador2.Text;

                    MainWindow m = new MainWindow();
                    m.Show();
                    this.Close();
                }
            }
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            t_jugador1.Focus();
            t_jugador2.Text = "PC";
            t_jugador2.IsReadOnly = true;
            opcion = false;          
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            t_jugador1.Focus();
            t_jugador2.Text = "";
            t_jugador2.IsReadOnly = false;
            opcion = true;
        }
    }
}
