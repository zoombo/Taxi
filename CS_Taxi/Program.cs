using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using System.Reflection;
using System.Runtime.CompilerServices;


namespace CS_Taxi
{
    static class Program
    {

        static void help()
        {
            LoggerMaster.LoggerM.Debug("In class: " + nameof(Program) + " : " + "Try call: " + MethodBase.GetCurrentMethod());

            string commands =
                "\n" +
                "help                               \n" +
                "exit                               \n" +
                "clear                              \n" +
                "###############################################################\n" +
                "a, AddCar                          Добавить новый авто.        \n" +
                "sd, SetDriver                      Прикрепить водителя к авто. \n" +
                "d, DelCarViaNumber                 Удалить авто по номеру.     \n" +
                "dvb, DelCarViaBodyNumber           Удалить авто по VIN номеру. \n" +
                "s, SearchCarViaNumber              Найти авто по номеру.       \n" +
                "scbn, SearchCarViaBodyNumber       Найти авто по VIN номеру.   \n" +
                "gw, GetCarsWorked                  Найти авто в работе.        \n" +
                "gf, GetCarsFree                    Найти все свободные авто.   \n" +
                "gft, GetFreeTaxi                   Найти свободные такси.      \n" +
                "gfw, GetFreeWrecker                Найти свободные эвакуаторы. \n" +
                "gb, GetBrokenCars                  Найти авто с поломкой.      \n" +
                "gws, GetCarsWithoutService         Найти авто без ТО.          \n" +
                "r, RunAsMileage                    Запустить авто на заказ     \n" +
                "                                   с указанием расстояния.     \n" +
                "st, StopCar                        Авто вернулось с заказа.    \n" +
                "stl, ShowTmpList                   Показать результаты поиска. \n" +
                "scc, ShowCurrentCar                Показать выбранный авто.    \n" +
                "sa, ShowAllCars                    Показать все авто.          \n" +
                "gs, GetState                       Текущее состояние парка.    \n";
            //+
            // "SaveBase\n" +
            // "LoadBase";
            Console.WriteLine(commands);

            LoggerMaster.LoggerM.Debug("In class: " + nameof(Program) + " : " + "Was called: " + MethodBase.GetCurrentMethod());
        }

        static void SetDriver(ref Car tmpCar)
        {
            LoggerMaster.LoggerM.Debug("In class: " + nameof(Program) + " : " + "Try call: " + MethodBase.GetCurrentMethod());

            Console.Write("Фамилия: ");
            string driverSurname = Console.ReadLine().ToLower();

            Console.Write("Имя: ");
            string driverName = Console.ReadLine().ToLower();

            Console.Write("Отчество: ");
            string driverName2 = Console.ReadLine().ToLower();

            (tmpCar as Taxi).SetDriver(driverSurname, driverName, driverName2);

            LoggerMaster.LoggerM.Debug("In class: " + nameof(Program) + " : " + "Was called: " + MethodBase.GetCurrentMethod());
        }

        static void DelCarViaNumber(TaxiStation footInHands)
        {
            LoggerMaster.LoggerM.Debug("In class: " + nameof(Program) + " : " + "Try call: " + MethodBase.GetCurrentMethod());

            Console.Write("Введите гос. номер авто: ");
            bool status = footInHands.DelCarViaNumber(Console.ReadLine().ToLower());
            if (!status)
                Console.WriteLine("Удалить авто не получилось.");

            LoggerMaster.LoggerM.Debug("In class: " + nameof(Program) + " : " + "Was called: " + MethodBase.GetCurrentMethod());
        }

        static void DelCarViaBodyNumber(TaxiStation footInHands)
        {
            LoggerMaster.LoggerM.Debug("In class: " + nameof(Program) + " : " + "Try call: " + MethodBase.GetCurrentMethod());

            Console.Write("Введите VIN номер авто: ");
            bool status = footInHands.DelCarViaBodyNumber(Console.ReadLine().ToLower());
            if (!status)
                Console.WriteLine("Удалить авто не получилось.");

            LoggerMaster.LoggerM.Debug("In class: " + nameof(Program) + " : " + "Was called: " + MethodBase.GetCurrentMethod());
        }

