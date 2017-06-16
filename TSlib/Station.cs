using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Reflection;

namespace CS_Taxi
{
    /// <summary>
    /// Класс-singleton описывающий Таксопарк. Типа singleton...
    /// </summary>
    [Serializable]
    public class TaxiStation
    {
        public static ushort TsDay { get; protected set; } = 180; // Кол-во дней между ТО.
        public static uint TsKm { get; protected set; } = 15000; // Кол-во километров между ТО.
        public List<Car> AllCars { get; protected set; } = new List<Car>(); // Список автомобилек на станции.
        public uint WreckerMilePrice { get; protected set; } = 0; // Цена за километр у Эвакуатора.
        public uint TaxiMilePrice { get; protected set; } = 0; // Цена за километр у Такси.
        public uint ResultMoney { get; set; } = 0;

        //TODO: Допилить работу с эвакуаторами.
        //TODO: Сделать на запущенные автошки отдельные потоки чтобы каждая авто останавливалась сама.

        /// <summary>
        ///  Исключение о неизвестном типе авто.
        /// </summary>
        internal class UnknownTypeOfCar : System.ApplicationException { }

        /// <summary>
        /// В данном конструкторе задается цена на километр эвакуатора 
        /// и километр такси.
        /// </summary>
        /// <param name="wreckerPrice">Цена за километр эвакуатора.</param>
        /// <param name="taxiPrice">Цена за километр такси.</param>
        public TaxiStation(uint wreckerPrice, uint taxiPrice)
        {
            LoggerMaster.LoggerM.Debug("In class: " + nameof(TaxiStation) + " : " + "Try call: " + MethodBase.GetCurrentMethod());

            this.WreckerMilePrice = wreckerPrice;
            this.TaxiMilePrice = taxiPrice;
        }

        /// <summary>
        /// Функция добавления нового экземпляра авто в список станции.
        /// </summary>
        /// <param name="someCar">Экземпляр класса Car.</param>
        public void AddCar(Car someCar)
        {
            LoggerMaster.LoggerM.Debug("In class: " + nameof(TaxiStation) + " : " + "Try call: " + MethodBase.GetCurrentMethod());

            //foreach (Car c in this.AllCars)
            //    if (someCar.Number == c.Number)
            //    {
            //        Console.WriteLine("Авто с таким гос. номером уже зарегистрирован.");
            //        return;
            //    }
            //    else if (someCar.NumberBody == c.NumberBody)
            //    {
            //        Console.WriteLine("Авто с таким VIN номером уже зарегистрирован.");
            //        return;
            //    }
            //    AllCars.Add(someCar);

            Car mCar = this.AllCars.FirstOrDefault(c => c.Number == someCar.Number || c.NumberBody == someCar.NumberBody);
            if (mCar == null)
            {
                AllCars.Add(someCar);
                return;
            }
            if (someCar.Number == mCar.Number)
            {
                Console.WriteLine("Авто с таким гос. номером уже зарегистрирован.");
                return;
            }
            if (someCar.NumberBody == mCar.NumberBody)
            {
                Console.WriteLine("Авто с таким VIN номером уже зарегистрирован.");
                return;
            }


        }

        /// <summary>
        /// Удалить авто из списка станции, используя гос. номер авто.
        /// </summary>
        /// <param name="delNumber"></param>
        /// <returns></returns>
        public bool DelCarViaNumber(string delNumber)
        {
            LoggerMaster.LoggerM.Debug("In class: " + nameof(TaxiStation) + " : " + "Try call: " + MethodBase.GetCurrentMethod());

            //foreach (Car c in AllCars)
            //    if (c.Number == delNumber)
            //    {
            //        AllCars.Remove(c);
            //        return true;
            //    }
            //return false;

            Car mCar = this.AllCars.FirstOrDefault(c => c.Number == delNumber);
            if (mCar != null)
            {
                AllCars.Remove(mCar);
                return true;
            }
            return false;

        }

