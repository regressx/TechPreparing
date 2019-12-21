﻿namespace NavisElectronics.TechPreparation.Services
{
    using System.Collections.Generic;
    using System.Text;
    using Interfaces.Entities;
    using ViewModels.TreeNodes;

    internal class TechRouteSetService
    {
        internal void SetTechRoute(ICollection<MyNode> elements, IList<TechRouteNode> resultNodesList, bool append)
        {
            foreach (MyNode element in elements)
            {
                IntermechTreeElement treeElement = (IntermechTreeElement)element.Tag;
                element.TechProcessReference = treeElement.TechProcessReference.Name;
                element.StockRate = treeElement.StockRate;
                element.SampleSize = treeElement.SampleSize;
                element.TechTask = treeElement.TechTask;

                IList<TechRouteNode> nodes = resultNodesList;
                StringBuilder stringId = new StringBuilder();
                StringBuilder caption = new StringBuilder();
                if (append)
                {
                    if (nodes.Count > 0)
                    {
                        stringId.AppendFormat("|| {0}", nodes[0].Id.ToString());
                        caption.AppendFormat(" / {0}", nodes[0].GetCaption());
                    }

                    for (int i = 1; i < nodes.Count; i++)
                    {
                        stringId.AppendFormat(";{0}", nodes[i].Id.ToString());
                        caption.AppendFormat("-{0}", nodes[i].GetCaption());
                    }

                    string oldTechRouteCodes = treeElement.TechRoute;
                    string newTechRouteCodes = string.Format("{0}{1}", oldTechRouteCodes, stringId.ToString());
                    treeElement.TechRoute = newTechRouteCodes;

                    string oldCaption = element.Route;
                    element.Route = oldCaption + caption;
                }
                else
                {
                    if (nodes.Count > 0)
                    {
                        stringId.Append(nodes[0].Id.ToString());
                        caption.Append(nodes[0].GetCaption());
                    }

                    for (int i = 1; i < nodes.Count; i++)
                    {
                        stringId.Append(";" + nodes[i].Id.ToString());
                        caption.Append("-" + nodes[i].GetCaption());
                    }

                    element.Route = caption.ToString();
                    treeElement.TechRoute = stringId.ToString();
                }
            }
        }
    }
}