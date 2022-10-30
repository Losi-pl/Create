using Create.Virtuals;

namespace Create.Render;

public abstract class ModelConstructor
{
    Dictionary<Type, ChunkModel> models;

    VirtualDictionaty<Type, ChunkModel> model_mekanizm;
    public VirtualDictionaty<Type, ChunkModel> ModelMekanizm => model_mekanizm;
    protected Dictionary<Type, ChunkModel> Models => models;

    public ModelConstructor()
    {
        models = new();
        model_mekanizm = create_dictionary(this);
    }

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
