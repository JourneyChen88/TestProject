using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sort
{
    //class Program
    //{
    //    //static void Main(string[] args)
    //    //{
    //    //    B b = new B();
    //    //    b.PrintFields();
    //    //    Console.ReadLine();
    //    //}
    //    Class1
    //}
    class Program
    {

        private string str = "Class1.str";

        private int i = 0;

        static void StringConvert(string str)
        {

            str = "string being converted.";

        }

        static void StringConvert(Program c)
        {

            c.str = "string being converted.";

        }

        static void Add(int i)
        {

            i++;

        }

        static void AddWithRef(ref int i)
        {

            i++;

        }

        static void Main()
        {

            int i1 = 10;

            int i2 = 20;

            string str = "str";

            Program c = new Program();

            Add(i1);

            AddWithRef(ref i2);

            Add(c.i);

            StringConvert(str);

            StringConvert(c);

            Console.WriteLine(i1);

            Console.WriteLine(i2);

            Console.WriteLine(c.i);

            Console.WriteLine(str);

            Console.WriteLine(c.str);

            Console.ReadLine();

        }

    }

    class A

    {

        public A()
        {
            PrintFields();
        }
        public virtual void PrintFields() { }
    }


    class B : A
    {
        int x = 1;
        int y;
        public B() { y = -1; }


        public override void PrintFields()
        { Console.WriteLine("x={0},y={1}", x, y);
        }

    }
}
