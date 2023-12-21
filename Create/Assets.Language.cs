using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Diagnostics;

namespace Create;

partial class Assets
{
    public static class Language
    {
        static Dictionary<string, string> words = new();

        internal static void AddWord(string key, string content)
        {
            if(words.TryAdd(key, content))
                return;
            words[key] = content;
        }

        public static bool TryGetFromKey(string key, out string text)
        {
            key = key.Count(c => char.IsLetter(c) && char.IsUpper(c)) > 0 ? key.ToLower() : key;
            return words.TryGetValue(key, out text!);
        }
        public static string GetFromKey(string key) => TryGetFromKey(key, out var text) ? text : key;
    }
}
