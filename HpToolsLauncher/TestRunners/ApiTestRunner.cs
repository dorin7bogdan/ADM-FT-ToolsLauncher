/*
 * Certain versions of software accessible here may contain branding from Hewlett-Packard Company (now HP Inc.) and Hewlett Packard Enterprise Company.
 * This software was acquired by Micro Focus on September 1, 2017, and is now offered by OpenText.
 * Any reference to the HP and Hewlett Packard Enterprise/HPE marks is historical in nature, and the HP and Hewlett Packard Enterprise/HPE marks are the property of their respective owners.
 * __________________________________________________________________
 * MIT License
 *
 * Copyright 2012-2024 Open Text
 *
 * The only warranties for products and services of Open Text and
 * its affiliates and licensors ("Open Text") are as may be set forth
 * in the express warranty statements accompanying such products and services.
 * Nothing herein should be construed as constituting an additional warranty.
 * Open Text shall not be liable for technical or editorial errors or
 * omissions contained herein. The information contained herein is subject
 * to change without notice.
 *
 * Except as specifically indicated otherwise, this document contains
 * confidential information and a valid license is required for possession,
 * use or copying. If this work is provided to the U.S. Government,
 * consistent with FAR 12.211 and 12.212, Commercial Computer Software,
 * Computer Software Documentation, and Technical Data for Commercial Items are
 * licensed to the U.S. Government under vendor's standard commercial license.
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * ___________________________________________________________________
 */
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using HpToolsLauncher.Common;
using HpToolsLauncher.Interfaces;
using HpToolsLauncher.Properties;

namespace HpToolsLauncher
{
    public class ApiTestRunner : IFileSysTestRunner
    {
        public const string STRunnerName = "ServiceTestExecuter.exe";
        public const string STRunnerTestArg = @"-test";
        public const string STRunnerReportArg = @"-report";
        public const string STRunnerInputParamsArg = @"-inParams";
        private const int PollingTimeMs = 500;
        private bool _stCanRun;
        private string _stExecuterPath = Directory.GetCurrentDirectory();
        private readonly IAssetRunner _runner;
        private Stopwatch _stopwatch = null;
        private RunCancelledDelegate _runCancelled;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="runner">parent runner</param>
        public ApiTestRunner(IAssetRunner runner)
        {
            _stopwatch = Stopwatch.StartNew();
            _stCanRun = TrySetSTRunner();
            _runner = runner;
        }

        /// <summary>
        /// Search ServiceTestExecuter.exe in the current running process directory,
        /// and if not found, in the installation folder (taken from registry)
        /// </summary>
        /// <returns></returns>
        public bool TrySetSTRunner()
        {
            if (File.Exists(STRunnerName))
                return true;
            _stExecuterPath = Helper.GetSTInstallPath();
            if ((!string.IsNullOrEmpty(_stExecuterPath)))
            {
                _stExecuterPath += Helper.BIN;
                return true;
            }
            _stCanRun = false;
            return false;
        }


