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
                    nuevo2.setId("Llave Cierre"); nuevo2.setColumna(columna); nuevo2.setFila(fila);
                    Tokens.Add(nuevo2);
                    concatenar = "";
                    //iterador++;
                    //columna++;

                }


                ///////////////////Tipo de Comentario 1
                if (cadena[iterador] == '/' && cadena[iterador+1] == '/') {
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




                //////  Guardo Expresiones Regulares
                if (concatenar2 == "->")
                {
                    Class_nodos nuevo3 = new Class_nodos(); nuevo3.setDato("-"); nuevo3.setId("guion");
                    nuevo3.setColumna(columna - 2); nuevo3.setFila(fila); Tokens.Add(nuevo3); concatenar = "";//Conjuntos.Add(nuevo3); 
                    Class_nodos nuevo4 = new Class_nodos(); nuevo4.setDato(">"); nuevo4.setId("mayor que");
                    nuevo4.setColumna(columna - 1); nuevo4.setFila(fila); Tokens.Add(nuevo4); concatenar = "";//Conjuntos.Add(nuevo3); 
                    for (int j = 0; j < cadena.Length; j++)
                    {
                        if (cadena[iterador] != ' ' && cadena[iterador] != ';' && cadena[iterador] != '\n') { concatenar = concatenar + cadena[iterador]; } //si es diferente a espacio concatena
                        if (cadena[iterador] == '\n') {  break; }
                        iterador++;
                       // columna++;

                    } /// se cierra el for

                            Class_nodos nuevo2 = new Class_nodos();
                            nuevo2.setDato(concatenar);
                            nuevo2.setId("ER"); nuevo2.setColumna(columna); nuevo2.setFila(fila);
                            Expresiones.Add(nuevo2);///Conjuntos.Add(nuevo2);
                            concatenar = "";

                    columna = 1;
                } //se cierra el if de expresiones



                









                    if (cadena[iterador] != ' ' && cadena[iterador] != ':' && cadena[iterador] != '\n' ) { ///si detecto estos no concatena en la var concatenar2
                    concatenar2 = concatenar2 + cadena[iterador]; }
                if (cadena[iterador] == ' ' || cadena[iterador] == ':' || cadena[iterador] == '\n') { // si detecto estos me setea concatenar2
                if (cadena[iterador] == '\n') { columna = 0; fila++; }
                 //  richTextBox2.Text = richTextBox2.Text +concatenar2+ '\n'; //Verifico que es lo que capta
                   concatenar2 = ""; }



                iterador++;
                columna++;

            } // Aca se cierra el while
      






     //   richTextBox2.Text = richTextBox2.Text + "------------- Tokens \n";

   //            foreach (Class_nodos pa in Tokens)
     //       {
     //           richTextBox2.Text = richTextBox2.Text + pa.getDato() + "   ID: " + pa.getId() +  "   Colum: " + pa.getColumna() + "   Fila: " + pa.getFila() + '\n';
     //       }

     //      richTextBox2.Text = richTextBox2.Text + "------------- Conjuntos \n";

    //        foreach (Class_nodos pa in Conjuntos)
      //      {
    //      richTextBox2.Text = richTextBox2.Text + pa.getDato() + "   ID: "+pa.getId() + "   TIPO: " + pa.getTipo() + "   Colum: " + pa.getColumna() + "   Fila: " + pa.getFila() + '\n';
    //        }




            //lista de lista de expresiones nodos para formar afn
            List<List<Class_nodos>> AFNS = new List<List<Class_nodos>>();

            //lista de nodos de afn           
            List<Class_nodos> Nodos_afn = new List<Class_nodos>();





            //////////////Lista de expresion regular
            List<string>  expresionLista= new List<string>();  /// tengo la expresion solo como una lista de string, separadas de comillas y llaves
            richTextBox2.Text = richTextBox2.Text + "------------- Expresiones \n";

            foreach (Class_nodos pa in Expresiones)
            {
                 richTextBox2.Text = richTextBox2.Text + pa.getDato() + "   ID: "+pa.getId() + "   TIPO: " + pa.getTipo() + "   Colum: " + pa.getColumna() + "   Fila: " + pa.getFila() + '\n';



                expresionLista= pa.arregloExpresiones(); //por cada iteración obtengo una lista de string de las ER

                foreach (string dar in expresionLista)   //convierto a nodos la lista de string y les coloco atributos como tipo de nodo y si son unarios o binarios
                {


                          if (dar == "*" )
                          {

                        Class_nodos n = new Class_nodos();
                        n.setDato(dar);
                        n.setId("Unario"); n.setTipoNodo("ceroVarios");
                        Nodos_afn.Add(n);

                    } else if (dar=="+") {
                        Class_nodos n = new Class_nodos();
                        n.setDato(dar);
                        n.setId("Unario"); n.setTipoNodo("unoVarios");
                        Nodos_afn.Add(n);

                    }
                          else if (dar == "?")
                          {
                        Class_nodos n = new Class_nodos();
                        n.setDato(dar);
                        n.setId("Unario"); n.setTipoNodo("ceroUno");
                        Nodos_afn.Add(n);

                    }
                          else if (dar == ".")
                          {
                        Class_nodos n = new Class_nodos();
                        n.setDato(dar);
                        n.setId("Binario"); n.setTipoNodo("concatenar");
                        Nodos_afn.Add(n);

                    }
                    else if (dar == "|")
                    {
                        Class_nodos n = new Class_nodos();
                        n.setDato(dar);
                        n.setId("Binario"); n.setTipoNodo("alter");
                        Nodos_afn.Add(n);

                    }
                    else{
                        Class_nodos n = new Class_nodos();
                        n.setDato(dar);
                        n.setId("op"); n.setTipoNodo("op");
                        Nodos_afn.Add(n);

                    }
                }


                ///meto la lista en la lista de listas del AFN

                AFNS.Add(Nodos_afn);

                Nodos_afn = new List<Class_nodos>();






            }



            
            List<List<Class_nodos>> AF = new List<List<Class_nodos>>();// aca ingreso los nuevos nodos convertidos
            List<Class_nodos> sustituto = new List<Class_nodos>();

            //Stack<string> numbers = new Stack<string>();
            Stack<Class_nodos> cons = new Stack<Class_nodos>();


            foreach (List<Class_nodos> lista in AFNS)
            { //testeo la lista generada de la expresion regular

                



                for (int i =lista.Count()-1 ; i>=0; i--)
                {

                  
                richTextBox2.Text = richTextBox2.Text + lista[i].getDato() + "     Tipo-nodo: " + lista[i].getTipoNodo() + "     Id: " + lista[i].getId() + '\n';

                    if (lista[i].getDato() == "|") ///Cuando detecto un operador alter
                    {
                        Class_nodos aux1 = new Class_nodos();
                        aux1 = cons.Pop();
                        Class_nodos aux2 = new Class_nodos();
                        aux2 = cons.Pop();
                        if (aux1.getTipoNodo() == "op" && aux2.getTipoNodo() == "op")
                        {
                            Class_nodos n = new Class_nodos(); n.setDato("afn"); n.setId("afn"); n.setTipoNodo("afn");
                            AFN sus = new AFN(); sus.alter_CC(aux1.getDato(), aux2.getDato());
                            n.setAFN(sus);
                            cons.Push(n);

                        }
                        else if (aux1.getTipoNodo() == "afn" && aux2.getTipoNodo() == "op")
                        {
                            Class_nodos n = new Class_nodos(); n.setDato("afn"); n.setId("afn"); n.setTipoNodo("afn");
                            AFN sus = new AFN(); sus.alter_afCadena(aux1.getAFN(), aux2.getDato());
                            n.setAFN(sus);
                            cons.Push(n);

                        }
                        else if (aux1.getTipoNodo() == "op" && aux2.getTipoNodo() == "afn")
                        {
                            Class_nodos n = new Class_nodos(); n.setDato("afn"); n.setId("afn"); n.setTipoNodo("afn");
                            AFN sus = new AFN(); sus.alter_afCadena(aux1.getDato(), aux2.getAFN());
                            n.setAFN(sus);
                            cons.Push(n);

                        }
                        else if (aux1.getTipoNodo() == "afn" && aux2.getTipoNodo() == "afn")
                        {
                            Class_nodos n = new Class_nodos(); n.setDato("afn"); n.setId("afn"); n.setTipoNodo("afn");
                            AFN sus = new AFN(); sus.alter_afaf(aux1.getAFN(), aux2.getAFN());
                            n.setAFN(sus);
                            cons.Push(n);

                        }



                    }
                    else if (lista[i].getDato() == ".") ///Cuando detecto un operador concatenar
                    {
                        Class_nodos aux1 = new Class_nodos();
                        aux1 = cons.Pop();
                        Class_nodos aux2 = new Class_nodos();
                        aux2 = cons.Pop();
                        if (aux1.getTipoNodo() == "op" && aux2.getTipoNodo() == "op")
                        {
                            Class_nodos n = new Class_nodos(); n.setDato("afn"); n.setId("afn"); n.setTipoNodo("afn");
                            AFN sus = new AFN(); sus.concatenar_CC(aux1.getDato(), aux2.getDato());
                            n.setAFN(sus);
                            cons.Push(n);

                        }
                        else if (aux1.getTipoNodo() == "afn" && aux2.getTipoNodo() == "op")
                        {
                            Class_nodos n = new Class_nodos(); n.setDato("afn"); n.setId("afn"); n.setTipoNodo("afn");
                            AFN sus = new AFN(); sus.concatenar_afCadena(aux1.getAFN(), aux2.getDato());
                            n.setAFN(sus);
                            cons.Push(n);

                        }
                        else if (aux1.getTipoNodo() == "op" && aux2.getTipoNodo() == "afn")
                        {
                            Class_nodos n = new Class_nodos(); n.setDato("afn"); n.setId("afn"); n.setTipoNodo("afn");
                            AFN sus = new AFN(); sus.concatenar_afCadena(aux1.getDato(), aux2.getAFN());
                            n.setAFN(sus);
                            cons.Push(n);

                        }
                        else if (aux1.getTipoNodo() == "afn" && aux2.getTipoNodo() == "afn")
                        {
                            Class_nodos n = new Class_nodos(); n.setDato("afn"); n.setId("afn"); n.setTipoNodo("afn");
                            AFN sus = new AFN(); sus.concatenar_afaf(aux1.getAFN(), aux2.getAFN());
                            n.setAFN(sus);
                            cons.Push(n);

                        }




                    } //cierro llave de concatenar

                    else if (lista[i].getDato() == "*") ///Cuando detecto un operador alter
                    {
                        Class_nodos aux = new Class_nodos();
                        aux = cons.Pop();
                        if (aux.getTipoNodo() == "op")
                        {
                            Class_nodos n = new Class_nodos(); n.setDato("afn"); n.setId("afn"); n.setTipoNodo("afn");
                            AFN sus = new AFN(); sus.kleene_Cadena(aux.getDato());
                            n.setAFN(sus);
                            cons.Push(n);

                        }
                        else if (aux.getTipoNodo() == "afn")
                        {
                            Class_nodos n = new Class_nodos(); n.setDato("afn"); n.setId("afn"); n.setTipoNodo("afn");
                            AFN sus = new AFN(); sus.kleene_af(aux.getAFN());
                            n.setAFN(sus);
                            cons.Push(n);

                        }





                    }
                    else if (lista[i].getDato() == "+") ///Cuando detecto un operador unoVarios
                    {
                        Class_nodos aux = new Class_nodos();
                        aux = cons.Pop();

                        if (aux.getTipoNodo() == "op")
                        {
                            Class_nodos n = new Class_nodos(); n.setDato("afn"); n.setId("afn"); n.setTipoNodo("afn");
                            AFN sus = new AFN(); sus.positiva_Cadena(aux.getDato());
                            n.setAFN(sus);
                            cons.Push(n);

                        }
                        else if (aux.getTipoNodo() == "afn")
                        {
                            Class_nodos n = new Class_nodos(); n.setDato("afn"); n.setId("afn"); n.setTipoNodo("afn");
                            AFN sus = new AFN(); sus.positiva_af(aux.getAFN());
                            n.setAFN(sus);
                            cons.Push(n);

                        }



                    }
                    else if (lista[i].getDato() == "?") ///Cuando detecto un operador unoVarios
                    {
                        Class_nodos aux = new Class_nodos();
                        aux = cons.Pop();

                        if (aux.getTipoNodo() == "op")
                        {
                            Class_nodos n = new Class_nodos(); n.setDato("afn"); n.setId("afn"); n.setTipoNodo("afn");
                            AFN sus = new AFN(); sus.ceroUno_C(aux.getDato());
                            n.setAFN(sus);
                            cons.Push(n);

                        }
                        else if (aux.getTipoNodo() == "afn")
                        {
                            Class_nodos n = new Class_nodos(); n.setDato("afn"); n.setId("afn"); n.setTipoNodo("afn");
                            AFN sus = new AFN(); sus.ceroUno_af(aux.getAFN());
                            n.setAFN(sus);
                            cons.Push(n);

                        }


                    } //cierro unoVarios 



                    //cierro if de alter
                    else { cons.Push(lista[i]); }

                 


                }

                if(cons.LongCount()!=0)
                sustituto.Add(cons.Peek());
                AF.Add(sustituto);
                sustituto = new List<Class_nodos>();

            
            }









            foreach (List<Class_nodos> lista in AF)
            { //testeo la lista generada de la expresion regular
                richTextBox3.Text = richTextBox3.Text +" lista numero: "+lista.Count()+ " -----------------------------------------------\n";
                List<Class_nodos> comodin = lista;
                for (int i = 0; i < comodin.Count(); i++)
                {

                    if (comodin[i].getDato() != " ")
                        richTextBox3.Text = richTextBox3.Text + comodin[i].getDato() + "     Tipo-nodo: " + comodin[i].getTipoNodo() + "     Id: " + comodin[i].getId() + '\n';
                    richTextBox3.Text = richTextBox3.Text + comodin[i].getAFN().generarTxt();



                }
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


