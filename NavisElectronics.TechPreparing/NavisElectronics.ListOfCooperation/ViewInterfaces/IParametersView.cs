namespace NavisElectronics.ListOfCooperation.ViewInterfaces
{
    using System.Windows.Forms;

    /// <summary>
    /// Интерфейс окна для ввода параметров
    /// </summary>
    public interface IParametersView
    {
        double GetStockRate();
        string GetSampleSize();
        DialogResult ShowDialog();
    }
}