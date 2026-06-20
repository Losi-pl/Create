using Create.OpenGL;
using Create.OpenGL.Textures;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Create.Elements;

partial class Item
{
    public static ItemModel GenerateItemModel(string itemTexture)
    {
        ItemModel im = new();
        if(BazeItemModel.loadetTextures.TryGetValue(itemTexture, out var t))
            im.model = new BazeItemModel() { texture = t };
        else
        {
            var mod_name = itemTexture.Remove(itemTexture.IndexOf(':'));
            var content_name = itemTexture.Substring(itemTexture.IndexOf(":") + 1);
            var texture = Assets.GetTexture($"{mod_name}:items/{content_name}");
            im.model = new BazeItemModel() { texture = texture };
        }
        return im;
    }
}

file class BazeItemModel : IDrawable
{
    public static Dictionary<string, Texture2D> loadetTextures = new();
    
    static Shader shader = Assets.GetShader("create:basic/item");
    static Mesh mesh = Mesh.Create(shader).SetVertex("uv", new Vector2[] { new(0, 1), new(1, 1), new(0, 0), new(1, 0) })
        .SetTrangles(new[] { 0, 1, 2, 1, 2, 3 })
        .Finish();

    public Texture2D texture { init; private get; }

    public void Draw(Matrix4 projection, Matrix4 model)
    {
        shader.SetUniform("texture_", texture);
        mesh.Draw(projection, model);
    }
}