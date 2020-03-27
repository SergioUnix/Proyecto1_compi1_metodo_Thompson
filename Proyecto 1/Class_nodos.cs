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

        List<string> alfabeto = new List<string>();

        string estado_aceptacion = "";


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

        public void quitarRepitencia_alfabeto() {
            List<string> result = new List<string>();
            string aux;
            Boolean esta = false;

            for (int i = 0; i < alfabeto.Count();i++) {
                esta = false;
                aux = alfabeto[i];
                for (int j = 0; j < result.Count(); j++)
                {
                    if (aux==result[j])
                    {  esta = true;
                    }
                  



                }

                if (esta==false)
                { result.Add(aux);}

                }

            setAlfabeto_AFN(result);
            alfabeto = result;


        }

        public string getAlfabeto_imprimir()
        {
            string result = "";
            for (int i = 0; i < this.alfabeto.Count(); i++) { result = result + this.alfabeto[i] + ","; }
            return result;

        }

        public void setEstado_aceptacion(string a)
        {
            this.estado_aceptacion=a;
        }
        public string getEstado_aceptacion()
        {
           return this.estado_aceptacion;
        }


        public void setAlfabeto_AFN(List<string> a)
        {
            this.automata.setAlfabeto(a);
        }

        public void setAlfabeto(List<string> a) {
            this.alfabeto = a;
        }
        public List<string> getAlfabeto() {
            return alfabeto;

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








        public List<Class_nodos> NodosExpresiones()
        {
            List<Class_nodos> result = new List<Class_nodos>();    //genero esta lista para separar los id de las expresiones
            string opera = getDato();                   ///agarro el string de expresiones
            char[] cadena = opera.ToCharArray();
            string concatenar = "";

            for (int i = 0; i < opera.Length; i++)
            {

                if (cadena[i] == '.' || cadena[i] == '|' || cadena[i] == '?' || cadena[i] == '*' || cadena[i] == '+')
                {
                    if (concatenar.Length > 1) { Class_nodos b = new Class_nodos(); b.setDato(concatenar); result.Add(b); concatenar = ""; }
                    concatenar = "" + cadena[i];
                    Class_nodos a = new Class_nodos(); a.setDato(concatenar);
                    if (cadena[i] == '|')
                    {
                        a.setId("Binario"); a.setTipoNodo("alter");
                    }
                    else if (cadena[i] == '.') {
                        a.setId("Binario"); a.setTipoNodo("concatenar");
                    }
                    else if (cadena[i] == '?')
                    {
                        a.setId("Unario"); a.setTipoNodo("ceroUno");
                    }
                    else if (cadena[i] == '*')
                    {
                        a.setId("Unario"); a.setTipoNodo("ceroVarios");
                    }
                    else if (cadena[i] == '+')
                    {
                        a.setId("Unario"); a.setTipoNodo("unoVarios");
                    }

                    result.Add(a);
                    concatenar = "";


                }
                if (cadena[i] == '"')
                {
                    if (concatenar.Length > 1) { Class_nodos b = new Class_nodos(); b.setDato(concatenar); result.Add(b); concatenar = ""; }
                    i++;
                    for (int j = 0; j < opera.Length; j++)
                    {
                        if (cadena[i] == '"') { break; }
                        concatenar = concatenar + cadena[i];
                        i++;
                    }
                    Class_nodos a = new Class_nodos(); a.setDato(concatenar); a.setId("op"); a.setTipoNodo("op"); concatenar = "";
                    result.Add(a);



                }
                if (cadena[i] == '{')
                {
                    if (concatenar.Length > 1) { Class_nodos b = new Class_nodos(); b.setDato(concatenar); result.Add(b); concatenar = ""; }
                    i++;
                    for (int j = 0; j < opera.Length; j++)
                    {
                        if (cadena[i] == '}') { break; }
                        concatenar = concatenar + cadena[i];
                        i++;
                    }
                    Class_nodos a = new Class_nodos(); a.setDato(concatenar); a.setId("op"); a.setTipoNodo("op"); concatenar = "";
                    result.Add(a);


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
