namespace LeetCodeProblems;

public class Solution
{
    //https://leetcode.com/problems/difference-between-element-sum-and-digit-sum-of-an-array/description/
    public int DifferenceOfSum(int[] nums)
    {
        var numSum = 0;
        var digitSum = 0;

        foreach (var item in nums)
        {
            var num = item;
            
            numSum += num;

            var mod = 0;
            var div = 0;

            while (num > 0)
            {
                digitSum += num % 10;
                num /= 10;
            }
        }

        return numSum - digitSum;
    }
    
    //https://leetcode.com/problems/sort-integers-by-the-number-of-1-bits/description/
    public int[] SortByBits(int[] arr)
    {
        return arr.OrderBy(item => CountBites(item)).ThenBy(item => item).ToArray();
    }

    public int CountBites(int num)
    {
        var count = 0;
        
        while (num > 0)
        {
            var bitDecimalValue = (int)Math.Pow(2, (int)Math.Floor(Math.Log2(num)));
            num -= bitDecimalValue;
            count++;
        }

        return count;
    }
    
    //https://leetcode.com/problems/last-stone-weight/description/
    public int LastStoneWeight(int[] stones)
    {
        var stonesList = new List<int>(stones);
        
        var length = stones.Length;
        
        while (!length.Equals(1))
        {
            var secondStone = stonesList.Max();
            stonesList.Remove(secondStone);

            var firstStone = stonesList.Max();
            stonesList.Remove(firstStone);
            
            stonesList.Add(secondStone - firstStone);
            
            length--;
        }
        
        return stonesList.First();
    }
    
    //https://leetcode.com/problems/champagne-tower/description/
    
    public double ChampagneTower(int poured, int query_row, int query_glass)
    {
        var glassesArray = new double[query_row + 2, query_row + 2];

        //первый стакан заполняется отдельно
        glassesArray[0, 0] = poured;
        
        for (var row = 0; row < query_row + 1; row++)
        {
            for (var glassIndex = 0; glassIndex < query_row + 1; glassIndex++)
            {
                //если стакан переполняется
                if (glassesArray[row, glassIndex] > 1)
                {
                    //стакан становится полным
                    var champagneOut = glassesArray[row, glassIndex] - 1;
                    glassesArray[row, glassIndex] = 1;
                    
                    //шампанское стекает поровну на следующие стаканы под текущим
                    glassesArray[row + 1, glassIndex] += champagneOut / 2;
                    glassesArray[row + 1, glassIndex + 1] += champagneOut/ 2;
                }
                
            }
        }

        return glassesArray[query_row, query_glass];
    }

    //https://leetcode.com/problems/rotate-image/description/
    public void Rotate(int[][] matrix)
    {
        var rows = matrix.Length;
        var columns = matrix[0].Length;
        
        for (var row = 0; row < rows; row++)
        {
            for (var column = row; column < columns; column++)
            {
                (matrix[row][column], matrix[column][row]) = (matrix[column][row], matrix[row][column]);
            }
        }
        
        for (var column = 0; column < columns/2; column++)
        {
            for (var row = 0; row < rows; row++)
            {
                (matrix[row][columns - 1 - column], matrix[row][column]) = (matrix[row][column], matrix[row][columns - 1 - column]);
            }
        }
    }
    
}

