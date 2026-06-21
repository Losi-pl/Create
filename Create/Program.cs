using System.Reflection;
using Create.Graphics;
using Create.Registry;

Console.WriteLine("Create, World!");
foreach (var val in  Assembly.GetCallingAssembly().GetManifestResourceNames())
    Console.WriteLine(val);

Window.Main.ThreadBind();
Window.Main.Scene = new LoadingScene();
Window.Main.Run();