using Create.Conteiner;
using Create.OpenGL;
using OpenTK.Mathematics;

namespace Create.Render
{
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

        internal void set_entity(LivingEntity entity) => this.entity = entity;
        public IDrawable Model => drawable;
        public LivingEntity Entity => entity;
        public virtual void OnModelDraw() { }
    }
}
