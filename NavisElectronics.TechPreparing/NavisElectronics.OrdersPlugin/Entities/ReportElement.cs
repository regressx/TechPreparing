using System.Drawing;

namespace NavisElectronics.Orders.Entities
{
    public class ReportElement
    {
        public long ObjectId { get; set; }
        public string Caption { get; set; }
        public double AmountWithUse { get; set; }
        public string MeasureUnits { get; set; }
        public Color Color { get; set; }
    }
}