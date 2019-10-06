using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace NavisElectronics.TechPreparation.Data
{
    using System;
    using System.Data;
    using Entities;
    using Intermech.Interfaces;

    /// <summary>
    /// The tree builder service.
    /// </summary>
    public class TreeBuilderService
    {
        public IntermechTreeElement Build(DataSet ds)
        {
            // строим главный узел
            IntermechTreeElement root = new IntermechTreeElement();
            root.Id = (long)ds.Tables["Order"].Rows[0][0];
            root.ObjectId = (long)ds.Tables["Order"].Rows[0]["OrderObjectId"];
            root.Name = (string)ds.Tables["Order"].Rows[0]["Name"];
            root.Amount = 1;
            root.AmountWithUse = 1;
            root.StockRate = 1;
            root.TotalAmount = root.StockRate * root.AmountWithUse;
            if (ds.Tables["Order"].Rows[0]["BigNote"] != DBNull.Value)
            {
                root.Note = (string)ds.Tables["Order"].Rows[0]["BigNote"];
            }

            CreateRelations(ds, root);

            return root;
        }

                /// <summary>
        /// Внутренний метод собирания дерева из Dataset
        /// </summary>
        /// <param name="element">
        /// Элемент, в который собираем данные
        /// </param>
        /// <param name="ds">
        /// Набор данных Dataset
        /// </param>
        internal void CreateRelations(DataSet ds, IntermechTreeElement element)
        {
            for (int i = 0; i < ds.Tables["ProductRelations"].Rows.Count; i++)
            {
                if ((long)ds.Tables["ProductRelations"].Rows[i]["ParentId"] == element.Id)
                {
                    IntermechTreeElement childElement = new IntermechTreeElement();
                    childElement.Id = (long)ds.Tables["ProductRelations"].Rows[i]["ChildId"];
                    childElement.Amount = Convert.ToSingle(ds.Tables["ProductRelations"].Rows[i]["Amount"]);

                    if (ds.Tables["ProductRelations"].Rows[i]["Agent"] == DBNull.Value)
                    {
                        childElement.Agent = string.Empty;
                    }
                    else
                    {
                        childElement.Agent = (string)ds.Tables["ProductRelations"].Rows[i]["Agent"];
                    }

                    if (ds.Tables["ProductRelations"].Rows[i]["TechProcessReference"] != DBNull.Value)
                    {
                        long id = (long)ds.Tables["ProductRelations"].Rows[i]["TechProcessReference"];
                        if (id != 0)
                        {
                            using (SessionKeeper keeper = new SessionKeeper())
                            {
                                IDBObject obj = keeper.Session.GetObject(id);
                                childElement.TechProcessReference = new TechProcess() { Id = obj.ObjectID, Name = obj.Caption };
                            }
                        }

                    }
         
                    if (ds.Tables["ProductRelations"].Rows[i]["StockRate"] != DBNull.Value)
                    {
                        childElement.StockRate = (double)ds.Tables["ProductRelations"].Rows[i]["StockRate"];
                    }

                    if (ds.Tables["ProductRelations"].Rows[i]["SampleSize"] == DBNull.Value)
                    {
                        childElement.SampleSize = string.Empty;
                    }
                    else
                    {
                        childElement.SampleSize = (string)ds.Tables["ProductRelations"].Rows[i]["SampleSize"];
                    }

                    if (ds.Tables["ProductRelations"].Rows[i]["TechRoute"] == DBNull.Value)
                    {
                        childElement.TechRoute = string.Empty;
                    }
                    else
                    {
                        childElement.TechRoute = (string)ds.Tables["ProductRelations"].Rows[i]["TechRoute"];
                    }

                    childElement.AmountWithUse = childElement.Amount * element.AmountWithUse;
                    childElement.TotalAmount = childElement.AmountWithUse * childElement.StockRate;

                    if (ds.Tables["ProductRelations"].Rows[i]["CooperationFlag"] == DBNull.Value)
                    {
                        childElement.CooperationFlag = false;
                    }
                    else
                    {
                        childElement.CooperationFlag = (bool)ds.Tables["ProductRelations"].Rows[i]["CooperationFlag"];
                    }

                    try
                    {
                        if (ds.Tables["ProductRelations"].Rows[i]["InnerCooperation"] == DBNull.Value)
                        {
                            childElement.InnerCooperation = false;
                        }
                        else
                        {
                            childElement.InnerCooperation = (bool)ds.Tables["ProductRelations"].Rows[i]["InnerCooperation"];
                        }
                    }
                    catch (Exception)
                    {
                        childElement.InnerCooperation = false;
                    }

                    try
                    {
                        if (ds.Tables["ProductRelations"].Rows[i]["ContainsInnerCooperation"] == DBNull.Value)
                        {
                            childElement.ContainsInnerCooperation = false;
                        }
                        else
                        {
                            childElement.ContainsInnerCooperation = (bool)ds.Tables["ProductRelations"].Rows[i]["ContainsInnerCooperation"];
                        }
                    }
                    catch (Exception e)
                    {
                        childElement.ContainsInnerCooperation = false;
                    }

                    if (ds.Tables["ProductRelations"].Rows[i]["RouteNote"] == DBNull.Value)
                    {
                        childElement.RouteNote = string.Empty;
                    }
                    else
                    {
                        childElement.RouteNote = (string)ds.Tables["ProductRelations"].Rows[i]["RouteNote"];
                    }

                    if (ds.Tables["ProductRelations"].Rows[i]["SubInfo"] == DBNull.Value)
                    {
                        childElement.SubstituteInfo = string.Empty;
                    }
                    else
                    {
                        childElement.SubstituteInfo = (string)ds.Tables["ProductRelations"].Rows[i]["SubInfo"];
                    }

                    if (ds.Tables["ProductRelations"].Rows[i]["Position"] == DBNull.Value)
                    {
                        childElement.Position = string.Empty;
                    }
                    else
                    {
                        childElement.Position = (string)ds.Tables["ProductRelations"].Rows[i]["Position"];
                    }

                    if (ds.Tables["ProductRelations"].Rows[i]["PositionDesignation"] == DBNull.Value)
                    {
                        childElement.PositionDesignation = string.Empty;
                    }
                    else
                    {
                        childElement.PositionDesignation = (string)ds.Tables["ProductRelations"].Rows[i]["PositionDesignation"];
                    }

                    try
                    {
                        if (ds.Tables["ProductRelations"].Rows[i]["TechTask"] == DBNull.Value)
                        {
                            childElement.TechTask = string.Empty;
                        }
                        else
                        {
                            childElement.TechTask = (string)ds.Tables["ProductRelations"].Rows[i]["TechTask"];
                        }
                    }
                    catch (Exception)
                    {
                        childElement.TechTask = string.Empty;
                    }

                    try
                    {
                        if (ds.Tables["ProductRelations"].Rows[i]["WithdrawalType"] == DBNull.Value)
                        {
                            childElement.TypeOfWithDrawal = string.Empty;
                        }
                        else
                        {
                            childElement.TypeOfWithDrawal = (string)ds.Tables["ProductRelations"].Rows[i]["WithdrawalType"];
                        }
                    }
                    catch (Exception)
                    {
                        childElement.TypeOfWithDrawal = string.Empty;
                    }

                    try
                    {
                        if (ds.Tables["ProductRelations"].Rows[i]["SubstituteGroupNumber"] == DBNull.Value)
                        {
                            childElement.SubstituteGroupNumber = 0;
                        }
                        else
                        {
                            childElement.SubstituteGroupNumber = (int)ds.Tables["ProductRelations"].Rows[i]["SubstituteGroupNumber"];
                        }
                    }
                    catch (Exception)
                    {
                        childElement.SubstituteGroupNumber = 0;
                    }

                    try
                    {
                        if (ds.Tables["ProductRelations"].Rows[i]["NumberInSubstituteGroup"] == DBNull.Value)
                        {
                            childElement.SubstituteNumberInGroup = 0;
                        }
                        else
                        {
                            childElement.SubstituteNumberInGroup = (int)ds.Tables["ProductRelations"].Rows[i]["NumberInSubstituteGroup"];
                        }
                    }
                    catch (Exception)
                    {
                        childElement.SubstituteNumberInGroup = 0;
                    }

                    try
                    {
                        if (ds.Tables["ProductRelations"].Rows[i]["ComplectNodeFlag"] == DBNull.Value)
                        {
                            childElement.IsToComplect = false;
                        }
                        else
                        {
                            childElement.IsToComplect = (bool)ds.Tables["ProductRelations"].Rows[i]["ComplectNodeFlag"];
                        }
                    }
                    catch (Exception)
                    {
                        childElement.IsToComplect = false;
                    }



                    for (int j = 0; j < ds.Tables["Product"].Rows.Count; j++)
                    {
                        if ((long)ds.Tables["Product"].Rows[j]["Id"] == childElement.Id)
                        {
                            if (ds.Tables["Product"].Rows[j]["Designation"] == DBNull.Value)
                            {
                                childElement.Designation = string.Empty;
                            }
                            else
                            {
                                childElement.Designation = (string)ds.Tables["Product"].Rows[j]["Designation"];
                            }

                            if (ds.Tables["Product"].Rows[j]["Name"] == DBNull.Value)
                            {
                                childElement.Name = string.Empty;
                            }
                            else
                            {
                                childElement.Name = (string)ds.Tables["Product"].Rows[j]["Name"];
                            }

                            if (ds.Tables["Product"].Rows[j]["Note"] == DBNull.Value)
                            {
                                childElement.Note = string.Empty;
                            }
                            else
                            {
                                childElement.Note = (string)ds.Tables["Product"].Rows[j]["Note"];
                            }

                            try
                            {
                                if (ds.Tables["Product"].Rows[j]["MeasureUnits"] == DBNull.Value)
                                {
                                    childElement.MeasureUnits = string.Empty;
                                }
                                else
                                {
                                    childElement.MeasureUnits = (string)ds.Tables["Product"].Rows[j]["MeasureUnits"];
                                }
                            }
                            catch (ArgumentNullException)
                            {
                                childElement.MeasureUnits = string.Empty;
                            }

                            if (ds.Tables["Product"].Rows[j]["ObjectId"] == DBNull.Value)
                            {
                                childElement.ObjectId = 0;
                            }
                            else
                            {
                                childElement.ObjectId = (long)ds.Tables["Product"].Rows[j]["ObjectId"];
                            }

                            try
                            {
                                if (ds.Tables["Product"].Rows[j]["ChangeNumber"] == DBNull.Value)
                                {
                                    childElement.ChangeNumber = string.Empty;
                                }
                                else
                                {
                                    childElement.ChangeNumber = (string)ds.Tables["Product"].Rows[j]["ChangeNumber"];
                                }
                            }
                            catch (Exception)
                            {
                                childElement.ChangeNumber = string.Empty;
                            }

                            try
                            {
                                if (ds.Tables["Product"].Rows[j]["IsPCB"] == DBNull.Value)
                                {
                                    childElement.IsPcb = false;
                                }
                                else
                                {
                                    childElement.IsPcb = (bool)ds.Tables["Product"].Rows[j]["IsPCB"];
                                }
                            }
                            catch (Exception)
                            {
                                childElement.IsPcb = false;
                            }

                            try
                            {
                                if (ds.Tables["Product"].Rows[j]["PcbVersion"] == DBNull.Value)
                                {
                                    childElement.PcbVersion = 0;
                                }
                                else
                                {
                                    childElement.PcbVersion = (byte)ds.Tables["Product"].Rows[j]["PcbVersion"];
                                }
                            }
                            catch (Exception)
                            {
                                childElement.PcbVersion = 0;
                            }

                            childElement.Type = (int)ds.Tables["Product"].Rows[j]["Type"];

                            try
                            {
                                if (ds.Tables["Product"].Rows[j]["Class"] != DBNull.Value)
                                {
                                    childElement.Class = (string)ds.Tables["Product"].Rows[j]["Class"];
                                }
                                else
                                {
                                    childElement.Class = string.Empty;
                                }
                            }
                            catch (Exception)
                            {
                                childElement.Class = string.Empty;
                            }

                            try
                            {
                                if (ds.Tables["Product"].Rows[j]["Supplier"] != DBNull.Value)
                                {
                                    childElement.Supplier = (string)ds.Tables["Product"].Rows[j]["Supplier"];
                                }
                                else
                                {
                                    childElement.Supplier = string.Empty;
                                }
                            }
                            catch (Exception)
                            {
                                childElement.Supplier = string.Empty;
                            }

                            try
                            {
                                if (ds.Tables["Product"].Rows[j]["PartNumber"] != DBNull.Value)
                                {
                                    childElement.PartNumber = (string)ds.Tables["Product"].Rows[j]["PartNumber"];
                                }
                                else
                                {
                                    childElement.PartNumber = string.Empty;
                                }
                            }
                            catch (Exception)
                            {
                                childElement.PartNumber = string.Empty;
                            }


                            try
                            {
                                if (ds.Tables["Product"].Rows[j]["Case"] != DBNull.Value)
                                {
                                    childElement.Case = (string)ds.Tables["Product"].Rows[j]["Case"];
                                }
                                else
                                {
                                    childElement.Case = string.Empty;
                                }
                            }
                            catch (Exception)
                            {
                                childElement.Case = string.Empty;
                            }


                            break;
                        }
                    }

                    element.Add(childElement);
                    if (childElement.Type == 1078 || childElement.Type == 1074 || childElement.Type == 1052 || childElement.Type == 1159 || childElement.Type == 0 || childElement.Type == 1097)
                    {
                        CreateRelations(ds, childElement);
                    }
                }
            }
        }

    }
}