using System;

namespace CS_Taxi
{
    static class HelperFunctions
    {
        /// <summary>
        /// Создание нового автомобиля. Инфа вводится руками.
        /// </summary>
        /// <returns></returns>
        public static Car NewCar()
        {
            string setDriver;
            string driverSurname;
            string driverName;
            string driverName2;

            string model;
            ushort startAge = 0;
            string number;
            string numberBody;
            uint mileage;
            bool smoke;
            byte childPositions;
            Taxi.CarClases carClases;
            bool animals;

            Console.WriteLine("Добавление нового авто.");

            Console.Write("Модель: ");
            model = Console.ReadLine().ToLower();

            // year_loop:
            bool iscorrect = false;
            while (!iscorrect)
                try
                {
                    Console.Write("Год выпуска: ");
                    startAge = Convert.ToUInt16(Console.ReadLine());
                    iscorrect = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Недопустимое значение.");
                    // goto year_loop;
                }

            Console.Write("Номерной знак: ");
            number = Console.ReadLine().ToLower();

            Console.Write("VIN номер: ");
            numberBody = Console.ReadLine().ToLower();

            mileage_loop:
            try
            {
                Console.Write("Пробег: ");
                mileage = Convert.ToUInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Недопустимое значение.");
                goto mileage_loop;
            }

            smoke_loop:
            Console.Write("Курение разрешено: y/n? : ");
            string yesNoSmoke = Console.ReadLine().ToLower();
            if (yesNoSmoke == "y")
                smoke = true;
            else if (yesNoSmoke == "n")
                smoke = false;
            else
            {
                Console.WriteLine("Недопустимый ответ.");
                goto smoke_loop;
            }

            child_loop:
            try
            {
                Console.Write("Детских мест: ");
                childPositions = Convert.ToByte(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Недопустимое значение.");
                goto child_loop;
            }

            car_class_loop:
            Console.Write("Класс авто: BASE(1)/STANDARD(2)/PREMIUM(3) : ");
            string carC = Console.ReadLine();
            if (carC == "1")
                carClases = Taxi.CarClases.Base;
            else if (carC == "2")
                carClases = Taxi.CarClases.Standard;
            else if (carC == "3")
                carClases = Taxi.CarClases.Premium;
            else
            {
                Console.WriteLine("Такого класса нет.");
                goto car_class_loop;
            }

            animals_loop:
            Console.Write("Возможность проезда с животными: y/n? : ");
            string setAnim = Console.ReadLine().ToLower();
            if (setAnim == "y")
                animals = true;
            else if (setAnim == "n")
                animals = false;
            else
            {
                Console.WriteLine("Недопустимый ответ.");
                goto animals_loop;
            }

            driver_loop:
            Console.Write("Водитель в комплекте: y/n? : ");
            setDriver = Console.ReadLine().ToLower();
            if (setDriver == "y")
            {
                Console.Write("Фамилия: ");
                driverSurname = Console.ReadLine().ToLower();

                Console.Write("Имя: ");
                driverName = Console.ReadLine().ToLower();

                Console.Write("Отчество: ");
                driverName2 = Console.ReadLine().ToLower();

                return new Taxi(model,
                    startAge,
                    number,
                    numberBody,
                    driverSurname,
                    driverName,
                    driverName2,
                    mileage, smoke,
                    childPositions,
                    carClases,
                    animals);
            }
            else if (setDriver == "n")
                return new Taxi(model,
                    startAge,
                    number,
                    numberBody,
                    mileage, smoke,
                    childPositions,
                    carClases,
                    animals);
            else
            {
                Console.WriteLine("Недопустимый ответ.");
                goto driver_loop;
            }
        }
        /// <summary>
        /// Этот рандом для функции CarGenerator.
        /// </summary>
        static Random _r = new Random();
        /// <summary>
        /// Функция для генерации автомобилек.
        /// </summary>
        /// <returns></returns>
        public static Taxi CarGenerator()
        {

            string RChar()
            {
                char[] chars = { 'a', 'b', 'c', 'e', 'h', 'k', 'm', 'n', 'o' };
                return Convert.ToString(chars[_r.Next(0, chars.Length)]);
            }

            bool RBool()
            {
                if (_r.Next(0, 2) == 1)
                    return true;
                return false;
            }

            Taxi.CarClases Rtc()
            {
                int tmp = _r.Next(1, 4);
                if (tmp == 1)
                    return Taxi.CarClases.Base;
                if (tmp == 2)
                    return Taxi.CarClases.Standard;
                return Taxi.CarClases.Premium;
            }

            string[] model = { "lifan", "bmw", "lada", "chevrolet", "fiat", "honda", "kia" };
            ushort startAge = Convert.ToUInt16(_r.Next(1950, 2018));
            string number = RChar() + _r.Next(100, 1000) + RChar() + RChar();

            string numberBody = Convert.ToString(_r.Next(100, 1000)) +
                                (RChar() + RChar() + RChar()) +
                                Convert.ToString(_r.Next(100, 1000)) +
                                (RChar() + RChar() + RChar()) +
                                Convert.ToString(_r.Next(100, 1000)) +
                                (RChar() + RChar() + RChar());

            string[] driverSurname = { "иванов", "смирнов", "кузнецов", "попов", "васильев", "петров", "соколов", "михайлов", "новиков", "федоров", "морозов" };
            string[] driverName = { "василий", "георгий", "геннадий", "юрий", "виктор", "дмитрий", "алексей" };
            string[] driverName2 = { "иванович", "петрович", "васильевич", "георгиевич", "геннадьевич", "дмитриевич", "алексеевич" };

            uint mileage = (uint)_r.Next(1, 800000);
            bool smoke = RBool();
            byte childPositions = (byte)_r.Next(0, 6);
            Taxi.CarClases carCl = Rtc();
            bool animals = RBool();
            if (RBool())
                return new Taxi(model[_r.Next(0, model.Length)], startAge, number,
                    numberBody, driverSurname[_r.Next(0, driverSurname.Length)],
                    driverName[_r.Next(0, driverName.Length)],
                    driverName2[_r.Next(0, driverName2.Length)],
                    mileage, smoke, childPositions,
                    carCl, animals);
            else
                return new Taxi(model[_r.Next(0, model.Length)],
                    startAge, number,
                    numberBody, mileage,
                    smoke, childPositions,
                    carCl, animals);
        }
    }
}
