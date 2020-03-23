using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_1
{
    public class Class_alfabeto
    {
        string numeroNodo;
        string letra;
        List<Class_transiciones> intervalo = new List<Class_transiciones>();


        List<string> intervalo_clausura = new List<string>();

        //List<string> alfabeto_total = new List<string>();


        List<string> alfabeto = new List<string>();



        public Class_alfabeto()
        {
            this.numeroNodo = "";
            this.letra = "";
            
        }



        public void setAlfabeto(List<string> a)
        {
            this.alfabeto = a;
        }
        public List<string> getAlfabeto()
        {
            return alfabeto;

        }


       // public void addAlfabeto(string a)
        //{
         //  this.alfabeto_total.Add(a);
       // }
       // public List<string> get_listAlfabeto()
        //{
         //   return this.alfabeto_total;
        //}




        public void setIntervalo_clausura(List<string> a)
        {
            this.intervalo_clausura = a;
        }
        public List<string> getIntervalo_clausura()
        {
            return this.intervalo_clausura;
        }

        public string getIntervalo_clausura_imprimir()
        {
            string result="";
            for (int i = 0; i < intervalo_clausura.Count(); i++) { result = result + intervalo_clausura[i] + ","; }
            return result;

        }
        public string getIntervalo_imprimir()
        {
            string result = "";
            for (int i = 0; i < intervalo.Count(); i++) { result = result + intervalo[i].getDireccion() + ","; }
            return result;

        }

        public void addIntervalo_clausura(string a)
        {
            this.intervalo_clausura.Add(a);
        }




        public Boolean existeC_intervalo(string a) { ///verifico si existe el nodo
            Boolean result = false;
            foreach (Class_transiciones pa in intervalo)
            {
                if (pa.getNombre() == a) { result = true; }
            }
            return result;
        }

        public List<Class_transiciones> getIntervalo()
        {
            return this.intervalo;

        }

        public void addIntervalo(Class_transiciones a) {
            this.intervalo.Add(a);
        }

        public void setnumeroNodo(string a)
        {
            this.numeroNodo = a;
        }
        public string getnumeroNodo()
        {
            return this.numeroNodo;
        }

        public void setLetra(string a) {
            this.letra = a;

        }
        public string getLetra() {
            return this.letra;
        }















    }
}
