using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavisElectronics.TechPreparation.Services
{
    internal static class ParametersParser
    {
        internal static IEnumerable<string> GetParameters(string formula)
        {
            if (string.IsNullOrEmpty(formula))
            {
                throw new ArgumentNullException("formula", "У материала не указана формула!");
            }
            ICollection<string> parameters = new List<string>();
            int openedIndex = -1;
            int i = 0;
            foreach (char c in formula)
            {
                switch (c)
                {
                    case '[':
                        openedIndex = i;
                        break;

                    case ']':

                        // вырезать то, что между скобками
                        string parameter = formula.Substring(openedIndex + 1, i - openedIndex - 1);
                        parameters.Add(parameter);
                        openedIndex = -1;
                        break;
                }
                i++;
            }
            return parameters;
        }


        private static bool IsValidParentheses(string s)
        {
            Stack<char> toClose = new Stack<char>();
            foreach (char c in s)
            {
                if (c == '(')
                {
                    toClose.Push(')');
                }
                else
                {
                    if (c == '[')
                    {
                        toClose.Push(']');
                    }
                    else
                    {
                        if (c == '{')
                        {
                            toClose.Push('}');
                        }
                        else
                        {
                            if (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || c == '*' || c == '+' || c == '-')
                            {
                                continue;
                            }

                            if (toClose.Count == 0)
                            {
                                return false;
                            }

                            if (c != toClose.Peek())
                            {
                                return false;
                            }
                            toClose.Pop();
                        }
                    }
                }
            }

            return toClose.Count == 0;
        }

    }
}
