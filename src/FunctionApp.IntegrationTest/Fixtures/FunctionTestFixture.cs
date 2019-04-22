using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using FunctionApp.IntegrationTest.Settings;

namespace FunctionApp.IntegrationTest.Fixtures
{
    public class FunctionTestFixture : IDisposable
    {
        private readonly Process _funcHostProcess;

        public int Port { get; } = 7071;

        public readonly HttpClient Client = new HttpClient();

        public FunctionTestFixture()
        {
            var functionAppFolder = Path.GetRelativePath(Directory.GetCurrentDirectory(), ConfigurationHelper.Settings.FunctionApplicationPath);

            _funcHostProcess = new Process
            {
                StartInfo =
                {
                    FileName = "func.exe",
                    Arguments = $"host start -p {Port}",
                    WorkingDirectory = functionAppFolder
                }
            };
            var success = _funcHostProcess.Start();
            if (!success)
            {
                throw new InvalidOperationException("Could not start Azure Functions host.");
            }

            Client.BaseAddress = new Uri($"http://localhost:{Port}");
        }

        public virtual void Dispose()
        {
            if (!_funcHostProcess.HasExited)
            {
                _funcHostProcess.Kill();
            }

            _funcHostProcess.Dispose();
        }
    }
}
