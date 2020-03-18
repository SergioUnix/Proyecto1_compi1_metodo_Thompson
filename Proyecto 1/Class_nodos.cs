using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_1
{
    public class Class_nodos
    {
         Class_nodos next1;

         Class_nodos next2;
        



        int estado;
        string Id;
        string Tipo;
        string dato;
        int columna;
        int fila;

       
        List<Class_transiciones> transiciones = new List<Class_transiciones>();


        int contador_nodo;
        public static int cont;
        private string tipoNodo; // tipo para nodos de afn
        private AFN automata;


        public Class_nodos()
        {
            this.dato = "";
            this.Id = "";
            this.dato = "";
            this.Tipo = "";
            this.estado = 0;
            this.columna = 0;
            this.fila = 0;
            this.tipoNodo = "";
            this.next1 = null;
            this.next2 = null;


        }



        public void setAFN(AFN b)
        {
            this.automata = b;
        }
        public AFN getAFN()
        {
            return this.automata;
        }

        public void setListTransiciones(List<Class_transiciones> b)
        {
            this.transiciones = b;
        }
        public List<Class_transiciones> getListTransiciones()
        {
            return this.transiciones;
        }

        public void aumentarcount()
        {
          contador_nodo = cont;
            cont++;
        }

        public void addTransicion(Class_transiciones b)
        {
            this.transiciones.Add(b);
        }
        public void setDato(string a)
        {
            this.dato = a;
        }
        public void setEstado(int s)
        {
            this.estado = s;
        }
        public void setId(string i)
        {
            this.Id = i;
        }
        public string getDato()
        {
            return this.dato;
        }
        public int getEstado()
        {
           return  this.estado;
        }
        public string getId()
        {
            return this.Id;
        }


        public void setTipo(string b)
        {
            this.Tipo = b;
        }


        public string getTipo()
        {
            return this.Tipo;
        }


        public void setColumna(int b)
        {
            this.columna = b;
        }
        public int getColumna()
        {
            return this.columna;
        }


        public void setFila(int b)
        {
            this.fila = b;
        }
        public int getFila()
        {
            return this.fila;
        }


        public void setNext1(Class_nodos next1)
        {
            this.next1 = next1;
        }
        public Class_nodos getNext1()
       {
            return this.next1;
       }

        public void setNext2(Class_nodos next2)
        {
            this.next2 = next2;
        }
        public Class_nodos getNext2()
        {
          return this.next2;
        }



        public void setTipoNodo(string n)
        {
            this.tipoNodo = n;
        }
        public string getTipoNodo()
        {
            return this.tipoNodo;
        }


        public int getContadorNodo()
        {
        return this.contador_nodo;
        }



        public List<string> arregloExpresiones() {
            List<string> result = new List<string>();    //genero esta lista para separar los id de las expresiones
            string opera = getDato();                   ///agarro el string de expresiones
            char[] cadena = opera.ToCharArray();
            string concatenar = "";

            for (int i = 0; i < opera.Length; i++) {

                if (cadena[i] == '.' || cadena[i] == '|' || cadena[i] == '?' || cadena[i] == '*' || cadena[i] == '+') {
                    if (concatenar.Length > 1) { result.Add(concatenar); concatenar = ""; }
                    concatenar = ""+cadena[i];
                    result.Add(concatenar);
                    concatenar = "";
                   
                    
                }
                if (cadena[i] == '"') {
                    if (concatenar.Length > 1) { result.Add(concatenar); concatenar = ""; }
                    i++;
                    for (int j = 0; j < opera.Length; j++)
                    {
                        if (cadena[i] == '"') { break; }
                        concatenar = concatenar + cadena[i];
                        i++;
                    } result.Add(concatenar); concatenar = "";

                

                }
                if (cadena[i] == '{')
                {
                    if (concatenar.Length > 1) { result.Add(concatenar); concatenar = ""; }
                    i++;
                    for (int j = 0; j < opera.Length; j++)
                    {
                        if (cadena[i] == '}') { break; }
                        concatenar = concatenar + cadena[i];
                        i++;
                    } result.Add(concatenar); concatenar = "";
                   

                }

                if (cadena[i] != '.' || cadena[i] != '|' || cadena[i] != '?' || cadena[i] != '*' || cadena[i] != '+')
                {
                    concatenar = concatenar + cadena[i];
                }
                if (cadena[i] == '.' || cadena[i] == '|' || cadena[i] == '?' || cadena[i] == '*' || cadena[i] == '+' || cadena[i] == '}' || cadena[i] == '"')
                {
                    concatenar = "";
                }




            }







            return result; 
        }






    }
}
