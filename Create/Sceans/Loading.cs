using Create.Net;
using Create.OpenGL;
using OpenTK.Windowing.Common;
using System.Reflection;

namespace Create.Sceans;

internal sealed class Loading : Scean
{
    RenderLayer? render_layer;
    Task? loading_task;
    
    protected override void Load()
    {

        loading_task = Task.Run(() =>
        {
            (Assembly?, Resource.Resources) main_mod = (Assembly.GetExecutingAssembly(), Register.create_resource());
            Register.load_mods(new[] { main_mod });
        });
        OpenGL.Engine.Title = $"Create - {Engine.Version}";
        OpenGL.Engine.Size = new(960, 540);
        OpenGL.Engine.Visible = true;
        render_layer = RenderLayer.Create().Finisch();
        render_layer.Color = System.Drawing.Color.FromArgb(255, 239, 39, 39);
    }
    protected override void RenderFrame(FrameEventArgs args)
    {
        render_layer!.UpdateContent();
        render_layer.Draw();
        OpenGL.Engine.FinishFrame();
    }
    protected override void UpdateFrame(FrameEventArgs args)
    {
        if (loading_task!.IsCompleted)
        {
            if (loading_task.IsFaulted)
                throw loading_task.Exception!;
            Client.load_save();
        }
    }
    protected override void Resize(ResizeEventArgs args)
    {
        if(render_layer?.Size != args.Size.ToTumple())
            render_layer?.Resize(args.Size);
    }
    protected override void SceanUnload()
    {
        render_layer?.Dispose();
    }
}
