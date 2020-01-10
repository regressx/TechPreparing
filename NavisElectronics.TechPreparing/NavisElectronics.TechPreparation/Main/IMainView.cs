using System;
using System.Windows.Forms;

namespace NavisElectronics.TechPreparation.Main
{
    /// <summary>
    /// MainView interface.
    /// </summary>
    public interface IMainView
    {
        #region Events

        event EventHandler Load;
        event EventHandler EditTechRoutes;
        event EventHandler CheckOk;
        event EventHandler Save;
        event EventHandler CompareTwoTrees;
        #endregion

        #region Methods

        void Close();
        void Show();
        void LockButtons();
        void UnLockButtons();
        void UpdateStatusLabel(string message);
        void UpdateCaption(string caption);
        void LayoutMdi(MdiLayout mdiLayout);

        #endregion

    }
}