        /// <summary>
        /// runs the given test
        /// </summary>
        /// <param name="testinf"></param>
        /// <param name="errorReason"></param>
        /// <param name="runCancelled">cancellation delegate, holds the function that checks cancellation</param>
        /// <returns></returns>
        public TestRunResults RunTest(TestInfo testinf, ref string errorReason, RunCancelledDelegate runCancelled)
        {

            TestRunResults runDesc = new TestRunResults() { StartDateTime = DateTime.Now };
            ConsoleWriter.ActiveTestRun = runDesc;
            ConsoleWriter.WriteLine(DateTime.Now.ToString(Launcher.DateFormat) + " Running: " + testinf.TestPath);

            runDesc.TestPath = testinf.TestPath;

            // check if the report path has been defined
            if (!string.IsNullOrWhiteSpace(testinf.ReportPath))
            {
                runDesc.ReportLocation = testinf.ReportPath;
                ConsoleWriter.WriteLine(DateTime.Now.ToString(Launcher.DateFormat) + " Report path is set explicitly: " + runDesc.ReportLocation);
            }
            else if (!string.IsNullOrEmpty(testinf.ReportBaseDirectory))
            {
                if(!Helper.TrySetTestReportPath(runDesc, testinf,ref errorReason))
                {
                    return runDesc;
                }
                ConsoleWriter.WriteLine(DateTime.Now.ToString(Launcher.DateFormat) + " Report path is generated under base directory: " + runDesc.ReportLocation);
            }
            else
            {
                // default report location is the next available folder under test path
                // for example, "path\to\tests\APITest\Report123", the name "Report123" will also be used as the report name
                string reportBasePath = testinf.TestPath;
                string testReportPath = Path.Combine(reportBasePath, "Report" + DateTime.Now.ToString("ddMMyyyyHHmmssfff"));
                int index = 0;
                while (index < int.MaxValue)
                {
                    index++;
                    string dir = Path.Combine(reportBasePath, "Report" + index.ToString());
                    if (!Directory.Exists(dir))
                    {
                        testReportPath = dir;
                        break;
                    }
                }
                runDesc.ReportLocation = testReportPath;
                ConsoleWriter.WriteLine(DateTime.Now.ToString(Launcher.DateFormat) + " Report path is automatically generated: " + runDesc.ReportLocation);
            }

            runDesc.ErrorDesc = errorReason;
            runDesc.TestState = TestState.Unknown;
            if (!Helper.IsServiceTestInstalled())
            {
                runDesc.TestState = TestState.Error;
                runDesc.ErrorDesc = string.Format(Resources.LauncherStNotInstalled, System.Environment.MachineName);
                ConsoleWriter.WriteErrLine(runDesc.ErrorDesc);
                Environment.ExitCode = (int)Launcher.ExitCodeEnum.Failed;
                return runDesc;
            }

            _runCancelled = runCancelled;
            if (!_stCanRun)
            {
                runDesc.TestState = TestState.Error;
                runDesc.ErrorDesc = Resources.STExecuterNotFound;
                return runDesc;
            }
            string fileName = Path.Combine(_stExecuterPath, STRunnerName);

            if (!File.Exists(fileName))
            {
                runDesc.TestState = TestState.Error;
                runDesc.ErrorDesc = Resources.STExecuterNotFound;
                ConsoleWriter.WriteErrLine(Resources.STExecuterNotFound);
                return runDesc;
            }

            //write the input parameter xml file for the API test
            string paramFileName = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10);
            string tempPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestParams");
            Directory.CreateDirectory(tempPath);
            string paramsFilePath = Path.Combine(tempPath, "params" + paramFileName + ".xml");
            string paramFileContent = testinf.GenerateAPITestXmlForTest();

            string argumentString = "";
            if (!string.IsNullOrWhiteSpace(paramFileContent))
            {
                File.WriteAllText(paramsFilePath, paramFileContent);
                argumentString = string.Format("{0} \"{1}\" {2} \"{3}\" {4} \"{5}\"", STRunnerTestArg, testinf.TestPath, STRunnerReportArg, runDesc.ReportLocation, STRunnerInputParamsArg, paramsFilePath);
            }
            else
            {
                argumentString = string.Format("{0} \"{1}\" {2} \"{3}\"", STRunnerTestArg, testinf.TestPath, STRunnerReportArg, runDesc.ReportLocation);
            }

            Stopwatch s = Stopwatch.StartNew();
            runDesc.TestState = TestState.Running;

            if (!ExecuteProcess(fileName,
                                argumentString,
                                ref errorReason))
            {
                runDesc.TestState = TestState.Error;
                runDesc.ErrorDesc = errorReason;
            }
            else
            {
                // consider backward compatibility, here move the report folder one outside
                // that is, after test run, the report file might be at "path\to\tests\APITest1\Report123\Report\run_results.html"
                // here move the last directory "Report" one level outside, which is, "path\to\tests\APITest1\Report123\run_results.html"
                string apiTestReportPath = Path.Combine(runDesc.ReportLocation, "Report");  // apiTestReportPath: path\to\tests\APITest1\Report123\Report
                string targetReportDir = Path.GetDirectoryName(apiTestReportPath);          // reportDir: path\to\tests\APITest1\Report123
                string reportBaseDir = Path.GetDirectoryName(targetReportDir);              // reportBaseDir: path\to\tests\APITest1
                string tmpDir = Path.Combine(reportBaseDir, "tmp_" + DateTime.Now.ToString("ddMMyyyyHHmmssfff")); // tmpDir: path\to\tests\APITest1\tmp_ddMMyyyyHHmmssfff
                string tmpReportDir = Path.Combine(tmpDir, "Report");                       // tmpReportDir: path\to\tests\APITest1\tmp_ddMMyyyyHHmmssfff\Report

                // since some files might not be closed yet, move the report folder might fail
                // so here will try a few times to move folder and let it as is (not moved) if still failed after several retry
                bool moveSuccess = false;
                string lastMoveError = string.Empty;
                int retry = 10;
                while (retry >= 0)
                {
                    try
                    {
                        // steps:
                        //   1. move directory "path\to\tests\APITest1\Report123" to "path\to\tests\APITest1\tmp_ddMMyyyyHHmmssfff"
                        Directory.Move(targetReportDir, tmpDir);
                        //   2. move directory "path\to\tests\APITest1\tmp_ddMMyyyyHHmmssfff\Report" to "path\to\tests\APITest1\Report123"
                        Directory.Move(tmpReportDir, targetReportDir);
                        //   3. delete empty directory "path\to\test1\tmp_ddMMyyyyHHmmssfff"
                        Directory.Delete(tmpDir, true);
                        //   4. update report location
                        runDesc.ReportLocation = targetReportDir;
                        moveSuccess = true;
                        break;
                    }
                    catch (Exception ex)
                    {
                        lastMoveError = ex.Message;
                        retry--;
                        System.Threading.Thread.Sleep(500);
                    }
                }
                if (!moveSuccess)
                {
                    ConsoleWriter.WriteLine("Warning: Failed to change the report folder structure. " + lastMoveError);
                }

                if (!File.Exists(Path.Combine(runDesc.ReportLocation, "Results.xml")) && !File.Exists(Path.Combine(runDesc.ReportLocation, "run_results.html")))
                {
                    runDesc.TestState = TestState.Error;
                    runDesc.ErrorDesc = "No Results.xml or run_results.html file found";
                }
            }
			//File.Delete(paramsFilePath);
            runDesc.Runtime = s.Elapsed;
            return runDesc;
        }

