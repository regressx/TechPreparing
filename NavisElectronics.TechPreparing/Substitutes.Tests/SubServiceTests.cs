namespace SubstitutesTests
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NavisElectronics.Substitutes;

    [TestClass]
    public class SubServiceTests
    {   
        [TestMethod]
        public void Decrypt()
        {
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

            SubstituteSubGroup substituteSubGroup = new SubstituteSubGroup(4);
            substituteSubGroup.Subsitutes.Add(productPos53);

        }

    }
}
