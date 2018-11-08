using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BionicTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //System.Console.WriteLine("Ciao Mondo!");
            //System.Console.ReadLine();

           

            Animale a = new Animale("Pippo");
            a.VisualizzaNome();
            Tasso b = new Tasso();
            b.VisualizzaNome();
            b.Ruggisci();
            Animale c = new Tasso();
            c.FaiUnVerso();
            int num1 = 10;
            int num2 = 0;
            try {
                double risultato = DivisioneNumeri(num1, num2);
                Console.WriteLine(risultato);
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine("ZORODIVISION:" + e.Message);
            }
            catch (ErrorLog e){}
            finally{
                Console.WriteLine("Non tutto viene con il buco");
            }


        }

        public static double DivisioneNumeri(int a, int b)
        {
            if (b == 0) throw new ErrorLog("ERRORE!!!!!!");
            return a / b;
        }
    }
}