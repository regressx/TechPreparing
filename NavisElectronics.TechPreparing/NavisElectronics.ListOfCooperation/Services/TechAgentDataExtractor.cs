namespace NavisElectronics.TechPreparation.Services
{
    using System;

    /// <summary>
    /// Класс извлекает данные по тегам из указанной строки
    /// </summary>
    public class TechAgentDataExtractor
    {
        /// <summary>
        /// Метод извлекает данные по Id предприятия, получая нужное содержимое тех. маршрута
        /// </summary>
        /// <param name="data">
        /// Строка с данными
        /// </param>
        /// <param name="agentId">
        /// Идентификатор агента
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public string ExtractData(string data, string agentId)
        {
            if (agentId == null)
            {
                throw new ArgumentNullException("agentId");
            }
            string temp = string.Empty;

            // отделим друг от друга изготовителей
            string[] lines = data.Split(new char[] { '/', '>' });

            if (lines.Length > 0)
            {
                foreach (string line in lines)
                {
                    if (line.Contains(agentId))
                    {
                        int colIndex = line.IndexOf(':');
                        if (colIndex > -1)
                        {
                            temp = line.Substring(colIndex + 1);
                        }
                        break;
                    }
                }
            }
            return temp;
        }

        public string RemoveData(string data, string agentId)
        {
            string temp = string.Empty;

            int index = data.IndexOf(agentId);

            if (index > 0)
            {
                int indexEnd = data.IndexOf("/>");
                if (indexEnd > 0)
                {
                    // к каждому индексу прибавляем единицу, а также добавляем единицу к общему количеству, тогда получим искомое количество для удаления
                    temp = data.Remove(index-1, indexEnd + 1 - index + 1 + 1);
                }
            }
            else
            {
                temp = data;
            }
            return temp;
        }

    }
}