using System.Collections.Concurrent;
using System.Diagnostics;

namespace SecondPracticeTask;

public static class EratosthenesSieve
{
    public static List<int> ConsistentSearchWrapper(int n)
    {
        var stopwatch = Stopwatch.StartNew();
        
        var result = ConsistentSearch(n);
            
        Console.WriteLine($"{stopwatch.ElapsedMilliseconds} мс");

        return result;
    }
    
    public static List<int> ConsistentSearch(int n)
    {
        var primeNumbers = Enumerable.Range(2, n - 1).ToList();

        for (var i = 0; i < primeNumbers.Count; i++)
        {
            for (var j = i + 1; j < primeNumbers.Count; j++)
            {
                if (primeNumbers[j] % primeNumbers[i] == 0)
                {
                    primeNumbers.Remove(primeNumbers[j]);
                }
            }
        }
        
        return primeNumbers;
    }

    public static List<int> ModifiedConsistentSearch(int n)
    {
        var stopwatch = Stopwatch.StartNew();
        
        var primeNumbersLessSqrt = ConsistentSearch((int)Math.Floor(Math.Sqrt(n)));
        
        var primeNumbersMoreSqrt = Enumerable.Range(primeNumbersLessSqrt.Last() + 1, n - primeNumbersLessSqrt.Last()).ToList();

        for (var i = 0; i < primeNumbersLessSqrt.Count; i++)
        {
            for (var j = 0; j < primeNumbersMoreSqrt.Count; j++)
            {
                if (primeNumbersMoreSqrt[j] % primeNumbersLessSqrt[i] == 0)
                {
                    primeNumbersMoreSqrt.Remove(primeNumbersMoreSqrt[j]);
                }
            }
        }

        primeNumbersLessSqrt.AddRange(primeNumbersMoreSqrt);
        
        Console.WriteLine($"{stopwatch.ElapsedMilliseconds} мс");
        
        return primeNumbersLessSqrt;
    }

    public static List<int> DecompositeDataSearch(int n, int threadCount)
    {
        var stopwatch = Stopwatch.StartNew();
        
        var primeNumbersLessSqrt = ConsistentSearch((int)Math.Floor(Math.Sqrt(n)));

        var primeNumbersMoreSqrt = Enumerable.Range(primeNumbersLessSqrt.Last() + 1, n - primeNumbersLessSqrt.Last()).ToList();

        var threadList = new List<Thread>();

        var concurrentDictionary = new ConcurrentDictionary<int, List<int>>();
        
        var dataSize = (int)Math.Ceiling((double)primeNumbersMoreSqrt.Count / threadCount);

        
        for (var i = 0; i < threadCount; i++)
        {
            var threadIndex = i;
            
            var thread = new Thread(() => ThreadJob.DecompositeDataSearchJob(primeNumbersLessSqrt, primeNumbersMoreSqrt, dataSize, threadIndex, concurrentDictionary));
            
            threadList.Add(thread);
        }

        foreach (var thread in threadList)
        {
            thread.Start();
        }

        while (threadList.Any(item => item.IsAlive))
        {
            
        }

        foreach (var primeNumbers in concurrentDictionary.Values)
        {
            primeNumbersLessSqrt.AddRange(primeNumbers);
        }
        
        Console.WriteLine($"{stopwatch.ElapsedMilliseconds} мс");
        
        return primeNumbersLessSqrt;
    }
    
    public static List<int> DecompositeNumbersSearch(int n, int threadCount)
    {
        var stopwatch = Stopwatch.StartNew();
        
        var primeNumbersLessSqrt = ConsistentSearch((int)Math.Floor(Math.Sqrt(n)));

        var primeNumbersMoreSqrt = Enumerable.Range(primeNumbersLessSqrt.Last() + 1, n - primeNumbersLessSqrt.Last()).ToList();

        var threadList = new List<Thread>();

        var concurrentDictionary = new ConcurrentDictionary<int, List<int>>();
        
        var numbersSize = (int)Math.Ceiling((double)primeNumbersLessSqrt.Count / threadCount);
        
        for (var i = 0; i < threadCount; i++)
        {
            var threadIndex = i;
            
            var thread = new Thread(() => ThreadJob.DecompositeNumbersSearchJob(primeNumbersLessSqrt, new List<int>(primeNumbersMoreSqrt), numbersSize, threadIndex, concurrentDictionary));
            
            threadList.Add(thread);
        }

        foreach (var thread in threadList)
        {
            thread.Start();
        }

        while (threadList.Any(item => item.IsAlive))
        {
            
        }

        var compositeNumbers = concurrentDictionary.Values.SelectMany(x => x).Distinct().ToList();

        primeNumbersMoreSqrt.RemoveAll(item => compositeNumbers.Contains(item));
        
        primeNumbersLessSqrt.AddRange(primeNumbersMoreSqrt);
        
        Console.WriteLine($"{stopwatch.ElapsedMilliseconds} мс");
        
        return primeNumbersLessSqrt;
    }

