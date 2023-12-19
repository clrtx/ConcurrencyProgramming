// See https://aka.ms/new-console-template for more information

using SecondPracticeTask;

var n = 100_000;
var threadCount = 20;

Console.WriteLine("Последовательный алгоритм");
var consistentSearchResult = EratosthenesSieve.ConsistentSearchWrapper(n);
Console.WriteLine(consistentSearchResult.Count);
// Console.WriteLine(string.Join(", ", consistentSearchResult));

Console.WriteLine("\nМодифицированный последовательный алгоритм");
var modifiedConsistentSearchResult = EratosthenesSieve.ModifiedConsistentSearch(n);
Console.WriteLine(modifiedConsistentSearchResult.Count);
// Console.WriteLine(string.Join(", ", modifiedConsistentSearchResult));

Console.WriteLine("\nПараллельный алгоритм №1: декомпозиция по данным");
var decompositeDataSearchResult = EratosthenesSieve.DecompositeDataSearch(n, threadCount);
Console.WriteLine(decompositeDataSearchResult.Count);
// Console.WriteLine(string.Join(", ", decompositeDataSearchResult));

Console.WriteLine("\nПараллельный алгоритм №2: декомпозиция набора простых чисел");
var decompositeNumbersSearchResult = EratosthenesSieve.DecompositeNumbersSearch(n, threadCount);
Console.WriteLine(decompositeNumbersSearchResult.Count);
// Console.WriteLine(string.Join(", ", decompositeNumbersSearchResult));

Console.WriteLine("\nПараллельный алгоритм №3: применение пула потоков");
var threadPoolSearchResult = EratosthenesSieve.ThreadPoolSearch(n);
Console.WriteLine(threadPoolSearchResult.Count);
// Console.WriteLine(string.Join(", ", threadPoolSearchResult));

Console.WriteLine("\nПараллельный алгоритм №4: последовательный перебор простых чисел");
var primeNumbersConsistentEnumerationSearchResult = EratosthenesSieve.PrimeNumbersConsistentEnumerationSearch(n, threadCount);
Console.WriteLine(primeNumbersConsistentEnumerationSearchResult.Count);
// Console.WriteLine(string.Join(", ", primeNumbersConsistentEnumerationSearchResult));

Console.WriteLine();
Console.WriteLine(consistentSearchResult.SequenceEqual(modifiedConsistentSearchResult)
                  && modifiedConsistentSearchResult.SequenceEqual(decompositeDataSearchResult)
                  && decompositeDataSearchResult.SequenceEqual(decompositeNumbersSearchResult)
                  && decompositeNumbersSearchResult.SequenceEqual(threadPoolSearchResult)
                  && threadPoolSearchResult.SequenceEqual(primeNumbersConsistentEnumerationSearchResult));
