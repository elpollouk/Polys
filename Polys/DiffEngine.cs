using System;
using System.Text.RegularExpressions;

namespace Polys
{
    public static class DiffEngine
    {
        private static readonly Regex _parser = new Regex("^([0-9]*)([a-zA-Z]*)(\\^([0-9]+))?$");

        static int FindVariableIndex(string expression)
        {
            for (var i = 0; i < expression.Length; i++)
                if (expression[i] > '9') return i; 

            return expression.Length;
        }

        private static string SubReduce(string expression)
        {
            if (expression == string.Empty) throw new FormatException("Attempted to evaluate an empty string");
            var match = _parser.Match(expression);
            if (match.Captures.Count == 0) throw new FormatException("Expression doesn't look to be well formed");

            var parts = expression.Split('^');
            var other = parts[0];
            var strPower = (parts.Length != 1) ? parts[1] : "1";
            var iPower = int.Parse(strPower);

            int variableIndex = FindVariableIndex(other);
            if (variableIndex == other.Length) return string.Empty;

            var multiplyer = other.Substring(0, variableIndex);
            var variable = other.Substring(variableIndex);

            if (multiplyer != string.Empty)
                multiplyer = (iPower * int.Parse(multiplyer)).ToString();
            else
                multiplyer = strPower;

            var newPower = (iPower - 1).ToString();
            if (newPower == "0")
                variable = newPower = string.Empty;
            else if (newPower == "1")
                newPower = string.Empty;
            else
                newPower = "^" + newPower;

            return multiplyer + variable + newPower;
        }

        public static string Diff(string expression)
        {
            var subExpressions = expression.Split('+');

            var result = string.Empty;
            foreach (var subExpression in subExpressions)
            {
                var reduced = SubReduce(subExpression);
                if (reduced != string.Empty)
                    if (result == string.Empty)
                        result = reduced;
                    else
                        result += "+" + reduced;

            }

            return result;
        }
    }
}
