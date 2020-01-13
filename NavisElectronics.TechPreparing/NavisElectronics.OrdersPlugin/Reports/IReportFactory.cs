using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace NavisElectronics.Orders.Reports
{
    public interface IReportFactory
    {
        void Create(ReportNode element);
    }
}