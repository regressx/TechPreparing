using System.Windows.Forms;

namespace NavisElectronics.TechPreparation.TechRouteMap
{
    using System;
    using System.Collections.Generic;
    using Aga.Controls.Tree;
    using EventArguments;
    using ViewInterfaces;
    using ViewModels.TreeNodes;

    /// <summary>
    /// Интерфейс окна работы с маршрутами изготовления
    /// </summary>
    public interface ITechRouteMap : IView
    {
        event EventHandler<EditTechRouteEventArgs> EditMassTechRouteClick;

        event EventHandler DownloadInfoFromIPS;
        event EventHandler UpdateNodeFromIps;
        /// <summary>
        /// Переход к старому архиву
        /// </summary>
        event EventHandler<SaveClickEventArgs> GoToOldArchive;

        /// <summary>
        /// Удалить выделенные маршруты
        /// </summary>
        event EventHandler<NodesCollectionEventArgs> DeleteRouteClick;

        /// <summary>
        /// Загрузка формы
        /// </summary>
        event EventHandler Load;

        /// <summary>
        /// Редактирование примечания
        /// </summary>
        event EventHandler<SaveClickEventArgs> EditNoteClick;

        /// <summary>
        /// The create report click.
        /// </summary>
        event EventHandler CreateReportClick;

        /// <summary>
        /// Создать разделительную ведомость
        /// </summary>
        event EventHandler CreateDevideList;

        /// <summary>
        /// Создать ведомость кооперации
        /// </summary>
        event EventHandler CreateCooperationList;


        /// <summary>
        /// Проставить внутрипроизводственную кооперацию
        /// </summary>
        event EventHandler<NodesCollectionEventArgs> SetInnerCooperation;

        /// <summary>
        /// Убрать внутрипроизводственную кооперацию
        /// </summary>
        event EventHandler<NodesCollectionEventArgs> RemoveInnerCooperation;

        /// <summary>
        /// Установить модель в дерево
        /// </summary>
        /// <param name="treeModel">
        /// The tree model.
        /// </param>
        void SetTreeModel(TreeModel treeModel);

        /// <summary>
        /// Получить выбранные строки
        /// </summary>
        /// <returns>
        /// The <see cref="ICollection"/>.
        /// </returns>
        ICollection<MyNode> GetSelectedRows();

        /// <summary>
        /// Получить корень дерева
        /// </summary>
        /// <returns>
        /// The <see cref="MyNode"/>.
        /// </returns>
        MyNode GetMainNode();

        Form MdiParent { get; set; }

        TreeViewAdv GetTreeView();
    }
}