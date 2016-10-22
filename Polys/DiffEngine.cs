using System;
using System.Text.RegularExpressions;

namespace Polys
{
    public static class DiffEngine
    {
        private const string _errorMessage = "Expression doesn't look to be well formed";

        // Matches following groups in a complete string:
        // (3)(x)^(2) - The last group being optional
        // This regex results in 5 groups being returned if matched, for the above example, the groups would be:
        // 0 - "3x^2"
        // 1 - "3"
        // 2 - "x"
        // 3 - "^2"
        // 4 - "2"
        // As a result, we're only interested in groups 1, 2 and 4
        private static readonly Regex _parser = new Regex("^([0-9]*)([a-zA-Z]*)(\\^([0-9]+))?$");

        private static string SubReduce(string expression)
        {
            var match = _parser.Match(expression);
            if (match.Groups.Count != 5) throw new FormatException(_errorMessage);

            var multiplier = match.Groups[1].Value;
            var variable = match.Groups[2].Value;
            var strPower = match.Groups[4].Value;

            // Validate we have everything we need for the calculation
            if (multiplier == string.Empty && variable == string.Empty) throw new FormatException(_errorMessage);
            if (variable == string.Empty) return string.Empty;

            if (strPower == string.Empty) strPower = "1";
            var iPower = int.Parse(strPower);

            if (multiplier != string.Empty)
                multiplier = (iPower * int.Parse(multiplier)).ToString();
            else 
                multiplier = strPower;

            var newPower = (iPower - 1).ToString();
            // Pretty up the output by reducing down redundant terms
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
