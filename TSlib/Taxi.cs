using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CS_Taxi
{
    /// <summary>
    /// Исключение для метода Run. Пусть останется для примера.
    /// </summary>
    internal class RunException : System.ApplicationException
    {
        public enum type
        {
            AlreadyRunning = 1,
            NoDriver,
            IsBroken
        }
        public type Type { get; protected set; }
        public RunException(type t) { Type = t; }
        public RunException(type t, string message) : base(message) { Type = t; }
    }

    /// <summary>
    /// Исключение для метода Stop Класса Taxi.
    /// </summary>
    public class StopException : System.ApplicationException { }

    /// <summary>
    /// Класс описывающий один автомобиль типа "такси".
    /// </summary>
    [Serializable]
    public class Taxi : CarIface
    {
        public enum CarClases
        {
            Base = 100,
            Standard,
            Premium
        }
        public CarClases CarClass { get; protected set; } = CarClases.Base;
        //public bool IsRun { get; protected set; } = false; // Флаг говорящий о том что авто на вызове.
        //public bool is_broken { get; protected set; } = false;
        public uint MileageBeforeService { get; protected set; } = 0;
        public bool IsBroken { get; set; } = false; // Временно сделаем доступным на запись извне.
        //public bool IsServiced { get; protected set; } = true; // Признак пройденного ТО.
        public uint LastMileage { get; protected set; } = 0;
        public uint LastRoadTime { get; protected set; } = 0;
        // public bool set_driver { get; protected set; } = true;

        /*
        /// <summary>
        /// Параметры уже запущенной машины. 
        /// </summary>
        [Serializable]
        public struct RunningParams 
        {
            public uint RunnungMile { get; set; }
            public uint RunnungTime { get; set; }
            public bool Baggage { get; set; }
            public bool Animals { get; set; }
        }
        public RunningParams RunParams = new RunningParams();
        */

        /// <summary>
        /// Боливар с водилой в комплекте.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="startAge"></param>
        /// <param name="number"></param>
        /// <param name="numberBody"></param>
        /// <param name="driverSurname"></param>
        /// <param name="driverName"></param>
        /// <param name="driverName2"></param>
        /// <param name="mileage"></param>
        /// <param name="smoke"></param>
        /// <param name="childPositions"></param>
        /// <param name="carCl"></param>
        /// <param name="animals"></param>
        public Taxi(
            string model,
            ushort startAge,
            string number,
            string numberBody,
            string driverSurname,
            string driverName,
            string driverName2,
            uint mileage,
            bool smoke,
            byte childPositions,
            CarClases carCl = CarClases.Base,
            bool animals = false)
        {
            this.Model = model;
            this.StartAge = startAge;
            this.Number = number;
            this.NumberBody = numberBody;
            this.Driverst.DriverSurname = driverSurname;
            this.Driverst.DriverName = driverName;
            this.Driverst.DriverName2 = driverName2;
            base.SetDriver = true;
            this.SummaryMileage = mileage;
            this.Smoke = smoke;
            this.ChildPositions = childPositions;
            this.CarClass = carCl;
            this.Animals = animals;
        }

        /// <summary>
        /// Водилы в комплекте нет.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="startAge"></param>
        /// <param name="number"></param>
        /// <param name="numberBody"></param>
        /// <param name="mileage"></param>
        /// <param name="smoke"></param>
        /// <param name="childPositions"></param>
        /// <param name="carCl"></param>
        /// <param name="animals"></param>
        public Taxi(
            string model,
            ushort startAge,
            string number,
            string numberBody,
            uint mileage,
            bool smoke,
            byte childPositions,
            CarClases carCl = CarClases.Base,
            bool animals = false)
        {
            this.Model = model;
            this.StartAge = startAge;
            this.Number = number;
            this.NumberBody = numberBody;
            this.SummaryMileage = mileage;
            this.Smoke = smoke;
            this.ChildPositions = childPositions;
            this.CarClass = carCl;
            this.Animals = animals;
            base.SetDriver = false;
        }

        /// <summary>
        /// Если водилы у сего тарантаса нет, то тут его можно задать.
        /// </summary>
        /// <param name="driverSurname"></param>
        /// <param name="driverName"></param>
        /// <param name="driverName2"></param>
        public void SetDriver(
            string driverSurname,
            string driverName,
            string driverName2)
        {
            this.Driverst.DriverSurname = driverSurname;
            this.Driverst.DriverName = driverName;
            this.Driverst.DriverName2 = driverName2;
            base.SetDriver = true;
        }

        /// <summary>
        /// Отвязать водилу от авто.
        /// </summary>
        public void DelDriver()
        {
            this.Driverst.DriverSurname = null;
            this.Driverst.DriverName = null;
            this.Driverst.DriverName2 = null;
            base.SetDriver = false;
        }

        /// <summary>
        /// Запустить тарантас выполнять заказ.
        /// </summary>
        public override void Run()
        {
            if (this.IsRun)
            {
                RunException carIsRunningException = new RunException(RunException.type.AlreadyRunning);
                throw carIsRunningException;
            }
            else if (!base.SetDriver)
            {
                RunException noSetDriver = new RunException(RunException.type.NoDriver);
                throw noSetDriver;
            }
            else if (this.IsBroken)
            {
                RunException autoIsBroken = new RunException(RunException.type.IsBroken);
                throw autoIsBroken;
            }
            this.IsRun = true;
        }

        /// <summary>
        /// Функция которая должна дергаться со стороны водилы и возвращает словарь с
        /// результатами поездки "miles:<кол-во_пройденных_миль>" и "time:<время_в_дороге>".
        /// Пока дергается диспетчером.
        /// Говнокод. После прочтения - сжечь.
        /// </summary>
        /// <returns>
        /// Словарь с реально пройденным километражем и реальным временем в дороге.
        /// </returns>
        public Dictionary<string, uint> Stop()
        {
            if (this.IsRun)
            {
                this.IsRun = false;
                // DEBUG. Эмитация поездки.
                Random r = new Random();
                this.LastMileage = (uint)r.Next(0, 1000);
                if (this.LastMileage < this.RunParams.RunnungMile)
                    this.LastMileage = this.RunParams.RunnungMile;
                this.SummaryMileage += this.LastMileage;
                this.LastRoadTime = (uint)r.Next(0, 1440); // 24 часа в минутах.
                Dictionary<string, uint> result = new Dictionary<string, uint>();
                result.Add("miles", this.LastMileage);
                result.Add("time", this.LastRoadTime);


                this.MileageBeforeService += this.LastMileage;

                if (this.MileageBeforeService >= TaxiStation.TsKm)
                    this.IsServiced = false;
                if (r.Next(1, 50) == 1)
                {
                    this.IsBroken = true;
                    Console.WriteLine("Произошла поломка.");
                }
                Console.WriteLine(this.Number + " - Освобождена.");
                // END_DEBUG.
                return result;
            }
            else
            {
                StopException carIsStoped = new StopException();
                throw carIsStoped;
            }
        }

        /// <summary>
        /// Вывести инфу об автомобиле.
        /// </summary>
        /// <returns></returns>
        public string ShowInfo()
        {
            return (
                    "Тип: Такси" + "\n" +
                    "Модель: " + Model + "\n" +
                    "Год выпуска: " + Convert.ToString(StartAge) + "\n" +
                    "Гос. номер: " + Number + "\n" +
                    "VIN номер: " + NumberBody + "\n" +
                    "Фамилия водителя: " + Driverst.DriverSurname + "\n" +
                    "Имя водителя: " + Driverst.DriverName + "\n" +
                    "Отчество водителя: " + Driverst.DriverName2 + "\n" +
                    "Пробег: " + Convert.ToString(SummaryMileage) + "\n" +
                    "Курение: " + Convert.ToString(Smoke) + "\n" +
                    "Детских мест:" + Convert.ToString(ChildPositions) + "\n" +
                    "Животные: " + Convert.ToString(Animals) + "\n" +
                    "Класс авто: " + this.CarClass.ToString() + "\n"
                );
        }

    }
}

