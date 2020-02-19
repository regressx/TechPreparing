using Aga.Controls.Tree;

namespace NavisElectronics.Orders
{
    public class OrderNode : Node
    {
        public long VersionId { get; set; }

        public long ObjectId { get; set; }

        public int Type { get; set; }
        public string Designation { get; set; }
        public string Name { get; set; }
        public string FirstUse { get; set; }
        public string Status { get; set; }
        public string BaseVersionSign { get; set; }
        public double Amount { get; set; }
        public double AmountWithUse { get; set; }
        public string Letter { get; set; }
        public string ChangeNumber { get; set; }
        public string ChangeDocument { get; set; }
        public string Note { get; set; }
        public bool DoNotProduce { get; set; }
        public string RelationType { get; set; }
        public string SubInfo { get; set; }
        public int PcbVersion { get; set; }
        public bool IsPcb { get; set; }
    }
}