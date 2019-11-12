using System;
using System.Threading.Tasks;
using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace NavisElectronics.TechPreparation.Interfaces.Services
{
    /// <summary>
    /// Сервис сохранения данных
    /// </summary>
    public class SaveService
    {
        private IProgress<ProgressReport> _progress;

        public event EventHandler<SaveServiceEventArgs> Saving; 

        /// <summary>
        /// The _writer.
        /// </summary>
        private readonly IDatabaseWriter _writer;

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveService"/> class.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        public SaveService(IDatabaseWriter writer)
        {
            _writer = writer;
        }

        /// <summary>
        /// The save.
        /// </summary>
        /// <param name="orderVersionId">
        /// The order version id.
        /// </param>
        /// <param name="mainTreeElement">
        /// The main tree element.
        /// </param>
        public void Save(long orderVersionId, IntermechTreeElement mainTreeElement)
        {
            _writer.WriteFileAttribute(orderVersionId, mainTreeElement);
        }

        /// <summary>
        /// The save async.
        /// </summary>
        /// <param name="orderVersionId">
        /// The order version id.
        /// </param>
        /// <param name="mainTreeElement">
        /// The main tree element.
        /// </param>
        /// <param name="progress">
        /// The progress.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task SaveAsync(long orderVersionId, IntermechTreeElement mainTreeElement)
        {
            _progress = new Progress<ProgressReport>((progressReport) =>
            {
                if (Saving != null)
                {
                    Saving(this, new SaveServiceEventArgs(progressReport.Percent, progressReport.Message));
                }
            });
            return _writer.WriteFileAttributeAsync(orderVersionId, mainTreeElement, _progress);
        }

        public Task SaveIntoBlobAttributeAsync<T>(long orderVersionId, T elementToSave, int attributeId, string comment)
        {
            return _writer.WriteBlobAttributeAsync<T>(orderVersionId, elementToSave, attributeId, comment);
        }
    }
}