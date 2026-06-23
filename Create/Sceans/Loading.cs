using Create.Linq;
using Create.Net;
using Create.OpenGL;
using OpenTK.Windowing.Common;
using System.Reflection;

namespace Create.Sceans;

internal sealed class Loading : Scean
{
    RenderLayer? _renderLayer;
    Task? loading_task;
    
    protected override void Load()
    {

        loading_task = Task.Run(() =>
        {
            (Assembly?, Resource.Resources) main_mod = (Assembly.GetExecutingAssembly(), Register.GetPrimaryResources());
            Register.load_mods(new[] { main_mod });
        });
        OpenGL.Engine.Title = $"Create - {Engine.Version}";
        OpenGL.Engine.Size = new(1443, 866);
        OpenGL.Engine.Visible = true;
        _renderLayer = RenderLayer.Create().Finisch();
        _renderLayer.Color = System.Drawing.Color.FromArgb(255, 239, 39, 39);
    }
    protected override void RenderFrame(FrameEventArgs args)
    {
        _renderLayer!.UpdateContent();
        _renderLayer.Draw();
        OpenGL.Engine.FinishFrame();
    }
    protected override void UpdateFrame(FrameEventArgs args)
    {
        if (loading_task!.IsCompleted)
        {
            if (loading_task.IsFaulted)
                throw new("Loading error", loading_task.Exception.InnerException!);
            Client.load_save();
        }
    }
    protected override void Resize(ResizeEventArgs args)
    {
        if(_renderLayer?.Size != args.Size.ToTumple())
            _renderLayer?.Resize(args.Size);
    }
    protected override void SceanUnload()
    {
        _renderLayer?.Dispose();
    }
}
