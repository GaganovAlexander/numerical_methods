using System;
using System.Linq;
public static class Program 
{
    public static void Main(string[] args) {
        double[][] A, res;
        double eps = 0.1;
        if (args.Length == 0 || args[0] == "1") 
        {
            A = new double[][] {new double[] {5, 1, 2}, new double[] {1, 4, 1}, new double[] {2, 1, 3}};
            res = IterationsMethod(A, eps);
            
        }
        else if (args[0] == "2")
        {
            A = new double[][] {new double[] {-26, -33, -25}, new double[] {31, 42, 23}, new double[] {-11, -15, -4}};
            res = IterationsMethod(A, eps);
        }
        else return;
        Console.WriteLine("Собственный вектор V = [" + string.Join(" ", res[0]) + "]");
        Console.WriteLine("Собственное значение L = " + res[1][0]);
        Console.WriteLine("A*V = " + string.Join(" ", res[0].Mult(res[1][0])));
        Console.WriteLine("A*L = " + string.Join(" ", A.Mult(res[0])));
    }

    private static double[][] IterationsMethod(double[][] A, double eps)
    {
        double[] x0 = Enumerable.Repeat(1.0, A.Length).ToArray();
        double[] x1 = A.Mult(x0);
        double lambda0 = x1[0] / x0[0];
        double lambda1 = lambda0;
        int iterations = 0;
        do 
        {
            Array.Copy(x1, x0, x1.Length);
            x1 = A.Mult(x0);

            lambda0 = lambda1;
            lambda1 = x1[0] / x0[0];

            if (iterations % 5 == 0) x1.Normalize2();

            iterations++;
            if (iterations == 500)
            {
                Console.WriteLine("Max terations completed");
                break;
            }
        } while (Math.Abs(lambda1 - lambda0) > eps);
        x1.Normalize2();
        return new double[][] {x1, new double[] {lambda1}};
    }

    private static void Normalize2(this double[] x)
    {
        double norma = 0;
        for (int i = 0; i < x.Length; i++)
        {
            norma += x[i] * x[i];
        }
        norma = Math.Sqrt(norma);

        for (int i = 0; i < x.Length; i++)
        {
            x[i] /= norma;
        }
    }

    private static double Mult(this double[] x, double[] y)
    {
        double res = 0;
        for (int i = 0; i < x.Length; i++)
        {
            res += x[i] * y[i];
        }
        return res;
    }

    private static double[] Mult(this double[][] A, double[] x)
    {
        double[] res = new double[A.Length];
        for (int i = 0; i < A.Length; i++)
        {
            res[i] = A[i].Mult(x);
        }
        return res;
    }

    private static double[] Mult(this double[] x, double y)
    {
        double[] res = new double[x.Length];
        for (int i = 0; i < x.Length; i++)
        {
            res[i] = x[i] * y;
        }
        return res;
    }
}
