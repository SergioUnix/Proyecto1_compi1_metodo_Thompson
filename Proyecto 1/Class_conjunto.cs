using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_1
{
    public class Class_conjunto
    {


        string nombre;
        string conjunto; ///va toda el conjunto aun no separado    2~9  ó  2,3,4,5,6
        string tipo;
        int inferior;   ///si es tipo rango existe un inferior en ascci
        int superior;    ///superior en ASCCI

        List<int> interI = new List<int>();   //intervalos de ints
        List<string> interC = new List<string>(); //intervalos de cadenas

        public List<int> getInterI() {
            return this.interI;
        }
        public Class_conjunto()
        {
            this.nombre = "";
            this.conjunto = "";
            this.tipo = "";
            this.inferior = 0;
            this.superior = 0;
        }

        public void addInterI(int ascci) {
            this.interI.Add(ascci);


        }


        public int getSuperior() {
            return this.superior;
        }
        public void setSuperior(int d) {
            this.superior = d;
        }
        public int getInferior() {
            return this.inferior; 
        }
        public void setInferior(int c) {
            this.inferior =c;

        }

        public  string getTipo() {
            return this.tipo;
        }
        public void setTipo(string c)
        {
            this.tipo = c;
        }
        public  string getConjunto()
        {
            return this.conjunto;
        }
        public void setConjunto(string b)
        {
            this.conjunto = b;
        }

        public void setNombre(string a)
        {
            this.nombre = a;
        }
        public string getNombre()
        {
            return this.nombre;
        }


        public void analizar() {
        string cadena_unix = "";
        char[] pap_char = conjunto.ToCharArray();
            /////clasifico
            if (conjunto.Length == 3 && (int)pap_char[1] == 126) {     ////si es de tamaño 3, puede ser un rango de letras o de numeros
                   //si el caracter en la posicion 1 es igual a ~ en valor ascci entonces
                    setTipo("Rango");
                    setInferior((int)pap_char[0]);   //seteo el intervalo inferior con el valor ascci
                    setSuperior((int)pap_char[2]);    //seteo el valor con el valor ascci

                for (int i = (int)pap_char[0]; i < (int)pap_char[2] + 1; i++) {
                    addInterI(i);
                }
                







            }
            else if (conjunto.Length >= 3)
            {   //si es mayor a 3 pueden ser varios valores  \t,\r,\n   o   a,b,c,d
                setTipo("Intervalo");
                for (int i = 0; i < pap_char.Count(); i++)
                {
                    if (pap_char[i] != ',') { cadena_unix = cadena_unix + pap_char[i]; } else if (pap_char[i] == ',') { if (cadena_unix != "") { addInterI((int)(cadena_unix.ToCharArray()[0]));  } cadena_unix = ""; }

                    if (cadena_unix == "\\n")
                    {
                        
                        addInterI(10); cadena_unix = "";
                    }
                    else if (cadena_unix == "\'")
                    {
                        addInterI((int)'\''); cadena_unix = "";
                    }
                    else if (cadena_unix == "\\\"")
                    {
                        addInterI((int)'"'); cadena_unix = "";
                    }
                    else if (cadena_unix == "\\t") { addInterI(11); cadena_unix = ""; }

                    if (i == pap_char.Count() - 1 && cadena_unix!="") { addInterI((int)(cadena_unix.ToCharArray()[0])); } 

                    

                }//cierro el for


            }
            else if (conjunto.Length < 3)
            {   //si es menor pueden ser varios valores  \n   ó  a  ó  1 
                setTipo("Caracter");
                for (int i = 0; i < pap_char.Count(); i++)
                {
                    if (pap_char[i] != ',') { cadena_unix = cadena_unix + pap_char[i]; } else if (pap_char[i] == ',') { if (cadena_unix != "") { addInterI((int)(cadena_unix.ToCharArray()[0])); } cadena_unix = ""; }

                    if (cadena_unix == "\\n")
                    {

                        addInterI(10); cadena_unix = "";
                    }
                    else if (cadena_unix == "\'")
                    {
                        addInterI((int)'\''); cadena_unix = "";
                    }
                    else if (cadena_unix == "\\\"")
                    {
                        addInterI((int)'"'); cadena_unix = "";
                    }
                    else if (cadena_unix == "\\t") { addInterI(11); cadena_unix = ""; }

                    if (i == pap_char.Count() - 1 && cadena_unix != "") { addInterI((int)(cadena_unix.ToCharArray()[0])); }



                }//cierro el for


            }

        }











    }

}
