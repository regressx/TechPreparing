// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParametersView.cs" company="NavisElectronics">
//   ---
// </copyright>
// <summary>
//   The parameters view.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NavisElectronics.TechPreparation.Views
{
    using System.Windows.Forms;

    using NavisElectronics.TechPreparation.ViewInterfaces;

    /// <summary>
    /// The parameters view.
    /// </summary>
    public partial class ParametersView : Form, IParametersView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParametersView"/> class.
        /// </summary>
        public ParametersView()
        {
            InitializeComponent();
            sampleSizeNumericUpDown.Select();
        }

        /// <summary>
        /// The get stock rate.
        /// </summary>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        public double GetStockRate()
        {
            return (double)stockRateNumericUpDown.Value;
        }

        /// <summary>
        /// The get sample size.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetSampleSize()
        {
            return string.Format($"{ sampleSizeNumericUpDown.Value} %");
        }
    }
}
