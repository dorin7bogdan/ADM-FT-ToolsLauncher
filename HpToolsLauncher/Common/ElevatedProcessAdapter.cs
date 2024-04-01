using HpToolsLauncher.Interfaces;

namespace HpToolsLauncher.Common
{
    public class ElevatedProcessAdapter(ElevatedProcess elevatedProcess) : IProcessAdapter
    {
        public int ExitCode
        {
            get
            {
                return elevatedProcess.ExitCode;
            }
        }

        public bool HasExited
        {
            get
            {
                return elevatedProcess.HasExited;
            }
        }

        public void Start()
        {
            elevatedProcess.StartElevated();
        }

        public void WaitForExit()
        {
            elevatedProcess.WaitForExit();
        }

        public bool WaitForExit(int milliseconds)
        {
            return elevatedProcess.WaitForExit(milliseconds);
        }

        public void Kill()
        {
            elevatedProcess.Kill();
        }

        public void Close()
        {
            elevatedProcess.Close();
        }
    }
}
