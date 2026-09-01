namespace Create.General;

public static class StringOps
{
    extension(string path)
    {
        /// <summary>
        /// Returns a modified string with everything behind the last dot cut
        /// </summary>
        public string CutExtension()
        {
            var dotInd = path.LastIndexOf('.');
            if(dotInd == -1)
                return path;
            return path[..dotInd];
        }
    }
}