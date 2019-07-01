// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportService.cs" company="NavisElectronics">
//   ---
// </copyright>
// <summary>
//   Defines the ReportService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NavisElectronics.TechPreparation.Reports
{
    using System;

    using Aga.Controls.Tree;

    using NavisElectronics.TechPreparation.Entities;

    /// <summary>
    /// The report service.
    /// </summary>
    public class ReportService : IReportService
    {
        /// <summary>
        /// The create report.
        /// </summary>
        /// <param name="mainElement">
        /// The main element.
        /// </param>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="reportType">
        /// The report type.
        /// </param>
        /// <param name="documentType">
        /// The document type.
        /// </param>
        /// <param name="mainManufacturer">
        /// The main manufacturer.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public void CreateReport(Node mainElement, string path, ReportType reportType, DocumentType documentType)
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
                report.Create(mainElement, path);
            }

        }

    }
}