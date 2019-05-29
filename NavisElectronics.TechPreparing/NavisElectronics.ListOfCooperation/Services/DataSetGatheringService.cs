namespace NavisElectronics.ListOfCooperation.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Entities;

    /// <summary>
    /// Сервис для создания Dataset из узла дерева
    /// </summary>
    public class DataSetGatheringService
    {
        /// <summary>
        /// Собирает Dataset из указанного узла 
        /// </summary>
        /// <param name="mainElement">
        /// Узел дерева, из которого собираем данные об отношениях и самих изделиях
        /// </param>
        /// <returns>
        /// The <see cref="DataSet"/>.
        /// </returns>
        public DataSet Gather(IntermechTreeElement mainElement)
        {
            return GetDataSetInternal(mainElement);
        }

        /// <summary>
        /// Метод создания пустого Dataset из пяти таблиц: Order, Product and ProductRelations, companiesStructTable, withdrawalTypeTable
        /// </summary>
        /// <returns>Dataset</returns>
        public DataSet CreateDataset()
        {
            DataTable companiesStructTable = new DataTable("CompaniesStructTable");
            DataColumn companiesStructKeyColumn = new DataColumn("Id", typeof(long));
            DataColumn companiesStructNameColumn = new DataColumn("Name", typeof(string));
            DataColumn companiesStructPartitionNameColumn = new DataColumn("PartitionName", typeof(string));
            DataColumn companiesStructWorkshopNameColumn = new DataColumn("WorkshopName", typeof(string));
            DataColumn companiesStructPartitionIdColumn = new DataColumn("PartitionId", typeof(long));
            DataColumn companiesStructWorkshopIdColumn = new DataColumn("WorkshopId", typeof(long));
            companiesStructTable.Columns.AddRange(
                new DataColumn[]
                {
                    companiesStructKeyColumn,
                    companiesStructNameColumn,
                    companiesStructPartitionNameColumn,
                    companiesStructWorkshopNameColumn,
                    companiesStructPartitionIdColumn,
                    companiesStructWorkshopIdColumn
                });
            companiesStructTable.PrimaryKey = new DataColumn[] { companiesStructKeyColumn };
            companiesStructTable.AcceptChanges();

            DataTable companiesStructRelationsTable = new DataTable("CompaniesStructRelationsTable");
            DataColumn childPartColumn = new DataColumn("ChildId", typeof(long));
            DataColumn parentPartColumn = new DataColumn("ParentId", typeof(long));
            companiesStructRelationsTable.Columns.AddRange(new DataColumn[] {parentPartColumn, childPartColumn});
            companiesStructRelationsTable.PrimaryKey = new DataColumn[] { parentPartColumn, childPartColumn };

            DataTable withdrawalTypeTable = new DataTable("WithdrawalTypes");
            DataColumn withdrawalTypeIdKeyColumn = new DataColumn("Id", typeof(long));
            DataColumn withdrawalTypeIdColumn = new DataColumn("TypeId", typeof(byte));
            DataColumn withdrawalTypeDescColumn = new DataColumn("Description", typeof(string));
            DataColumn withdrawalTypeValue = new DataColumn("Value", typeof(string));
            DataColumn withdrawalTypeYear = new DataColumn("Year", typeof(int));
            withdrawalTypeTable.Columns.Add(withdrawalTypeIdKeyColumn);
            withdrawalTypeTable.Columns.Add(withdrawalTypeIdColumn);
            withdrawalTypeTable.Columns.Add(withdrawalTypeDescColumn);
            withdrawalTypeTable.Columns.Add(withdrawalTypeValue);
            withdrawalTypeTable.Columns.Add(withdrawalTypeYear);
            withdrawalTypeTable.PrimaryKey = new DataColumn[] { withdrawalTypeIdColumn };
            withdrawalTypeTable.AcceptChanges();


            DataTable orderTable = new DataTable("Order");
            DataColumn orderIdColumn = new DataColumn("OrderId", typeof(long));
            DataColumn orderObjectIdColumn = new DataColumn("OrderObjectId", typeof(long));
            DataColumn bigNoteColumn = new DataColumn("BigNote", typeof(string));
            DataColumn nameColumn = new DataColumn("Name", typeof(string));
            orderTable.Columns.Add(orderIdColumn);
            orderTable.Columns.Add(orderObjectIdColumn);
            orderTable.Columns.Add(bigNoteColumn);
            orderTable.Columns.Add(nameColumn);

            orderTable.PrimaryKey = new DataColumn[] {orderIdColumn};
            orderTable.AcceptChanges();

            DataTable productTable = new DataTable("Product");
            DataColumn productIdColumn = new DataColumn("Id", typeof(long));
            productTable.Columns.Add(productIdColumn);
            productTable.Columns.Add(new DataColumn("ObjectId", typeof(long)));
            productTable.Columns.Add(new DataColumn("ChangeNumber", typeof(string)));
            productTable.Columns.Add(new DataColumn("Designation", typeof(string)));
            productTable.Columns.Add(new DataColumn("Name", typeof(string)));
            productTable.Columns.Add(new DataColumn("Note", typeof(string)));
            productTable.Columns.Add(new DataColumn("Type", typeof(int)));
            productTable.Columns.Add(new DataColumn("IsPCB", typeof(bool)));
            productTable.Columns.Add(new DataColumn("PcbVersion", typeof(byte)));
            productTable.Columns.Add(new DataColumn("MeasureUnits", typeof(string)));
            productTable.Columns.Add(new DataColumn("Class", typeof(string)));
            productTable.Columns.Add(new DataColumn("Supplier", typeof(string)));
            productTable.Columns.Add(new DataColumn("PartNumber", typeof(string)));
            productTable.Columns.Add(new DataColumn("Case", typeof(string)));
            productTable.PrimaryKey = new DataColumn[] { productIdColumn };
            productTable.AcceptChanges();

            DataTable productRelationsTable = new DataTable("ProductRelations");
            DataColumn parentIdColumn = new DataColumn("ParentId", typeof(long));
            DataColumn childIdColumn = new DataColumn("ChildId", typeof(long));
            productRelationsTable.Columns.Add(parentIdColumn);
            productRelationsTable.Columns.Add(childIdColumn);
            productRelationsTable.Columns.Add(new DataColumn("Amount", typeof(double)));
            productRelationsTable.Columns.Add(new DataColumn("RelationNote", typeof(string)));
            productRelationsTable.Columns.Add(new DataColumn("StockRate", typeof(double)));
            productRelationsTable.Columns.Add(new DataColumn("SampleSize", typeof(string)));
            productRelationsTable.Columns.Add(new DataColumn("CooperationFlag", typeof(bool)));
            productRelationsTable.Columns.Add(new DataColumn("InnerCooperation", typeof(bool)));
            productRelationsTable.Columns.Add(new DataColumn("ContainsInnerCooperation", typeof(bool)));
            productRelationsTable.Columns.Add(new DataColumn("TechRoute", typeof(string)));
            productRelationsTable.Columns.Add(new DataColumn("Agent", typeof(string)));
            productRelationsTable.Columns.Add(new DataColumn("TechProcessReference", typeof(long)));
            productRelationsTable.Columns.Add(new DataColumn("RouteNote", typeof(string)));
            productRelationsTable.Columns.Add(new DataColumn("SubInfo", typeof(string)));
            productRelationsTable.Columns.Add(new DataColumn("Position", typeof(string)));
            productRelationsTable.Columns.Add(new DataColumn("PositionDesignation", typeof(string)));
            productRelationsTable.Columns.Add(new DataColumn("TechTask", typeof(string)));
            productRelationsTable.Columns.Add(new DataColumn("WithdrawalType", typeof(string)));
            productRelationsTable.Columns.Add(new DataColumn("SubstituteGroupNumber", typeof(int)));
            productRelationsTable.Columns.Add(new DataColumn("NumberInSubstituteGroup", typeof(int)));
            productRelationsTable.Columns.Add(new DataColumn("ComplectNodeFlag", typeof(bool)));

            productRelationsTable.PrimaryKey = new DataColumn[] { parentIdColumn, childIdColumn };
            productRelationsTable.AcceptChanges();

            return new DataSet("TechDataForOrder")
            {
                Tables =
                {
                    orderTable,
                    productTable,
                    productRelationsTable,
                    withdrawalTypeTable,
                    companiesStructTable,
                    companiesStructRelationsTable
                }
            };
        }

        /// <summary>
        /// Создаем строку таблицы ProductRelation
        /// </summary>
        /// <param name="dataSet">
        /// сам Dataset
        /// </param>
        /// <param name="mainElement">
        /// Главный элемент
        /// </param>
        /// <param name="childNode">
        /// Входящий элемент(дочерний или потомок)
        /// </param>
        /// <returns>
        /// The <see cref="DataRow"/>.
        /// </returns>
        private DataRow CreateProductRelationRow(DataSet dataSet, IntermechTreeElement mainElement, IntermechTreeElement childNode)
        {
            DataRow relationRow = dataSet.Tables["ProductRelations"].NewRow();
            relationRow["ParentId"] = mainElement.Id;
            relationRow["ChildId"] = childNode.Id;
            relationRow["Amount"] = childNode.Amount;
            relationRow["Agent"] = childNode.Agent;
            relationRow["CooperationFlag"] = childNode.CooperationFlag;
            relationRow["InnerCooperation"] = childNode.InnerCooperation;
            relationRow["ContainsInnerCooperation"] = childNode.ContainsInnerCooperation;
            relationRow["StockRate"] = childNode.StockRate;
            relationRow["SampleSize"] = childNode.SampleSize;
            relationRow["TechRoute"] = childNode.TechRoute;
            relationRow["RouteNote"] = childNode.RouteNote;
            relationRow["SubInfo"] = childNode.SubstituteInfo;
            relationRow["Position"] = childNode.Position;
            relationRow["PositionDesignation"] = childNode.PositionDesignation;
            relationRow["TechTask"] = childNode.TechTask;
            relationRow["WithdrawalType"] = childNode.TypeOfWithDrawal;
            relationRow["SubstituteGroupNumber"] = childNode.SubstituteGroupNumber;
            relationRow["NumberInSubstituteGroup"] = childNode.SubstituteNumberInGroup;
            relationRow["ComplectNodeFlag"] = childNode.IsToComplect;
            if (childNode.TechProcessReference != null)
            {
                relationRow["TechProcessReference"] = childNode.TechProcessReference.Id;
            }

            return relationRow;
        }

        /// <summary>
        /// Собирает Dataset из указанного узла 
        /// </summary>
        /// <param name="mainElement">
        /// The main element.
        /// </param>
        /// <returns>
        /// The <see cref="DataSet"/>.
        /// </returns>
        internal DataSet GetDataSetInternal(IntermechTreeElement mainElement)
        {
            DataSet dataSet = CreateDataset();

            DataRow row = dataSet.Tables["Order"].NewRow();
            row["OrderId"] = mainElement.Id;
            row["OrderObjectId"] = mainElement.ObjectId;
            row["BigNote"] = mainElement.Note;
            row["Name"] = mainElement.Name;
            dataSet.Tables["Order"].Rows.Add(row);

            Queue<IntermechTreeElement> elementQueue = new Queue<IntermechTreeElement>();

            foreach(IntermechTreeElement childNode in mainElement.Children)
            {
                DataRow relationRow = CreateProductRelationRow(dataSet, mainElement, childNode);
                try
                {
                    dataSet.Tables["ProductRelations"].Rows.Add(relationRow);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message, e);
                }

                elementQueue.Enqueue(childNode);
            }

            while (elementQueue.Count > 0)
            {
                IntermechTreeElement elementFromQueue = elementQueue.Dequeue();
                DataRow productRow = CreateProductRow(dataSet, elementFromQueue);
                try
                {
                    dataSet.Tables["Product"].Rows.Add(productRow);
                }
                catch (ConstraintException)
                {
                    // элемент уже был добавлен, а значит его входящие тоже. Смело делаем continue
                    continue;
                }

                if (elementFromQueue.Children.Count > 0)
                {
                    foreach (IntermechTreeElement child in elementFromQueue.Children)
                    {
                        DataRow relationRow = CreateProductRelationRow(dataSet, elementFromQueue, child);
                        try
                        {
                            dataSet.Tables["ProductRelations"].Rows.Add(relationRow);
                        }
                        catch (ConstraintException)
                        {
                            // ни черта здесь не делаем. Проглотили исключение намеренно
                        }

                        elementQueue.Enqueue(child);
                    }
                }
            }
            dataSet.AcceptChanges();
            return dataSet;
        }

        /// <summary>
        /// СОздает строку таблицы Product
        /// </summary>
        /// <param name="dataSet">
        /// сам Dataset
        /// </param>
        /// <param name="elementFromQueue">
        /// Элемент, для которого создаем строку
        /// </param>
        /// <returns>
        /// The <see cref="DataRow"/>.
        /// </returns>
        private DataRow CreateProductRow(DataSet dataSet, IntermechTreeElement elementFromQueue)
        {
            DataRow productRow = dataSet.Tables["Product"].NewRow();
            productRow["Id"] = elementFromQueue.Id;
            productRow["ObjectId"] = elementFromQueue.ObjectId;
            productRow["Name"] = elementFromQueue.Name;
            productRow["Designation"] = elementFromQueue.Designation;
            productRow["ChangeNumber"] = elementFromQueue.ChangeNumber;
            productRow["Type"] = elementFromQueue.Type;
            productRow["IsPCB"] = elementFromQueue.IsPCB;
            productRow["PcbVersion"] = elementFromQueue.PcbVersion;
            productRow["Note"] = elementFromQueue.Note;
            if (elementFromQueue.MeasureUnits == null)
            {
                productRow["MeasureUnits"] = string.Empty;
            }
            else
            {
                productRow["MeasureUnits"] = elementFromQueue.MeasureUnits;
            }

            productRow["Class"] = elementFromQueue.Class;
            productRow["Supplier"] = elementFromQueue.Supplier;
            productRow["PartNumber"] = elementFromQueue.PartNumber;
            productRow["Case"] = elementFromQueue.Case;
            return productRow;
        }
    }
}