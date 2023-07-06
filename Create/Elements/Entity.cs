using Create.Conteiner;
using Create.OpenGL;
using Create.Render;

namespace Create.Elements;

/// <summary>
/// Baza budowy bytów
/// </summary>
public abstract class Entity : Baze
{
    // Ustawienie typu elementu na Entity
    public sealed override Type ElementBazicType => typeof(Entity);

    /// <summary>
    /// Wywoływana gdy byt został stworzony
    /// </summary>
    /// <param name="entity">Stworzony byt</param>
    /// <param name="args">Opcjonalne parametry</param>
    internal protected virtual void OnSpawn(LivingEntity entity, object? args) { }
    
    /// <summary>
    /// Obliczanie fizyki bytu
    /// </summary>
    /// <param name="entity">Byt do obliczeń fizyki</param>
    /// <param name="delta">Od ostatniego obliczenia</param>
    public virtual void OnPhisicUpdate(LivingEntity entity, float delta) { }
    
    /// <summary>
    /// Generuje model bytu
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual EntityModel GetModel(LivingEntity entity) => new(IDrawable.None);
}
