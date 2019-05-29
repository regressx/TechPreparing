﻿namespace NavisElectronics.ListOfCooperation
{
    using IO;
    using NavisArchiveWork.Data;
    using NavisArchiveWork.Model;
    using Ninject.Modules;
    using Services;
    using ViewInterfaces;
    using ViewModels;

    /// <summary>
    /// Модуль для настройки контейнера
    /// </summary>
    public class CommonModule : NinjectModule
    {
        /// <summary>
        /// Метод регистрации и загрузки основных модулей системы, по сути Composition Root
        /// </summary>
        public override void Load()
        {
            Bind<IDataRepository>().To<IntermechReader>();
            Bind<IRepository>().To<IntermechPathRepository>();
            Bind<Search>().ToSelf();
            Bind<OpenFolderService>().ToSelf();
            Bind<DataSetGatheringService>().ToSelf();
            Bind<MainViewModel>().ToSelf();
            Bind<IMainView>().To<MainView>();
            Bind<ITreeComparerView>().To<TreeComparerView>();
        }
    }
}