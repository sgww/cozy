﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyLauncher.PluginBase;
using System.Diagnostics;

namespace CozyLauncher.Plugin.Core
{
    public class Main : IPlugin
    {
        private PluginInitContext _context { get; set; }

        public PluginInfo Init(PluginInitContext context)
        {
            _context = context;
            var info = new PluginInfo()
            {
                Keyword = "*",
            };
            return info;
        }

        public List<Result> Query(Query query)
        {
            var rl = new List<Result>();

            if (query.RawQuery == "exit")
            {
                var r = new Result()
                {
                    Title       = "Exit",
                    SubTitle    = "关闭",
                    IcoPath     = "exit",
                    Score       = 100,
                    Action      = e =>
                    {
                        _context.Api.CloseApp();
                        return true;
                    }
                };

                rl.Add(r);
            }
            else if(query.RawQuery == "config" || query.RawQuery == "setting")
            {
                var r = new Result()
                {
                    Title       = "Config / Setting",
                    SubTitle    = "设置",
                    IcoPath     = "setting",
                    Score       = 100,
                    Action = e  =>
                    {
                        _context.Api.ShowPanel("config");
                        return true;
                    }
                };

                rl.Add(r);
            }
            else if (query.RawQuery == "about")
            {
                var r = new Result()
                {
                    Title       = "About",
                    SubTitle    = "关于",
                    IcoPath     = "help",
                    Score       = 100,
                    Action      = e =>
                    {
                        _context.Api.ShowPanel("about");
                        return true;
                    }
                };

                rl.Add(r);
            }
            else if(query.RawQuery == "cozy")
            {
                var r = new Result()
                {
                    Title = "Cozy",
                    SubTitle = "主页",
                    IcoPath = "help",
                    Score = 100,
                    Action = e =>
                    {
                        Process.Start(@"http://cozy.laorouji.com");
                        return true;
                    }
                };

                rl.Add(r);
            }
            else if(query.RawQuery == "update")
            {
                var r = new Result()
                {
                    Title = "Update",
                    SubTitle = "更新",
                    IcoPath = "",
                    Score = 100,
                    Action = e =>
                    {
                        _context.Api.Update();
                        return true;
                    }
                };

                rl.Add(r);
            }

            return rl;
        }
    }
}
