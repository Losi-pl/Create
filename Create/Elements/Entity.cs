using Create.Conteiner;
using Create.OpenGL;
using Create.Render;
using Create.Space;
using OpenTK.Mathematics;

namespace Create.Elements;

public abstract class Entity : Baze
{
    public sealed override Type ElementBazicType => typeof(Entity);

    internal protected virtual void OnSpawn(LivingEntity entity, object? args) { }
    public virtual void OnPhisicUpdate(LivingEntity entity, float delta) { }
    public virtual EntityModel GetModel(LivingEntity entity) => new(IDrawable.None);
}
