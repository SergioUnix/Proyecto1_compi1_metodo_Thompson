using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_1
{
    public class Class_nodos
    {
        
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
            this.columna = 0;
            this.fila = 0;



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






    }
}
