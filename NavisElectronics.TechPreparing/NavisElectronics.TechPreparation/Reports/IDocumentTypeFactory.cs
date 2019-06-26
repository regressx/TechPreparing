// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDocumentTypeFactory.cs" company="NavisElectronics">
//   ---
// </copyright>
// <summary>
//   Defines the IDocumentTypeFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NavisElectronics.TechPreparation.Reports
{
    using Aga.Controls.Tree;

    /// <summary>
    /// The DocumentTypeFactory interface.
    /// </summary>
    public interface IDocumentTypeFactory
    {
        void Create(Node mainElement, string name);
    }
}