        /// <summary>
        /// Удалить авто из списка станции, используя VIN номер.
        /// </summary>
        /// <param name="delNumber"></param>
        /// <returns></returns>
        public bool DelCarViaBodyNumber(string delNumber)
        {
            LoggerMaster.LoggerM.Debug("In class: " + nameof(TaxiStation) + " : " + "Try call: " + MethodBase.GetCurrentMethod());

            //foreach (Car c in AllCars)
            //    if (c.NumberBody == delNumber)
            //    {
            //        AllCars.Remove(c);
            //        return true;
            //    }
            //return false;

            Car mCar = this.AllCars.FirstOrDefault(c => c.NumberBody == delNumber);
            if (mCar != null)
            {
                AllCars.Remove(mCar);
                return true;
            }
            return false;


        }

        /// <summary>
        /// Найти авто по гос. номеру.
        /// </summary>
        /// <param name="searchNumber"></param>
        /// <returns></returns>
        public Car SearchCarViaNumber(string searchNumber)
        {
            LoggerMaster.LoggerM.Debug("In class: " + nameof(TaxiStation) + " : " + "Try call: " + MethodBase.GetCurrentMethod());

            return AllCars.FirstOrDefault(c => c.Number == searchNumber);
        }

        /// <summary>
        /// Найти авто по VIN номеру.
        /// </summary>
        /// <param name="searchNumber"></param>
        /// <returns></returns>
        public Car SearchCarViaBodyNumber(string searchNumber)
        {
            LoggerMaster.LoggerM.Debug("In class: " + nameof(TaxiStation) + " : " + "Try call: " + MethodBase.GetCurrentMethod());

            //foreach (Car c in AllCars)
            //    if (c.NumberBody == searchNumber)
            //    {
            //        return c;
            //    }
            //return null;

            return this.AllCars.FirstOrDefault(c => c.NumberBody == searchNumber);

        }

        /// <summary>
        /// Найти все автомобильки находящиеся на выполнении заказов.
        /// </summary>
        /// <returns></returns>
        public List<Car> GetCarsWorked()
        {
            LoggerMaster.LoggerM.Debug("In class: " + nameof(TaxiStation) + " : " + "Try call: " + MethodBase.GetCurrentMethod());

            //List<Car> carsInWork = new List<Car>();
            //foreach (Car c in AllCars)
            //    if ((c as CarIface).IsRun)
            //    {
            //        carsInWork.Add(c);
            //    }
            //return carsInWork;

            return (from car in this.AllCars where (car as CarIface).IsRun select car).ToList();

        }

        /// <summary>
        /// Найти все свободные автомобильки.
        /// </summary>
        /// <returns></returns>
        public List<Car> GetCarsFree()
        {
            LoggerMaster.LoggerM.Debug("In class: " + nameof(TaxiStation) + " : " + "Try call: " + MethodBase.GetCurrentMethod());

            //List<Car> carsFree = new List<Car>();
            //foreach (Car c in AllCars)
            //    if (!(c as CarIface).IsRun)
            //    {
            //        carsFree.Add(c);
            //    }
            //return carsFree;

            return (from car in this.AllCars where (car as CarIface).IsRun == false select car).ToList();

        }

        /// <summary>
        /// Найти все автомобили с поломкой.
        /// </summary>
        /// <returns></returns>
        public List<Car> GetBrokenCars()
        {
            LoggerMaster.LoggerM.Debug("In class: " + nameof(TaxiStation) + " : " + "Try call: " + MethodBase.GetCurrentMethod());

            var brokenCars = new List<Car>();
            foreach (var car in AllCars)
            {
                if (car is Wrecker wrecker && (wrecker.IsBrokenRun || wrecker.IsBrokenNoRun))
                    brokenCars.Add(car);
                if (car is Taxi taxi && taxi.IsBroken)
                    brokenCars.Add(car);
            }

            VeryBrokenWrekerNotification(brokenCars.Where(t => t is Wrecker wrecker && wrecker.IsBrokenNoRun));
            return brokenCars;
        }

        private void VeryBrokenWrekerNotification(IEnumerable<Car> brokenWrakers)
        {
            LoggerMaster.LoggerM.Debug("In class: " + nameof(TaxiStation) + " : " + "Try call: " + MethodBase.GetCurrentMethod());

            foreach (var brokenWraker in brokenWrakers)
            {
                Console.WriteLine("Wrecker " + brokenWraker.Number +
                                  " is very broken!"); // Предупреждение что нужен сложный ремонт.
            }
        }

