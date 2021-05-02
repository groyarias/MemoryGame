using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GridTutorial
{
    public partial class MainPage : ContentPage
    {
        private const int FILAS = 5;
        private const int COLUMNAS = 5;

        private List<string> letras;
        private string[][] matrizLetras;
        private Button primerSeleccion;
        private Button segundaSeleccion;
        private Button terceraSeleccion;
        private Color defaultColor = Color.FromHex("#646464");
        int x1, y1, x2, y2;

        public MainPage()
        {
            InitializeComponent();
            InicializarListaLetras();
            GenerarMatrizDeLetras();
        }

        private void InicializarListaLetras()
        {
            letras = new List<string>() { 
                "A","B","C","D","E","F","G","H","I","J","K","L","M"
                //,"N","O","P","Q","R","S","T","U","V","W","X","Y","Z"
            };
        }

        private void GenerarMatrizDeLetras() {
            Random random = new Random();
            int randomNumber;
            matrizLetras = new string[FILAS][];
            
            for (int i = 0; i < FILAS; i++)
            {
                matrizLetras[i] = new string[COLUMNAS];
            }
            for (int i = 0; i < FILAS; i++)
            {
                for (int j = 0; j < COLUMNAS; j++)
                {
                    randomNumber = random.Next(0, letras.Count);
                    matrizLetras[i][j] = letras[randomNumber];
                }
            }
        }

        async void OnImprimirClicked(object sender, EventArgs args) {
            for (int i = 0; i < FILAS; i++)
            {
                for (int j = 0; j < COLUMNAS; j++)
                {
                    Console.Write("[" + matrizLetras[i][j] + "]");
                    
                }
                Console.WriteLine();
            }
        }

        async void OnButtonClicked(object sender, EventArgs args)
        {
            var btn = sender as Button;
            btn.BackgroundColor = Color.FromHex("FFC300");

            if (primerSeleccion is null)
            {
                if (!(terceraSeleccion is null))
                {
                    primerSeleccion = terceraSeleccion;
                    terceraSeleccion = null;
                }
                else
                {
                    primerSeleccion = btn;
                }
                //Se otienen los indices
                string[] primeraPos = primerSeleccion.ClassId.Split('/');
                x1 = Int16.Parse(primeraPos[0]);
                y1 = Int16.Parse(primeraPos[1]);
                primerSeleccion.Text = matrizLetras[x1][y1];//Se muestra el contenido

            }
            else if (segundaSeleccion is null)
            {
                segundaSeleccion = btn;
                //Se otienen los indices
                string[] segundaPos = segundaSeleccion.ClassId.Split('/');
                x2 = Int16.Parse(segundaPos[0]);
                y2 = Int16.Parse(segundaPos[1]);
                segundaSeleccion.Text = matrizLetras[x2][y2];//Se muestra el contenido

                if ((matrizLetras[x1][y1] == matrizLetras[x2][y2]) && !(x1 == x2 && y1 == y2))
                {//Ambos elementos son iguales y no es el mismo botón
                    primerSeleccion.TextColor = Color.Black;
                    segundaSeleccion.TextColor = Color.Black;
                    primerSeleccion.BackgroundColor = Color.White;
                    segundaSeleccion.BackgroundColor = Color.White;
                }
                else
                {

                    //Mostrar mensaje con la posición en la matriz de botones
                    await DisplayAlert("Incorrecto", String.Format("{0} != {1}", matrizLetras[x1][y1], matrizLetras[x2][y2]), "Aceptar");
                    //Se eliminar el texto de los botones
                    primerSeleccion.Text = "";
                    segundaSeleccion.Text = "";
                    //Se retornan los botones seleccionados al color por defecto
                    primerSeleccion.BackgroundColor = defaultColor;
                    segundaSeleccion.BackgroundColor = defaultColor;
                }
                //Se reinician las variables
                primerSeleccion = null;
                segundaSeleccion = null;


            }
            else if (terceraSeleccion is null)
            {
                terceraSeleccion = btn;
                //Se otienen los indices
                string[] primeraPos = terceraSeleccion.ClassId.Split('/');
                x1 = Int16.Parse(primeraPos[0]);
                y1 = Int16.Parse(primeraPos[1]);
                terceraSeleccion.Text = matrizLetras[x1][y1];//Se muestra el contenido
                //Se reinician las variables
                primerSeleccion = null;
                segundaSeleccion = null;
            }

        }

    }
}
