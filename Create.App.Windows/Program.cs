using Create;

#if DEBUG
Engine.CreateActivator().Finish();
#else
try
{
    Console.WriteLine("Create running...");
    Engine.CreateActivator().Finish();
}
catch (Exception ex)
{
    write_ex(ex);
    Console.ReadKey();
}

void write_ex(Exception ex, int sp = 0)
{
    Console.WriteLine(new string(' ', sp) + ex.Message);
    if(ex.InnerException != null)
        write_ex(ex.InnerException, sp + 2);
}
#endif