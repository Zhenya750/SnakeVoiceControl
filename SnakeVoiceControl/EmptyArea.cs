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

        public override bool CanGo(int x, int y)
        {
            return true;
        }

        public override void GenerateEntity(Entity entity, int count = 1)
        {
            return;
        }

        public override bool IsTarget(int x, int y)
        {
            return false;
        }
    }
}
