using System;
using System.Collections.Generic;
using System.Linq;

namespace NavisElectronics.Substitutes
{
    /// <summary>
    /// The subsitute group grabber.
    /// </summary>
    public class SubsituteGroupGrabber : ISubsituteGroupGrabber
    {
        private IEnumerable<IProduct> _products;

        /// <summary>
        /// Список групп допустимых замен
        /// </summary>
        private readonly IList<SubstituteGroup> _subGroups = new List<SubstituteGroup>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SubsituteGroupGrabber"/> class.
        /// </summary>
        /// <param name="products">
        /// Список изделий (это могут быть общие данные или переменные)
        /// </param>
        public SubsituteGroupGrabber(IEnumerable<IProduct> products)
        {
            _products = products;
        }

        /// <summary>
        /// Получаем список групп допустимых замен
        /// </summary>
        public ICollection<SubstituteGroup> SubGroups
        {
            get { return _subGroups; }
        }

        /// <summary>
        /// Получает набор групп заменителей из всех входящих
        /// </summary>
        public void GetGroups()
        {
            if (_products == null)
            {
                throw new NullReferenceException("Нет списка изделий, чтобы найти группы заменителей");
            }
            foreach (IProduct intermechProduct in _products)
            {
                if (intermechProduct.SubstituteGroupNumber != 0)
                {
                    SubstituteGroup gr = null;
                    try
                    {
                        gr = SubGroups.First(group => group.Number == intermechProduct.SubstituteGroupNumber);
                    }
                    catch (InvalidOperationException)
                    {
                        gr = new SubstituteGroup(intermechProduct.SubstituteGroupNumber);
                        _subGroups.Add(gr);
                    }

                    if (intermechProduct.SubstituteNumberInGroup == 0)
                    {
                        gr.ActualSub.Add(intermechProduct);
                    }
                    else
                    {
                        SubstituteSubGroup subGroup = null;
                        try
                        {
                            subGroup =
                                gr.SubGroups.First(subGr => subGr.Number == intermechProduct.SubstituteNumberInGroup);
                        }
                        catch (InvalidOperationException)
                        {
                            subGroup = new SubstituteSubGroup(intermechProduct.SubstituteNumberInGroup);
                            gr.SubGroups.Add(subGroup);
                        }

                        subGroup.Subsitutes.Add(intermechProduct);
                    }
                }
            }
        }


    }
}