namespace Game.GameLogic;

public static class Extensions
{
    public static T[,] ConvertTo2D<T>(this T[] a)
    {
        int size = Convert.ToInt32(Math.Sqrt(a.Length));
        int counter = 0;
        T[,] result = new T[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                result[i, j] = a[counter++];
            }
        }
        return result;
    }
    public static T[] ConvertTo1D<T>(this T[,] a) => a.Cast<T>().Select(c => c).ToArray();
}
