using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CS_Taxi;

namespace CS_Test_TSlib
{

    [TestClass]
    public class UnitTest1
    {


        /// <summary>
        /// Тест проверяющий что цены задались правильно.
        /// </summary>
        [TestMethod]
        public void TestConstructor()
        {
            TaxiStation testSStation = new TaxiStation(55, 25);

            Assert.AreEqual(testSStation.WreckerMilePrice, (uint)55);
            Assert.AreEqual(testSStation.TaxiMilePrice, (uint)25);
        }

        /// <summary>
        /// Тест проверяющий что авто было добавлено.
        /// </summary>
        [TestMethod]
        public void TestAddCar()
        {
            TaxiStation testSStation = new TaxiStation(55, 25);

            // Проверяем что количество машинок 0.
            Assert.AreEqual(testSStation.AllCars.Count, 0);

            Taxi testTaxiCar = new Taxi("honda",
                                        1985,
                                        "c433mn",
                                        "156bmk239cne207khh",
                                        "morozov",
                                        "uri",
                                        "dmitrievich",
                                        672006,
                                        true,
                                        2,
                                        Taxi.CarClases.Standard,
                                        false);

            testSStation.AddCar(testTaxiCar);
            // Теперь должно быть 1.
            Assert.AreEqual(testSStation.AllCars.Count, 1);

        }

        /// <summary>
        /// Тест метода TaxiStation.SearchCarViaNumber().
        /// </summary>
        [TestMethod]
        public void TestSearchCar()
        {
            TaxiStation testSStation = new TaxiStation(55, 25);

            Taxi testTaxiCar = new Taxi("honda",
                                        1985,
                                        "c433mn",
                                        "156bmk239cne207khh",
                                        "morozov",
                                        "uri",
                                        "dmitrievich",
                                        672006,
                                        true,
                                        2,
                                        Taxi.CarClases.Standard,
                                        false);

            testSStation.AddCar(testTaxiCar);

            var tmpCar = testSStation.SearchCarViaNumber("c433mn");

            Taxi tmpCarAfterCast = tmpCar as Taxi;

            Assert.AreEqual(tmpCarAfterCast.Model, "honda");
            Assert.AreEqual(tmpCarAfterCast.StartAge, (ushort)1985);
            Assert.AreEqual(tmpCarAfterCast.Number, "c433mn");
            Assert.AreEqual(tmpCarAfterCast.NumberBody, "156bmk239cne207khh");
            Assert.AreEqual(tmpCarAfterCast.Driverst.DriverSurname, "morozov");
            Assert.AreEqual(tmpCarAfterCast.Driverst.DriverName, "uri");
            Assert.AreEqual(tmpCarAfterCast.Driverst.DriverName2, "dmitrievich");
            Assert.AreEqual(tmpCarAfterCast.SummaryMileage, (uint)672006);
            Assert.AreEqual(tmpCarAfterCast.Smoke, true);
            Assert.AreEqual(tmpCarAfterCast.ChildPositions, 2);
            Assert.AreEqual(tmpCarAfterCast.CarClass, Taxi.CarClases.Standard);
            Assert.AreEqual(tmpCarAfterCast.Animals, false);

        }
    }
}
