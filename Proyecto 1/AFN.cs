using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Proyecto_1
{


    public class AFN
    {
        Class_nodos inicio;
        Class_nodos final;
        int size = 0;


        public AFN()
        {
            inicio = null;
            final = null;

        }


        public void primerAfn(string a)
        {
            //AFN result = new Proyecto_1.AFN();
            Class_nodos i = new Class_nodos(); i.aumentarcount(); i.setTipoNodo("afn"); // le da numeracion al nodo            
            Class_nodos f = new Class_nodos(); f.aumentarcount(); i.setTipoNodo("afn");// le da numeracion al nodo
            i.addTransicion(new Class_transiciones(a, f.getContadorNodo().ToString())); // agrego una transicion hacia el nodo final de este afn
            i.setNext1(f);
            this.inicio = i; this.final = f;
        }

        public AFN primer(string a)
        {
            AFN result = new Proyecto_1.AFN();
            Class_nodos i = new Class_nodos(); i.aumentarcount(); i.setTipoNodo("afn"); // le da numeracion al nodo            
            Class_nodos f = new Class_nodos(); f.aumentarcount(); i.setTipoNodo("afn");// le da numeracion al nodo
            i.addTransicion(new Class_transiciones(a, f.getContadorNodo().ToString())); // agrego una transicion hacia el nodo final de este afn
            i.setNext1(f);
            result.setInicio(i); result.setFinal(f);

            return result;
        }


        public void concatenar_afaf(AFN a, AFN b) {
            Class_nodos i = new Class_nodos(); i.aumentarcount(); i.setTipoNodo("alter");// le da numeracion al nodo            
            Class_nodos f = new Class_nodos(); f.aumentarcount();// le da numeracion al nodo
            i.addTransicion(new Class_transiciones("£",a.getInicio().getContadorNodo().ToString()));//punteros de i
            i.addTransicion(new Class_transiciones("£", b.getInicio().getContadorNodo().ToString()));//punteros de i
            i.setNext1(a.getInicio()); a.getFinal().setNext1(f);   a.getFinal().addTransicion(new Class_transiciones("£", f.getContadorNodo().ToString()));//punteros de a.final
            i.setNext2(b.getInicio()); b.getFinal().setNext1(f); b.getFinal().addTransicion(new Class_transiciones("£", f.getContadorNodo().ToString()));//punteros de a.final

            this.inicio = i; this.final = f;
           }


    
        //// De entradas recibe uns string y un AFN
        public void concatenar_afCadena(string a, AFN b)
        {
            AFN aa = new Proyecto_1.AFN();  
            aa.primerAfn(a);           //convierto al string a a un afn
            concatenar_afaf(aa, b);
        }
        /// Hace lo contrario del anterior primero recibe un AFN
        public void concatenar_afCadena(AFN a, string b)
        {
            AFN bb = new Proyecto_1.AFN();
            bb.primerAfn(b);           //convierto al string b a un afn
            concatenar_afaf(bb, a);
        }







        public Class_nodos getInicio()
        {
          return  this.inicio;
        }
        public Class_nodos getFinal()
        {
            return this.final;
        }
        public void setInicio(Class_nodos a)
        {
            this.inicio = a;
        }
        public void setFinal(Class_nodos b)
        {
            this.final = b;
        }




    }
}
