﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.MapEditor.Event
{
    public class TiledDataMessageArgs : EventArgs
    {
        public int X { get; set; }
        public int Y { get; set; }
        public uint Data { get; set; }

        public TiledDataMessageArgs(int x, int y, uint data)
        {
            X = x;
            Y = y;
            Data = data;
        }
    }
}
