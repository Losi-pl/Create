namespace Create.Net
{
    public readonly struct Account
    {
        readonly Guid guid;

        public Account()
        {
            guid = Guid.NewGuid();
        }
    }
}
