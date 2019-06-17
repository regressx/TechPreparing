// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportService.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the ReportService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NavisElectronics.TechPreparation.Reports
{
    using System;

    using Aga.Controls.Tree;

    using NavisElectronics.ListOfCooperation.Reports;
    using NavisElectronics.TechPreparation.Entities;

    public class ReportService : IReportService
    {
        public void CreateReport(Node mainElement, string path, ReportType reportType, DocumentType documentType,
            Agent mainManufacturer)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            if (mainElement == null)
            {
                throw new ArgumentNullException("mainElement");
            }

            IReport report = null;
            switch (documentType)
            {
                case DocumentType.Intermech:
                    report = new ReportIntermech(reportType);
                    break;
            }

            if (report != null)
            {
                report.Create(mainElement, path, mainManufacturer);
            }

        }

    }

    public interface IReportService
    {
        void CreateReport(Node mainElement, string path, ReportType reportType, DocumentType documentType,
            Agent mainManufacturer);
    }
}