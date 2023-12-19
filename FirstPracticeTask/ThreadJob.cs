using System.Collections.Concurrent;
using System.Diagnostics;

namespace FirstPracticeTask;

public static class ThreadJob
{
    public static void DoubleArrayElems(int[] arrayPart)
    {

        for (var i = 0; i < arrayPart.Length; i++)
        {
            arrayPart[i] *= 2;
        }


    }
    
    public static void Sqrt(int[] arrayPart)
    {
        
        for (var i = 0; i < arrayPart.Length; i++)
        {
            var acc = (double)arrayPart[i];
            
            for (var j = 0; j < 100; j++)
            {
                acc = Math.Sqrt(acc);
            }
        }
        
    }

}