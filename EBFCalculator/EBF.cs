namespace EBFCalculator;

public class EBF
{
    public static double GetUsingBiSection(int depth, int nodes, double error = 0.001) =>
        BiSectionSolve.Get(depth, nodes, error);
    private static double SolveFor(int d, double x) => (Math.Pow(x, d + 1) - 1) / (x - 1);

    private class BiSectionSolve
    {
        private static Func<int, int, double> GetEstimate = (d, n) => Math.Pow(n, (double)1 / d);
        public static double Get(int depth, int nodes, double error)
        {
            double lowerBound = 1 + error;
            double upperBound = GetEstimate(depth, nodes);
            return BiSection(lowerBound, upperBound, depth, nodes, error);
        }
        private static double BiSection(double a, double b, int depth, int nodes, double error)
        {
            double x = (a + b) / 2;
            double fx = SolveFor(depth, x);
            if (Math.Abs(fx - (nodes + 1)) <= error) return x;
            if (fx > nodes + 1) return BiSection(a, x, depth, nodes, error);
            else return BiSection(x, b, depth, nodes, error);
        }
    }
}
