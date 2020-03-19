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

        public Class_alfabeto()
        {
            this.numeroNodo = "";
            this.letra = "";
            
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
