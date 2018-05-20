using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DoubleRandom
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "DoubleRandom" в коде и файле конфигурации.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class DoubleRandom : IDoubleRandom
    {
        List<double> DoubleList = new List<double>();
        int Time;

        void IDoubleRandom.CreateDoubleList()
        {
            DoubleList.Add(1.34);
        }

        double IDoubleRandom.GetDoubleDom()
        {
            Random rnd = new Random();
            return Math.Round(-100.132 + rnd.NextDouble() * (100.003 + 100.132), 13);
        }

        List<double> IDoubleRandom.GetDoubleList()
        {
            return DoubleList;
        }

        int IDoubleRandom.GetTime()
        {
            return Time;
        }

        void IDoubleRandom.SetDoubleList(double Tolist)
        {
            DoubleList.Add(Tolist);
        }

        void IDoubleRandom.SetTime(int Timing)
        {
            Time = Timing;
        }
    }
}
