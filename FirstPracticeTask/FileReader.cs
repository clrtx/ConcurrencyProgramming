using static System.Int32;

namespace FirstPracticeTask;

public static class FileReader
{
    public static int[] GetArrayFromFile(int start, int length)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "file.txt");

        // using var sr = File.OpenText(path);

        var numbersList = new List<int>();

        using (var sr = new StreamReader(path))
        {
            while (sr.ReadBlock("5".ToArray()) <= 0)
            {
                Console.Write((char)sr.Read());
            }
        }

        // var lines = File.ReadAllLines(path);
        //
        // foreach (var line in lines)
        // {
        //     numbersList.AddRange(line.Split(", ").Select(int.Parse).ToList());
        //     Console.WriteLine(numbersList.Count);
        // }
        
        return numbersList.ToArray();
        
    }

    public static int GetArrayPartCharsLength(int startElement, int arrayPartLength)
    {
        var lastElement = startElement + (arrayPartLength - 1);

        var charsLength = 0;
        
        for (var i = startElement; i <= lastElement; i++)
        {
            charsLength += i.ToString().ToArray().Length; // '25' => 2, '123' => 3
            charsLength += 2; // ', '
        }

        return charsLength;
    }

    public static int[] GetArrayPart(int startIndex, int lastIndex)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "file.txt");

        var numBytes = new byte[lastIndex - startIndex];
        
        using var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        fs.Seek(startIndex, SeekOrigin.Begin);
        fs.Read(numBytes, 0, lastIndex - startIndex);


        var textString = System.Text.Encoding.UTF8.GetString(numBytes);

        
        var numStrings = textString.Split(", ");

        var numList = new List<int>();
        
        foreach (var numString in numStrings)
        {
            var num = 0;
            TryParse(numString, out num);
            
            numList.Add(num);
        }

        return numList.ToArray();
    }
}