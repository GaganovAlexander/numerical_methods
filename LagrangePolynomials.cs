public static class Program
{
    public static void Main(string[] args)
    {
        double[][] example = [[2, 3, 4, 5],
                              [7, 5, 8, 7]];
        double[] res = LagrangePolynomials(example);

        // Just a beautiful print of function aproximation
        Console.Write($"f(x) ≈ ({res[0]}) + ");
        Console.Write($"({res[1]}x) + ");
        for(int i = 2; i < res.Length-1; i++) Console.Write($"({res[i]}x^{i}) + ");
        Console.WriteLine($"({res[^1]}x^{res.Length-1})");
        try
        {
            double exampleX = double.Parse(args[0]);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Missed 1 required argument(example x where we're finding approximate value)");
            return;
        }
        Console.WriteLine($"f({exampleX}) ≈ {res.Calc(exampleX)}");
    }

    public static double[] LagrangePolynomials(double[][] A)
    {
        double[][] sumOuter = new double[A[1].Length][];
        for (int i = 0; i < A[0].Length; i++) 
        {
            double[] slice = [..A[0][..i], ..A[0][(i+1)..]];
            slice.Mult(-1);
            sumOuter[i] = CreateEquation(slice);
            foreach (double j in slice) sumOuter[i].Divide(A[0][i] + j);
            sumOuter[i].Mult(A[1][i]);
        }
        return sumOuter.Sum();
    }

    public static double[] CreateEquation(double[] v)
    {
        double[] res = new double[v.Length+1];
        for (int i = 0; i < v.Length; i++)
        {
            res[i] = 0;
            var combs = GetKCombs(v, v.Length - i);
            foreach(var comb in combs)
            {
                double tempRes = 1;
                foreach(var k in comb) tempRes *= k;
                res[i] += tempRes;
            }
        }
        res[v.Length] = 1;
        return res;
    }

    private static double[] Sum(this double[][] A)
    {
        double[] res = A[0];
        foreach (var x in A[1..]) res.Plus(x);
        return res;
    }

    // Refactored from PVIterMethod
    private static void Divide(this double[] x, double y)
    {
        for (int i = 0; i < x.Length; i++) x[i] /= y;
    }

    private static void Mult(this double[] x, double y)
    {
        for (int i = 0; i < x.Length; i++) x[i] *= y;
    }

    private static void Plus(this double[] x, double[] y)
    {
        for(int i = 0; i < x.Length; i++) x[i] += y[i];
    }

    // From PVDirectUnwindingMethod.cs
    public static double Calc(this double[] equation, double x)
    {
        double res = 0;
        for (int i = 0; i < equation.Length; i++) res += equation[i] * Math.Pow(x, i);
        return res;
    }

    // Я не хотел реализовывать Combinations сам, так что эта функция взята с переполнения стека)
    static IEnumerable<IEnumerable<T>> GetKCombs<T>(IEnumerable<T> list, int length) where T : IComparable
    {
        if (length == 1) return list.Select(t => new T[] { t });
        return GetKCombs(list, length - 1)
            .SelectMany(t => list.Where(o => o.CompareTo(t.Last()) > 0), 
                (t1, t2) => t1.Concat([t2]));
    }
}