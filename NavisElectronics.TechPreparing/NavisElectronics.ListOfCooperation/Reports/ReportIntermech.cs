using System;
using Aga.Controls.Tree;
using NavisElectronics.ListOfCooperation.Reports.CompleteList;
using NavisElectronics.ListOfCooperation.Reports.CooperationList;
using NavisElectronics.ListOfCooperation.ViewModels;

namespace NavisElectronics.ListOfCooperation.Reports
{
    using Entities;

    /// <summary>
    /// Класс для получения отчета в IPS
    /// </summary>
    public class ReportIntermech : IReport
    {
        private ReportType _reportType;
        private IDocumentTypeFactory _documentTypeFactory;
        public ReportIntermech(ReportType reportType)
        {
            _reportType = reportType;
        }

        /// <summary>
        /// Метод создания отчета
        /// </summary>
        /// <param name="mainElement"></param>
        /// <param name="path"></param>
        /// <param name="agent"></param>
        public void Create(Node mainElement, string path, Agent agent)
        {
            switch (_reportType)
            {
                case ReportType.ListOfTechRoutes:
                    _documentTypeFactory = new TechRouteListDocumentFactory(); 
                    break;

                case ReportType.DividingList:
                    _documentTypeFactory = new DividingListFactory();
                    break;

                case ReportType.ListOfCooperation:
                    _documentTypeFactory = new CooperationListViewInDepth();
                    //_documentTypeFactory = new CooperationListViewUniqueAssemblies(); 
                    break;

                case ReportType.CompleteList:
                    _documentTypeFactory = new CompleteListViewFactory();
                    break;
                default:
                    throw new NotImplementedException("Метод выгрузки данных в эту ведомость еще не был разработан");

            }
            _documentTypeFactory.Create(mainElement, path, agent);
        }
    }
}