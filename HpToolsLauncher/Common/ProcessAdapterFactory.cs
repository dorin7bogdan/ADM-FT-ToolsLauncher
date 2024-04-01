using HpToolsLauncher.Interfaces;
using System.Diagnostics;

namespace HpToolsLauncher.Common
{
    public static class ProcessAdapterFactory
    {
        /// <summary>
        /// Create a process adapter based on the type of process.
        /// </summary>
        /// <param name="process">the process object</param>
        /// <returns>an adapter for the given process, null if no adapter available</returns>
        public static IProcessAdapter CreateAdapter(object process)
        {
            if (process is Process p) return new ProcessAdapter(p);
            if (process is ElevatedProcess ep) return new ElevatedProcessAdapter(ep);

            return null;
        }
    }
}
