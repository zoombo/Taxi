using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace CS_Taxi
{
    public static class DBManage
    {
        /// <summary>
        /// Сохранить базу автомобилек в файл.
        /// </summary>
        public static void SaveBase(List<Car> allCars, uint resultMoney, string fileBase = "data_base.bin") 
        {
            BinaryFormatter saver = new BinaryFormatter();
            using (FileStream fsd = new FileStream(fileBase, FileMode.OpenOrCreate))
            {
                saver.Serialize(fsd, allCars);
            }
            // Сохранить кассу.
            string fileCashbox = "data_cashbox.bin";
            using (FileStream fsd = new FileStream(fileCashbox, FileMode.OpenOrCreate))
            {
                saver.Serialize(fsd, resultMoney);
            }
        }

        /// <summary>
        /// Загрузить базу автомобилек из файла.
        /// </summary>
        public static void LoadBase(out List<Car> allCars, out uint resultMoney, string fileBase = "data_base.bin")
        {
            BinaryFormatter loader = new BinaryFormatter();
            using (FileStream fsd = new FileStream(fileBase, FileMode.OpenOrCreate))
            {
                allCars = (List<Car>)loader.Deserialize(fsd);
            }

            // Загрузить кассу из файла.
            string fileCashbox = "data_cashbox.bin";
            using (FileStream fsd = new FileStream(fileCashbox, FileMode.OpenOrCreate))
            {
                resultMoney = (uint)loader.Deserialize(fsd);
            }
        }

    }
}