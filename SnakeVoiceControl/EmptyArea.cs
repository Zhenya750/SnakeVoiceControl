using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeVoiceControl
{
    public class EmptyArea : Area
    {
        public override Dictionary<(int, int), Cell> Cells { get; protected set; }

        public EmptyArea(int widthInCells, int heightInCells)
            : base(widthInCells, heightInCells)
        {
        }
    }
}
