public static class Program
{
    public static void Main(string[] args)
    {
        double[][] A = [[2, 2, 3],
                        [4, 5, 6],
                        [7, 8, 9]];
        Console.WriteLine(CreateEquation(A).Newton(1, 0.1));
    }

    public static double[] CreateEquation(double[][] A)
    {
        if (A.Length == 2)
        {
            return [A[0][0]*A[1][1] - A[0][1]*A[1][0], -(A[1][1]+A[0][0]), 1];
        }
        else if (A.Length == 3)
        {
            return [A[0][0]*A[1][1]*A[2][2] + A[0][1]*A[1][2]*A[2][0] + A[0][2]*A[1][0]*A[2][1]
                    - A[0][2]*A[1][1]*A[2][0] - A[0][1]*A[1][0]*A[2][2] - A[0][0]*A[1][2]*A[2][1],
                    A[0][0]*A[1][1] - A[0][0]*A[2][2] - A[1][1]*A[2][2] + A[0][2]*A[2][0]
                    + A[0][1]*A[1][0] + A[1][2]*A[2][1],
                    A[0][0] + A[1][1] + A[2][2],
                    1];
        }
        return [];
    } 

    // Below is funcs to recreate Newton's method for eqations solving in c#
    public static double Newton(this double[] equation, double x0, double eps)
    {
        double x1;
        do
        {
            x1 = x0 - equation.Calc(x0) / equation.Derivative().Calc(x0);
        } while (Math.Abs(x1 - x0) < eps);
        return x1;
    }

    public static double[] Derivative(this double[] equation)
    {
        double[] der = new double[equation.Length];
        Array.Copy(equation, der, equation.Length);

        for (int i = 1; i < der.Length; i++) der[i] *= i;
        return der.Skip(1).ToArray();
    }

    public static double Calc(this double[] equation, double x)
    {
        double res = 0;
        for (int i = 0; i < equation.Length; i++) res += equation[i] * Math.Pow(x, i);
        return res;
    }
}