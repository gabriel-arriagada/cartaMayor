using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;


namespace CartaMayor
{
    class Carta
    {
        ArrayList cartas = new ArrayList();
        ArrayList mazo = new ArrayList();
        ArrayList valores = new ArrayList();
        ArrayList desordenados = new ArrayList();
        Random random = new Random();
        int valorRandom;
        int numero=112;
        bool cambio = false;
        bool cambio1 = false;
        int indice = 0;
        int iMazo = 26;

        //lleno array con cartas y otro con valores respectivos
        //112 = p
        //99 = c
        //100 = d
        //116 = t
        public void llenarArreglo() 
        {
            for (int i = 1; i <= 4; i++)
            {            
                for (int j = 1; j <= 13; j++)
                {
                    cartas.Add("Imagenes/Cartas/"+j+(char)numero+".png");
                    int valor = j;
                    if (j == 1)
                    {
                        valor = 20;
                    }
                    valores.Add(valor);
                }

                if (!cambio)
                {
                    numero = numero - 13;
                    cambio = true;
                }
                else if (!cambio1)
                {
                    numero = numero + 1;
                    cambio1 = true;
                }
                else
                    numero = 116;
                }
            //agrego jokers
            cartas.Add("Imagenes/Cartas/1j.png");
            valores.Add(15);
            cartas.Add("Imagenes/Cartas/2j.png");
            valores.Add(15);
        }

        //lleno ARRAY CON RUTA DE IMAGENES DE MAZO en forma de dorsos
        public void llenarMazo()
        {
            for (int i = 1; i <= 27; i++)
            {
                mazo.Add("Imagenes/Cartas/" + i + "do.png");
            }
        }

        //RESCATAR mazo completo descontando
        public Image rescatarMazo()
        {
            Image carta = new Image();
            BitmapImage b = new BitmapImage();
            if (iMazo < 0)
                carta.Source = b;
            else
            {
                String ruta = (String)mazo[iMazo];
                b.BeginInit();
                b.UriSource = new Uri(ruta, UriKind.Relative);
                b.EndInit();
                carta.Source = b;
                iMazo --;
            }
            return carta;    
        }

        //RESCATAR DORSO
        public Image rescatarDorso()
        {
            Image dorso = new Image();
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri("Imagenes/Cartas/dorso.png", UriKind.Relative);
            b.EndInit();
            dorso.Source = b;
            return dorso;
        }

        //RESCATAR POZA CARTAS
        public Image rescatarPozaCarta()
        {
            Image pozaCarta = new Image();
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri("Imagenes/Cartas/poza_carta.png", UriKind.Relative);
            b.EndInit();
            pozaCarta.Source = b;
            return pozaCarta;
        }

      
        //obtener desde el array dsordenado un numero aleatorio para ser ocupado como indice
        //en los otros 3 arrays(imagen, vallor y pinta) sin repetir cartas.
        public void generarCartaBarajada() 
        {
            if (indice < desordenados.Count)
            {              
                valorRandom = (int)desordenados[indice];
                indice++;
            }   
        }
        
        //obtener la direccion de la imagen carta
        public Image rescatarCarta()
        {
            String ruta = (String)cartas[valorRandom];
            Image carta = new Image();
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri(ruta, UriKind.Relative);
            b.EndInit();
            carta.Source = b;
            return carta;           
        }

        //rescato valor de carta
        public int rescatarValor()
        {
            return (int)valores[valorRandom];
        }

        //METODO QUE RESCATA EL VALOR DE LA LETRA QUE DETERMINA LA PINTA
        public Char rescatarPinta()
        {
            String ruta = (String)cartas[valorRandom];
            return (char)ruta[ruta.Length - 5];
        }

       





        //llena un array con numeros aleatorios no repetidos
        public void barajar()
        {
            desordenados.Clear();
            int contador = 1;
            bool repetido;
            int n;
            desordenados.Add(random.Next(0, 54));

            while (contador != 54)
            {
                repetido = false;
                n = random.Next(0, 54);
                for (int i = 0; i < desordenados.Count; i++)
                {
                    if ((int)desordenados[i] == n)
                    {
                        repetido = true;
                        break;
                    }
                }
                if (!repetido)
                {
                    desordenados.Add(n);
                    contador++;
                }
            }
        } 

        //RESET iMazo Y indice;
        public void resetImazo_e_Indice()
        {
            iMazo = 26;
            indice = 0;
        }



       //comprobar valores de cartas y url
       public String ComprobarArreglo()
        {
            String url="";
            for (int i  = 0; i  <valores.Count; i ++)
            {
                url = url + (String)cartas[i] + " = " + (int)valores[i]+"\n";
            }
            return url;
        }







       public UriKind relativeUri { get; set; }
    }
}