        /// <summary>
        /// performs global cleanup code for this type of runner
        /// </summary>
        public void CleanUp()
        {
        }

        #region Process

        /// <summary>
        /// executes the run of the test by using the Init and RunProcss routines
        /// </summary>
        /// <param name="proc"></param>
        /// <param name="fileName"></param>
        /// <param name="arguments"></param>
        /// <param name="enableRedirection"></param>
        private bool ExecuteProcess(string fileName, string arguments, ref string failureReason)
        {
            Process proc = null;
            try
            {
                using (proc = new Process())
                {
                    InitProcess(proc, fileName, arguments, true);
                    RunProcess(proc, true);

                    //it could be that the process already existed
                    //before we could handle the cancel request
                    if (_runCancelled())
                    {
                        failureReason = "Process was stopped since job has timed out!";
                        ConsoleWriter.WriteLine(failureReason);

                        if (!proc.HasExited)
                        {

                            proc.OutputDataReceived -= OnOutputDataReceived;
                            proc.ErrorDataReceived -= OnErrorDataReceived;
                            proc.Kill();
                            return false;
                        }
                    }
                    if (proc.ExitCode != 0)
                    {
                        failureReason = "The Api test runner's exit code was: " + proc.ExitCode;
                        ConsoleWriter.WriteLine(failureReason);
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                failureReason = e.Message;
                return false;
            }
            finally
            {
                if (proc != null)
                {
                    proc.Close();
                }
            }

            return true;
        }

        /// <summary>
        /// initializes the ServiceTestExecuter process
        /// </summary>
        /// <param name="proc"></param>
        /// <param name="fileName"></param>
        /// <param name="arguments"></param>
        /// <param name="enableRedirection"></param>
        private void InitProcess(Process proc, string fileName, string arguments, bool enableRedirection)
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                WorkingDirectory = Directory.GetCurrentDirectory()
            };

            if (!enableRedirection) return;

            processStartInfo.ErrorDialog = false;
            processStartInfo.UseShellExecute = false;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardError = true;

            proc.StartInfo = processStartInfo;

            proc.EnableRaisingEvents = true;
            proc.StartInfo.CreateNoWindow = true;

            proc.OutputDataReceived += OnOutputDataReceived;
            proc.ErrorDataReceived += OnErrorDataReceived;
        }

        /// <summary>
        /// runs the ServiceTestExecuter process after initialization
        /// </summary>
        /// <param name="proc"></param>
        /// <param name="enableRedirection"></param>
        private void RunProcess(Process proc, bool enableRedirection)
        {
            proc.Start();
            if (enableRedirection)
            {
                proc.BeginOutputReadLine();
                proc.BeginErrorReadLine();
            }
            proc.WaitForExit(PollingTimeMs);
            while (!_runCancelled() && !proc.HasExited)
            {
                proc.WaitForExit(PollingTimeMs);
            }
        }

        /// <summary>
        /// callback function for spawnd process errors
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            var p = sender as Process;

            if (p == null) return;
            try
            {
                if (!p.HasExited || p.ExitCode == 0) return;
            }
            catch { return; }

            string errorData = e.Data;

            if (string.IsNullOrEmpty(errorData))
            {
                errorData = string.Format("External process has exited with code {0}", p.ExitCode);

            }

            ConsoleWriter.WriteErrLine(errorData);
        }

        /// <summary>
        /// callback function for spawnd process output
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                string data = e.Data;
                ConsoleWriter.WriteLine(data);
            }
        }

        #endregion

    }
}