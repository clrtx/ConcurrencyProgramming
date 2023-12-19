using System.Collections.Concurrent;

namespace SecondPracticeTask;

public static class ThreadJob
{
    public static void DecompositeDataSearchJob(List<int> primeNumbersLessSqrt, List<int> primeNumbersMoreSqrt, int dataSize, int threadIndex, ConcurrentDictionary<int, List<int>> dictionary)
    {
        var data = primeNumbersMoreSqrt.Skip(dataSize * threadIndex).Take(dataSize).ToList();
                
        foreach (var number in primeNumbersLessSqrt)
        {
            for (var j = 0; j < data.Count; j++)
            {
                if (data[j] % number == 0)
                {
                    data.Remove(data[j]);
                }
            }
        }

        dictionary.TryAdd(threadIndex, data);
    }
    
    public static void DecompositeNumbersSearchJob(List<int> primeNumbersLessSqrt, List<int> primeNumbersMoreSqrt, int numbersSize, int threadIndex, ConcurrentDictionary<int, List<int>> dictionary)
    {
        var data = primeNumbersLessSqrt.Skip(numbersSize * threadIndex).Take(numbersSize).ToList();
        
        var compositeNumbers = new List<int>();
        
        foreach (var number in data)
        {
            compositeNumbers.AddRange(primeNumbersMoreSqrt.Where(item => item % number == 0));
        }

        dictionary.TryAdd(threadIndex, compositeNumbers);
    }

    public static void ThreadPrimeNumbersConsistentEnumerationJob(ConcurrentBag<int> primeNumbersLessSqrt, List<int> primeNumbersMoreSqrt, ConcurrentBag<int> compositeNumbers)
    {
        
        while (primeNumbersLessSqrt.Any())
        {
            var primeNumber = 0;
            primeNumbersLessSqrt.TryTake(out primeNumber);

            foreach (var number in primeNumbersMoreSqrt)
            {
                if (number % primeNumber == 0)
                {
                    compositeNumbers.Add(number);
                }
            }
            
        }
    }

}