        static void SearchCarViaNumber(TaxiStation footInHands, ref Car tmpCar)
        {
            LoggerMaster.LoggerM.Debug("In class: " + nameof(Program) + " : " + "Try call: " + MethodBase.GetCurrentMethod());

            Console.Write("Введите гос. номер авто: ");
            tmpCar = footInHands.SearchCarViaNumber(Console.ReadLine().ToLower());
            if (tmpCar == null)
                Console.WriteLine("Авто не найдено.");

            LoggerMaster.LoggerM.Debug("In class: " + nameof(Program) + " : " + "Was called: " + MethodBase.GetCurrentMethod());
        }

        static void SearchCarViaBodyNumber(TaxiStation footInHands, ref Car tmpCar)
        {
            LoggerMaster.LoggerM.Debug("In class: " + nameof(Program) + " : " + "Try call: " + MethodBase.GetCurrentMethod());

            Console.Write("Введите VIN номер авто: ");
            tmpCar = footInHands.SearchCarViaBodyNumber(Console.ReadLine().ToLower());
            if (tmpCar == null)
                Console.WriteLine("Авто не найдено.");

            LoggerMaster.LoggerM.Debug("In class: " + nameof(Program) + " : " + "Was called: " + MethodBase.GetCurrentMethod());
        }



