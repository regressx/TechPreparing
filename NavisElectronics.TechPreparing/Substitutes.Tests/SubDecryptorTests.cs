using Microsoft.VisualStudio.TestTools.UnitTesting;
using NavisElectronics.Substitutes;

namespace SubstitutesTests
{
    /// <summary>
    /// The sub dectiptor tests.
    /// </summary>
    [TestClass]
    public class SubDecryptorTests
    {
        private ISubGroupDecryptor _subDecryptor;

        public SubDecryptorTests()
        {
            _subDecryptor = new SubGroupDecryptor();
        }

        /// <summary>
        /// The decript many.
        /// </summary>
        [TestMethod]
        public void DecriptMany()
        {
            IProduct productPos52 = new ProductMock()
            {
                Position = "52",
                Name = "Product 1"

            };
            IProduct productPos53 = new ProductMock()
            {
                Position = "53",
                Name = "Product 2"
            };
            IProduct productPos55 = new ProductMock()
            {
                Position = "55",
                Name = "Product 3"
            };
            SubstituteSubGroup substituteSubGroup = new SubstituteSubGroup(1);
            substituteSubGroup.Subsitutes.Add(productPos52);
            substituteSubGroup.Subsitutes.Add(productPos53);
            substituteSubGroup.Subsitutes.Add(productPos55);
            _subDecryptor.DecryptElements(substituteSubGroup, false);
            Assert.AreEqual("Применяется с Product 2, Product 3 взамен ", substituteSubGroup.Subsitutes[0].SubstituteInfo);
            Assert.AreEqual("Применяется с Product 1, Product 3 взамен ", substituteSubGroup.Subsitutes[1].SubstituteInfo);
            Assert.AreEqual("Применяется с Product 1, Product 2 взамен ", substituteSubGroup.Subsitutes[2].SubstituteInfo);
        }

        /// <summary>
        /// The decrypt one.
        /// </summary>
        [TestMethod]
        public void DecryptOne()
        {
            IProduct productPos52 = new ProductMock()
            {
                Position = "52",
                Name = "Product 1"
            };
            SubstituteSubGroup substituteSubGroup = new SubstituteSubGroup(1);
            substituteSubGroup.Subsitutes.Add(productPos52);
            _subDecryptor.DecryptElements(substituteSubGroup, false);
            Assert.AreEqual("Взамен ", substituteSubGroup.Subsitutes[0].SubstituteInfo);

        }



        [TestMethod]
        public void DecriptManyForActualSub()
        {
            IProduct productPos52 = new ProductMock()
            {
                Position = "52",
                Name = "Product 3"

            };
            IProduct productPos53 = new ProductMock()
            {
                Position = "53",
                Name = "Product 4"
            };
            IProduct productPos55 = new ProductMock()
            {
                Position = "55",
                Name = "Product 5"
            };
            SubstituteSubGroup substituteSubGroup = new SubstituteSubGroup(1);
            substituteSubGroup.Subsitutes.Add(productPos52);
            substituteSubGroup.Subsitutes.Add(productPos53);
            substituteSubGroup.Subsitutes.Add(productPos55);
            string result = _subDecryptor.GetInfoAboutElementsForActualElement(substituteSubGroup,false);
            Assert.AreEqual("Product 3 совместно с Product 4, Product 5", result);
        }


        [TestMethod]
        public void DecriptOneForActualSub()
        {
            IProduct productPos52 = new ProductMock()
            {
                Position = "52",
                Name = "Product 2"
            };
            SubstituteSubGroup substituteSubGroup = new SubstituteSubGroup(4);
            substituteSubGroup.Subsitutes.Add(productPos52);
            string result = _subDecryptor.GetInfoAboutElementsForActualElement(substituteSubGroup,false);
            Assert.AreEqual("Product 2", result);
        }
    }
}
