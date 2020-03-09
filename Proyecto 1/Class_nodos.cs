using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_1
{
    public class Class_nodos
    {
        Class_nodos next = new Class_nodos();
        Class_nodos anterior = new Class_nodos();



        int estado;
        string Id;
        string Tipo;
        string dato;

        int columna;
        int fila;

        List<Class_transiciones> transiciones = new List<Class_transiciones>();


        public Class_nodos()
        {
            this.dato = "";
            this.Id = "";
            this.dato = "";
            this.Tipo = "";
            this.estado = 0;
            this.columna = 0;
            this.fila = 0;


            next = null;
            anterior = null;


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


        public void setNext(Class_nodos b)
        {
            this.next = b;
        }
        public Class_nodos getNext()
        {
            return this.next;
        }

        public void setAnterior(Class_nodos b)
        {
            this.anterior = b;
        }
        public Class_nodos getAnterior()
        {
            return this.anterior;
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
