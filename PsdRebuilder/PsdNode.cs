using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.PsdRebuilder
{
    public class PsdNode
    {
        public string Name;
        public string Type;
        public int X;
        public int Y;
        public int Width;
        public int Height;
        public PsdNode[] Children;
    }
}
