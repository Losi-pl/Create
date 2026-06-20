using System.Reflection;

Console.WriteLine("Hello, World!");
foreach (var val in  Assembly.GetCallingAssembly().GetManifestResourceNames())
{
    Console.WriteLine(val);
}