namespace NavisElectronics.Orders.Presenters
{
    public interface IPresenter
    {
        void Run();
    }

    public interface IPresenter<T>
    {
        void Run(T parameter);
    }

    public interface IPresenter<T,V>
    {
        void Run(T parameterT, V parameterV);
    }
}