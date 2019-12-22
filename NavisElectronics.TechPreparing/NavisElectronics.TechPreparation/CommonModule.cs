// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommonModule.cs" company="NavisElectronics">
//   ---
// </copyright>
// <summary>
//   Модуль для настройки контейнера
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using NavisElectronics.TechPreparation.TechRouteMap;

namespace NavisElectronics.TechPreparation
{
    using Data;
    using Entities;
    using Interfaces;
    using Interfaces.Services;
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
        /// Метод регистрации основных модулей системы
        /// </summary>
        public override void Load()
        {
            Bind<IDatabaseWriter>().To<IntermechWriter>();
            Bind<IDataRepository>().To<IntermechReader>();
            Bind<IRepository>().To<IntermechPathRepository>();
            Bind<ITechPreparingSelector<IdOrPath>>().To<TechPreparingSelector>();
            Bind<MergeNodesService>().ToSelf();
            Bind<Search>().ToSelf();
            Bind<ReportService>().ToSelf();
            Bind<OpenFolderService>().ToSelf();
            Bind<SaveService>().ToSelf();
            Bind<TreeComparerService>().ToSelf();
            Bind<MainViewModel>().ToSelf();
            Bind<TreeNodeDialogViewModel>().ToSelf();
            Bind<TechRouteModel>().ToSelf();
            Bind<TreeComparerViewModel>().ToSelf();
            Bind<ITechRouteView>().To<TechRouteEditView>();
            Bind<IMainView>().To<MainView>();
            Bind<ITreeComparerView>().To<TreeComparerView>();
            Bind<ITechRouteMap>().To<TechRoutesMap>();
        }
    }
}