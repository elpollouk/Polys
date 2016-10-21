using System;
namespace Polys
{
    public static class DiffEngine
    {
        static int FindVariableIndex(string expression)
        {
            for (var i = 0; i < expression.Length; i++)
                if (expression[i] < '0' || expression[i] > '9') return i; 

            return expression.Length;
        }

        private static string SubReduce(string expression)
        {
            var parts = expression.Split('^');
            var other = parts[0];
            var power = (parts.Length != 1) ? parts[1] : "1";

            int variableIndex = FindVariableIndex(other);
            if (variableIndex == other.Length) return string.Empty;

            var multiplyer = other.Substring(0, variableIndex);
            var variable = other.Substring(variableIndex);


            if (multiplyer != string.Empty)
            {
                multiplyer = (int.Parse(power) * int.Parse(multiplyer)).ToString();
            }
            else
                multiplyer = power;

            var newPower = (int.Parse(power) - 1).ToString();
            if (newPower == "0")
                variable = newPower = string.Empty;
            else if (newPower == "1")
                newPower = string.Empty;
            else
                newPower = "^" + newPower;

            var result = multiplyer + variable + newPower;

            return result;
        }

        public static string Diff(string expression)
        {
            return SubReduce(expression);
        }
    }
}
