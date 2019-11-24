namespace SubstitutesTests
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NavisElectronics.Substitutes;

    [TestClass]
    public class SubGroupsDecryptorTests
    {
        /// <summary>
        /// Расшифровывает строку групп допустимых замен, чтобы затем поместить ее к актуальному заменителю.
        /// Случай с несколькими группами и одним элементом в каждой группе
        /// </summary>
        [TestMethod]
        public void DecryptWithSomeGroupsTest()
        {
            ISubGroupsDecryptor groupsDecryptor = new SubGroupsDecryptor(new SubGroupDecryptor());
            IProduct productPos50 = new ProductMock()
            {
                Position = "50",
                Name = "Product 0"
            };
            IProduct productPos52 = new ProductMock()
            {
                Position = "52",
                Name = "Product 2"
            };
            SubstituteSubGroup substituteSubGroup = new SubstituteSubGroup(1);

            substituteSubGroup.Subsitutes.Add(productPos52);

            SubstituteSubGroup anotherSubstituteSubGroup = new SubstituteSubGroup(2);
            anotherSubstituteSubGroup.Subsitutes.Add(productPos50);

            string result =
                groupsDecryptor.GetInfoAboutGroupsForActualElement(
                    new List<SubstituteSubGroup>() { substituteSubGroup, anotherSubstituteSubGroup });

            Assert.AreEqual("Product 2 или Product 0", result);
        }

        /// <summary>
        /// Расшифровывает строку групп допустимых замен, чтобы затем поместить ее к актуальному заменителю.
        /// Случай с несколькими группами и несколькими элементами в каждой группе
        /// </summary>
        [TestMethod]
        public void DecryptWithSomeGroupsAndSomeProductsInGroupsTest()
        {
            ISubGroupsDecryptor groupsDecryptor = new SubGroupsDecryptor(new SubGroupDecryptor());

            SubstituteGroup subGroup = new SubstituteGroup(1);
            IProduct productPos50 = new ProductMock()
            {
                Position = "50",
                Name = "Product 0"
            };
            IProduct productPos51 = new ProductMock()
            {
                Position = "51",
                Name = "Product 1"
            };
            IProduct productPos52 = new ProductMock()
            {
                Position = "52",
                Name = "Product 2"
            };
            IProduct productPos53 = new ProductMock()
            {
                Position = "53",
                Name = "Product 3"
            };
            SubstituteSubGroup substituteSubGroup = new SubstituteSubGroup(1);

            substituteSubGroup.Subsitutes.Add(productPos51);
            substituteSubGroup.Subsitutes.Add(productPos52);

            SubstituteSubGroup anotherSubstituteSubGroup = new SubstituteSubGroup(2);
            anotherSubstituteSubGroup.Subsitutes.Add(productPos50);
            anotherSubstituteSubGroup.Subsitutes.Add(productPos53);
            string result =
                groupsDecryptor.GetInfoAboutGroupsForActualElement(
                    new List<SubstituteSubGroup>() { substituteSubGroup, anotherSubstituteSubGroup });

            Assert.AreEqual("Product 1 совместно с Product 2 или Product 0 совместно с Product 3", result);
        }



        [TestMethod]
        public void DescriptSubGroupsTest()
        {
            ISubGroupsDecryptor groupsDecryptor = new SubGroupsDecryptor(new SubGroupDecryptor());
            IProduct productPos50 = new ProductMock()
            {
                Position = "50",
                Name = "Product 0"
            };
            IProduct productPos51 = new ProductMock()
            {
                Position = "51",
                Name = "Product 1"
            };
            IProduct productPos52 = new ProductMock()
            {
                Position = "52",
                Name = "Product 2"
            };
            IProduct productPos53 = new ProductMock()
            {
                Position = "53",
                Name = "Product 3"
            };
            SubstituteSubGroup substituteSubGroup = new SubstituteSubGroup(1);

            substituteSubGroup.Subsitutes.Add(productPos51);
            substituteSubGroup.Subsitutes.Add(productPos52);

            SubstituteSubGroup anotherSubstituteSubGroup = new SubstituteSubGroup(2);
            anotherSubstituteSubGroup.Subsitutes.Add(productPos50);
            anotherSubstituteSubGroup.Subsitutes.Add(productPos53);

            groupsDecryptor.DescriptSubGroups(new List<SubstituteSubGroup>() { substituteSubGroup, anotherSubstituteSubGroup });

            Assert.AreEqual( "Применяется с Product 0 взамен ", productPos53.SubstituteInfo);
        }
    }
}
