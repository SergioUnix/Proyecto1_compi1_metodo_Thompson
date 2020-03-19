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

        
        public void alter_afaf(AFN a, AFN b) {
            Class_nodos i = new Class_nodos(); i.aumentarcount(); i.setTipoNodo("alter");// le da numeracion al nodo            
            Class_nodos f = new Class_nodos(); f.aumentarcount();// le da numeracion al nodo
            i.addTransicion(new Class_transiciones("£",a.getInicio().getContadorNodo().ToString()));//punteros de i
            i.addTransicion(new Class_transiciones("£", b.getInicio().getContadorNodo().ToString()));//punteros de i
            i.setNext1(a.getInicio()); a.getFinal().setNext1(f);   a.getFinal().addTransicion(new Class_transiciones("£", f.getContadorNodo().ToString()));//punteros de a.final
            i.setNext2(b.getInicio());  b.getFinal().addTransicion(new Class_transiciones("£", f.getContadorNodo().ToString()));//punteros de a.final
            //b.getFinal().setNext1(f); //tal vez de error al recorrer
            this.inicio = i; this.final = f;
           }


    
        //// De entradas recibe uns string y un AFN  , sirve para el alter
        public void alter_afCadena(string a, AFN b)
        {
            AFN aa = new Proyecto_1.AFN();  
            aa.primerAfn(a);           //convierto al string a a un afn
            concatenar_afaf(aa, b);
        }
        /// Hace lo contrario del anterior primero recibe un AFN , sirve para el ater
        public void alter_afCadena(AFN a, string b)
        {
            AFN bb = new Proyecto_1.AFN();
            bb.primerAfn(b);           //convierto al string b a un afn
            alter_afaf(bb, a);
        }

        public void alter_CC(string a, string b) // recibe dos strings y los convierte en afn, eso sirve para el alter
        {
            AFN bb = new Proyecto_1.AFN();
            bb.primerAfn(b);           //convierto al string b a un afn

            AFN aa = new Proyecto_1.AFN();
            aa.primerAfn(a);           //convierto al string a a un afn
            alter_afaf(aa, bb); // llamo el metodo que crea un inicio y un final con dos afn


        }


        //Funcion para la concatenacion

        public void concatenar_afaf(AFN a, AFN b)
        {

            a.getFinal().setNext1(b.getInicio().getNext1());  /// agarro el f de a y le pongo el next del inicio de b
            if (b.getInicio().getNext2() != null)   //si no esta vacio agrego un next2 al f de a, 
            a.getFinal().setNext2(b.getInicio().getNext2());

            a.getFinal().setListTransiciones(b.getInicio().getListTransiciones());  // le paso las transiciones al f de a, tambien

              this.inicio = a.getInicio(); this.final = b.getFinal();
        }


        public void concatenar_afCadena(string a, AFN b)
        {
            AFN aa = new Proyecto_1.AFN();
            aa.primerAfn(a);

            aa.getFinal().setNext1(b.getInicio().getNext1());  /// agarro el f de a y le pongo el next del inicio de b
            if (b.getInicio().getNext2() != null)   //si no esta vacio agrego un next2 al f de a, 
                aa.getFinal().setNext2(b.getInicio().getNext2());

            aa.getFinal().setListTransiciones(b.getInicio().getListTransiciones());  // le paso las transiciones al f de a, tambien

            this.inicio = aa.getInicio(); this.final = b.getFinal();
        }


        public void concatenar_afCadena(AFN a, string b)
        {
            AFN bb = new Proyecto_1.AFN();
            bb.primerAfn(b);

            a.getFinal().setNext1(bb.getInicio().getNext1());  /// agarro el f de a y le pongo el next del inicio de b
            if (bb.getInicio().getNext2() != null)   //si no esta vacio agrego un next2 al f de a, 
                a.getFinal().setNext2(bb.getInicio().getNext2());

            a.getFinal().setListTransiciones(bb.getInicio().getListTransiciones());  // le paso las transiciones al f de a, tambien

            this.inicio = a.getInicio(); this.final = bb.getFinal();
        }

        public void concatenar_CC(string a, string b)
        {
            AFN aa = new Proyecto_1.AFN();
            aa.primerAfn(a);

            AFN bb = new Proyecto_1.AFN();
            bb.primerAfn(b);
            concatenar_afaf(aa,bb);
        }

        /// Cerradura de Kleene * 
        public void kleene_af(AFN a)
        {
            Class_nodos i = new Class_nodos(); i.aumentarcount(); i.setTipoNodo("ceroVarios");// le da numeracion al nodo            
            Class_nodos f = new Class_nodos(); f.aumentarcount(); f.setTipoNodo("ceroVarios"); // le da numeracion al nodo
            i.addTransicion(new Class_transiciones("£", a.getInicio().getContadorNodo().ToString()));//punteros de i
            i.addTransicion(new Class_transiciones("£", f.getContadorNodo().ToString()));//punteros de i
            i.setNext1(a.getInicio()); a.getFinal().setNext1(f); a.getFinal().addTransicion(new Class_transiciones("£", f.getContadorNodo().ToString()));//punteros de a.final
            a.getFinal().addTransicion(new Class_transiciones("£", a.getInicio().getContadorNodo().ToString())); // hago una trans del a.final al a.inicio
            
            this.inicio = i; this.final = f;
        }

        public void kleene_Cadena(string aa)
        {

            AFN a = new Proyecto_1.AFN();
            a.primerAfn(aa);

            Class_nodos i = new Class_nodos(); i.aumentarcount(); i.setTipoNodo("ceroVarios");// le da numeracion al nodo            
            Class_nodos f = new Class_nodos(); f.aumentarcount(); f.setTipoNodo("ceroVarios"); // le da numeracion al nodo
            i.addTransicion(new Class_transiciones("£", a.getInicio().getContadorNodo().ToString()));//punteros de i
            i.addTransicion(new Class_transiciones("£", f.getContadorNodo().ToString()));//punteros de i
            i.setNext1(a.getInicio()); a.getFinal().setNext1(f); a.getFinal().addTransicion(new Class_transiciones("£", f.getContadorNodo().ToString()));//punteros de a.final
            a.getFinal().addTransicion(new Class_transiciones("£", a.getInicio().getContadorNodo().ToString())); // hago una trans del a.final al a.inicio
            this.inicio = i; this.final = f;
        }




        ////////////////////////////// cerradura positiva

        //funciona bien solo que en el proyecto nos piden transformar la positiva como un  .a*a que es lo mismo 
          public void positiva_af(AFN a)
        {
         Class_nodos i = new Class_nodos(); i.aumentarcount(); i.setTipoNodo("unoVarios");// le da numeracion al nodo            
        Class_nodos f = new Class_nodos(); f.aumentarcount(); f.setTipoNodo("unoVarios"); // le da numeracion al nodo
         i.addTransicion(new Class_transiciones("£", a.getInicio().getContadorNodo().ToString()));//punteros de i         
         i.setNext1(a.getInicio()); a.getFinal().setNext1(f); a.getFinal().addTransicion(new Class_transiciones("£", f.getContadorNodo().ToString()));//punteros de a.final
         a.getFinal().addTransicion(new Class_transiciones("£", a.getInicio().getContadorNodo().ToString())); // hago una trans del a.final al a.inicio
         this.inicio = i; this.final = f;
         }

        // public void positiva_Cadena(string aa)
        //{

        //  AFN a = new Proyecto_1.AFN();
        // a.primerAfn(aa);

        // Class_nodos i = new Class_nodos(); i.aumentarcount(); i.setTipoNodo("unoVarios");// le da numeracion al nodo            
        // Class_nodos f = new Class_nodos(); f.aumentarcount(); f.setTipoNodo("unoVarios"); // le da numeracion al nodo
        // i.addTransicion(new Class_transiciones("£", a.getInicio().getContadorNodo().ToString()));//punteros de i         
        // i.setNext1(a.getInicio()); a.getFinal().setNext1(f); a.getFinal().addTransicion(new Class_transiciones("£", f.getContadorNodo().ToString()));//punteros de a.final
        //  a.getFinal().addTransicion(new Class_transiciones("£", a.getInicio().getContadorNodo().ToString())); // hago una trans del a.final al a.inicio
        // this.inicio = i; this.final = f;
        // }




        public void positiva_Cadena(string a)
        {
            AFN klee = new Proyecto_1.AFN();
            klee.kleene_Cadena(a);                 //aca hago un afn con aux* que es la cerradura de kleen
            concatenar_afCadena(a,klee);
        }











        ///////////////////////////////////////////////////////////////////cerradura  ?

        //funciona pero en el proyecto nos piden que convirtamos el ?  de la siguiente manera |a£ una alter con el afn y £
        //public void ceroUno_af(AFN a)
        //{
        //  Class_nodos i = new Class_nodos(); i.aumentarcount(); i.setTipoNodo("ceroUno");// le da numeracion al nodo            
        // Class_nodos f = new Class_nodos(); f.aumentarcount(); f.setTipoNodo("ceroUno"); // le da numeracion al nodo
        // i.addTransicion(new Class_transiciones("£", a.getInicio().getContadorNodo().ToString()));//punteros de i         
        // i.setNext1(a.getInicio()); a.getFinal().setNext1(f); a.getFinal().addTransicion(new Class_transiciones("£", f.getContadorNodo().ToString()));//punteros de a.final
        // i.addTransicion(new Class_transiciones("£", f.getContadorNodo().ToString())); // hago una trans del inicial al final
        //this.inicio = i; this.final = f;
        //}

        //public void ceroUno_C(string aa)
        //{
        //  AFN a = new Proyecto_1.AFN();
        //a.primerAfn(aa);
        // Class_nodos i = new Class_nodos(); i.aumentarcount(); i.setTipoNodo("ceroUno");// le da numeracion al nodo            
        // Class_nodos f = new Class_nodos(); f.aumentarcount(); f.setTipoNodo("ceroUno"); // le da numeracion al nodo
        // i.addTransicion(new Class_transiciones("£", a.getInicio().getContadorNodo().ToString()));//punteros de i         
        // i.setNext1(a.getInicio()); a.getFinal().setNext1(f); a.getFinal().addTransicion(new Class_transiciones("£", f.getContadorNodo().ToString()));//punteros de a.final
        // i.addTransicion(new Class_transiciones("£", f.getContadorNodo().ToString())); // hago una trans del inicial al final
        // this.inicio = i; this.final = f;
        // }

        public void ceroUno_af(AFN a)
        {
            Class_nodos i = new Class_nodos(); i.aumentarcount(); i.setTipoNodo("ceroUno");// le da numeracion al nodo            
            Class_nodos f = new Class_nodos(); f.aumentarcount(); f.setTipoNodo("ceroUno");// le da numeracion al nodo
            i.addTransicion(new Class_transiciones("£", a.getInicio().getContadorNodo().ToString()));//punteros de i
            i.addTransicion(new Class_transiciones("£", f.getContadorNodo().ToString()));//punteros de i hacia el final porque es ?
            i.setNext1(a.getInicio()); a.getFinal().setNext1(f); a.getFinal().addTransicion(new Class_transiciones("£", f.getContadorNodo().ToString()));//punteros de a.final
            this.inicio = i; this.final = f;
         }

        public void ceroUno_C(string aa)
        {
            AFN a = new Proyecto_1.AFN();
            a.primerAfn(aa);

            Class_nodos i = new Class_nodos(); i.aumentarcount(); i.setTipoNodo("ceroUno");// le da numeracion al nodo            
            Class_nodos f = new Class_nodos(); f.aumentarcount(); f.setTipoNodo("ceroUno");// le da numeracion al nodo
            i.addTransicion(new Class_transiciones("£", a.getInicio().getContadorNodo().ToString()));//punteros de i
            i.addTransicion(new Class_transiciones("£", f.getContadorNodo().ToString()));//punteros de i hacia el final porque es ?
            i.setNext1(a.getInicio()); a.getFinal().setNext1(f); a.getFinal().addTransicion(new Class_transiciones("£", f.getContadorNodo().ToString()));//punteros de a.final
            this.inicio = i; this.final = f;
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


        public string generarTxt() {
            string linea1 = "digraph G{ \n rankdir=LR; \n";
            string lineafinal = "} \n";
            string nodos = "";
            string direcciones = "";

            Class_nodos aux = this.inicio;
            Class_nodos auxnext2 = new Class_nodos();
            while (aux!=null) {

                nodos = nodos + "node" + aux.getContadorNodo() + "; \n";
                for (int j = 0; j < aux.getListTransiciones().Count(); j++) {

                    direcciones = direcciones + "node" + aux.getContadorNodo() + "-> node" + aux.getListTransiciones()[j].getDireccion() + "[label = \"" + aux.getListTransiciones()[j].getNombre() + "\"];  \n";

                }
                if (aux.getNext2() != null) {

                             auxnext2 = aux.getNext2();

                    while (auxnext2 != null)
                    {

                        nodos = nodos + "node" + auxnext2.getContadorNodo() + ";  \n";
                        for (int j = 0; j < auxnext2.getListTransiciones().Count(); j++)
                        {

                            direcciones = direcciones + "node" + auxnext2.getContadorNodo() + "-> node" + auxnext2.getListTransiciones()[j].getDireccion() + "[label = \"" + auxnext2.getListTransiciones()[j].getNombre() + "\"]; \n";

                        }
                        auxnext2 = auxnext2.getNext1();
                              }












                    }
               



                aux = aux.getNext1();
            }

            string total = linea1 + nodos + direcciones + lineafinal;

            return total;




        }







    }
}
