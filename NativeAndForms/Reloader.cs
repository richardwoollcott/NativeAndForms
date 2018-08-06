namespace NativeAndForms
{
    public class Reloader
    {
        public void Initialize()
        {
            // Initialize Live Reload.
#if DEBUG
            LiveReload.Init();
#endif

        }
    }
}
