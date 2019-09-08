// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PresentationFactory.cs" company="NavisElectronics">
//   ----
// </copyright>
// <summary>
//   Фабрика представителей. Умеет выдавать нужного представителя
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NavisElectronics.TechPreparation.Presenters
{
    using Ninject;

    /// <summary>
    /// Фабрика представителей. Умеет выдавать нужного представителя
    /// </summary>
    public class PresentationFactory : IPresentationFactory
    {
        private IKernel _container;

        public PresentationFactory(IKernel container)
        {
            _container = container;
        }

        /// <summary>
        /// The get presenter.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IPresenter"/>.
        /// </returns>
        public IPresenter GetPresenter<T>() where T: class, IPresenter
        {
            if (!_container.CanResolve<T>())
            {
                _container.Bind<IPresenter>().To<T>();
            }
            return (IPresenter)_container.Get<T>();
        }

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
        public IPresenter<V> GetPresenter<T, V>() where T : class, IPresenter<V>
        {
            if (!_container.CanResolve<T>())
            {
                _container.Bind<T>().To<T>();
            }
            return _container.Get<T>();
        }

        public IPresenter<V, Z> GetPresenter<T, V, Z>() where T : class, IPresenter<V, Z>
        {
            if (!_container.CanResolve<T>())
            {
                _container.Bind<T>().To<T>();
            }
            return _container.Get<T>();
        }

        public IPresenter<V, Z, W> GetPresenter<T, V, Z, W>() where T : class, IPresenter<V, Z, W>
        {
            if (!_container.CanResolve<T>())
            {
                _container.Bind<T>().To<T>();
            }
            return _container.Get<T>();
        }

        public void ReleasePresenter<TPresenter>()
        {
            if (_container.CanResolve<TPresenter>())
            {
                _container.Release(typeof(TPresenter));
            }
        }

    }
}