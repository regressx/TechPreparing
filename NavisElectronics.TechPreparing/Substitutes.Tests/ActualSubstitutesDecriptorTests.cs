using Microsoft.VisualStudio.TestTools.UnitTesting;
using NavisElectronics.Substitutes;

namespace SubstitutesTests
{
    [TestClass]
    public class ActualSubstitutesDecriptorTests
    {
        private ActualSubsitutesDecryptor actualDecriptor;
        public ActualSubstitutesDecriptorTests()
        {
            actualDecriptor = new ActualSubsitutesDecryptor();
        }

        /// <summary>
        /// Тест проверяет правильность формирования строки, когда актуальный заменитель в группе всего лишь один
        /// </summary>
        [TestMethod]
        public void DecriptActualSubMethodOneTest()
        {
            SubstituteGroup subGroup = new SubstituteGroup(1);
            IProduct productPos50 = new ProductMock()
            {
                Position = "50"
            };
            subGroup.ActualSub.Add(productPos50);
            actualDecriptor.DecriptActualSubstitutes(subGroup, false);

            Assert.AreEqual("Допускается замена на ", subGroup.ActualSub[0].SubstituteInfo);
        }

        /// <summary>
        /// Тест проверяет правильность формирования строки, в актуальном заменителе находится целая группа элементов
        /// </summary>
        [TestMethod]
        public void DecriptActSubMethodManyTest()
        {
            SubstituteGroup subGroup = new SubstituteGroup(1);
            IProduct productPos50 = new ProductMock()
            {
                Position = "50",
                Name = "Изделие 1"
            };
            IProduct productPos51 = new ProductMock()
            {
                Position = "51",
                Name = "Изделие 2"
            };

            subGroup.ActualSub.Add(productPos50);
            subGroup.ActualSub.Add(productPos51);
            actualDecriptor.DecriptActualSubstitutes(subGroup,false);

            Assert.AreEqual("Допускается замена совместно с Изделие 2 на ", subGroup.ActualSub[0].SubstituteInfo);
            Assert.AreEqual("Допускается замена совместно с Изделие 1 на ", subGroup.ActualSub[1].SubstituteInfo);
        }



        /// <summary>
        /// Метод формирования строки для группы допустимых заменителей
        /// </summary>
        [TestMethod]
        public void DecriptManySub()
        {
            SubstituteGroup subGroup = new SubstituteGroup(1);
            IProduct productPos50 = new ProductMock()
            {
                Position = "50",
                Name = "Изделие 1"
            };
            IProduct productPos51 = new ProductMock()
            {
                Position = "51",
                Name = "Изделие 2"
            };
            subGroup.ActualSub.Add(productPos50);
            subGroup.ActualSub.Add(productPos51);
            actualDecriptor.DecriptActualSubstitutes(subGroup,false);
            string result = actualDecriptor.GetInfoAboutSubsititutionsForSub(subGroup, false);
            Assert.AreEqual("Изделие 1 совместно с Изделие 2", result);
        }

        [TestMethod]
        public void DecriptOneSub()
        {
            SubstituteGroup subGroup = new SubstituteGroup(1);
            IProduct productPos50 = new ProductMock()
            {
                Position = "50",
                Name = "Изделие 1"
            };
            subGroup.ActualSub.Add(productPos50);
            actualDecriptor.DecriptActualSubstitutes(subGroup,false);
            string result = actualDecriptor.GetInfoAboutSubsititutionsForSub(subGroup, false);
            Assert.AreEqual("Изделие 1", result);
        }
    }
}
