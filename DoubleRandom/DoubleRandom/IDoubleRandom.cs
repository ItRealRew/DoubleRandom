using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DoubleRandom
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IDoubleRandom" в коде и файле конфигурации.
    [ServiceContract]
    public interface IDoubleRandom
    {
        [OperationContract]
        double GetDoubleDom();

        [OperationContract]
        List<double> GetDoubleList();

        [OperationContract]
        void SetDoubleList(double Tolist);

        [OperationContract]
        void CreateDoubleList();

        [OperationContract]
        void SetTime(int Timing);

        [OperationContract]
        int GetTime();
    }
}
