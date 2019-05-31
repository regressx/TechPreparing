using System;
using Ninject;

namespace NavisElectronics.ListOfCooperation.Presenters
{
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
            return (IPresenter<V>)_container.Get<T>();
        }

        public IPresenter<V,Z> GetPresenter<T, V,Z>() where T : class, IPresenter<V,Z>
        {
            if (!_container.CanResolve<T>())
            {
                _container.Bind<T>().To<T>();
            }
            return (IPresenter<V,Z>)_container.Get<T>();
        }


        public void ReleasePresenter<TPresenter>()
        {
            if (_container.CanResolve<TPresenter>())
            {
                _container.Release(typeof(TPresenter));
            }
        }

    }

    public interface IPresentationFactory
    {
        IPresenter GetPresenter<T>() where T : class, IPresenter;
        IPresenter<V> GetPresenter<T, V>() where T : class, IPresenter<V>;
        IPresenter<V,Z> GetPresenter<T, V, Z>() where T : class, IPresenter<V,Z>;
        void ReleasePresenter<TPresenter>();

    }

}