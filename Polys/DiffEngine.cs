using System;
using System.Text.RegularExpressions;

namespace Polys
{
    public static class DiffEngine
    {
        private const string _errorMessage = "Expression doesn't look to be well formed";
        private static readonly Regex _parser = new Regex("^([0-9]*)([a-zA-Z]*)(\\^([0-9]+))?$");

        static int FindVariableIndex(string expression)
        {
            for (var i = 0; i < expression.Length; i++)
                if (expression[i] > '9') return i; 

            return expression.Length;
        }

        private static string SubReduce(string expression)
        {
            var match = _parser.Match(expression);
            if (match.Groups.Count != 5) throw new FormatException(_errorMessage);

            var multiplier = match.Groups[1].Value;
            var variable = match.Groups[2].Value;
            var strPower = match.Groups[4].Value;

            // Validate we have everything
            if (multiplier == string.Empty && variable == string.Empty) throw new FormatException(_errorMessage);
            if (variable == string.Empty) return string.Empty;

            if (strPower == string.Empty) strPower = "1";
            var iPower = int.Parse(strPower);

            if (multiplier != string.Empty)
                multiplier = (iPower * int.Parse(multiplier)).ToString();
            else 
                multiplier = strPower;

            var newPower = (iPower - 1).ToString();
            if (newPower == "0")
                variable = newPower = string.Empty;
            else if (newPower == "1")
                newPower = string.Empty;
            else
                newPower = "^" + newPower;

            return multiplier + variable + newPower;
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
