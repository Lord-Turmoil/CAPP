using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Launcher;

static class Program
{
    private const int SW_HIDE = 0;

    // Minimize the console window
    [DllImport("kernel32.dll")]
    private static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    private static void Main(string[] args)
    {
        IntPtr handle = GetConsoleWindow();
        ShowWindow(handle, SW_HIDE);

        try
        {
            Directory.SetCurrentDirectory(@".\Server");
            Process.Start(@".\Server.exe");
            Directory.SetCurrentDirectory(@"..\");
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to launch CAPP server! Caused by:");
            Console.WriteLine(e);
            return;
        }

        Thread.Sleep(500);

        try
        {
            Directory.SetCurrentDirectory(@".\Client");
            Process.Start(@".\Client.exe");
            Directory.SetCurrentDirectory(@"..\");
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to launch CAPP client! Caused by:");
            Console.WriteLine(e);
        }
    }
}