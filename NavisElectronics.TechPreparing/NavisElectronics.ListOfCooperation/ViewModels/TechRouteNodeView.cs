
using Aga.Controls.Tree;

namespace NavisElectronics.ListOfCooperation.ViewModels
{
    public class TechRouteNodeView: Node
    {
        public long Id { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
        public string WorkshopName { get; set; }
        public string PartitionName { get; set; }
    }
}