using System;

class TwoGirlsFallacy
{
    static Random rand = new Random();

    static int MonteCarloTGF()
    {
        
        int result = 0;
        int child1 = rand.Next(2); // 0 = boy, 1 = girl
        int child2 = rand.Next(2);

        if (child1 == 1 || child2 == 1) // atleast one child is a girl
        {
            if (child1 == 1 && child2 == 1) // both children are girls
            {
                result = 1;
            }
        }

        if (child1 == 0 && child2 == 0)
        {
            // Cases where both are boys are to be ignored
            // Run MonteCarlo again
            result = MonteCarloTGF(); 
        }

        return result;
    }
    public static void Main()
    {
        int n = 100000;
        int[] results = new int[n];
        double sum = 0;
        Console.WriteLine($"Running Monte Carlo (TGF) for: {n} times.");

        for (int i = 0; i < n; i++)
        {
            results[i] = MonteCarloTGF();
            sum += results[i];
        }

        Console.WriteLine(sum);

        double mean = sum / n;

        // Variance
        double varianceSum = 0;
        for (int i = 0; i < n; i++)
        {
            varianceSum += Math.Pow(results[i] - mean, 2);
        }
        double variance = varianceSum / (n - 1);

        // RMSE
        double rmse = Math.Sqrt(variance / n);

        // RMSE = 0.001
        double newSampleSize = variance / (0.001 * 0.001);

        Console.WriteLine($"Estimated Probability: {mean}");
        Console.WriteLine($"Variance: {variance}");
        Console.WriteLine($"RMSE: {rmse}");
        Console.WriteLine($"New Sample Size Required for RMSE 0.001: {Math.Ceiling(newSampleSize)}");

        // Confidence Interval

        double stdDev = Math.Sqrt(variance);
        double marginOfError = 2.58 * (stdDev / Math.Sqrt(n));
        double lowerBound = mean - marginOfError;
        double upperBound = mean + marginOfError;

        Console.WriteLine($"Confidence Interval: [{lowerBound}, {upperBound}]");
    }
}