using System.Collections.Generic;
using System.Windows.Forms;

namespace UI
{
    using NavisElectronics.TechPreparation.Entities;
    using NavisElectronics.TechPreparation.Services;

    public class TempSelector : ITechPreparingSelector<IdOrPath>
    {
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