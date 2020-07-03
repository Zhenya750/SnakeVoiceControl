using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeVoiceControl
{
    public class Cell : IEquatable<Cell>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Entity Entity { get; set; }

        public Cell(int x, int y, Entity entity)
        {
            X = x;
            Y = y;
            Entity = entity;
        }

        public bool Equals(Cell other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return (X + Y).GetHashCode();
        }
    }
}