        /// <summary>
        /// Найти все авто требующие планового ТО.
        /// </summary>
        /// <returns></returns>
        public List<Car> GetCarsWithoutService()
        {
            LoggerMaster.LoggerM.Debug("In class: " + nameof(TaxiStation) + " : " + "Try call: " + MethodBase.GetCurrentMethod());

            //List<Car> serviceCars = new List<Car>();
            //foreach (Car c in AllCars)
            //    if ((c as CarIface).IsServiced == false)
            //        serviceCars.Add(c);
            //return serviceCars;

            return (from car in this.AllCars where (car as CarIface).IsServiced == false select car).ToList();

        }

        /// <summary>
        /// Найти все свободные авто попадающие под определенные параметры. Только Таксишки!
        /// Работает методом исключения: сначала ищет все свободные авто 
        /// соответствующие заданному классу комфорта; 
        /// потом из полученного списка удаляет авто не совпадающие с заданными 
        /// параметрами(по одному параметру за раз).
        /// </summary>
        /// <param name="smoke"></param>
        /// <param name="childPositions"></param>
        /// <param name="animals"></param>
        /// <param name="carClass"></param>
        /// <returns></returns>
        public List<Car> GetFreeTaxi(bool smoke, byte childPositions, bool animals, Taxi.CarClases carClass = Taxi.CarClases.Base)
        {
            LoggerMaster.LoggerM.Debug("In class: " + nameof(TaxiStation) + " : " + "Try call: " + MethodBase.GetCurrentMethod());

            //List<Car> freeCars = new List<Car>();

            //foreach (Car c in AllCars)
            //    if ((c is Taxi) && !(c as Taxi).IsRun && (c as Taxi).CarClass == carClass)
            //        freeCars.Add(c);

            //if (smoke)
            //    foreach (Car c in freeCars.ToArray())
            //        if (!c.Smoke)
            //            freeCars.Remove(c);
            //if (childPositions > 0)
            //    foreach (Car c in freeCars.ToArray())
            //        if (c.ChildPositions < childPositions)
            //            freeCars.Remove(c);
            //if (animals)
            //    foreach (Car c in freeCars.ToArray())
            //        if (!c.Animals)
            //            freeCars.Remove(c);

            //return freeCars;

            return AllCars.Select(it => it as Taxi).Where(c => c != null &&
                                                                !c.IsRun &&
                                                                c.CarClass == carClass &&
                                                                c.Smoke == smoke &&
                                                                c.ChildPositions >= childPositions &&
                                                                c.Animals == animals).ToList<Car>();

        }

        /// <summary>
        /// Найти свободные эвакуаторы.
        /// </summary>
        /// <returns></returns>
        public List<Car> GetFreeWrecker()
        {
            //List<Car> freeCars = new List<Car>();

            //foreach (Car c in AllCars)
            //    if ((c is Wrecker) && !(c as Taxi).IsRun)
            //        freeCars.Add(c);

            //return freeCars;

            return (from car in this.AllCars where (car is Wrecker) && (car as CarIface).IsRun == false select car).ToList();

        }

        /*
        /// <summary>
        /// Запускает заданный автомобильчик в работу с указанием расстояния. 
        /// Учитываются наличие багажа и животных.
        /// Возвращает ориентировочную цену поездки.
        /// </summary>
        /// <param name="miles"></param>
        /// <param name="runCar"></param>
        /// <param name="baggage"></param>
        /// <param name="animals"></param>
        /// <returns></returns>
        public uint RunAsMileage(uint miles, Car runCar, bool baggage, bool animals)
        {
            uint priceResult = 0;
            if (runCar is Wrecker)
                priceResult = miles * this.WreckerMilePrice;
            else if (runCar is Taxi)
            {
                priceResult = miles * this.TaxiMilePrice;
                if (baggage)
                    priceResult += (priceResult / 100) * 10;
                if (animals)
                    priceResult += (priceResult / 100) * 30;
                (runCar as Taxi).RunParams.RunnungMile = miles;
            }

            (runCar as CarIface).Run();
            return priceResult;
        }
        */