        static void GetFreeTaxi(TaxiStation footInHands, ref List<Car> tmpCarsList)
        {
            LoggerMaster.LoggerM.Debug("In class: " + nameof(Program) + " : " + "Try call: " + MethodBase.GetCurrentMethod());

            string tmp;

        ask1_loop:
            Console.Write("Курящий салон: y/n? : ");
            tmp = Console.ReadLine().ToLower();
            bool smoke = false;
            if (tmp == "y")
                smoke = true;
            else if (tmp == "n")
                smoke = false;
            else
            {
                Console.WriteLine("Недопустимый ответ.");
                goto ask1_loop;
            }

        ask2_loop:
            Console.Write("Есть животные: y/n? : ");
            tmp = Console.ReadLine().ToLower();
            bool animals;
            if (tmp == "y")
                animals = true;
            else if (tmp == "n")
                animals = false;
            else
            {
                Console.WriteLine("Недопустимый ответ.");
                goto ask2_loop;
            }

        ask3_loop:
            byte childs;
            try
            {
                Console.Write("Количество детских мест: ");
                childs = Convert.ToByte(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Недопустимое значение.");
                goto ask3_loop;
            }
            catch (OverflowException)
            {
                Console.WriteLine("Недопустимое значение.");
                goto ask3_loop;
            }

            Taxi.CarClases cCl;
            Console.Write("Класс авто: BASE(1)/STANDARD(2)/PREMIUM(3) : ");
            string ccla = Console.ReadLine().ToLower();
            if (ccla == "1" || ccla == "base")
                cCl = Taxi.CarClases.Base;
            else if (ccla == "2" || ccla == "standard")
                cCl = Taxi.CarClases.Standard;
            else if (ccla == "3" || ccla == "premium")
                cCl = Taxi.CarClases.Premium;
            else
            {
                cCl = Taxi.CarClases.Base;
                Console.WriteLine("Класс не определен. Выбран класс BASE.");
            }

            tmpCarsList = footInHands.GetFreeTaxi(smoke, childs, animals, cCl);

            LoggerMaster.LoggerM.Debug("In class: " + nameof(Program) + " : " + "Was called: " + MethodBase.GetCurrentMethod());
        }

        static void RunAsMileage(TaxiStation footInHands, ref Car tmpCar)
        {
            LoggerMaster.LoggerM.Debug("In class: " + nameof(Program) + " : " + "Try call: " + MethodBase.GetCurrentMethod());

            if (tmpCar == null)
            {
                Console.WriteLine("Не выбран авто.");
                return;
            }

            if (!(tmpCar as CarIface).SetDriver)
            {
                Console.WriteLine("У данного авто нет водителя. Выберите другой авто.");
                return;
            }

            if (tmpCar != null)
                footInHands.ResultMoney += footInHands.RunAsMileage(tmpCar);
            else
                Console.WriteLine("Не выбран автомобиль.");


            LoggerMaster.LoggerM.Debug("In class: " + nameof(Program) + " : " + "Was called: " + MethodBase.GetCurrentMethod());
        }


        static void Main(string[] args)
        {
            LoggerMaster.LoggerM.Debug("Program started.");

            TaxiStation footInHands = new TaxiStation(40, 15);
            try
            {
                footInHands.LBase();
            }
            catch (System.Runtime.Serialization.SerializationException ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //// Это рабочая генерация автомобилей!
            //for (ushort i = 0; i < 500; i++)
            //    footInHands.AddCar(HelperFunctions.CarGenerator());

            //// Пока сделаем все машинки с пройденным ТО.
            //foreach (Car c in footInHands.AllCars)
            //    (c as Taxi).IsServiced = true;

            //foreach (Car c in FootInHands.all_cars)
            //    if (c.number == "b718kc")
            //        (c as Taxi).is_serviced = false;

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



            List<Car> tmpCarsList = new List<Car>();
            Car tmpCar = null;
            bool exitState = false;
            while (!exitState)
            {
                string cmd;
                Console.Write("::::> ");
                cmd = Console.ReadLine().ToLower();
                LoggerMaster.LoggerM.Debug("User cmd: " + cmd);

                switch (cmd)
                {
                    case "h":
                    case "help":
                        //_mlogger.Debug("Call: " + nameof(help));
                        help();
                        break;
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    case "exit":
                        LoggerMaster.LoggerM.Debug("Exit.");
                        exitState = true;
                        break;
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    case "clear":
                        //_mlogger.Debug("Call: " + nameof(Console.Clear));
                        Console.Clear();
                        break;
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    case "a":
                    case "add" + "car": // AddCar
                        //_mlogger.Debug("Call: " + nameof(footInHands.AddCar));
                        footInHands.AddCar(HelperFunctions.NewCar());
                        break;
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    case "sd":
                    case "set" + "driver": // SetDriver
                        //_mlogger.Debug("Call: " + nameof(SetDriver));
                        SetDriver(ref tmpCar);
                        break;
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    case "d":
                    case "del" + "car" + "via" + "number": // DelCarViaNumber 
                        //_mlogger.Debug("Call: " + nameof(DelCarViaNumber));
                        DelCarViaNumber(footInHands);
                        break;
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    case "dvb":
                    case "del" + "car" + "via" + "bodynumber": // DelCarViaBodyNumber
                        //_mlogger.Debug("Call: " + nameof(DelCarViaBodyNumber));
                        DelCarViaBodyNumber(footInHands);
                        break;
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    case "s":
                    case "search" + "car" + "via" + "number": // SearchCarViaNumber
                        //_mlogger.Debug("Call: " + nameof(SearchCarViaNumber));
                        SearchCarViaNumber(footInHands, ref tmpCar);
                        break;
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    case "scbn":
                    case "search" + "car" + "via" + "bodynumber": // SearchCarViaBodyNumber
                        SearchCarViaBodyNumber(footInHands, ref tmpCar);
                        break;
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    case "gw":
                    case "get" + "cars" + "worked": // GetCarsWorked
                        tmpCarsList = footInHands.GetCarsWorked();
                        if (tmpCarsList.Count == 0)
                            Console.WriteLine("Ничего не найдено.");
                        break;
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    case "gf":
                    case "get" + "cars" + "free": // GetCarsFree
                        tmpCarsList = footInHands.GetCarsFree();
                        if (tmpCarsList.Count == 0)
                            Console.WriteLine("Ничего не найдено.");
                        break;
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    case "gb":
                    case "get" + "broken" + "cars": // GetBrokenCars
                        tmpCarsList = footInHands.GetBrokenCars();
                        if (tmpCarsList.Count == 0)
                            Console.WriteLine("Сломанных авто не найдено.");
                        break;
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    case "gws":
                    case "get" + "cars" + "without" + "service": //GetCarsWithoutService
                        tmpCarsList = footInHands.GetCarsWithoutService();
                        break;
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    case "gft":
                    case "get" + "free" + "taxi": // GetFreeTaxi
                        GetFreeTaxi(footInHands, ref tmpCarsList);
                        break;
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    case "gfw":
                    case "get" + "free" + "wrecker":
                        tmpCarsList = footInHands.GetFreeWrecker();
                        if (tmpCarsList.Count == 0)
                            Console.WriteLine("Ничего не найдено.");
                        break;
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    case "r":
                    case "run" + "as" + "mileage": // RunAsMileage
                        RunAsMileage(footInHands, ref tmpCar);
                        break;
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    case "st":
                    case "stop" + "car": // StopCar 

                        // Dictionary<string, uint> run_result;
                        // run_result = (tmp_car as Taxi).Stop();
                        //uint result_miles;
                        //result_miles = run_result["miles"];
                        try
                        {
                            (tmpCar as Taxi).Stop();
                        }
                        catch (StopException)
                        {
                            Console.WriteLine("Немогу остановить авто - оно уже остановлено.");
                        }
                        break;
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    case "runasmileage-rand": // RunAsMileage
                        break;
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    case "stl":
                    case "show" + "tmp" + "list": // ShowTmpList - отобразить tmp_cars_list.
                        foreach (Car c in tmpCarsList)
                            Console.WriteLine((c as Taxi).ShowInfo());
                        break;
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    case "scc":
                    case "show" + "current" + "car": // ShowCurrentCar
                        if (tmpCar != null)
                            Console.WriteLine((tmpCar as Taxi).ShowInfo());
                        else
                            Console.WriteLine("Нет выбранных авто.");
                        break;
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    case "sa":
                    case "show" + "all" + "cars":
                        foreach (Car c in footInHands.AllCars)
                            Console.WriteLine((c as Taxi).ShowInfo());
                        break;
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    case "gs":
                    case "get" + "state": // GetState
                        Console.WriteLine(
                            "Всего авто зарегистрировано: " + Convert.ToString(footInHands.AllCars.Count) + "\n" +
                            "Автошек сломано: " + Convert.ToString(footInHands.GetBrokenCars().Count) + "\n" +
                            "Выручка: " + Convert.ToString(footInHands.ResultMoney) + "\n" +
                            "Автошек требуют ТО: " + Convert.ToString(footInHands.GetCarsWithoutService().Count) + "\n"
                            );

                        break;


                    //case "savebase":
                    //    FootInHands.SaveBase();
                    //    break;
                    //case "l":
                    //case "loadbase":
                    //    FootInHands.LoadBase();
                    //    break;

                    default:
                        break;
                }


            }

            footInHands.SBase();
            return;

        }
    }



    //class Program
    //{
    //    static void Main(string[] args)
    //    {

    //        TaxiStation Station = new TaxiStation(40, 15);
    //        if (TaxiStation.ts_day == 0)
    //            Console.WriteLine("TS_day in null.");
    //        else if (TaxiStation.ts_km == 0)
    //            Console.WriteLine("TS_km in null.");



    //        Taxi car1 = new Taxi();
    //        try
    //        {
    //            car1.Run();
    //        }
    //        catch (RunException ex) when (ex.Type == RunException.type.AlreadyRunning)
    //        {
    //            Console.WriteLine("Это авто еще на выезде.");
    //        }
    //        catch (RunException ex) when (ex.Type == RunException.type.NoDriver)
    //        {
    //            Console.WriteLine("У этого авто нет водителя.");
    //        }



    //        try
    //        {
    //            car1.Stop();
    //        }
    //        catch (StopException)
    //        {

    //        }


    //    }
    //}

}