using Create.Conteiner;
using Create.OpenGL;
using Create.Space;
using OpenTK.Mathematics;

namespace Create.Elements;

public abstract class Entity : Baze
{
    public sealed override Type ElementBazicType => typeof(Entity);

    static Shader tmp_shader { get; } = Shader.Create()
        .VertexCode(@"#version 440 core
            in vec3 poz;

            uniform mat4 matrix;

            void main()
            {
                gl_Position = matrix * vec4(poz, 1.0);
            }")
        .FragmentCode(@"#version 440 core
            uniform vec4 color;

            out vec4 color_o;

            void main()
            {
                color_o = color;
            }")
        .ProjectionMatrixUniform("matrix")
        .Finish(s => s.SetUniform("color", new Vector4(61 / 255f, 172 / 255f, 27 / 255f, 1)));

    internal protected virtual void OnSpawn(LivingEntity entity, object? args) { }
    public virtual void OnPhisicUpdate(LivingEntity entity, float delta) { }
    public virtual IDrawable GetModel(LivingEntity entity)
    {
        var mob_size = entity.Entity.GetMobSize(entity);
        var model = Mesh.Create(tmp_shader)
            .SetVertex("poz", new Vector3[]
            {
                new(0, 0, 0),
                new(1, 0, 0),
                new(1, 0, 1),
                new(0, 0, 1),
                new(0, 1, 0),
                new(1, 1, 0),
                new(1, 1, 1),
                new(0, 1, 1)
            }.ConvertAll(v => (v * new Vector3(mob_size.width, mob_size.height, mob_size.width)) - new Vector3(mob_size.width / 2, 0, mob_size.width / 2)))
            .SetTrangles(new[]
            {
                1,4,3, 1,3,2,
                5,8,7, 5,7,6,
                4,8,7, 4,7,3,
                //1,5,8, 1,8,4,
                //2,6,7, 2,3,7,
                //1,5,6, 1,6,2
            }.ConvertAll(t => --t))
            .Finish();

        return model;
    }

}
