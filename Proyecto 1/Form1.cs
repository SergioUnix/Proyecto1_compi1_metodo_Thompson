using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Proyecto_1
{
    public partial class Form1 : Form
    {

        int contador_estado = 0;
        List<Class_nodos> Tokens = new List<Class_nodos>();
        string concatenar = "";
        string concatenar2 = "";
        string comodin = "";

        int columna = 1;
        int fila = 1;

        List<Class_nodos> Conjuntos = new List<Class_nodos>();
        List<Class_nodos> Expresiones = new List<Class_nodos>();
        List<Class_nodos> Lexemas = new List<Class_nodos>();
        List<Class_nodos> Comentarios = new List<Class_nodos>();






        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen(); //centra nuestra pantalla no importando el tamaño
            
        }




        private void Analizador()
        {
            char[] cadena = richTextBox1.Text.ToCharArray();
            string concat = "";
            int iterador = 0;



            while (iterador < cadena.Length)
            {
                ///////////////////  Tipo de Caracter llave
                if (cadena[iterador] == '{')
                {
                    concatenar = "" + cadena[iterador];
                    Class_nodos nuevo2 = new Class_nodos();
                    nuevo2.setDato(concatenar);
                    nuevo2.setId("Llave Apertura"); nuevo2.setColumna(columna); nuevo2.setFila(fila);
                    Tokens.Add(nuevo2);
                    concatenar = "";

                    //iterador++;
                    //columna++;
                } if (cadena[iterador] == '}')
                {
                    
                    concatenar = "" + cadena[iterador];
                    Class_nodos nuevo2 = new Class_nodos();
                    nuevo2.setDato(concatenar);
                    nuevo2.setId("Llave Cierre"); nuevo2.setColumna(columna-concatenar.Length); nuevo2.setFila(fila);
                    Tokens.Add(nuevo2);
                    concatenar = "";
                    //iterador++;
                    //columna++;

                }


                ///////////////////Tipo de Comentario 1
                if (cadena[iterador] == '/') {
                    //label4.Text = "detecto diagonal";
                    for (int j = 0; j < cadena.Length; j++) {
                       
                        if (cadena[iterador] == '\n') {
                            columna = 1; fila++;
                            break;
                        }
                        concatenar = concatenar + cadena[iterador];

                        iterador++;
                        columna++;
                    }
                    //ahora que concateno todo los comentarios los ingreso a la lista de comentarios
                    Class_nodos nuevo = new Class_nodos();
                    nuevo.setDato(concatenar);
                    nuevo.setId("Comentario");
                    Comentarios.Add(nuevo); //Tokens.Add(nuevo);
                    concatenar = "";
                    iterador++;
                }

                /////////////////Tipo de Comentario 2 <!  !>
                if (cadena[iterador] == '<')
                {
                    for (int j = 0; j < cadena.Length; j++)
                    {
                        if (cadena[iterador] == '\n') { columna = 1; fila++; }         //////////////
                        concatenar = concatenar + cadena[iterador];
                        if (cadena[iterador] == '>')
                        { break; } iterador++;
                    }
                    //ahora que concateno todo los comentarios los ingreso a la lista de comentarios
                    Class_nodos nuevo = new Class_nodos();
                    nuevo.setDato(concatenar);
                    nuevo.setId("Comentario");
                    Comentarios.Add(nuevo);// Tokens.Add(nuevo);
                    concatenar = "";
                    iterador++;
                }






                //////////////    Aca captamos los CONJ 
                if (concatenar2 == "CONJ")
                {
                    columna = 1;
                    // label4.Text = "detecto CONJ";
                    Class_nodos nuevo = new Class_nodos();
                    nuevo.setDato(concatenar2);
                    nuevo.setId("CONJ"); nuevo.setColumna(columna); nuevo.setFila(fila);
                    Tokens.Add(nuevo); //Conjuntos.Add(nuevo); 
                    columna = 5;
                    concatenar = "";
                    for (int j = 0; j < cadena.Length; j++)
                    {
                        if (cadena[iterador] != ' ' && cadena[iterador] != '-' && cadena[iterador] != ';') { concatenar = concatenar + cadena[iterador]; } //si es diferente a espacio concatena

                        if (concatenar == ":")
                        {
                            Class_nodos nuevo2 = new Class_nodos();
                            nuevo2.setDato(concatenar);
                            nuevo2.setId("Dos Puntos"); nuevo2.setColumna(columna); nuevo2.setFila(fila);
                            Tokens.Add(nuevo2);///Conjuntos.Add(nuevo2);
                            concatenar = "";
                        }

                        if (cadena[iterador] == '-')
                        {
                            Class_nodos nuevo3 = new Class_nodos();
                            nuevo3.setDato(concatenar); comodin = concatenar;
                            nuevo3.setId("Nom_conjunto"); nuevo3.setColumna(columna - concatenar.Length-1); nuevo3.setFila(fila);
                            Tokens.Add(nuevo3); concatenar = "";//Conjuntos.Add(nuevo3); 
                            concatenar = concatenar + cadena[iterador];
                            Class_nodos nuevo4 = new Class_nodos();
                            nuevo4.setDato(concatenar);
                            nuevo4.setId("guion");nuevo4.setColumna(columna); nuevo4.setFila(fila);
                            Tokens.Add(nuevo4); //Conjuntos.Add(nuevo3);
                            concatenar = "";
                        }
                        if (concatenar == ">")
                        {
                            Class_nodos nuevo2 = new Class_nodos();
                            nuevo2.setDato(concatenar);
                            nuevo2.setId("Guion_Mayor");nuevo2.setColumna(columna); nuevo2.setFila(fila);
                            Tokens.Add(nuevo2);///Conjuntos.Add(nuevo2);
                            concatenar = "";
                        }
                        if (cadena[iterador] == ';')
                        {
                            label4.Text = "detecto comillas";
                            Class_nodos nuevo2 = new Class_nodos();
                            nuevo2.setDato(concatenar); nuevo2.setColumna(columna - concatenar.Length); nuevo2.setFila(fila);
                            nuevo2.setId("CONJ"); nuevo2.setTipo(comodin); comodin = "";
                            Tokens.Add(nuevo2); Conjuntos.Add(nuevo2); nuevo2.setFila(fila);
                            concatenar = "";
                            concatenar = concatenar + cadena[iterador];
                            Class_nodos nuevo5 = new Class_nodos();
                            nuevo5.setDato(concatenar);nuevo5.setColumna(columna); nuevo5.setFila(fila);
                            nuevo5.setId("Punto y Coma");
                            Tokens.Add(nuevo5);
                            concatenar = "";

                        }

                        if (cadena[iterador] == '\n'){ break; }

                        iterador++;
                        columna++;
                    } //cierro for dentro del if de CONJ



                } //termina el if de CONJ

















                if (cadena[iterador] != ' ' && cadena[iterador] != ':' && cadena[iterador] != '\n') { ///si detecto estos no concatena en la var concatenar2
                    concatenar2 = concatenar2 + cadena[iterador]; }
                if (cadena[iterador] == ' ' || cadena[iterador] == ':' || cadena[iterador] == '\n') { // si detecto estos me setea concatenar2
                if (cadena[iterador] == '\n') { columna = 1; fila++; }
                   // richTextBox2.Text = richTextBox2.Text +concatenar2+ '\n'; //Verifico que es lo que capta
                   concatenar2 = ""; }



                iterador++;
                columna++;

            } // Aca se cierra el while
        

           richTextBox2.Text = richTextBox2.Text + "---------- Comentarios \n";

            foreach (Class_nodos pa in Comentarios)
            {
                richTextBox2.Text = richTextBox2.Text + pa.getDato() +'\n';
             }

            richTextBox2.Text = richTextBox2.Text + "------------- Tokens \n";

            foreach (Class_nodos pa in Tokens)
            {
                richTextBox2.Text = richTextBox2.Text + pa.getDato() + "   ID: " + pa.getId() +  "   Colum: " + pa.getColumna() + "   Fila: " + pa.getFila() + '\n';
            }

            richTextBox2.Text = richTextBox2.Text + "------------- Conjuntos \n";

            foreach (Class_nodos pa in Conjuntos)
            {
                richTextBox2.Text = richTextBox2.Text + pa.getDato() + "   ID: "+pa.getId() + "   TIPO: " + pa.getTipo() + "   Colum: " + pa.getColumna() + "   Fila: " + pa.getFila() + '\n';
            }










        }













        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        SaveFileDialog guardar = new SaveFileDialog();/// necesario para guardar documento


        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamWriter escribir = new StreamWriter(guardar.FileName);
            foreach (object line in richTextBox1.Lines)
            {
                escribir.WriteLine(line);

            }
            if (MessageBox.Show("Datos Guardados", "Nota", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {

            }
            escribir.Close();
        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            guardar.Filter = "Documento de Lenguaje|*.er";
            guardar.Title = "Guardar RichTexBox";
            guardar.FileName = "Sin titulo 1.er";
            var resultado = guardar.ShowDialog();
            if (resultado == DialogResult.OK)
            {
                StreamWriter escribir = new StreamWriter(guardar.FileName);
                foreach (object line in richTextBox1.Lines)
                {
                    escribir.WriteLine(line);

                }
                escribir.Close();

            }
    }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();
            abrir.Filter = "Documento|*.er";
            abrir.Title = "AbrirRichTextBox";
            abrir.FileName = "sin titulo 1";
            var resultado = abrir.ShowDialog();
            if (resultado == DialogResult.OK)
            {
                guardar.Filter = abrir.Filter;
                guardar.Title = abrir.Title;
                guardar.FileName = abrir.FileName;

                StreamReader leer = new StreamReader(abrir.FileName);
                richTextBox1.Text = leer.ReadToEnd();
                leer.Close();

            }
        }



        private static void GenerateGraph(string fileName, string path)
        {

            // se manda a llamar asi GenerateGraph("Grafica.txt", "C:\\Users\\ADMIN\\Desktop");
            try
            {
                var command = string.Format("dot -Tjpg {0} -o {1}", Path.Combine(path, fileName), Path.Combine(path, fileName.Replace(".txt", ".jpg")));
                var procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/C " + command);
                var proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                proc.WaitForExit();
            }
            catch 
            {
                MessageBox.Show("error");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            //this.pictureBox1.Size = new System.Drawing.Size(140, 140); // le da el tamaño al label
            this.pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;  // Establece SizeMode para centrar la imagen.
            this.pictureBox1.BorderStyle = BorderStyle.Fixed3D;// Set the border style to a three-dimensional border.
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage; // hace que la imagen se ajuste en el label donde se muestra
            pictureBox1.Image = Image.FromFile("C:\\Users\\ADMIN\\Desktop\\automata.png");
        }

        private void generarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Analizador();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}


