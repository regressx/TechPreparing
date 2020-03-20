using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCalc;

namespace NavisElectronics.TechPreparation.Calculations
{
    public class CalculationEngine
    {
       public object Calculate(string formula)
       {
            return new Expression(formula).Evaluate();
       }
    }
}
