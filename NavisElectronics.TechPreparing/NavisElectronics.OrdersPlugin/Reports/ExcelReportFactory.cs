using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using NavisElectronics.TechPreparation.Interfaces.Services;

namespace NavisElectronics.Orders.Reports
{
    class ExcelReportFactory : IReportFactory
    {
        private readonly string _reportName;
        private MapTreeOnListService<ReportNode> _mapTreeOnListService;
        private ICollection<LevelColor> _colorCollection;

        public ExcelReportFactory(string reportName, MapTreeOnListService<ReportNode> mapTreeOnListService, ICollection<LevelColor> colorCollection)
        {
            _reportName = reportName;
            if (colorCollection == null)
            {
                throw new ArgumentNullException("colorCollection","Отсутствует ссылка на коллекцию цветов состава изделия");
            }

            if (colorCollection == null)
            {
                throw new ArgumentNullException("colorCollection","Отсутствует ссылка на коллекцию цветов состава изделия");
            }

            if (mapTreeOnListService == null)
            {
                throw new ArgumentNullException("mapTreeOnListService","Отсутствует ссылка маппер из дерева на список");
            }

            _colorCollection = colorCollection;
            _mapTreeOnListService = mapTreeOnListService;
        }

        public IReport Create()
        {
            return new ExcelReport(_reportName,_mapTreeOnListService,_colorCollection);
        }







    }
}