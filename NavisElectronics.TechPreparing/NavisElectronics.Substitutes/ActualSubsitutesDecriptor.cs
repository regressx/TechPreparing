using System.Text;

namespace NavisElectronics.Substitutes
{
    /// <summary>
    /// Класс для расшифровки актуального заменителя
    /// </summary>
    public class ActualSubsitutesDecryptor
    {
        private int _current;
        private int _i;
        private StringBuilder _descriptionOnSub = new StringBuilder();

        /// <summary>
        /// Получает инфу об актуальном заменителе, чтобы потом добавить результирующую строку к допустимым заменителям
        /// </summary>
        /// <param name="group"> Группа допустимых замен</param>
        /// <param name="showPositions">
        /// Переменная, где указываем, показываем позицию или полное имя элемента
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetInfoAboutSubsititutionsForSub(SubstituteGroup group, bool showPositions)
        {
            _descriptionOnSub.Clear();
            for (int i = 0; i < group.ActualSub.Count; i++)
            {
                if (showPositions)
                {
                    _descriptionOnSub.AppendFormat("поз. {0}, ", group.ActualSub[i].Position);
                }
                else
                {
                    _descriptionOnSub.AppendFormat("{0}, ", string.Format("{0} {1}", group.ActualSub[i].Name, group.ActualSub[i].Designation).Trim());
                }
                if (i == 0 && group.ActualSub.Count > 1)
                {
                    // Отрезаем лишний пробел и запятую
                    _descriptionOnSub.Remove(_descriptionOnSub.Length - 2, 2);

                    // добавляем, совместно с чем идет компонент
                    _descriptionOnSub.AppendFormat(" совместно с ");
                }
            }
            _descriptionOnSub.Remove(_descriptionOnSub.Length - 2, 2);
            return _descriptionOnSub.ToString();
        }

        /// <summary>
        /// Расшифровывает актуальный заменитель
        /// </summary>
        /// <param name="substituteGroup">
        /// The substitute group.
        /// </param>
        /// <param name="showPositions">
        /// Укажите, следует ли использовать позиции в спецификации или же имена элементов
        /// </param>
        public void DecriptActualSubstitutes(SubstituteGroup substituteGroup, bool showPositions)
        {
            _i = 0;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Допускается замена ");
            if (substituteGroup.ActualSub.Count > 1)
            {
                sb.AppendFormat("совместно с ");
                foreach (IProduct intermechProduct in substituteGroup.ActualSub)
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
                        sb.AppendFormat("{0}, ", string.Format("{0} {1}", intermechProduct.Name, intermechProduct.Designation).Trim());
                    }
                }
                sb.Remove(sb.Length - 2, 2);
                sb.AppendFormat(" на ");
            }
            else
            {
                sb.AppendFormat("на ");
            }

            string result = sb.ToString();
            substituteGroup.ActualSub[_current].SubstituteInfo = result;
            if (_current != (substituteGroup.ActualSub.Count - 1))
            {
                _current++;
                DecriptActualSubstitutes(substituteGroup, showPositions);
            }
            else
            {
                _current = 0;
            }

        }

    }
}