        /// <summary>
        /// Запускает заданный автомобильчик в работу с указанием расстояния. 
        /// </summary>
        /// <param name="runCar"></param>
        /// <returns></returns>
        public uint RunAsMileage(Car runCar)
        {
            LoggerMaster.LoggerM.Debug("In class: " + nameof(TaxiStation) + " : " + "Try call: " + MethodBase.GetCurrentMethod());

            uint priceResult = 0;
            if (runCar is Wrecker)
                priceResult = RunWreckerAsMileage(runCar);
            else if (runCar is Taxi)
                priceResult = RunTaxiAsMileage(runCar);

            (runCar as CarIface).Run();
            return priceResult;
        }

        private uint RunWreckerAsMileage(Car runCar)
        {
            LoggerMaster.LoggerM.Debug("In class: " + nameof(TaxiStation) + " : " + "Try call: " + MethodBase.GetCurrentMethod());

            if (!(runCar as CarIface).SetDriver)
            {
                Console.WriteLine("У данного авто нет водителя. Выберите другой авто.");
                return 0;
            }

        ask1_runasmil:
            uint mileage;
            try
            {
                Console.Write("Введите предполагаемое расстояние: ");
                mileage = Convert.ToUInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Недопустимое значение.");
                goto ask1_runasmil;
            }
            uint priceResult = mileage * this.WreckerMilePrice;
            //(runCar as Taxi).RunParams.RunnungMile = mileage;
            return priceResult;
        }

        private uint RunTaxiAsMileage(Car runCar)
        {
            LoggerMaster.LoggerM.Debug("In class: " + nameof(TaxiStation) + " : " + "Try call: " + MethodBase.GetCurrentMethod());

            if (!(runCar as CarIface).SetDriver)
            {
                Console.WriteLine("У данного авто нет водителя. Выберите другой авто.");
                return 0;
            }

        ask1_runasmil:
            uint mileage;
            try
            {
                Console.Write("Введите предполагаемое расстояние: ");
                mileage = Convert.ToUInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Недопустимое значение.");
                goto ask1_runasmil;
            }

        ask2_runasmil:
            bool baggage;
            Console.Write("Есть багаж: y/n? : ");
            string tmp = Console.ReadLine().ToLower();
            if (tmp == "y")
                baggage = true;
            else if (tmp == "n")
                baggage = false;
            else
            {
                Console.WriteLine("Недопустимый ответ.");
                goto ask2_runasmil;
            }

        ask3_runasmil:
            bool anims;
            Console.Write("Есть животные: y/n? : ");
            tmp = Console.ReadLine().ToLower();
            if (tmp == "y")
                anims = true;
            else if (tmp == "n")
                anims = false;
            else
            {
                Console.WriteLine("Недопустимый ответ.");
                goto ask3_runasmil;
            }

            uint priceResult = mileage * this.TaxiMilePrice;
            if (baggage)
                priceResult += (priceResult / 100) * 10;
            if (anims)
                priceResult += (priceResult / 100) * 30;
            (runCar as Taxi).RunParams.RunnungMile = mileage;

            return priceResult;
        }

        /// <summary>
        /// Арендовать авто на заданное время. 
        /// Высчитываем стоимость минуты исходя из цены мили и средней скорости движения(по городу). 
        /// Если это Такси то учитывается наличие багажа и блохастых.
        /// </summary>
        /// <param name="timeMinutes"></param>
        /// <param name="city"></param>
        /// <param name="runCar"></param>
        /// <param name="baggage"></param>
        /// <param name="animals"></param>
        /// <returns></returns>
        public uint RunAsTime(uint timeMinutes, bool city, Car runCar, bool baggage, bool animals)
        {
            LoggerMaster.LoggerM.Debug("In class: " + nameof(TaxiStation) + " : " + "Try call: " + MethodBase.GetCurrentMethod());

            uint oneMinutePrice;
            uint priceResult;
            uint meanSpeed = 50; // Средняя скорость движения по городу.

            if (runCar is Wrecker)
            {
                oneMinutePrice = (meanSpeed * this.WreckerMilePrice) / 60;
                priceResult = timeMinutes * oneMinutePrice;
            }
            else if (runCar is Taxi)
            {
                oneMinutePrice = (meanSpeed * this.TaxiMilePrice);
                priceResult = timeMinutes * oneMinutePrice;
                if (baggage)
                    priceResult += (priceResult / 100) * 10;
                if (animals)
                    priceResult += (priceResult / 100) * 30;
            }
            else
            {
                throw new UnknownTypeOfCar();
            }
            (runCar as CarIface).Run();
            return priceResult;
        }

