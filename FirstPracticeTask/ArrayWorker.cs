namespace FirstPracticeTask;

public static class ArrayWorker
{
    public static void DoubleArrayElements(int[] array)
    {
        for (var i = 0; i < array.Length; i++)
        {
            array[i] *= 2;
        }
    }
}