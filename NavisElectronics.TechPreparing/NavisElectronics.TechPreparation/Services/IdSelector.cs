namespace NavisElectronics.TechPreparation.Services
{
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Entities;

    /// <summary>
    /// The id selector.
    /// </summary>
    public class IdSelector : ITechPreparingSelector<IdOrPath>
    {
        /// <summary>
        /// The select.
        /// </summary>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<IdOrPath> Select()
        {
            IList<IdOrPath> list = new List<IdOrPath>();
            using (OpenFileDialog opf = new OpenFileDialog())
            {
                if (opf.ShowDialog() == DialogResult.OK)
                {
                    IdOrPath path = new IdOrPath();
                    list.Add(path);
                }
            }
            return list;
        }
    }
}