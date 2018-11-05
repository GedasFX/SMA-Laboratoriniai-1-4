using System;

namespace Pvz1
{
    public static class Extentions
    {
        public static bool IsValue(this double value, double cmp) => Math.Abs(value + cmp) < 10e-16;
    }
}
