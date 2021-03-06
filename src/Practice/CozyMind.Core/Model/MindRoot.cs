﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyMind.Core.Model
{
    public class MindRoot
    {
        public MindNodeSurface Surface { get; set; }
        public MindNodeText Text { get; set; }
        public List<MindNode> Childs { get; set; }
    }
}