        /// <summary>
        /// Рассчитать примерную стоимость аренды автомобиля на время.
        /// Высчитываем стоимость минуты исходя из цены мили и средней скорости движения(по городу). 
        /// Если это Такси то учитывается наличие багажа и блохастых.
        /// </summary>
        /// <param name="time"></param>
        /// <param name="city"></param>
        /// <param name="carType"></param>
        /// <param name="baggage"></param>
        /// <param name="animals"></param>
        /// <returns></returns>
        public uint CalculatePriceAsTime(uint time, bool city, string carType, bool baggage, bool animals)
        {
            LoggerMaster.LoggerM.Debug("In class: " + nameof(TaxiStation) + " : " + "Try call: " + MethodBase.GetCurrentMethod());

            uint oneMinutePrice;
            uint priceResult;
            uint meanSpeed = 50; // Средняя скорость движения по городу.

            if (carType.ToLower() == "wrecker")
            {
                oneMinutePrice = (meanSpeed * this.WreckerMilePrice) / 60;
                priceResult = time * oneMinutePrice;
            }
            else if (carType.ToLower() == "taxi")
            {
                oneMinutePrice = (meanSpeed * this.TaxiMilePrice);
                priceResult = time * oneMinutePrice;
                if (baggage)
                    priceResult += (priceResult / 100) * 10;
                if (animals)
                    priceResult += (priceResult / 100) * 30;
            }
            else
            {
                throw new UnknownTypeOfCar();
            }
            return priceResult;
        }

        /// <summary>
        /// Рассчитать примерную стоимость поездки на заданное расстояние.
        /// </summary>
        /// <param name="miles"></param>
        /// <param name="city"></param>
        /// <param name="carType"></param>
        /// <param name="baggage"></param>
        /// <param name="animals"></param>
        /// <returns></returns>
        public uint CalculatePriceAsMileage(uint miles, bool city, string carType, bool baggage, bool animals)
        {
            LoggerMaster.LoggerM.Debug("In class: " + nameof(TaxiStation) + " : " + "Try call: " + MethodBase.GetCurrentMethod());

            uint priceResult;

            if (carType.ToLower() == "wrecker")
                priceResult = miles * this.WreckerMilePrice;
            else if (carType.ToLower() == "taxi")
            {
                priceResult = miles * this.TaxiMilePrice;
                if (baggage)
                    priceResult += (priceResult / 100) * 10;
                if (animals)
                    priceResult += (priceResult / 100) * 30;
            }
            else
            {
                throw new UnknownTypeOfCar();
            }

            return priceResult;
        }

        /// <summary>
        /// Сохранить базу в файл.
        /// </summary>
        public void SBase()
        {
            LoggerMaster.LoggerM.Debug("In class: " + nameof(TaxiStation) + " : " + "Try call: " + MethodBase.GetCurrentMethod());

            DBManage.SaveBase(AllCars, ResultMoney);
        }

        /// <summary>
        /// Загрузить базу из файла.
        /// </summary>
        public void LBase()
        {
            LoggerMaster.LoggerM.Debug("In class: " + nameof(TaxiStation) + " : " + "Try call: " + MethodBase.GetCurrentMethod());

            List<Car> tmpv;
            uint rmtmp;
            DBManage.LoadBase(out tmpv, out rmtmp);
            this.AllCars = tmpv;
            this.ResultMoney = rmtmp;
        }


    }

}


