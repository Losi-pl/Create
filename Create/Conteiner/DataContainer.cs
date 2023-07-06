namespace Create.Conteiner;

public sealed class DataContainer
{
    Dictionary<string, object> data = new();

    /// <summary>
    /// Dotaje albo usuwa wartośći z pojemnika tupu strukt
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name">Nazwa wartości</param>
    /// <param name="value">Wartość</param>
    public void Set<T>(string name, T value) where T : unmanaged
    {
        if (data.ContainsKey(name))
            data[name] = value;
        else
            data.Add(name, value);
    }

    /// <summary>
    /// Dotaje albo usuwa wartośći z pojemnika tupu strukt
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name">Nazwa wartości</param>
    /// <param name="value">Wartość</param>
    public void Set<T>(string name, T? value) where T : unmanaged
    {
        if(value.HasValue)
        {
            if (data.ContainsKey(name))
                data[name] = value.Value;
            else
                data.Add(name, value.Value);
        }
        else
        {
            if(data.ContainsKey(name))
                data.Remove(name);
        }
    }

    /// <summary>
    /// Dotaje albo usuwa wartośći z pojemnika tupu strukt
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name">Nazwa wartości</param>
    /// <param name="value">Wartość</param>
    public void Set<T>(string name, T[]? value) where T : unmanaged
    {
        if (value == null)
        {
            if (data.ContainsKey(name))
                data.Remove(name);
        }
        else
        {
            if (data.ContainsKey(name))
                data[name] = clone_array(value);
            else
                data.Add(name, clone_array(value));
        }
    }

    /// <summary>
    /// Dotaje albo usuwa wartośći z pojemnika tupu strukt
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name">Nazwa wartości</param>
    /// <param name="value">Wartość</param>
    public void Set<T>(string name, T[,]? value) where T : unmanaged
    {
        if (value == null)
        {
            if (data.ContainsKey(name))
                data.Remove(name);
        }
        else
        {
            if (data.ContainsKey(name))
                data[name] = clone_array(value);
            else
                data.Add(name, clone_array(value));
        }
    }

    /// <summary>
    /// Dotaje albo usuwa wartośći z pojemnika tupu strukt
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name">Nazwa wartości</param>
    /// <param name="value">Wartość</param>
    public void Set<T>(string name, T[,,]? value) where T : unmanaged
    {
        if (value == null)
        {
            if (data.ContainsKey(name))
                data.Remove(name);
        }
        else
        {
            if (data.ContainsKey(name))
                data[name] = clone_array(value);
            else
                data.Add(name, clone_array(value));
        }
    }

    /// <summary>
    /// Dotaje albo usuwa wartośći z pojemnika tupu strukt
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name">Nazwa wartości</param>
    /// <param name="value">Wartość</param>
    public void Set(string name, string? value)
    {
        if (value != null)
        {
            if (data.ContainsKey(name))
                data[name] = value;
            else
                data.Add(name, value);
        }
        else
        {
            if (data.ContainsKey(name))
                data.Remove(name);
        }
    }

    /// <summary>
    /// Dotaje albo usuwa wartośći z pojemnika tupu strukt
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name">Nazwa wartości</param>
    /// <param name="value">Wartość</param>
    public void Set(string name, string[]? value)
    {
        if (value == null)
        {
            if (data.ContainsKey(name))
                data.Remove(name);
        }
        else
        {
            if (data.ContainsKey(name))
                data[name] = clone_array(value);
            else
                data.Add(name, clone_array(value));
        }
    }

    /// <summary>
    /// Dotaje albo usuwa wartośći z pojemnika tupu strukt
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name">Nazwa wartości</param>
    /// <param name="value">Wartość</param>
    public void Set(string name, string[,]? value)
    {
        if (value == null)
        {
            if (data.ContainsKey(name))
                data.Remove(name);
        }
        else
        {
            if (data.ContainsKey(name))
                data[name] = clone_array(value);
            else
                data.Add(name, clone_array(value));
        }
    }

    /// <summary>
    /// Dotaje albo usuwa wartośći z pojemnika tupu strukt
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name">Nazwa wartości</param>
    /// <param name="value">Wartość</param>
    public void Set(string name, string[,,]? value)
    {
        if (value == null)
        {
            if (data.ContainsKey(name))
                data.Remove(name);
        }
        else
        {
            if (data.ContainsKey(name))
                data[name] = clone_array(value);
            else
                data.Add(name, clone_array(value));
        }
    }

    /// <summary>
    /// Wyjmuje wartość z pojemnika
    /// </summary>
    /// <param name="name">Nazwa wartości</param>
    public object? Get(string name)
    {
        if (!data.ContainsKey(name))
            return null;
        var d = data[name];
        if (d is Array)
        {
            var a = (Array)d;
            switch (a.Rank)
            {
                case 1:
                    return clone_array((object[])a);
                case 2:
                    return clone_array((object[,])a);
                case 3:
                    return clone_array((object[,,])a);
            }
            return null;
        }
        return d;
    }

    /// <summary>
    /// Kopiuje tablice
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <returns>Kopia</returns>
    static T[] clone_array<T>(T[] array)
    {
        if (array == null)
            return null!;
        T[] coppy = new T[array.Length];
        for (int i = 0; i < array.Length; i++)
            coppy[i] = array[i];
        return coppy;
    }

    /// <summary>
    /// Kopiuje tablice
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <returns>Kopia</returns>
    static T[,] clone_array<T>(T[,] array)
    {
        if (array == null)
            return null!;
        var size = array.GetSize();
        T[,] coppy = new T[size.dim0, size.dim1];
        for (int d0 = 0; d0 < size.dim0; d0++)
            for (int d1 = 0; d1 < size.dim1; d1++)
                coppy[d0, d1] = array[d0, d1];
        return coppy;
    }

    /// <summary>
    /// Kopiuje tablice
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <returns>Kopia</returns>
    static T[,,] clone_array<T>(T[,,] array)
    {
        if (array == null)
            return null!;
        var size = array.GetSize();
        T[,,] coppy = new T[size.dim0, size.dim1, size.dim2];
        for (int d0 = 0; d0 < size.dim0; d0++)
            for (int d1 = 0; d1 < size.dim1; d1++)
                for (int d2 = 0; d2 < size.dim2; d2++)
                    coppy[d0, d1, d2] = array[d0, d1, d2];
        return coppy;
    }
}