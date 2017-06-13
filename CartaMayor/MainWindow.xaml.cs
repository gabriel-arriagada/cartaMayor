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
using System.Collections;
namespace CartaMayor
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        Carta carta = new Carta();
        int a = 0;
        double acumuladoJugdor1 = 0;
        double acumuladoJugador2 = 0;
        int valor1;
        int valor2;
        bool op = false;
        char pinta1;
        char pinta2;

        //VERIFICAR QUE PINTA ES MAYOR
        void verificarPinta()
        {
            int n1 = 0;
            int n2 = 0;
            switch (pinta1) {case'c':n1=4;break;case'p':n1=3;break;case'd':n1=2;break;case't':n1=1;break;case 'j':n1=0;break;};
            switch (pinta2) {case'c':n2=4;break;case'p':n2=3;break;case'd':n2=2;break;case't':n2=1;break;case 'j':n2=0;break;};
            if (n1 > n2)
            {
                acumuladoJugdor1 += Convert.ToDouble(t_apuestaJugador1.Text) +
                             Convert.ToDouble(t_apuestaJugador2.Text);
                label_ganador.Content = "¡ Gana " + L_nombreJuga1.Content+" !";
            }
            else
                if(n2 > n1)
            {
                acumuladoJugador2 += Convert.ToDouble(t_apuestaJugador2.Text) +
                            Convert.ToDouble(t_apuestaJugador1.Text);
                label_ganador.Content = "¡ Gana " + L_nombreJuga2.Content+ " !";
            }
                else
                    if(n1==0&&n2==0)
                        label_ganador.Content = "Empate";
        }


        public MainWindow()
        {
            InitializeComponent();
            if (!Inicio.opcion) t_apuestaJugador2.IsReadOnly = true;              
            carta.llenarArreglo();
            carta.llenarMazo();

            //ENVIO NOMBRE DE JUGADORES RESCATADOS DESDE EL FORMULARIO INICIO
            L_nombreJuga1.Content = Inicio.juga1;
            L_nombreJuga2.Content = Inicio.juga2;
            L_acumulado1.Content = "$ 0";
            L_acumulado2.Content = "$ 0";

            //ESCONDO BOTON DE JUGAR
            btn_jugar.Visibility = Visibility.Hidden;
        }

        //ENVIO VALOR A CAJA DE APUESTAS DE PC POR 1/4 MAS DE LO QUE APUESTA EL JUGADOR1
        //SOLO SI OPCION ES FALSE
        private void t_apuestaJugador1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (Inicio.opcion)
            {
                case false:
                    
                    if (!t_apuestaJugador1.Text.Equals(""))
                    {
                        t_apuestaJugador2.Text = Convert.ToString(Convert.ToInt32(t_apuestaJugador1.Text) +
                                                 (0.25 * Convert.ToInt32(t_apuestaJugador1.Text)));
                    }
                    else
                        t_apuestaJugador2.Text = "";
                 break;
            }
        }

        //BOTON JUGAR (REPARTIR Y VOLTEAR CARTAS)
        private void btn_jugar_Click(object sender, RoutedEventArgs e)
        {
            switch (a)
            {
                case 54:
                    //cuANDO SE ACABA EL MAZO:

                    //ivisibilizar boton jugar
                    btn_jugar.Visibility = Visibility.Hidden;

                    
                    carta.resetImazo_e_Indice();
                    b_barajar.Visibility = Visibility.Visible;
                    a = 0;

                    //enviar imagenes de poza carta 
                    p_carta1.Source = carta.rescatarPozaCarta().Source;
                    p_carta2.Source = carta.rescatarPozaCarta().Source;

                    //limpiar cajas de apuestas
                    t_apuestaJugador1.Text = "";
                    t_apuestaJugador2.Text = "";

                    //limpiar label ganador
                    label_ganador.Content = "";

                    break;

                default:
                    if (!op)
                    {
                        //RSCATAR CARTA DE MAZO IMAGEN PARA EFECTO DE MAZO EN DISMINUCION
                        p_mazo.Source = carta.rescatarMazo().Source;

                        //rescatar y enviar dorso, con tiempo detenido, a poza cartas para dar 
                        //impresion de morosidad
                        p_carta1.Source = carta.rescatarDorso().Source;
                        p_carta2.Source = carta.rescatarDorso().Source;

                        //ENVIO TEXTO A boton
                        btn_jugar.Content = "Voltear";

                        //limpiar label ganador
                        label_ganador.Content = "";

                        //OP =TRUE PARA QUE EN LA VUELTA SUIGUIENTE ENTRE AL ELSE Y ENVIE A
                        //LOS POZA CARTAS EL DORSO
                        op = true;
                    }
                    else
                    {
                        if (t_apuestaJugador1.Text.Equals("") || t_apuestaJugador2.Text.Equals(""))                   
                            MessageBox.Show("Debes apostar antes de voltear las cartas", "Aviso",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                        else 
                            if(t_apuestaJugador1.Text.Equals("0") || t_apuestaJugador2.Text.Equals("0"))
                                MessageBox.Show("El monto de apuesta debe ser mayor a cero", "Aviso",
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                        else
                        {
                            //ENVIO TEXTO A boton
                            btn_jugar.Content = "Repartir";

                            //OP = FLASE PARA QUE EN LA VUELTA SIGUIENTE ENTRE EN EL IF
                            op = false;

                            //RESCATO CARTA, VALOR y PINTA de CARTA 1
                            carta.generarCartaBarajada();
                            p_carta1.Source = carta.rescatarCarta().Source;
                            valor1 = carta.rescatarValor();
                            pinta1 = carta.rescatarPinta();
                               
                            //RESCATO CARTA, VALOR y PINTA de CARTA 2
                            carta.generarCartaBarajada();
                            p_carta2.Source = carta.rescatarCarta().Source;
                            valor2 = carta.rescatarValor();
                            pinta2 = carta.rescatarPinta();

                            //CUENTO CARTAS USADAS
                            a += 2;

                            //ENVIO VALORES A CAJA DE TEXTO PARA MOSTRARLOS
                            //aviso.Content = "Corazon>Pica>Diamante>Trebol\n" + (a += 2) + " CARTAS\n" + valor1 + "\t|\t" + valor2 + "\n" +
                            //pinta1 + "\t|\t" + pinta2;


                            //Funcion de comprobar valores de cartas
                            if (valor1 > valor2)
                            { 
                                acumuladoJugdor1 += Convert.ToDouble(t_apuestaJugador1.Text) +
                                Convert.ToDouble(t_apuestaJugador2.Text);
                                label_ganador.Content = "¡ Gana " + L_nombreJuga1.Content + " !";
                            }
                            else if (valor2 > valor1)
                            {
                                acumuladoJugador2 += Convert.ToDouble(t_apuestaJugador2.Text) +
                                Convert.ToDouble(t_apuestaJugador1.Text);
                                label_ganador.Content = "¡ Gana " + L_nombreJuga2.Content + " !";
                            }
                            else if (valor1 == valor2)
                                verificarPinta();
                        }
                    }
                    L_acumulado1.Content = "$ " + Convert.ToString(acumuladoJugdor1);
                    L_acumulado2.Content = "$ " + Convert.ToString(acumuladoJugador2);
                    break;
            }
        }

        //BOTON BARAJAR PINTA
        private void b_barajar_Click(object sender, RoutedEventArgs e)
        {
            carta.barajar();
            p_mazo.Source = carta.rescatarMazo().Source;
            b_barajar.Visibility = Visibility.Hidden;

            //hago visible boton jugar despues de barajar
            btn_jugar.Content = "Repartir";
            btn_jugar.Visibility = Visibility.Visible;
        }

        
        //APUESTA 1 SOLO NUMEROS
        private void t_apuestaJugador1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));
            if (ascci >= 48 && ascci <= 57)
                e.Handled = false;
            else
                e.Handled = true;
        }

        //APUESTA 2 SOLO NUMEROS
        private void t_apuestaJugador2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));
            if (ascci >= 48 && ascci <= 57)
                e.Handled = false;
            else
                e.Handled = true;
        }


        //LABEL ATRAS PARA GESTIONAR LA SALIDA DEL FORMULARIO
        private void lbl_atras_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //VALIDAR EL SALIR DE EL JUEGO SIEMPRE Y CUANDO SE HALLA VOLTEADO LA APUESTA
            if(op)
                MessageBox.Show("No puedes salir hasta terminar la apuesta !!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
            Inicio i = new Inicio();
            i.Show();
            this.Close();
            }
        }

        }//FIN CLASE MAIN_WINDOW     

    }//FIN NAMESPACE
