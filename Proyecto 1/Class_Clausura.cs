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




        public Class_Clausura()
        {
            this.Id = "";

        }
        public string getID()
        { return this.Id; }
        public void setID(string a)
        {
            this.Id = a;
        }

        public void addSub1(string b) {
            sub1.Add(b);

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
