using HpToolsLauncher.Interfaces;
using System.Diagnostics;

namespace HpToolsLauncher.Common
{
    public class ProcessAdapter(Process process) : IProcessAdapter
    {
        public int ExitCode
        {
            get
            {
                return process.ExitCode;
            }
        }

        public bool HasExited
        {
            get
            {
                return process.HasExited;
            }
        }

        public void Start()
        {
            process.ErrorDataReceived += Proc_ErrDataReceived;

            process.Start();

            if (process.StartInfo.RedirectStandardOutput)
            {
                process.BeginOutputReadLine();
            }

            if (process.StartInfo.RedirectStandardError)
            {
                process.BeginErrorReadLine();
            }
        }

        private void Proc_ErrDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!e.Data.IsNullOrWhiteSpace())
            {
                ConsoleWriter.ErrorSummaryLines.Add(e.Data);
            }
        }

        public void WaitForExit()
        {
            process.WaitForExit();
        }

        public bool WaitForExit(int milliseconds)
        {
            return process.WaitForExit(milliseconds);
        }

        public void Kill()
        {
            process.Kill();
        }

        public void Close()
        {
            process.Close();
        }
    }
}
