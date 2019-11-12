using System;
using Intermech.Interfaces;

namespace NavisElectronics.Orders.ViewModels
{
    using System.Threading;
    using System.Threading.Tasks;
    using Aga.Controls.Tree;
    using TechPreparation.Interfaces;
    using TechPreparation.Interfaces.Entities;
    using TechPreparation.Interfaces.Services;

    /// <summary>
    /// Фасад
    /// </summary>
    public class MainFormModel
    {
        /// <summary>
        /// Репозиторий с деревом заказа
        /// </summary>
        private readonly IDataRepository _reader;

        /// <summary>
        /// Сервис сохранения данных
        /// </summary>
        private readonly SaveService _saveService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainFormModel"/> class.
        /// </summary>
        /// <param name="reader">
        /// The reader.
        /// </param>
        /// <param name="saveService">
        /// The save service.
        /// </param>
        public MainFormModel(IDataRepository reader, SaveService saveService)
        {
            _reader = reader;
            _saveService = saveService;
        }


        /// <summary>
        /// Асинхронно получает заказ
        /// </summary>
        /// <param name="versionId">
        /// Идентификатор версии заказа
        /// </param>
        /// <param name="token">
        /// Токен отмемы
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<IntermechTreeElement> GetFullOrderAsync(long versionId, CancellationToken token)
        {
            return await _reader.GetFullOrderAsync(versionId, token);
        }

        public Task WriteBlobAttributeAsync<T>(long rootVersionId, T element, int attrubuteId, string comment)
        {
            return _saveService.SaveIntoBlobAttributeAsync(rootVersionId, element, attrubuteId, comment);
        }

        public Task<T> ReadDataFromBlobAttribute<T>(long rootVersionId, int attrubuteId) where T:class
        {
            return _reader.GetDataFromBinaryAttributeAsync<T>(rootVersionId, attrubuteId);
        }

        public Task<bool> AttributeExist(long versionId, int attributeId)
        {
            Func<bool> func = () =>
            {
                bool result = false;
                using (SessionKeeper keeper = new SessionKeeper())
                {
                    IDBObject orderObject = keeper.Session.GetObject(versionId);
                    IDBAttribute attribute =
                        orderObject.GetAttributeByID(attributeId);
                    if (attribute != null)
                    {
                        result = true;
                    }
                }

                return result;
            };

            return Task.Run(func);

        }

        public TreeModel GetTreeModel(IntermechTreeElement elementToView)
        {
            OrderNode root = new OrderNode();
            root.Amount = elementToView.Amount;
            root.AmountWithUse = elementToView.AmountWithUse;
            root.Name = elementToView.Name;
            root.Designation = elementToView.Designation;
            root.Tag = elementToView;
            GetOrderNodeRecursive(root, elementToView);
            TreeModel model = new TreeModel();
            model.Nodes.Add(root);
            return model;
        }

        private void GetOrderNodeRecursive(OrderNode root, IntermechTreeElement elementToView)
        {
            foreach (IntermechTreeElement child in elementToView.Children)
            {
                OrderNode node = new OrderNode();
                node.Amount = child.Amount;
                node.AmountWithUse = child.AmountWithUse;
                node.Name = child.Name;
                node.Designation = child.Designation;
                node.Tag = child;
                root.Nodes.Add(node);
                GetOrderNodeRecursive(node, child);
            }
        }

    }
}