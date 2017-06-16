using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Taxi
{
    /// <summary>
    /// Базовый класс для всех авто.
    /// </summary>
    [Serializable]
    public abstract class Car
    {
        // Абстрактный класс от которого наследуются все кто хотят попасть в списки таксопарка.
        public string Model { get; protected set; } // const
        public ushort StartAge { get; protected set; } // const
        public string Number { get; set; }
        public string NumberBody { get; protected set; } // const 
        public Driver Driverst = new Driver();
        public uint SummaryMileage { get; protected set; }
        public bool Smoke { get; protected set; }
        public byte ChildPositions { get; protected set; }
        public bool Animals { get; protected set; } = false;
    }

    /// <summary>
    /// Класс в котором содержаться некоторые методы которые,
    /// должны быть в каждом классе наследнике, но не могут 
    /// быть в базовом классе.
    /// Этот класс можно было не делать, а просто добавить метод Run() в класс Car.
    /// Но в целях обучения пусть останется так.
    /// </summary>
    [Serializable]
    public abstract class CarIface : Car
    {
        public bool IsRun { get; protected set; } = false; // Флаг говорящий о том что авто на вызове.
        public bool IsServiced { get; set; } = true; // Признак пройденного ТО.
        public bool SetDriver { get; protected set; } = true;
        /// <summary>
        /// Параметры уже запущенной машины. 
        /// </summary>
        [Serializable]
        public struct RunningParams
        {
            public uint RunnungMile { get; set; }
            public uint RunnungTime { get; set; }
        }
        public RunningParams RunParams = new RunningParams();
        public abstract void Run();
        //public abstract void Stop();
    }

}
