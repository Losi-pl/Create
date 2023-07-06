using Create.Conteiner;
using Create.OpenGL;
using OpenTK.Mathematics;

namespace Create.Render;

/// <summary>
/// Podstawa trzymająca cały model bytu złorzony z wielu elementów
/// </summary>
public class EntityModel : IDrawable
{
    IDrawable drawable;
    LivingEntity entity;

    public EntityModel(IDrawable drawable)
    {
        this.drawable = drawable;
        entity = null!;
    }

    public void Draw(Matrix4 projection, Matrix4 model)
    {
        if (entity.Dimention == null)
            return;
        {
            var e = this;
            e.OnModelDraw();
        }
        drawable.Draw(Matrix4.CreateTranslation(entity.PozitionByCenter) * projection, model);
    }

    /// <summary>
    /// Łączy model z konkretnym gytem
    /// </summary>
    /// <param name="entity"></param>
    internal void set_entity(LivingEntity entity) => this.entity = entity;
    
    /// <summary>
    /// Cały model
    /// </summary>
    public IDrawable Model => drawable;
    
    /// <summary>
    /// Instancja połączona z modelem
    /// </summary>
    public LivingEntity Entity => entity;
    
    /// <summary>
    /// Wywoływany przed renderowaniem modelu
    /// </summary>
    public virtual void OnModelDraw() { }
}
