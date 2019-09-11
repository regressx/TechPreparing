using Aga.Controls.Tree;

namespace NavisElectronics.Orders
{
    public class OrderNode : Node
    {
        public string Designation { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public double AmountWithUse { get; set; }
    }
}