    public static List<int> ThreadPoolSearch(int n)
    {
        var stopwatch = Stopwatch.StartNew();
        
        var primeNumbersLessSqrt = ConsistentSearch((int)Math.Floor(Math.Sqrt(n)));
        
        var primeNumbersMoreSqrt = Enumerable.Range(primeNumbersLessSqrt.Last() + 1, n - primeNumbersLessSqrt.Last()).ToList();

        var concurrentDictionary = new ConcurrentDictionary<int, List<int>>();
        
        foreach (var primeNumber in primeNumbersLessSqrt)
        {
            ThreadPool.QueueUserWorkItem((state) => ThreadPoolSearchJob(primeNumbersMoreSqrt, primeNumber, concurrentDictionary));
        }

        while (ThreadPool.CompletedWorkItemCount != primeNumbersLessSqrt.Count)
        {
            
        }
        
        var compositeNumbers = concurrentDictionary.Values.SelectMany(x => x).Distinct().ToList();

        primeNumbersMoreSqrt.RemoveAll(item => compositeNumbers.Contains(item));
        
        primeNumbersLessSqrt.AddRange(primeNumbersMoreSqrt);
        
        Console.WriteLine($"{stopwatch.ElapsedMilliseconds} мс");
        
        return primeNumbersLessSqrt;
    }
    
    static void ThreadPoolSearchJob(List<int> primeNumbersMoreSqrt, int primeNumber, ConcurrentDictionary<int, List<int>> dictionary)
    {
        var compositeNumbers = primeNumbersMoreSqrt.Where(number => number % primeNumber == 0).ToList();

        dictionary.TryAdd(primeNumber, compositeNumbers);
    }

    public static List<int> PrimeNumbersConsistentEnumerationSearch(int n, int threadCount)
    {
        var stopwatch = Stopwatch.StartNew();
        
        var primeNumbersLessSqrt = ConsistentSearch((int)Math.Floor(Math.Sqrt(n)));

        var primeNumbersMoreSqrt = Enumerable.Range(primeNumbersLessSqrt.Last() + 1, n - primeNumbersLessSqrt.Last()).ToList();

        var threadList = new List<Thread>();
        

        var primeNumbersLessSqrtBag = new ConcurrentBag<int>(primeNumbersLessSqrt);
        var primeNumbersMoreSqrtBag = new ConcurrentBag<int>();
        
        for (var i = 0; i < threadCount; i++)
        {
            var thread = new Thread(() => ThreadJob.ThreadPrimeNumbersConsistentEnumerationJob(primeNumbersLessSqrtBag, primeNumbersMoreSqrt, primeNumbersMoreSqrtBag));
            
            threadList.Add(thread);
        }
        
        foreach (var thread in threadList)
        {
            thread.Start();
        }

        while (threadList.Any(item => item.IsAlive))
        {
            
        }

        var bagSize = primeNumbersMoreSqrtBag.Count;
        
        var resultArray = new int[bagSize];
        primeNumbersMoreSqrtBag.CopyTo(resultArray, 0);

        var compositeNumbers = resultArray.Distinct().ToList();

        primeNumbersMoreSqrt.RemoveAll(item => compositeNumbers.Contains(item));
        
        primeNumbersLessSqrt.AddRange(primeNumbersMoreSqrt);
        
        Console.WriteLine($"{stopwatch.ElapsedMilliseconds} мс");
        
        return primeNumbersLessSqrt;
    }
}