﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommonModule.cs" company="NavisElectronics">
//   ---
// </copyright>
// <summary>
//   Модуль для настройки контейнера
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NavisElectronics.TechPreparation
{
    using NavisArchiveWork.Data;
    using NavisArchiveWork.Model;
    using NavisElectronics.TechPreparation.IO;
    using NavisElectronics.TechPreparation.Services;
    using NavisElectronics.TechPreparation.ViewInterfaces;
    using NavisElectronics.TechPreparation.ViewModels;
    using NavisElectronics.TechPreparation.Views;

    using Ninject.Modules;

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
            Bind<IDatabaseWriter>().To<IntermechWriter>();
            Bind<IDataRepository>().To<IntermechReader>();
            Bind<IRepository>().To<IntermechPathRepository>();
            Bind<Search>().ToSelf().InSingletonScope();
            Bind<OpenFolderService>().ToSelf().InSingletonScope();
            Bind<DataSetGatheringService>().ToSelf().InSingletonScope();
            Bind<MainViewModel>().ToSelf();
            Bind<CooperationViewModel>().ToSelf();
            Bind<TreeNodeDialogViewModel>().ToSelf();
            Bind<ITreeNodeDialogView>().To<TreeNodeDialogView>();
            Bind<IMainView>().To<MainView>();
            Bind<ITreeComparerView>().To<TreeComparerView>();
            Bind<ICooperationView>().To<CooperationView>();
        }
    }
}