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




        //esto es para los botones de las imagenes
        List<string> nombre_archivos = new List<string>();
        int j = 0;


        /// <summary>
        /// Tabla epsido para analizar
        /// </summary>
        List<List<Class_alfabeto>> Tabla_epsido_aux = new List<List<Class_alfabeto>>();



        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen(); //centra nuestra pantalla no importando el tamaño
            
        }
    

        private  List<string> obtengo_estados_epsido(string nombre_nodo, string caracter)
        {  List<string> n = new List<string>();
            foreach (List<Class_alfabeto> lista in Tabla_epsido_aux)
            {  //recorro las listas               
                foreach (Class_alfabeto nodo in lista)
                {    //recorro los nodos                  
                    if (nodo.getnumeroNodo() == nombre_nodo) { 
                    foreach (Class_transiciones p in nodo.getIntervalo())
                    {  // recorro las transiciones


                            //richTextBox3.Text = richTextBox3.Text +p.getNombre()+ p.getDireccion() + "\n";
                            if (p.getNombre() == caracter)                            {
                               // richTextBox3.Text = richTextBox3.Text +p.getDireccion()+ "\n";
                                n.Add(p.getDireccion());//lleno la lista de transiciones solo con el caracter dado
                            }                    }
                    }///cierro el if
                }




            }
            return n;
        }
        private List<string> sucesores_epsido(string nombre_nodo, string caracter)
        {   List<string> visitados = new List<string>();
            List<string> n = new List<string>();
            Stack<string> cons = new Stack<string>();
            n=obtengo_estados_epsido(nombre_nodo,caracter);
            if (n.Count() > 0) { 
            for(int i = 0; i < n.Count(); i++) {
                cons.Push(n[i]);
                visitados.Add(n[i]); //// guardo donde ya paso
            }
            n = new List<string>();
}
            while (cons.Count() > 0)
            {
                n=obtengo_estados_epsido(cons.Pop(),caracter);                
                if (n.Count() > 0) { 
                for (int i = 0; i < n.Count(); i++)
                {
                    cons.Push(n[i]);
                    visitados.Add(n[i]); //// guardo donde ya paso
                }
                }
                n = new List<string>();          
            }        
            return visitados;
        }













        private static void GenerateGraph(string fileName, string path)
        {// se manda a llamar asi GenerateGraph("Grafica.txt", "C:\\Users\\ADMIN\\Desktop");
            try
            {
                var command = string.Format("dot -Tjpg {0} -o {1}", Path.Combine(path, fileName), Path.Combine(path, fileName.Replace(".txt", ".jpg")));
                var procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/C " + command);
                var proc = new System.Diagnostics.Process();
                //probar si este no desplega la ventana de cmd
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo = procStartInfo;
                proc.Start();
                proc.WaitForExit();
            }
            catch
            { MessageBox.Show("error");
            }
        }
        private static void archivoTxt(string nombre, string cadena_ghrapviz)
        { using (StreamWriter outputFile = new StreamWriter( nombre + ".txt")) {
                outputFile.WriteLine(cadena_ghrapviz);
                };  }




        /// 
        /// Procedimiento para encontrar la Tabla de AFD
        /// 




        private List<string> genero_AFD(string nombre_nodo, List<string> intervalo2, List<string> alfabeto)
        {
            List<string> existentes = new List<string>();
            List<List<Class_Clausura>> total = new List<List<Class_Clausura>>();
            List<Class_Clausura> fila = new List<Class_Clausura>();
            Stack<string> cons = new Stack<string>();

            Class_Clausura inicial = new Class_Clausura();          
            inicial.addSub1(nombre_nodo); inicial.addSub2(intervalo2);
            fila.Add(inicial); //ingreso el primer Nodo que recibo


            Class_Clausura nodo = new Class_Clausura();
            ///genero primer intervalo del primer caracter
            foreach (string caracter in alfabeto) {
                nodo = new Class_Clausura();
                foreach (string numero in intervalo2) { 
                   List<string> aux= obtengo_estados_epsido(numero, caracter);
                    if (aux.Count() > 0) { for (int a = 0; a < aux.Count(); a++) { nodo.addSub1(aux[a]) ; } } ///si la lista obtenida es mas de uno los ingreso al primer intervalo



            }
         
        /// ingreso el nodo a la lista de fila
        }








            List<string> visitados = new List<string>();
            List<string> n = new List<string>();
            
            n = obtengo_estados_epsido(nombre_nodo, caracter);
            if (n.Count() > 0)
            {
                for (int i = 0; i < n.Count(); i++)
                {
                    cons.Push(n[i]);
                    visitados.Add(n[i]); //// guardo donde ya paso
                }
                n = new List<string>();
            }
            while (cons.Count() > 0)
            {
                n = obtengo_estados_epsido(cons.Pop(), caracter);
                if (n.Count() > 0)
                {
                    for (int i = 0; i < n.Count(); i++)
                    {
                        cons.Push(n[i]);
                        visitados.Add(n[i]); //// guardo donde ya paso
                    }
                }
                n = new List<string>();
            }
            return visitados;



        }














        private void Analizador()
        {
            char[] cadena = richTextBox1.Text.ToCharArray();
            string concat = "";
            int iterador = 0;


            ////////////////////////////////////////////// Esto es para setear las variables al analizar otro archivo
            j = 0;
            pictureBox1.Image = null;
            nombre_archivos = new List<string>();

            contador_estado = 0;
            Tokens = new List<Class_nodos>();
            concatenar = "";
            concatenar2 = "";
            comodin = "";

            columna = 1;
            fila = 1;

            Conjuntos = new List<Class_nodos>();
            Expresiones = new List<Class_nodos>();
             Lexemas = new List<Class_nodos>();
             Comentarios = new List<Class_nodos>();

            Tabla_epsido_aux = new List<List<Class_alfabeto>>();
            ////////////////////////////////////////////// termina seteo

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
                        if (cadena[iterador] == '>' && cadena[iterador-1] == '!')
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

                        if (cadena[iterador] == ' '  && cadena[iterador -1] == '"' && cadena[iterador+ 1] == '"' && cadena[iterador +2] == ' ') { concatenar = concatenar + cadena[iterador]; }
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




                // richTextBox2.Text = richTextBox2.Text + "------------- Comentarios \n";

                //        foreach (Class_nodos pa in Comentarios)
                //   {
                //       richTextBox2.Text = richTextBox2.Text + pa.getDato() + "   ID: " + pa.getId() + '\n';
                // }


             //richTextBox2.Text = richTextBox2.Text + "------------- Tokens \n";

            //           foreach (Class_nodos pa in Tokens)
              //     {
               //      richTextBox2.Text = richTextBox2.Text + pa.getDato() + "   ID: " + pa.getId() +  "   Colum: " + pa.getColumna() + "   Fila: " + pa.getFila() + '\n';
                //  }

    //         richTextBox2.Text = richTextBox2.Text + "------------- Conjuntos \n";

       //        foreach (Class_nodos pa in Conjuntos)
        //      {
          //     richTextBox2.Text = richTextBox2.Text + pa.getDato() + "   ID: "+pa.getId() + "   TIPO: " + pa.getTipo() + "   Colum: " + pa.getColumna() + "   Fila: " + pa.getFila() + '\n';
            //      }




            // lista de  nodos para formar afn, aca esta toda la expresion regular dada por Nodos clasificados segun su tipo
            List<List<Class_nodos>> AFNS = new List<List<Class_nodos>>();

            richTextBox2.Text = richTextBox2.Text + "------------- Expresiones \n";

            foreach (Class_nodos pa in Expresiones)
            {
                 richTextBox2.Text = richTextBox2.Text + pa.getDato() + "   ID: "+pa.getId() + "   TIPO: " + pa.getTipo() + "   Colum: " + pa.getColumna() + "   Fila: " + pa.getFila() + '\n';

                 AFNS.Add(pa.NodosExpresiones()); // metodo me devuelve una list<Class_nodos>, donde ya viene la expresion regular por nodos y clasificada
            }



            
            List<List<Class_nodos>> AF = new List<List<Class_nodos>>();// aca ingreso los nuevos nodos convertidos y queda el ultimo AFND final
            List<Class_nodos> sustituto = new List<Class_nodos>();            
            Stack<Class_nodos> cons = new Stack<Class_nodos>();//Pila donde voy ingresando los datos y AFN cuando voy generando el AFN total

            List<string> caracteres_alfabeto = new List<string>();
            
            foreach (List<Class_nodos> lista in AFNS)  //En este for each recorro la lista de nodos de derecha a izquierda y voy ingresando en la pila cons
            { 
                for (int i =lista.Count()-1 ; i>=0; i--)
                {                  
               // richTextBox3.Text = richTextBox3.Text + lista[i].getDato() + "     Tipo-nodo: " + lista[i].getTipoNodo() + "     Id: " + lista[i].getId() + '\n';

                    if (lista[i].getDato() == "|" && lista[i].getTipoNodo()=="alter") ///Cuando detecto un operador alter
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
                    else if (lista[i].getDato() == "." && lista[i].getTipoNodo()=="concatenar") ///Cuando detecto un operador concatenar
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

                    else if (lista[i].getDato() == "*" && lista[i].getTipoNodo() == "ceroVarios") ///Cuando detecto un operador ceroVarios
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
                    else if (lista[i].getDato() == "+" && lista[i].getTipoNodo() == "unoVarios") ///Cuando detecto un operador unoVarios
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
                    else if (lista[i].getDato() == "?" && lista[i].getTipoNodo() == "ceroUno") ///Cuando detecto un operador ceroUno
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
                    else {
                        caracteres_alfabeto.Add(lista[i].getDato());
                        cons.Push(lista[i]); } // cuando no es ninguno de los operandos ingreso solamente a la pila

                 


                }

                if (cons.LongCount() != 0) {
                Class_nodos aux = cons.Peek();
                aux.setAlfabeto(caracteres_alfabeto);  /// ingreso la lista de caracteres en cada AFN resultante
                sustituto.Add(aux); //ingreso en sustituto lo ultimo que quedo en la pila cons
                }
                AF.Add(sustituto); // luego ingreso la lista a  AF que es una lista de lista de nodos , don de en la primera posicion queda el AFN
                sustituto = new List<Class_nodos>(); // seteo sustituto para seguir con la siguiente lista de nodos a trasformar
                caracteres_alfabeto = new List<string>();
            
            }






            List<List<Class_alfabeto>> Tabla_epsido = new List<List<Class_alfabeto>>();
           
            int cont = 0;


            foreach (List<Class_nodos> lista in AF)
            { //testeo la lista generada de la expresion regular
                richTextBox3.Text = richTextBox3.Text + " lista numero: " + lista.Count() + " -----------------------------------------------\n";
                lista[0].quitarRepitencia_alfabeto();//quito repitencia en elfabeto
                List<string> alf = lista[0].getAlfabeto();
                if (alf.Count() > 0) { // Aca imprimo el Alfabeto 
                for (int k = 0; k <alf.Count();k++) {
                        richTextBox3.Text = richTextBox3.Text + alf[k]+" ---- ";

                }
                    richTextBox3.Text = richTextBox3.Text + "\n ";
                }
                


                List<Class_nodos> comodin = lista;
                for (int i = 0; i < comodin.Count(); i++)
                {
                    cont++;
                    if (comodin[i].getDato() != " ") ///muestra los datos del ultimo afn que se quedo al recorrer alla arriba toda la ER
                    richTextBox3.Text = richTextBox3.Text + comodin[i].getDato() + "     Tipo-nodo: " + comodin[i].getTipoNodo() + "     Id: " + comodin[i].getId() + '\n';
                    //richTextBox3.Text = richTextBox3.Text + comodin[i].getAFN().generarTxt(); /// aca obtengo el String completo de Ghrapviz 
                    archivoTxt(cont.ToString(), comodin[i].getAFN().generarTxt()); //con este metodo creo archivos.txt
                    nombre_archivos.Add(cont.ToString()); //guardo los nombres de archivos creados

                    GenerateGraph(cont.ToString()+".txt", "");  //con esto genero los png de las expresiones


                    Tabla_epsido.Add(comodin[i].getAFN().tablaEpsido()); // local
                  Tabla_epsido_aux = Tabla_epsido;// Global Tabla_epsido_aux


                }
            }



            List<string> prueba = new List<string>();
            
            foreach (List<Class_alfabeto> lista in Tabla_epsido)
            {  //recorro las listas
                richTextBox2.Text = richTextBox2.Text + " Nodos de Tompson ---------------------------- \n";
                foreach (Class_alfabeto nodo in lista) {    //recorro los nodos
                    richTextBox2.Text = richTextBox2.Text + "Numero de nodo " + nodo.getnumeroNodo()+"\n";



                       prueba = sucesores_epsido(nodo.getnumeroNodo(), "£");
                        nodo.setIntervalo_clausura(prueba); //guardo en cada nodo su clausura con £



                    richTextBox2.Text = richTextBox2.Text + "alfabeto de cada nodo  \n";
                 List < string> mux= nodo.getAlfabeto();
                    for (int k = 0; k < mux.Count(); k++) {
                        richTextBox2.Text = richTextBox2.Text + mux[k] + "  --  ";
                    }
                    richTextBox2.Text = richTextBox2.Text + " \n";




                    if (prueba.Count() > 0)
                        {
                          for(int i=0;i<prueba.Count();i++)
                           richTextBox2.Text = richTextBox2.Text + prueba[i] +"\n";


                         prueba = new List<string>();
                        }
                    }

                }    Tabla_epsido_aux = Tabla_epsido; //guardo los cambios en la otra tabla
                 

             

















        }//cierro metodo analizar
                













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





        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            //this.pictureBox1.Size = new System.Drawing.Size(140, 140); // le da el tamaño al label
            //this.pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;  // Establece SizeMode para centrar la imagen.
           // this.pictureBox1.BorderStyle = BorderStyle.Fixed3D;// Set the border style to a three-dimensional border.
           // pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage; // hace que la imagen se ajuste en el label donde se muestra
           // pictureBox1.Image = Image.FromFile("C:\\Users\\ADMIN\\Desktop\\automata.png");
        }

        private void generarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Analizador();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void limpiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            richTextBox3.Text = "";
            j = 0;
            pictureBox1.Image = null;
            nombre_archivos = new List<string>();


            contador_estado = 0;
            Tokens = new List<Class_nodos>();
            concatenar = "";
            concatenar2 = "";
            comodin = "";

            columna = 1;
            fila = 1;


            Conjuntos = new List<Class_nodos>();
            Expresiones = new List<Class_nodos>();
            Lexemas = new List<Class_nodos>();
            Comentarios = new List<Class_nodos>();

            Tabla_epsido_aux = new List<List<Class_alfabeto>>();


        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (nombre_archivos.Count() > 0) { 
                       j++;
            if (j > nombre_archivos.Count()) {
                j = 1;
            }
            //this.pictureBox1.Size = new System.Drawing.Size(140, 140); // le da el tamaño al label
            this.pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;  // Establece SizeMode para centrar la imagen.
            this.pictureBox1.BorderStyle = BorderStyle.Fixed3D;// Set the border style to a three-dimensional border.
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage; // hace que la imagen se ajuste en el label donde se muestra
            pictureBox1.Image = Image.FromFile(j+".jpg");
            }






        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (nombre_archivos.Count() > 0)
            {
                j--;
            if (j < 1)
            {
                j = nombre_archivos.Count();
            }
            //this.pictureBox1.Size = new System.Drawing.Size(140, 140); // le da el tamaño al label
            this.pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;  // Establece SizeMode para centrar la imagen.
            this.pictureBox1.BorderStyle = BorderStyle.Fixed3D;// Set the border style to a three-dimensional border.
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage; // hace que la imagen se ajuste en el label donde se muestra
            pictureBox1.Image = Image.FromFile(j + ".jpg");
            }



        }
    }
}


