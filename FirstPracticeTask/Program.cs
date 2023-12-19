
using System.Diagnostics;
using FirstPracticeTask;

var n = 10_000_000;
var m = 50;

var partSize = n / m;

FileWriter.CreateFilledFile(n);

var pos = 0;

//https://docs.google.com/spreadsheets/d/1-3ie8607DVuNZAsm94gIqgLaZbYrcr28PvneD1jxd60/edit#gid=0

var threadList = new List<Thread>();

for (var i = 0; i < m; i++)
{
    var charsLength = FileReader.GetArrayPartCharsLength(1 + (i * partSize),  partSize);

    var posAcc = pos;
    
    var arrayPart = FileReader.GetArrayPart(posAcc, posAcc + charsLength - 2);
    
    // var thread = new Thread(() => ThreadJob.DoubleArrayElems(arrayPart));
    var thread = new Thread(() => ThreadJob.Sqrt(arrayPart));
    
    threadList.Add(thread);

    pos += charsLength;
}

Console.WriteLine("start");

var stopwatch = Stopwatch.StartNew();

foreach (var thread in threadList)
{
    thread.Start();
}

while (threadList.Any(item => item.IsAlive))
{
            
}

stopwatch.Stop();
Console.WriteLine(stopwatch.ElapsedMilliseconds);

