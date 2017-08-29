using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConvertir_Click(object sender, EventArgs e)
        {
            // elipsoide de Hayford
            int a = 6378388; //semieje mayor
            double b = 6356911.94613; //semieje menor


            //definiendo variables
            double c, e1, e2, e22, GradLong, GradLat, RadLong, RadLat, huso, husoMer;
            double delamb, A, xi, eta, ni, zeta, A1, A2, j2, j4, j6;
            double alfa, beta, gamma, resultado;



            e1 = Math.Sqrt(Math.Pow(a, 2) - Math.Pow(b, 2)) / a; //Primera excentricidad
            e2 = Math.Sqrt(Math.Pow(a, 2) - Math.Pow(b, 2)) / b; //Segunda excentricidad
            e22 = Math.Pow(e2, 2);


            c = Math.Pow(a, 2) / b;

            try
            {
                //convirtiendo la Longitud y Latitud de grados sexagesimales a grados Decimales
                GradLong = Convert.ToDouble(txtGradosLong.Text) + ((Convert.ToDouble(txtMinLong.Text)) / 60) + ((Convert.ToDouble(txtSegLong.Text)) / 60) / 60;
                GradLat = Convert.ToDouble(txtGradosLat.Text) + ((Convert.ToDouble(txtMinLat.Text)) / 60) + ((Convert.ToDouble(txtSegLat.Text)) / 60) / 60;

                if (cbLong.Text.Equals("Oeste"))
                {
                    GradLong = GradLong * (-1); ///Si la Longitud es de Oeste multiplicar por (-1) 
                }
                if (cbLat.Text.Equals("Sur")) 
                {
                    GradLat = GradLat * (-1);//Si la Llatitud es de Sur multiplicar por (-1)
                }

                //convirtiendo la Longitud y Latitud de Decimales a Radianes
                RadLong = GradLong * Math.PI / 180;
                RadLat = GradLat * Math.PI / 180;

                //Calculando el Huso
                huso = Math.Truncate((GradLong / 6) + 31);
                husoMer = huso * 6 - 183;

                delamb = RadLong - ((husoMer * Math.PI) / 180);

                A = Math.Cos(RadLat) * Math.Sin(delamb);

                xi = 0.5 * Math.Log((1 + A) / (1 - A));

                eta = Math.Atan(Math.Tan(RadLat) / Math.Cos(delamb)) - RadLat;

                ni = (c / Math.Pow((1 + e22 * Math.Pow(Math.Cos(RadLat), 2)), (0.5))) * 0.9996;

                zeta = (e22 / 2) * Math.Pow(xi, 2) * Math.Pow(Math.Cos(RadLat), 2);

                A1 = Math.Sin(RadLat * 2);

                A2 = A1 * Math.Pow(Math.Cos(RadLat), 2);

                j2 = RadLat + (A1 / 2);

                j4 = ((3 * j2) + A2) / 4;

                j6 = (5 * j4 + A2 * (Math.Pow(Math.Cos(RadLat), 2))) / 3;

                alfa = (3 / 4) * e22;
                beta = (5 / 3) * Math.Pow(alfa, 2);
                gamma = (35 / 27) * Math.Pow(alfa, 3);



                resultado = xi * ni * (1 + zeta / 3) + 500000;


                txtResultado.Text = resultado.ToString();
                        
            }
            catch (Exception )
            {
                MessageBox.Show("Verifique que los datos esten correctos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
    }
}
