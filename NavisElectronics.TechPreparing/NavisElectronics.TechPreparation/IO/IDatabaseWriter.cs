namespace NavisElectronics.TechPreparation.IO
{
    /// <summary>
    /// The DatabaseWriter interface.
    /// </summary>
    public interface IDatabaseWriter
    {
        void WriteDataSet(long orderId, System.Data.DataSet ds);
    }
}