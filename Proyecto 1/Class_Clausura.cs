using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_1
{
    public class Class_Clausura
    {
        string Id;
        List<string> sub1;
        List<string> sub2;

        string caracter_encontrado;

        Boolean aceptacion;




        public Class_Clausura()
        {
            this.Id = "";
            this.sub1 = new List<string>();
            this.sub2 = new List<string>();
            this.caracter_encontrado = "";
            this.aceptacion = false;
        }


        public void setAceptacion()
        {
            this.aceptacion = true;
        }

        public string getSub1_imprimir()
        {
            string result = "";
            for (int i = 0; i < this.sub1.Count(); i++) { result = result + this.sub1[i] + ","; }
            return result;

        }
        public string getSub2_imprimir()
        {
            string result = "";
            for (int i = 0; i < this.sub2.Count(); i++) { result = result + this.sub2[i] + ","; }
            return result;

        }

        public string getcaracter_encontrado()
        { return this.caracter_encontrado; }


        public void setcaracter_encontrado(string a)
        { this.caracter_encontrado = a; }

        public string getID()
        { return this.Id; }
        public void setID(string a)
        {
            this.Id = a;
        }
        public List<string> getSub1()
        {
           return this.sub1;

        }
        public List<string> getSub2()
        {
           return  this.sub2;
        }

        public void addSub1(string v) {
            sub1.Add(v);

        }
        public void addSub1(List<string> b)
        {
            this.sub1 = b;

        }



        public void addSub2(string b)
        {
            sub2.Add(b);

        }
        public void addSub2(List<string> b)
        {
            this.sub2=b;

        }



    }
}
