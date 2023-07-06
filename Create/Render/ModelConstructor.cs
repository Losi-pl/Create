using Create.Virtuals;

namespace Create.Render;

/// <summary>
/// Mechanizm grupujące podrzędne mechanizm budowania modelu terenu
/// </summary>
public abstract class ModelConstructor
{
    Dictionary<Type, ChunkModel> models;

    VirtualDictionaty<Type, ChunkModel> model_mekanizm;

    /// <summary>
    /// Oddzielne mechanizmy do generowania modelu terenu
    /// </summary>
    public VirtualDictionaty<Type, ChunkModel> ModelMekanizm => model_mekanizm;

    /// <summary>
    /// Biblioteka z wrzystkimi urzytymi mechanizmami generowania terenu
    /// </summary>
    protected Dictionary<Type, ChunkModel> Models => models;

    public ModelConstructor()
    {
        models = new();
        model_mekanizm = create_dictionary(this);
    }

    /// <summary>
    /// Tworzy wirtualną biblioteke zwracajoncą odzielne mechanizmy do generowania modelu terenu a jeżeli te nie zostały jeszcze stworzone to je generuje
    /// </summary>
    /// <param name="mc">faktyczna bibliotega zawierająca mechanizmy</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    static VirtualDictionaty<Type, ChunkModel> create_dictionary(ModelConstructor mc) => VirtualDictionaty.Create(mc.models)
        .GetMethod(s =>
        {
            if (!s.IsSubclassOf(typeof(ChunkModel)))
                throw new ArgumentException($"{s} must be subclass of {typeof(ChunkModel)}");
            if(mc.models.TryGetValue(s, out var model))
                return model;
            var nc = (ChunkModel)Activator.CreateInstance(s)!;
            mc.models.Add(s, nc);
            return nc;
        })
        .Finsh();

}
