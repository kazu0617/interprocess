using System.Runtime.InteropServices;
using Cloudtoid.Interprocess.Semaphore.Linux;
using Cloudtoid.Interprocess.Semaphore.MacOS;
using Cloudtoid.Interprocess.Semaphore.Windows;
using Cloudtoid.Interprocess.Semaphore.Wine;

namespace Cloudtoid.Interprocess;

/// <summary>
/// This class opens or creates platform agnostic named semaphore. Named
/// semaphores are synchronization constructs accessible across processes.
/// </summary>
internal static class InterprocessSemaphore
{
    internal static IInterprocessSemaphoreWaiter CreateWaiter(string name)
    {
        // Steam Frame's ARM64 Wine can execute the x64 managed/PE payload via
        // ARM64EC/FEX, but cannot load Resonite's x86-64 ELF shmbridge.dll.so.
        // Wine's Win32 named semaphore implementation is compatible here and
        // avoids crossing into that mismatched native ELF helper.
        if (Util.IsWine)
            return new SemaphoreWindows(name);

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return new SemaphoreWindows(name);

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            return new SemaphoreMacOS(name);

        return new SemaphoreLinux(name);
    }

    internal static IInterprocessSemaphoreReleaser CreateReleaser(string name)
    {
        if (Util.IsWine)
            return new SemaphoreWindows(name);

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return new SemaphoreWindows(name);

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            return new SemaphoreMacOS(name);

        return new SemaphoreLinux(name);
    }
}
