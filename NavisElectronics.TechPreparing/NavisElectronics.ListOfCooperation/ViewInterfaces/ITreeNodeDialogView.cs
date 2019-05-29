using System;
using System.Windows.Forms;
using Aga.Controls.Tree;
using NavisElectronics.ListOfCooperation.Entities;

namespace NavisElectronics.ListOfCooperation.ViewInterfaces
{
    public interface ITreeNodeDialogView
    {
        event EventHandler Load;
        event EventHandler<IntermechTreeElement> AcceptClick;
        void FillTree(TreeModel model);
        void Show();
        IntermechTreeElement GetSelectedNode();
    }
}