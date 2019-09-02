namespace NavisElectronics.TechPreparation.Presenters
{
    /// <summary>
    /// Интерфейс фабрики представителей
    /// </summary>
    public interface IPresentationFactory
    {
        /// <summary>
        /// The get presenter.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IPresenter"/>.
        /// </returns>
        IPresenter GetPresenter<T>() where T : class, IPresenter;

        /// <summary>
        /// The get presenter.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <typeparam name="V">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IPresenter"/>.
        /// </returns>
        IPresenter<V> GetPresenter<T, V>() where T : class, IPresenter<V>;

        /// <summary>
        /// The get presenter.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <typeparam name="V">
        /// </typeparam>
        /// <typeparam name="Z">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IPresenter"/>.
        /// </returns>
        IPresenter<V,Z> GetPresenter<T, V, Z>() where T : class, IPresenter<V, Z>;


        /// <summary>
        /// The get presenter.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <typeparam name="V">
        /// </typeparam>
        /// <typeparam name="Z">
        /// </typeparam>
        /// <typeparam name="W">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IPresenter"/>.
        /// </returns>
        IPresenter<V,Z,W> GetPresenter<T, V, Z, W>() where T : class, IPresenter<V, Z, W>;

        /// <summary>
        /// The release presenter.
        /// </summary>
        /// <typeparam name="TPresenter">
        /// </typeparam>
        void ReleasePresenter<TPresenter>();

    }
}