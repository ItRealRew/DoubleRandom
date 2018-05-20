using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DoubleRandomHost
{
    class Program
    {
        static void Main()
        {
            using (var Host = new ServiceHost(typeof(DoubleRandom.DoubleRandom)))
            {
                Host.Open();
                Console.WriteLine("Хост работает...");
                Console.ReadLine();
            }
        }
    }
}
