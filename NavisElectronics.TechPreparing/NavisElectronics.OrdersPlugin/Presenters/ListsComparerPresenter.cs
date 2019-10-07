using System.Collections.Concurrent;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;

namespace NavisElectronics.Orders.Presenters
{
    using System;
    using System.Collections.Generic;
    using Entities;
    using TechPreparation.Interfaces.Entities;

    public class ListsComparerPresenter
    {
        private IntermechTreeElement _root;
        private IListsComparerView _view;
        private IDictionary<long, ReportElement> _firstDictionary;
        private IDictionary<long, ReportElement> _secondDictionary;
        public ListsComparerPresenter(IListsComparerView view)
        {
            _view = view;
            _view.Load += View_Load;
            _view.LoadCsv += View_LoadCsv;
            _view.StartComparing += View_StartComparing;
            _firstDictionary = new SortedDictionary<long, ReportElement>();
            _secondDictionary = new SortedDictionary<long, ReportElement>();
        }

        private void View_StartComparing(object sender, EventArgs e)
        {
            foreach (long key in _firstDictionary.Keys)
            {
                if (!_secondDictionary.ContainsKey(key))
                {
                    ReportElement firstElement = _firstDictionary[key];
                    firstElement.Color = Color.IndianRed;
                }
            }

            foreach (long key in _secondDictionary.Keys)
            {
                if (_firstDictionary.ContainsKey(key))
                {
                    ReportElement firstElement = _firstDictionary[key];
                    ReportElement secondElement = _secondDictionary[key];

                    // пропустить, если с элементом что-то не так
                    if (secondElement.Color == Color.IndianRed)
                    {
                        continue;
                    }

                    secondElement.Color = Color.LightGreen;

                    if (firstElement.MeasureUnits != secondElement.MeasureUnits)
                    {
                        secondElement.Color = Color.Yellow;
                    }


                    if (Math.Abs(firstElement.AmountWithUse - secondElement.AmountWithUse) > 0.0001F)
                    {
                        secondElement.Color = Color.Yellow;
                    }

                }
                else
                {
                    ReportElement secondElement = _secondDictionary[key];
                    secondElement.Color = Color.DeepSkyBlue;
                }
            }
            _view.FillFirstGrid(_firstDictionary.Values.ToList());
            _view.FillSecondGrid(_secondDictionary.Values.ToList());
        }

        private void View_LoadCsv(object sender, EventArgs e)
        {
            using (OpenFileDialog opf = new OpenFileDialog())
            {
                if (opf.ShowDialog() == DialogResult.OK)
                {
                    using (StreamReader sr = new StreamReader(opf.FileName, Encoding.Default))
                    {
                        while (!sr.EndOfStream)
                        {
                            string[] line = sr.ReadLine().Split(';');
                            ReportElement element = new ReportElement();
                            element.ObjectId = Convert.ToInt64(line[0]);
                            element.Caption = line[1];
                            if (line[2] != string.Empty)
                            {
                                element.AmountWithUse = Convert.ToDouble(line[2].Split(' ')[0]);
                                element.MeasureUnits = line[2].Split(' ')[1];
                            }

                            if (_secondDictionary.ContainsKey(element.ObjectId))
                            {
                                element = _secondDictionary[element.ObjectId];
                                element.Color = Color.IndianRed;
                            }
                            else
                            {
                                _secondDictionary.Add(element.ObjectId, element);
                            }
                        }
                    }

                    _view.FillSecondGrid(_secondDictionary.Values.ToList());
                }
            }

        }

        public void Run(IntermechTreeElement root)
        {
            _root = root;
            _view.Show();
        }

        private void View_Load(object sender, EventArgs e)
        {
            Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
            queue.Enqueue(_root);
            while (queue.Count > 0) 
            {
                IntermechTreeElement elementFromQueue = queue.Dequeue();

                ReportElement element = null;
                if (_firstDictionary.ContainsKey(elementFromQueue.ObjectId))
                {
                    element = _firstDictionary[elementFromQueue.ObjectId];
                    element.AmountWithUse += elementFromQueue.AmountWithUse;
                }
                else
                {
                    if (elementFromQueue.Type == 1128 || elementFromQueue.Type == 1125)
                    {
                        if (elementFromQueue.Parent.Type == 1052 || elementFromQueue.Parent.Type == 1159)
                        {
                            continue;
                        }
                    }

                    element = new ReportElement();
                    element.ObjectId = elementFromQueue.ObjectId;
                    element.Caption = elementFromQueue.Designation + " " + elementFromQueue.Name;
                    element.AmountWithUse = elementFromQueue.AmountWithUse;
                    element.MeasureUnits = elementFromQueue.MeasureUnits;
                    _firstDictionary.Add(element.ObjectId, element);
                }

                foreach (IntermechTreeElement child in elementFromQueue.Children)
                {
                    queue.Enqueue(child);
                }
            }

            _view.FillFirstGrid(_firstDictionary.Values.ToList());
        }


    }
}