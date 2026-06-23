using System.Reflection;
using Create.Graphics;
using Create.Registry;

Console.WriteLine("Create, World!");

Window.Main.ThreadBind();
Window.Main.Scene = new LoadingScene();
Window.Main.Run();