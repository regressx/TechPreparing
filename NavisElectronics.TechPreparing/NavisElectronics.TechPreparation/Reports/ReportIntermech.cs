﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportIntermech.cs" company="NavisElectronics">
//   ---
// </copyright>
// <summary>
//   Класс для получения отчета в IPS
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using NavisElectronics.TechPreparation.Reports.MaterialsList;

namespace NavisElectronics.TechPreparation.Reports
{
    using System;
    using Aga.Controls.Tree;
    using CompleteList;
    using CooperationList;
    using NavisElectronics.TechPreparation.Interfaces.Services;

    /// <summary>
    /// Класс для получения отчета в IPS
    /// </summary>
    public class ReportIntermech : IReport
    {
        /// <summary>
        /// The _report type.
        /// </summary>
        private readonly ReportType _reportType;
        private readonly RecountService recountService;

        /// <summary>
        /// The _document type factory.
        /// </summary>
        private IDocumentTypeFactory _documentTypeFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportIntermech"/> class.
        /// </summary>
        /// <param name="reportType">
        /// The report type.
        /// </param>
        public ReportIntermech(ReportType reportType, RecountService recountService)
        {
            _reportType = reportType;
            this.recountService = recountService;
        }

        /// <summary>
        /// Метод создания отчета
        /// </summary>
        /// <param name="mainElement"></param>
        /// <param name="path"></param>
        /// <param name="agent"></param>
        public void Create(Node mainElement, string path)
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
                    _documentTypeFactory = new CooperationListView(recountService);
                    break;

                case ReportType.CompleteList:
                    _documentTypeFactory = new CompleteListViewFactory();
                    break;

                case ReportType.MaterialsList:
                    _documentTypeFactory = new MaterialsListDocumentView(new MaterialsListDocumentModel(recountService));
                    break;

                default:
                    throw new NotImplementedException("Метод выгрузки данных в эту ведомость еще не был разработан");

            }
            _documentTypeFactory.Create(mainElement, path);
        }
    }
}