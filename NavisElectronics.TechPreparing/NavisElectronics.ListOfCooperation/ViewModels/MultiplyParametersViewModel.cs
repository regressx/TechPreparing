using System.Collections.Generic;

namespace NavisElectronics.ListOfCooperation.ViewModels
{
    public class MultiplyParametersViewModel : IMultiplyParametersViewModel
    {
        private IList<Parameter> _parameters;

        public MultiplyParametersViewModel()
        {
            _parameters = new List<Parameter>();
            Parameter amountParameter = new Parameter()
            {
                Name = "Количество",
                Value = string.Empty
            };

            Parameter stockRate = new Parameter()
            {
                Name = "Коэф. запаса",
                Value = string.Empty
            };
            _parameters.Add(amountParameter);
            _parameters.Add(stockRate);
        }

        public IList<Parameter> GetRegisteredParameters()
        {
            return _parameters;
        }

        public void RegisterParameter(Parameter parameter)
        {
            _parameters.Add(parameter);
        }
    }
}