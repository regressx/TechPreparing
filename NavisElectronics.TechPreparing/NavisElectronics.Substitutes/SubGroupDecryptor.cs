using System.Collections.Generic;
using System.Text;

namespace NavisElectronics.Substitutes
{
    /// <summary>
    /// Класс для расшифровки множества элементов в группе допустимых заменителей
    /// </summary>
    public class SubGroupDecryptor : ISubGroupDecryptor
    {
        private int _current;
        private int _i;
        private StringBuilder _descriptionOnActualSub = new StringBuilder();

        /// <summary>
        /// Расшифровывает элементы в группе допустимых заменителей 
        /// </summary>
        /// <param name="group">
        /// Группа допустимых заменителей
        /// </param>
        /// <param name="showPositions">
        /// Переменная, где указываем, показываем позицию или полное имя элемента
        /// </param>
        public void DecryptElements(SubstituteSubGroup group, bool showPositions)
        {
            IList<IProduct> products = group.Subsitutes;

            // переменная для построения итоговой строки
            StringBuilder sb = new StringBuilder();
            if (products.Count > 1)
            {
                sb.AppendFormat("Применяется с ");
                foreach (IProduct intermechProduct in products)
                {
                    if (_current == _i)
                    {
                        _i++;
                        continue;
                    }
                    _i++;
                    if (showPositions)
                    {
                        sb.AppendFormat("поз. {0}, ", intermechProduct.Position);
                    }
                    else
                    {
                        sb.AppendFormat("{0}, ",string.Format("{0} {1}", intermechProduct.Name, intermechProduct.Designation).Trim());
                    }

                }
                sb.Remove(sb.Length - 2, 2);

                sb.AppendFormat(" взамен ");
            }
            else
            {
                sb.AppendFormat("Взамен ");
            }
            products[_current].SubstituteInfo = sb.ToString();
            if (_current != (products.Count - 1))
            {
                _i = 0;
                _current++;
                DecryptElements(group, showPositions);
            }
            else
            {
                _current = 0;
                _i = 0;
            }
        }

        /// <summary>
        /// Метод получает информацию о наборе допустимых заменителей в группе и составляет строку для актуального заменителя, чтобы получилось,
        /// например: допускается замена на ... и список позиций, определенный в группе допустимых заменителей.
        /// </summary>
        /// <param name="group">группа допустимых заменителей</param>
        /// <param name="showPositions">
        /// Переменная, где указываем, показываем позицию или полное имя элемента
        /// </param>
        /// <returns>Возращает результирующую строку с перечнем компонентов, составляющих группу допустимых заменителей</returns>
        public string GetInfoAboutElementsForActualElement(SubstituteSubGroup group, bool showPositions)
        {
            _descriptionOnActualSub.Clear();
            for (int i = 0; i < group.Subsitutes.Count; i++)
            {
                if (showPositions)
                {
                    _descriptionOnActualSub.AppendFormat("поз. {0}, ", group.Subsitutes[i].Position);
                }
                else
                {
                    _descriptionOnActualSub.AppendFormat("{0}, ",string.Format("{0} {1}", group.Subsitutes[i].Name, group.Subsitutes[i].Designation).Trim());
                }
                if (i == 0 && group.Subsitutes.Count > 1)
                {
                    _descriptionOnActualSub.Remove(_descriptionOnActualSub.Length - 2, 2);
                    _descriptionOnActualSub.AppendFormat(" совместно с ");
                }
            }
            _descriptionOnActualSub.Remove(_descriptionOnActualSub.Length - 2, 2);

            return _descriptionOnActualSub.ToString();
        }

    }
}
