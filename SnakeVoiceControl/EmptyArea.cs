using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeVoiceControl
{
    public class EmptyArea : Area
    {
        public EmptyArea(int widthInCells, int heightInCells)
            : base(widthInCells, heightInCells)
        {
        }

        public override void GenerateEntity(Entity entity, int count = 1)
        {
            return;
        }
    }
}
