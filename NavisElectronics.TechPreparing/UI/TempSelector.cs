using System.Collections.Generic;
using System.Windows.Forms;
using NavisElectronics.ListOfCooperation.Services;

namespace UI
{
    public class TempSelector : ITechPreparingSelector<string>
    {
        public IList<string> Select()
        {
            IList<string> list = new List<string>();
            using (OpenFileDialog opf = new OpenFileDialog())
            {
                if (opf.ShowDialog() == DialogResult.OK)
                {
                    list.Add(opf.FileName);
                }
            }
            return list;
        }
    }
}