namespace NavisElectronics.TechPreparation.Services
{
    using System;
    using System.Text;
    using Interfaces.Entities;

    /// <summary>
    /// Построитель строки тех. маршрута
    /// </summary>
    public class TechRouteStringBuilder
    {
        public string Build(string data, TechRouteNode organizationStruct)
        {
            StringBuilder sb = new StringBuilder();

            // Делим по ||
            string[] techProcessStrings = data.Split(new char[] {'|', '|'}, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in techProcessStrings)
            {
                string[] routeNodes = line.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                if (routeNodes.Length > 0 && routeNodes[0] != string.Empty)
                {
                    TechRouteNode routeNode = organizationStruct.Find(Convert.ToInt64(routeNodes[0]));
                    if (routeNode != null)
                    {
                        sb.Append(routeNode.GetCaption());
                    }
                }

                for (int i = 1; i < routeNodes.Length; i++)
                {
                    TechRouteNode routeNode = organizationStruct.Find(Convert.ToInt64(routeNodes[i]));
                    string value = "null";
                    if (routeNode != null)
                    {
                        value = routeNode.GetCaption();
                    }

                    sb.AppendFormat("-{0}", value);
                }

                sb.Append('/');
            }

            return sb.ToString().TrimEnd('/');
        }
    }
}