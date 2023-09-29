// See https://aka.ms/new-console-template for more information

using LeetCodeProblems;

var solution = new Solution();

// Console.WriteLine(solution.DifferenceOfSum(new []{11, 22, 33, 44}));

// var arr = solution.SortByBits(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 });
// arr = solution.SortByBits(new[] { 1024, 512, 256, 128, 64, 32, 16, 8, 4, 2, 1 });

// foreach (var num in arr)
// {
//     Console.WriteLine(num); 
// }


// Console.WriteLine(solution.LastStoneWeight(new[]{2, 2}));

// Console.WriteLine(solution.ChampagneTower(100000009, 33, 17));

var matrix = new int[][]
{
    new []{1, 2, 3, 4},
    new []{5, 6, 7, 8},
    new []{9, 10, 11, 12},
    new []{13, 14, 15, 16}
};

solution.Rotate(matrix);

matrix.ToList().ForEach(item => Console.WriteLine(string.Join(", ", item.ToList())));