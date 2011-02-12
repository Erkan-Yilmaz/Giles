﻿using System;
using System.Diagnostics;
using Giles.Core.Configuration;

namespace Giles.Core.Runners
{
    public interface IBuildRunner : IRunner
    {
    }

    public class BuildRunner : RunnerBase, IBuildRunner
    {
        readonly GilesConfig config;
        readonly Settings settings;

        public BuildRunner(GilesConfig config, Settings settings)
        {
            this.config = config;
            this.settings = settings;
        }

        public void Run()
        {
            var buildProcess = SetupProcess(settings.MsBuild, config.SolutionPath);

            var watch = new Stopwatch();

            Console.WriteLine("Building...");

            watch.Start();
            buildProcess.Start();
            var output = buildProcess.StandardOutput.ReadToEnd();

            buildProcess.WaitForExit();
            buildProcess.Close();
            buildProcess.Dispose();
            watch.Stop();
            
            Console.WriteLine("Building complete in {0} seconds.", watch.Elapsed.TotalSeconds);

            //Console.WriteLine("\n\n======= BUILD RESULTS =======");
            //Console.WriteLine(output);
        }
    }
}