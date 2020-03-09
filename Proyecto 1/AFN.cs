using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Proyecto_1
{

   
    public class AFN
    {
 Class_nodos inicio = new Class_nodos();
 Class_nodos final = new Class_nodos();
        int size = 0;


        public AFN()
        {
            inicio = null;
            final = null;

        }


   public void agregarDatos(string a)
        {
            AFN result = new Proyecto_1.AFN();
            
            Class_nodos i = new Class_nodos();
            i.setDato(size.ToString());
            Class_nodos f = new Class_nodos();
            i.setDato(a);

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
        public void seFinal(Class_nodos b)
        {
            this.final = b;
        }




    }
}
