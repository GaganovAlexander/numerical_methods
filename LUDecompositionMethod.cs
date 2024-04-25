public static class Program
{
    public static void Main(string[] args)
    {
        const int n = 3;
        double[,] A = new double[n, n] {{1, 2, -1},
                                        {2, -3, 2},
                                        {3, 1, 1}};
        double[] b = [2, 2, 8];
        Console.WriteLine(string.Join(" ", LUDecomposition(A, b, n)));
    }

    public static double[] LUDecomposition(double[,] matrix, double[] rightPart, int n)
    {
        double[,] lu = new double[n, n];
        double sum;
        for (int i = 0; i < n; i++)
        {
            for (int j = i; j < n; j++)
            {
                sum = 0;
                for (int k = 0; k < i; k++)
                    sum += lu[i, k] * lu[k, j];
                lu[i, j] = matrix[i, j] - sum;
            }
            for (int j = i + 1; j < n; j++)
            {
                sum = 0;
                for (int k = 0; k < i; k++)
                    sum += lu[j, k] * lu[k, i];
                lu[j, i] = (matrix[j, i] - sum) / lu[i, i];
            }
        }

        double[] y = new double[n];
        for (int i = 0; i < n; i++)
        {
            sum = 0;
            for (int k = 0; k < i; k++)
                sum += lu[i, k] * y[k];
            y[i] = rightPart[i] - sum;
        }

        double[] x = new double[n];
        for (int i = n - 1; i >= 0; i--)
        {
            sum = 0;
            for (int k = i + 1; k < n; k++)
                sum += lu[i, k] * x[k];
            x[i] = (y[i] - sum) / lu[i, i];
        }
        
        return x;
    }
}