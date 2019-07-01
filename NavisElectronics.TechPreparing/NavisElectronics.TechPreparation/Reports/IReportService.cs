// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IReportService.cs" company="NavisElectronics">
//   ---
// </copyright>
// <summary>
//   The ReportService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NavisElectronics.TechPreparation.Reports
{
    using Aga.Controls.Tree;

    using NavisElectronics.TechPreparation.Entities;

    /// <summary>
    /// The ReportService interface.
    /// </summary>
    public interface IReportService
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
        void CreateReport(Node mainElement, string path, ReportType reportType, DocumentType documentType);
    }
}