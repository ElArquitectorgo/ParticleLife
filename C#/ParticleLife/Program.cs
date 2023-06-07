using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ParticleLife
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Stopwatch stopwatch = new();
            ApplicationConfiguration.Initialize();
            Application.Idle += new((s, ev) =>
            {
                while (AppStillIdle)
                {
                    try
                    {
                        long deltaTime = stopwatch.ElapsedMilliseconds;
                        stopwatch.Restart();
                        foreach (IFormLoop form in Application.OpenForms.OfType<IFormLoop>())
                        {
                            // Render a frame during idle time (no messages are waiting)
                            form.UpdateEnvironment(deltaTime);
                            form.RenderEnvironment(deltaTime);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            });
            Application.Run(new Simulation());
        }

        [System.Security.SuppressUnmanagedCodeSecurity] // We won't use this maliciously
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern bool PeekMessage(out Message msg, IntPtr hWnd, uint messageFilterMin, uint messageFilterMax, uint flags);
        private static bool AppStillIdle => !PeekMessage(out _, IntPtr.Zero, 0, 0, 0);

        internal interface IFormLoop
        {
            public void UpdateEnvironment(long deltaTime);
            public void RenderEnvironment(long deltaTime);
        }
    }
}