﻿using System;
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
        List<string> g_tabla = new List<string>();
        List<string> t_tabla = new List<string>();



        int j = 0;
        int g = 0;
        int t = 0;

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

        private List<string> union_lista_sucesores(List<string> intervalo_de_nodos)   ///este metodo devuelve la  union de sucesores de un intervalo ejemplo {3,8}  y hace {1,2,3,4,5,6,7,8} sin repetir
        {

            List<string> n = new List<string>();
            foreach (List<Class_alfabeto> lista in Tabla_epsido_aux)
            {  //recorro las listas               
                foreach (Class_alfabeto nodo in lista)
                {    //recorro los nodos              
                    
                    
                    for(int k = 0; k < intervalo_de_nodos.Count(); k++) {
                        if (nodo.getnumeroNodo() == intervalo_de_nodos[k])
                        {

                            List<string> hel = nodo.getIntervalo_clausura();
                            if (hel.Count() > 0){ for (int j = 0; j < hel.Count(); j++) { n.Add(hel[j]);    } } //cuando obtengo el intervalo clausura lo ingreso a n, valores puede ir repetidos

                      }///cierro el if
                    }  //ciero For
                } //cierro for each 2




            } //cierro for each 1


            ///////////aca quito los nodos repetidos para luego devolverlos
            List<string> result = new List<string>();
            string aux;
            Boolean esta = false;
           for (int i = 0; i < n.Count(); i++)
           {
                esta = false;
                aux = n[i];
                for (int j = 0; j < result.Count(); j++)
                {   if (aux == result[j]){esta = true;  }               }
                if (esta == false)
                { result.Add(aux); }
             }
            n = result;


            return n;
        }





        private Boolean igualdad(Class_Clausura parametro, List<Class_Clausura> existentes)
        {
            string intervalo = parametro.getBuscar_imprimir();
            Boolean result = false;
            for (int i = 0; i < existentes.Count(); i++) {
                if (intervalo == existentes[i].getBuscar_imprimir()) { result = true;  }

            }

                return result;
        }




        /////este metodo genera la ultima tabla de transiciones de AFD

        private List<List<Class_Clausura>> genero_AFD(List<string> inter1, List<string> intervalo, List<string> alfabeto)
        {

            List<List<Class_Clausura>> total = new List<List<Class_Clausura>>();
            List<Class_Clausura> fila = new List<Class_Clausura>();
            List<Class_Clausura> existentes = new List<Class_Clausura>();
            Queue<Class_Clausura> cons = new Queue<Class_Clausura>();


            List<string> intervalo1 = new List<string>(); intervalo1 = inter1;
            List<string> intervalo2 = new List<string>(); intervalo2 = intervalo;
    



            Class_Clausura inicial = new Class_Clausura(); inicial.addSub1(intervalo1); inicial.addSub2(intervalo2); inicial.unirBuscar(intervalo1, intervalo2);
            inicial.setcaracter_encontrado("cabecera");
            existentes.Add(inicial);
            // cons.Enqueue(inicial);
            fila.Add(inicial); //ingreso el primer Nodo que recibo


            List<string> union = new List<string>(); union = inicial.getBuscar();

           
 Class_Clausura nodo = new Class_Clausura();


 while (true)   { 
                if (union.Count()>0)
                {



                    ///genero primer intervalo del primer caracter
                    foreach (string caracter in alfabeto)
                    {
                       
                        nodo = new Class_Clausura();

                        foreach (string numero in union)
                        {  // dentro de este gestione el intervalo 1 de el nodo n
                            List<string> aux = new List<string>();
                            aux = obtengo_estados_epsido(numero, caracter);//los ingreso al primer intervalo
                        if (aux.Count() > 0)
                        {
                            // richTextBox3.Text = richTextBox3.Text + aux[0] + "\n";
                            for (int a = 0; a < aux.Count(); a++) { nodo.addSub1(aux[a]); }
                        }
                        }


                    //union de suscesores dados los nodos ejemplo de nodos {3,8}
                    if (nodo.getSub1().Count() > 0) { nodo.addSub2(union_lista_sucesores(nodo.getSub1())); } 
                    nodo.addBuscar(nodo.getSub2());

                        List<string> rango = new List<string>();
                        rango = nodo.getSub1();
                        for (int a = 0; a < rango.Count(); a++) { nodo.addBuscar(rango[a]); }
                        //ingreso al nodo tambien el caracter por donde paso
                        nodo.setcaracter_encontrado(caracter);
                        fila.Add(nodo); 

                        Boolean re = false;
                        re = igualdad(nodo, existentes);
   

                        if (re == false) {
                      //  richTextBox3.Text = richTextBox3.Text + " valor de igualdad for alfabeto :  " + re +  " # fila "+fila.Count()+"\n";
                        
                        cons.Enqueue(nodo);
                       // richTextBox3.Text = richTextBox3.Text + " nombre caracter " + nodo.getcaracter_encontrado() + "\n";

                     //existentes.Add(nodo);
                    }

                }//cierro el forech de alfabeto



                    total.Add(fila);
                    union = new List<string>();
                    fila = new List<Class_Clausura>();
                   // intervalo2 = null;

                } // cierro el if   





  if (cons.Count == 0) { break; }







                    Class_Clausura como = new Class_Clausura();
                    como = cons.Dequeue();
                    Boolean re2 = false;
                     re2 = igualdad(como, existentes);
                    //richTextBox3.Text = richTextBox3.Text + " --------------Valor booleano  :  " + re2 + "\n";


                if (re2 == false)
                {

                   // richTextBox3.Text = richTextBox3.Text + " -------------------antes nombre caracter " + como.getcaracter_encontrado() + "\n";

                   // richTextBox3.Text = richTextBox3.Text + " -----------------Valor despues de la cola :  " + igualdad(como, existentes) + " #Fila " + fila.Count() + "\n";
                    //como.setcaracter_encontrado("cabecera");
                    existentes.Add(como);
                    //richTextBox3.Text = richTextBox3.Text + " ------------------------nombre caracter " + como.getcaracter_encontrado() + "\n";

                    fila.Add(como);
                    union = como.getBuscar(); // setero el intervalo 2 




                
                }
                    // if (cons.Count() > 0)
                // richTextBox3.Text = richTextBox3.Text + cons.Dequeue().getSub2_imprimir() +"dddddddddd\n";


                // if (cons.Count() > 0)
                // richTextBox3.Text = richTextBox3.Text + cons.Dequeue().getSub2_imprimir() + "\n";



                // if (cons.Count() > 0)
                //   richTextBox3.Text = richTextBox3.Text + cons.Dequeue().getSub2_imprimir() + "\n";















              









            }//cierro el while




            return total;



        }





























        private void Analizador()
        {
            char[] cadena = richTextBox1.Text.ToCharArray();
            string concat = "";
            int iterador = 0;


            ////////////////////////////////////////////// Esto es para setear las variables al analizar otro archivo
            j = 0;
            g = 0;
            t = 0;
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






              richTextBox2.Text = richTextBox2.Text + "------------- Comentarios \n";

                        foreach (Class_nodos pa in Comentarios)
                   {
                      richTextBox2.Text = richTextBox2.Text + pa.getDato() + "   ID: " + pa.getId() + '\n';
               }






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
                sustituto[0].getAFN().estado_inicial(); //agrego un estado epsido al inicio
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




            List<List<Class_Clausura>> clau = new List<List<Class_Clausura>>();
            List<List<Class_Clausura>> clau2 = new List<List<Class_Clausura>>();
            //  List<string> inte1 = new List<string>();
            // inte1.Add("9"); inte1.Add("3");
            // List<string> inter22 = new List<string>();
            // inter22.Add("5"); inter22.Add("7"); inter22.Add("4"); inter22.Add("2"); inter22.Add("0"); inter22.Add("9"); inter22.Add("3");

           List< List<string>> lista_tabla = new List<List<string>>();
            List<string> tabla = new List<string>();

            List<List<Class_nodos>> tabla_nodos_total = new List<List<Class_nodos>>();
            List<Class_nodos> tabla_nodos = new List<Class_nodos>();





            foreach (List<Class_alfabeto> lista in Tabla_epsido_aux)
            {  //recorro las listas               

                richTextBox3.Text = richTextBox3.Text + "Nodo  " + lista[0].getnumeroNodo() + "  Intervalo £ : " + lista[0].getIntervalo_imprimir() + "  Clausura " + lista[0].getIntervalo_clausura_imprimir() + "\n" + "\n";

                List<string> inter2 = new List<string>(); inter2.Add(lista[0].getnumeroNodo());
                clau = genero_AFD(inter2, lista[0].getIntervalo_clausura(), lista[0].getAlfabeto());   //// llamo el metodo para obtener los estados del afd


                foreach (List<Class_Clausura> fila in clau) {
                    foreach (Class_Clausura p in fila) {
        //             richTextBox3.Text = richTextBox3.Text + "caracter :  " + p.getcaracter_encontrado() + "  inter1 = " + p.getSub1_imprimir() + "  inter2 = " + p.getSub2_imprimir() + "  Buscar  "+ p.getBuscar_imprimir()+ "  es  "+p.getCabecera() + "\n";


                        tabla.Add(p.getBuscar_imprimir());

                        Class_nodos comido = new Class_nodos();
                        comido.setDato(p.getBuscar_imprimir()); comido.setAlfabeto(lista[0].getAlfabeto()); comido.setTipoNodo(p.getcaracter_encontrado()); comido.setEstado_aceptacion(lista[0].getEstado_aceptacion());
                        tabla_nodos.Add(comido);


                    } }















            


                lista_tabla.Add(tabla);
                tabla_nodos_total.Add(tabla_nodos);
                tabla = new List<string>(); // seteo la tabla
                tabla_nodos = new List<Class_nodos>(); // seteo la lista de nodos

            }






















            List<string> abc = new List<string>();
            abc.Add("a"); abc.Add("b"); abc.Add("c"); abc.Add("d"); abc.Add("e"); abc.Add("f"); abc.Add("g"); abc.Add("h"); abc.Add("i"); abc.Add("j"); abc.Add("k"); abc.Add("l"); abc.Add("m"); abc.Add("n");
            abc.Add("o"); abc.Add("p"); abc.Add("q"); abc.Add("r"); abc.Add("s"); abc.Add("t"); abc.Add("u"); abc.Add("v"); abc.Add("w"); abc.Add("x"); abc.Add("y"); abc.Add("z");
            List<Class_nodos> nod = new List<Class_nodos>();
            List<string> res = new List<string>();





            int buscar_fila = 0;
                /////////elimino repitencia
                //   
                foreach (List<string> fil in lista_tabla)
                {
             //   richTextBox3.Text = richTextBox3.Text + " ///////////////   iteracion \n";
                    List<string> result = new List<string>();
                    string aux; Boolean esta = false;
                    for (int i = 0; i < fil.Count(); i++)
                    { esta = false; aux = fil[i]; for (int j = 0; j < result.Count(); j++) { if (aux == result[j]) { esta = true; } } if (esta == false) { result.Add(aux); } }
                    res = result;




            for (int j = 0; j < res.Count(); j++)
            {
                Class_nodos comodi = new Class_nodos();
                comodi.setDato(res[j]); comodi.setId(abc[j]);
                nod.Add(comodi);

            }

                int cabecera_nodo = tabla_nodos_total[buscar_fila][0].getAlfabeto().Count();
                int aun_cabecera = 0;
            //////les doy nombre a todos los nodos de class_nodos donde estan las transiciones 
            foreach (Class_nodos cad in nod)
            {
            richTextBox3.Text = richTextBox3.Text + "  nodo anali  " + cad.getDato() + "  se cambio " + cad.getId() + " cab---- "+  cabecera_nodo+ "\n";


                    for (int w = 0; w < tabla_nodos_total[buscar_fila].Count(); w++) {
                        if (cad.getDato() == tabla_nodos_total[buscar_fila][w].getDato()) { tabla_nodos_total[buscar_fila][w].setId(cad.getId());        }


                        if (aun_cabecera > cabecera_nodo) { aun_cabecera = 0; }

                        if (aun_cabecera == 0) {
                            tabla_nodos_total[buscar_fila][w].setTipoNodo("cabecera");


                        }
                       

                     
                       aun_cabecera++;
                    }

                  
            }

          

                buscar_fila++;
            }
           
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                /////////// MUESTRO LOS NODOS CON SUS RESPECTIVOS INTERVALOS Y NOMBRES DADOS ARRIBA, TAMBIEN MUESTRO SU ALFABETO

                foreach (List<Class_nodos> lNodos in tabla_nodos_total)
            {

     //           richTextBox3.Text = richTextBox3.Text + "////////////////////  nodos totales  \n";
                foreach (Class_nodos Nnodo in lNodos)
                {  ////////recorriendo noddos totales



                 //   richTextBox3.Text = richTextBox3.Text + "  dato  " + Nnodo.getDato() + " Id    " + Nnodo.getId() + "  alfa  " + Nnodo.getAlfabeto_imprimir() +  " tipo  "+Nnodo.getTipoNodo()+ "\n";


                }
            }




            List<string> lista_grap = new List<string>();

            //////////////////////creo el text graphviz     para el AFD
            foreach (List<Class_nodos> lNodos in tabla_nodos_total)
            {




                string linea1 = "digraph finite_state_machine { \n";
                string linea2 = "rankdir = LR; size = \"8,5\" \n";
                string linea3 = "node [shape = point ]; qi\nnode[shape = doublecircle margin = 0 fontcolor = white fontsize = 15 width = 0.5 style = filled, fillcolor = black]; ";
                string linea4 = "\nnode[margin = 0 fontcolor = white fontsize = 15 width = 0.5 shape = circle]; \n";
                string lineafinal = "} \n";
                string nodos = "";
                string direcciones = "";


                Class_nodos aux = lNodos[0];
                Class_nodos auxnext2 = new Class_nodos();
                string aceptacion = aux.getEstado_aceptacion();
                string cadena_unix = "";
                iterador = 0;
                int primero= 0;

                while (iterador < lNodos.Count())
                {
                    aux = lNodos[iterador];

                    if (aux.getTipoNodo() == "cabecera")
                    {
                        if (primero == 0) { nodos = nodos + " qi ->" + aux.getId() + "\n"; primero++; }

                        bool b = false;
                        String tes = aux.getDato();
                        char[] charArr = tes.ToCharArray();
                        for (int i = 0; i < charArr.Count(); i++)
                        {
                            if (charArr[i] != ',') { cadena_unix = cadena_unix + charArr[i]; } else if (charArr[i] == ',') { cadena_unix = ""; }
                            if (aceptacion == cadena_unix) { b = true; }

                        }


                        if (b == true) { linea3=linea3  + " " + aux.getId(); }

                       
                        nodos = nodos + "" + aux.getId() + "; \n";



                        for (int j = iterador + 1; j < lNodos.Count(); j++)
                        {
                            Class_nodos aux2 = new Class_nodos();
                            aux2 = lNodos[j];
                            if (aux2.getTipoNodo() == "cabecera") { break; }

                            string etiqueta = aux2.getTipoNodo();
                            if (etiqueta == "\\n" || etiqueta == "\\t" || etiqueta == "\\r") { etiqueta = "\\" + etiqueta; }
                            direcciones = direcciones + "" + aux.getId() + "-> " + aux2.getId() + "[label = \"" + etiqueta+ "\"];  \n";

                        }


                    }//cierro el if

                    iterador++;

                }
               string total = linea1  +linea2 +linea3+linea4+nodos + direcciones + lineafinal;

              //  richTextBox3.Text = richTextBox3.Text + total;




                lista_grap.Add(total);






            }





            List<string> lista_grap_tabla = new List<string>();

            //////////////////////creo el text graphviz    para la TABLA
            foreach (List<Class_nodos> lNodos in tabla_nodos_total)
            {




                string linea1 = "digraph tabla{   \ntbl[ shape = plaintext \nlabel =< \n<table border = '2' cellborder = '1' color = 'black' cellspacing = '4' bgcolor=\"white\"> \n<tr> \n<td color =\"black\" colspan = '5'> TRANSICIONES </td> \n</tr> \n";
                string lineafinal = "</table> \n>]; \n}\n";
                string nodos = "";
                string direcciones = "";

                int parar = 0;

                Class_nodos aux = lNodos[0];
                Class_nodos auxnext2 = new Class_nodos();
                iterador = 0;
                while (iterador < lNodos.Count())
                {

                    


                    aux = lNodos[iterador];

                    if (aux.getTipoNodo() == "cabecera")
                    {
                        if (parar== 0) {
                            parar++;         nodos = nodos + "<tr>\n";         nodos = nodos + "\t \t <td color =\"grey\">" + "Estados" + "</td> \n";
                            for (int k = 0; k < aux.getAlfabeto().Count(); k++) { nodos = nodos + "\t \t <td color =\"black\">" + aux.getAlfabeto()[k] + "</td> \n"; }
                            nodos = nodos + "</tr>\n";
                        }





                        nodos = nodos + "<tr>\n";
                        nodos = nodos + "<td color =\"grey\">" + aux.getId() + "</td> \n";



                        for (int j = iterador + 1; j < lNodos.Count(); j++)
                        {
                            Class_nodos aux2 = new Class_nodos();
                            aux2 = lNodos[j];
                            if (aux2.getTipoNodo() == "cabecera") { break; }

                            nodos = nodos + "\t \t <td color =\"black\">" + aux2.getId() + "</td>  \n";

                        }

                        nodos = nodos + "</tr>  \n";

                    }//cierro el if

                    iterador++;

                }
                string total = linea1 + nodos + direcciones + lineafinal;


                lista_grap_tabla.Add(total);


              //  richTextBox3.Text = richTextBox3.Text + total;


                }



            ////////////////////////////////genero en jpg los archivos de AFD y Tabla

            int al = 0;
            foreach (string cara in lista_grap)
            {
                archivoTxt("G" + al, cara);   ////creo archivo txt

                GenerateGraph("G" +al +".txt", "");  //con esto genero los png de las expresiones

                al++;
            }
            g_tabla = lista_grap;
            int al2 = 0;

            foreach (string cara in lista_grap_tabla)
            {
                archivoTxt("T" + al2, cara);

                GenerateGraph("T"+al2 + ".txt", "");  //con esto genero los png de las expresiones

                al2++;
            }
            t_tabla = lista_grap_tabla;






















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
            g = 0;
            t = 0;
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

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (g_tabla.Count() > 0)
            {
                g++;
                if (g > g_tabla.Count())
                {
                    g = 1;
                }
                //this.pictureBox1.Size = new System.Drawing.Size(140, 140); // le da el tamaño al label
                this.pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;  // Establece SizeMode para centrar la imagen.
                this.pictureBox2.BorderStyle = BorderStyle.Fixed3D;// Set the border style to a three-dimensional border.
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage; // hace que la imagen se ajuste en el label donde se muestra
                pictureBox2.Image = Image.FromFile("G"+g+ ".jpg");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (t_tabla.Count() > 0)
            {
                t++;
                if (t > t_tabla.Count())
                {
                    t = 1;
                }
                //this.pictureBox1.Size = new System.Drawing.Size(140, 140); // le da el tamaño al label
                this.pictureBox3.SizeMode = PictureBoxSizeMode.CenterImage;  // Establece SizeMode para centrar la imagen.
                this.pictureBox3.BorderStyle = BorderStyle.Fixed3D;// Set the border style to a three-dimensional border.
                pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage; // hace que la imagen se ajuste en el label donde se muestra
                pictureBox3.Image = Image.FromFile("T" + t + ".jpg");
            }





        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (g_tabla.Count() > 0)
            {
                g--;
                if (g < 1)
                {
                    g = g_tabla.Count();
                }
                //this.pictureBox1.Size = new System.Drawing.Size(140, 140); // le da el tamaño al label
                this.pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;  // Establece SizeMode para centrar la imagen.
                this.pictureBox2.BorderStyle = BorderStyle.Fixed3D;// Set the border style to a three-dimensional border.
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage; // hace que la imagen se ajuste en el label donde se muestra
                pictureBox2.Image = Image.FromFile("G" + g + ".jpg");
            }








        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (t_tabla.Count() > 0)
            {
                t--;
                if (t < 1)
                {
                    t = t_tabla.Count();
                }
                //this.pictureBox1.Size = new System.Drawing.Size(140, 140); // le da el tamaño al label
                this.pictureBox3.SizeMode = PictureBoxSizeMode.CenterImage;  // Establece SizeMode para centrar la imagen.
                this.pictureBox3.BorderStyle = BorderStyle.Fixed3D;// Set the border style to a three-dimensional border.
                pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage; // hace que la imagen se ajuste en el label donde se muestra
                pictureBox3.Image = Image.FromFile("T" + t + ".jpg");
            }
        }
    }
}


