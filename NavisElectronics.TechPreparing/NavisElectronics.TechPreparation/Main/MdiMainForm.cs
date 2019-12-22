using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NavisElectronics.TechPreparation.ViewInterfaces;

namespace NavisElectronics.TechPreparation.Main
{
    public partial class MdiMainForm : Form, IMainView
    {
        public MdiMainForm()
        {
            InitializeComponent();
            workWithRoutesButton.Click += (sender, args) => Invoke(EditTechRoutes, sender, args);
            compareTwoTreesButton.Click += (sender, args) => Invoke(CompareTwoTrees, sender, args);
            checkOkButton.Click += (sender, args) => Invoke(CheckOk, sender, args);
            saveButton.Click += (sender, args) => Invoke(Save, sender, args);
        }

        #region Events

        public event EventHandler EditTechRoutes;
        public event EventHandler CheckOk;
        public event EventHandler Save;
        public event EventHandler CompareTwoTrees;

        #endregion


        public void LockButtons()
        {
            foreach (ToolStripPanelRow toolStripPanelRow in toolStripPanelTop.Rows)
            {
                foreach (Control control in toolStripPanelRow.Controls)
                {
                    control.Enabled = false;
                }
            }
        }

        public void UnLockButtons()
        {
            foreach (ToolStripPanelRow toolStripPanelRow in toolStripPanelTop.Rows)
            {
                foreach (Control control in toolStripPanelRow.Controls)
                {
                    control.Enabled = true;
                }
            }
        }

        public void UpdateStatusLabel(string message)
        {
            statusLabel.Text = message;
        }

        public void UpdateCaption(string caption)
        {
            this.Text = caption;
        }

        private void Invoke(EventHandler eventHandler, object sender, EventArgs e)
        {
            if (eventHandler != null)
            {
                eventHandler(sender,e);
            }
        }

    }
}
