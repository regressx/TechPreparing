using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace NavisElectronics.Orders.Reports
{
    public interface IReportFactory
    {
        IReport Create(IntermechTreeElement element);
    }
}