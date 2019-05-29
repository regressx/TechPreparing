using NavisElectronics.Substitutes;

namespace SubstitutesTests
{
    public class ProductMock:IProduct
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public float Amount { get; set; }
        public string Measures { get; set; }
        public string Manufacturer { get; set; }
        public string Designation { get; set; }
        public int SubstituteGroupNumber { get; set; }
        public int SubstituteNumberInGroup { get; set; }
        public string SubstituteInfo { get; set; }
        public string Position { get; set; }
        public string Purchased { get; set; }
        public int Type { get; set; }
        public string PositionDesignation { get; set; }
        public string PartNumber { get; set; }
        public string ProductClass { get; set; }
        public bool Cooperation { get; set; }
        public float ConsumptionRate { get; set; }
        public int NumberPosition { get; set; }
        public string UseInAssembly { get; set; }
    }
}