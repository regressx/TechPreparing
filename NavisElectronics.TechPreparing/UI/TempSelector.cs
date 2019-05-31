using System.Collections.Generic;
using System.Windows.Forms;
using NavisElectronics.ListOfCooperation.Entities;
using NavisElectronics.ListOfCooperation.Services;

namespace UI
{
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