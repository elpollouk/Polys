using System;
namespace Polys
{
    public static class DiffEngine
    {
        public static string Diff(string expression)
        {
            var parts = expression.Split('^');
            var power = parts[1];
            var other = parts[0];



            var newPower = (int.Parse(power) - 1).ToString();
            if (newPower == "1")
                newPower = string.Empty;
            else
                newPower = "^" + newPower;

            var result = power + other + newPower;

            return result;
        }
    }
}
