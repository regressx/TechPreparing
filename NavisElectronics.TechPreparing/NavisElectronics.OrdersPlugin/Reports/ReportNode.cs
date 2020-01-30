using System;
using System.Collections;
using System.Collections.Generic;
using NavisElectronics.TechPreparation.Interfaces;

namespace NavisElectronics.Orders.Reports
{
    public class ReportNode : ITreeNode, IEnumerable<string>
    {
        private ICollection<ITreeNode> _nodes;

        public ReportNode(OrderNode node)
        {
            _nodes = new List<ITreeNode>();
            TypeId = node.Type;
            Designation = node.Designation;
            Name = node.Name;
            FirstUse = node.FirstUse;
            Status = node.Status;
            BaseVersionSign = node.BaseVersionSign;
            Amount = node.Amount;
            AmountWithUse = node.AmountWithUse;
            Letter = node.Letter;
            ChangeNumber = node.ChangeNumber;
            ChangeDocument = node.ChangeDocument;
            Note = node.Note;
            Index = node.Index;
            DoNotProduce = node.DoNotProduce;
            PcbVersion = node.PcbVersion;
            IsPcb = node.IsPcb;
        }

        public int TypeId { get; set; }

        public string NumberInOrder { get; set; }

        public int Level { get; set; }

        public int NumberOnLevel { get; set; }

        public ICollection<ITreeNode> Nodes => _nodes;

        public ITreeNode Parent { get; set; }

        public int Index { get; set; }

        public bool DoNotProduce { get; set; }


        public string Designation { get; set; }
        public string Name { get; set; }
        public string FirstUse { get; set; }
        public string Status { get; set; }
        public string BaseVersionSign { get; set; }
        public double Amount { get; set; }
        public double AmountWithUse { get; set; }
        public string Letter { get; set; }
        public string ChangeNumber { get; set; }
        public string ChangeDocument { get; set; }
        public string Note { get; set; }
        public int PcbVersion { get; set; }
        public bool IsPcb { get; set; }
        public void Add(ReportNode node)
        {
            _nodes.Add(node);
            node.Parent = this;
        }

        public IEnumerator<string> GetEnumerator()
        {
            string[] values = new string[]
            {
                NumberInOrder, Designation, Name, FirstUse, Amount.ToString("F0"), ChangeNumber, ChangeDocument, Note, Letter, TypeId.ToString(),string.Empty,Status
            };

            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] == null)
                {
                    values[i] = string.Empty;
                }

                if (values[4] == "0")
                {
                    values[4] = string.Empty;
                }
            }

            foreach (string str in values)
            {
                yield return str;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}