namespace NavisElectronics.ListOfCooperation.IO
{
    public interface IDatabaseWriter
    {
        void WriteDataSet(long orderId, System.Data.DataSet ds);
    }
}