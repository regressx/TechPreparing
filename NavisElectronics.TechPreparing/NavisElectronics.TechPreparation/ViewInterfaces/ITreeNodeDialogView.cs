using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace NavisElectronics.TechPreparation.ViewInterfaces
{
    using System;

    using Aga.Controls.Tree;

    using NavisElectronics.TechPreparation.Entities;

    public interface ITreeNodeDialogView
    {
        event EventHandler Load;
        event EventHandler<IntermechTreeElement> AcceptClick;
        void FillTree(TreeModel model);
        void Show();
        IntermechTreeElement GetSelectedNode();
        void Close();
    }
}