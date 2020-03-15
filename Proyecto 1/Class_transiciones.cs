using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_1
{
    public class Class_transiciones
    {
        string nombre;
        string direccion;
        public Class_transiciones(){
            this.nombre = "";
            this.direccion = "";    
        }
        public Class_transiciones(string no,string dir)
        {
            this.nombre = no;
            this.direccion = dir;
        }

        public void setNombre(string a)
        {
            this.nombre = a;
        }
        public void setDireccion(string b)
        {
            this.nombre = b;
        }
        public string getNombre()
        {
            return nombre;
        }
        public string getDireccion()
        {
            return direccion;
        }









    }
}
