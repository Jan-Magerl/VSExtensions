namespace TortoiseVS.Tortoise
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.Win32;

    internal class TortoiseProc
    {
        private static readonly Lazy<TortoiseProc> InstanceValue = new Lazy<TortoiseProc>(() => new TortoiseProc());

        private string path;

        private TortoiseProc()
        {
            path = FindPath();
        }

        public static TortoiseProc Instance
        {
            get
            {
                return InstanceValue.Value;
            }
        }

        internal void Blame(string file, int line)
        {
            Start($"/command:blame /line:{line}", file);
        }

        internal Task Update(string path)
        {
            return StartAsync($"/command:update /closeonend:2", path);
        }

        private string FindPath()
        {
            const string keyPath = @"Software\TortoiseSVN";
            const string valueName = "ProcPath";

            RegistryKey key = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, Environment.MachineName, RegistryView.Registry64).OpenSubKey(keyPath);
            string procPath = key.GetValue(valueName).ToString();
            if (!string.IsNullOrEmpty(procPath) && File.Exists(procPath))
            {
                return procPath;
            }

            key = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, Environment.MachineName, RegistryView.Registry32).OpenSubKey(keyPath);
            procPath = key.GetValue(valueName).ToString();
            if (!string.IsNullOrEmpty(procPath) && File.Exists(procPath))
            {
                return procPath;
            }
            else
            {
                return string.Empty;
            }
        }

        private void Start(string command, string file)
        {
            command += string.Format(@" /path:""{0}""", file);
            Start(command);
        }

        private Task StartAsync(string command, string file)
        {
            command += string.Format(@" /path:""{0}""", file);
            return StartAsync(command);
        }

        private Process Start(string arguments)
        {
            if (arguments.Length > 32767)
            {
                throw new Exception("to many args");
            }

            Process process = new Process();
            process.EnableRaisingEvents = true;
            process.StartInfo.FileName = path;
            process.StartInfo.Arguments = arguments;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            return process;
        }

        private Task StartAsync(string arguments)
        {
            var tcs = new TaskCompletionSource<bool>();
            Process process = Start(arguments);
            if (!process.HasExited)
            {
                process.Exited += (sender, args) =>
                {
                    tcs.SetResult(true);
                    process.Dispose();
                };
            }
            else
            {
                tcs.SetResult(false);
                process.Dispose();
            }
            return tcs.Task;
        }
    }
}
