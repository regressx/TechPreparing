namespace NavisElectronics.TechPreparation.Presenters
{
    public interface IPresentationFactory
    {
        IPresenter GetPresenter<T>() where T : class, IPresenter;
        IPresenter<V> GetPresenter<T, V>() where T : class, IPresenter<V>;
        void ReleasePresenter<TPresenter>();

    }
}