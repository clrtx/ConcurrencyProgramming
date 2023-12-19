using Microsoft.VisualBasic;

namespace FirstPracticeTask;

public static class FileWriter
{
    public static void CreateFilledFile(int n)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "file.txt");

        using var sw = File.CreateText(path);

        var currentNumber = 1;
        
        const int batchSize = 20_000_000;
        
        IEnumerable<int> range = Array.Empty<int>();
        
        while (n > 0)
        {
            if (n >= batchSize)
            {
                range = Enumerable.Range(currentNumber, batchSize);
                sw.WriteLine(string.Join(", ", range));
                currentNumber += batchSize;
                n -= batchSize;
            }
            else
            {
                range = Enumerable.Range(currentNumber, n);
                sw.WriteLine($"{string.Join(", ", range)}");
                break;
            }
            Console.WriteLine(n);
        }
        
    }
}