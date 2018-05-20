using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoubleRandomAdmin
{
    class Program
    {
        private static volatile bool _stopRequest = false;

        static void Main(string[] args)
        {
            DoubleRamdom.DoubleRandomClient client = new DoubleRamdom.DoubleRandomClient("NetTcpBinding_IDoubleRandom");
            client.SetTime(1000);

            Thread _workerThread = new Thread(Generate);
            _stopRequest = false;
            _workerThread.Start();

            Console.WriteLine("Генерация...");

            while(true)
            {
                Console.ReadLine();
                Console.WriteLine("Введите команду: ");
                string i = Console.ReadLine();
                switch (i)
                {
                    case "SetTime":
                        {
                            int res = int.Parse(Console.ReadLine());
                            client.SetTime(res);
                            Console.WriteLine("Новый таймфрейм установлен!");
                            break;
                        }
                    case "Close":
                        {
                            _stopRequest = true;
                            _workerThread.Abort();
                            client.Close();
                            Environment.Exit(0);
                            break;
                        }
                    case "Help":
                        {
                            Console.WriteLine("Help, список команд:");
                            Console.WriteLine("Установить новый таймфрейм - SetTime ");
                            Console.WriteLine("Выйход - Close ");
                            break;
                        }
                    default:
                        Console.WriteLine("Напишите команду Help");
                        break;
                }
            }
        }

        static void Generate()
        {
            while (!_stopRequest)
            {
                DoubleRamdom.DoubleRandomClient client = new DoubleRamdom.DoubleRandomClient("NetTcpBinding_IDoubleRandom");
                client.SetDoubleList(client.GetDoubleDom());
                Thread.Sleep(Convert.ToInt32(client.GetTime()));
                client.Close();
            }
        }
    }
}
