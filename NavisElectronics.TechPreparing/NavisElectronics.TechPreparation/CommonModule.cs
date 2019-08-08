// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommonModule.cs" company="NavisElectronics">
//   ---
// </copyright>
// <summary>
//   Модуль для настройки контейнера
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using NavisElectronics.TechPreparation.Interfaces;
using NavisElectronics.TechPreparing.Data;

namespace NavisElectronics.TechPreparation
{
    using Entities;
    using IO;
    using NavisArchiveWork.Data;
    using NavisArchiveWork.Model;
    using Ninject.Modules;
    using Reports;
    using Services;
    using ViewInterfaces;
    using ViewModels;
    using Views;

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
            Bind<ITechPreparingSelector<IdOrPath>>().To<TechPreparingSelector>();
            Bind<Search>().ToSelf().InSingletonScope();
            Bind<ReportService>().ToSelf().InSingletonScope();
            Bind<OpenFolderService>().ToSelf().InSingletonScope();
            Bind<MainViewModel>().ToSelf();
            Bind<CooperationViewModel>().ToSelf();
            Bind<TreeNodeDialogViewModel>().ToSelf();
            Bind<ITreeNodeDialogView>().To<TreeNodeDialogView>();
            Bind<IMainView>().To<MainView>();
            Bind<ISelectManufacturerView>().To<SelectManufacturerView>();
            Bind<ITreeComparerView>().To<TreeComparerView>();
            Bind<ICooperationView>().To<CooperationView>();
            Bind<ITechRouteMap>().To<TechRoutesMap>();
        }
    }
}