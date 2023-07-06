namespace Create.Net
{
    /// <summary>
    /// Identyfikator klienta
    /// </summary>
    public readonly struct Account
    {
        readonly Guid guid;

        public Account()
        {
            guid = Guid.NewGuid();
        }
    }
}
