namespace NavisElectronics.TechPreparation.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel;

    /// <summary>
    /// Класс для задания параметров, которые будут отображаться в окне просмотра
    /// </summary>
    public class Parameter
    {
        [Browsable(false)]
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class Parameter<T>
    {
        private IList<T> _myParameters;

        public Parameter()
        {
            _myParameters = new List<T>();
        }

        public void AddParameter(T value)
        {
            _myParameters.Add(value);
        }

        public void SetParameter(int index, T value)
        {
            _myParameters[index] = value;
        }

        public T GetParameter(int index)
        {
            return _myParameters[index];
        }

        public int Count
        {
            get
            {
                return _myParameters.Count;
            }
        }

    }

}