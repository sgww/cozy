﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyLauncher.PluginBase;
using System.IO;

namespace CozyLauncher.Plugin.Program.ProgramSource
{
    public class PathSource : ISource
    {
        public List<Result> LoadProgram(Query query)
        {
            var res = new List<Result>();
            var EnvVar = Environment.GetEnvironmentVariable("Path").Split(';');
            foreach (var path in EnvVar)
            {
                try
                {
                    var ActPath = Path.Combine(path, query.RawQuery + ".exe");
                    if (File.Exists(ActPath))
                    {
                        var r = new Result()
                        {
                            Title = query.RawQuery,
                            SubTitle = ActPath,
                            IcoPath = "app",
                            Score = 100,
                        };
                        res.Add(r);
                    }
                }
                catch
                {
                    continue;
                }
            }
            return res;
        }
    }
}
