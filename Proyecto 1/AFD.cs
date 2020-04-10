﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_1
{
    public class AFD
    {
        AFD next1;
        AFD next2;

        string dato;
        int estado;
        string Id;
        List<Class_transiciones> transiciones = new List<Class_transiciones>();
        List<Class_transiciones> transiciones_ascci = new List<Class_transiciones>();



        int contador_nodo;
        public static int iden; //Identificador unico de cada nodo  


        bool estado_aceptacion = false;


        public AFD()
        {
            this.dato = "";
            this.Id = "";
            this.estado = 0;
            this.next1 = null;
            this.next2 = null;


        }



        public string getID()
        {
            return this.Id;
        }
        public void setID(string a)
        {
            this.Id = a;
        }

        public void setEstado_aceptacion(bool a)
        {
            this.estado_aceptacion = a;
        }
        public bool getEstado_aceptacion()
        {
            return this.estado_aceptacion;
        }


        public void setListTransiciones(List<Class_transiciones> b)
        {
            this.transiciones = b;
        }
        public List<Class_transiciones> getListTransiciones()
        {
            return this.transiciones;
        }

        public void setListTransicionesAscci(List<Class_transiciones> b)
        {
            this.transiciones_ascci = b;
        }
        public List<Class_transiciones> getListTransicionesAscci()
        {
            return this.transiciones_ascci;
        }

        public void aumentarcount()
        {
            contador_nodo = iden;
            iden++;
        }

        public void addTransicion(Class_transiciones b)
        {
            this.transiciones.Add(b);
        }

        public void addTransicionAscci(Class_transiciones b)
        {
            this.transiciones_ascci.Add(b);
        }
        public void setDato(string a)
        {
            this.dato = a;
        }
        public void setEstado(int s)
        {
            this.estado = s;
        }
        public string getDato()
        {
            return this.dato;
        }
        public int getEstado()
        {
            return this.estado;
        }

        public void setNext1(AFD next1)
        {
            this.next1 = next1;
        }
        public AFD getNext1()
        {
            return this.next1;
        }

        public void setNext2(AFD next2)
        {
            this.next2 = next2;
        }
        public AFD getNext2()
        {
            return this.next2;
        }
        public int getContadorNodo()
        {
            return this.contador_nodo;
        }


    }

}
