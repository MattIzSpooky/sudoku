using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public readonly struct Coordinate
    {
        public int X { get; }

        public int Y { get; }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}