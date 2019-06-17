namespace NavisElectronics.TechPreparation.ViewInterfaces
{
    using System.Windows.Forms;

    public interface IAddNoteView
    {
        string GetNote();
        DialogResult ShowDialog();
    }
}