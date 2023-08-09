namespace Create.Linq;

public static class Arrays
{
    /// <summary>
    /// Konwertuje wartości w tablicy za pomocą <paramref name="func"/>
    /// </summary>
    public static TOut[] Convert<TIn, TOut>(this TIn[] array, Func<TIn, TOut> func)
    {
        var _array = new TOut[array.LongLength];
        for (long l = 0; l < array.LongLength; l++)
            _array[l] = func(array[l]);
        return _array;
    }

    /// <summary>
    /// Pobiera rozmiar tabeli w 2 osich
    /// </summary>
    public static (int dim0, int dim1) GetSize<T>(this T[,] array) => (array.GetLength(0), array.GetLength(1));

    /// <summary>
    /// Pobiera rozmiar tabeli w 3 osich
    /// </summary>
    public static (int dim0, int dim1, int dim2) GetSize<T>(this T[,,] array) => (array.GetLength(0), array.GetLength(1), array.GetLength(2));

    /// <summary>
    /// Pobiera rozmiar tabeli w 4 osich
    /// </summary>
    public static (int dim0, int dim1, int dim2, int dim3) GetSize<T>(this T[,,,] array) => (array.GetLength(0), array.GetLength(1), array.GetLength(2), array.GetLength(3));

    /// <summary>
    /// Pobiera rozmiar tabeli w 5 osich
    /// </summary>
    public static (int dim0, int dim1, int dim2, int dim3, int dim4) GetSize<T>(this T[,,,,] array) =>
        (array.GetLength(0), array.GetLength(1), array.GetLength(2), array.GetLength(3), array.GetLength(4));

    /// <summary>
    /// Pobiera rozmiar tabeli w 6 osich
    /// </summary>
    public static (int dim0, int dim1, int dim2, int dim3, int dim4, int dim5) GetSize<T>(this T[,,,,,] array) =>
        (array.GetLength(0), array.GetLength(1), array.GetLength(2), array.GetLength(3), array.GetLength(4), array.GetLength(5));

    /// <summary>
    /// Pobiera rozmiar tabeli w 7 osich
    /// </summary>
    public static (int dim0, int dim1, int dim2, int dim3, int dim4, int dim5, int dim6) GetSize<T>(this T[,,,,,,] array) =>
        (array.GetLength(0), array.GetLength(1), array.GetLength(2), array.GetLength(3), array.GetLength(4), array.GetLength(5), array.GetLength(6));

    /// <summary>
    /// Pobiera rozmiar tabeli w 8 osich
    /// </summary>
    public static (int dim0, int dim1, int dim2, int dim3, int dim4, int dim5, int dim6, int dim7) GetSize<T>(this T[,,,,,,,] array) =>
        (array.GetLength(0), array.GetLength(1), array.GetLength(2), array.GetLength(3), array.GetLength(4), array.GetLength(5), array.GetLength(6), array.GetLength(7));

    /// <summary>
    /// Pobiera rozmiar tabeli w 9 osich
    /// </summary>
    public static (int dim0, int dim1, int dim2, int dim3, int dim4, int dim5, int dim6, int dim7, int dim8) GetSize<T>(this T[,,,,,,,,] array) =>
        (array.GetLength(0), array.GetLength(1), array.GetLength(2), array.GetLength(3), array.GetLength(4), array.GetLength(5), array.GetLength(6), array.GetLength(7), array.GetLength(8));

    /// <summary>
    /// Pobiera rozmiar tabeli w 10 osich
    /// </summary>
    public static (int dim0, int dim1, int dim2, int dim3, int dim4, int dim5, int dim6, int dim7, int dim8, int dim9) GetSize<T>(this T[,,,,,,,,,] array) =>
        (array.GetLength(0), array.GetLength(1), array.GetLength(2), array.GetLength(3), array.GetLength(4), array.GetLength(5), array.GetLength(6), array.GetLength(7), array.GetLength(8), array.GetLength(9));
}
