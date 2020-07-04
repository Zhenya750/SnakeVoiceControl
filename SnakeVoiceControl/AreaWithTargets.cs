using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeVoiceControl
{
    public class AreaWithTargets : Area
    {
        private Random random;

        public AreaWithTargets(int widthInCells, int heightInCells)
            : base(widthInCells, heightInCells)
        {
            random = new Random();
        }

        public override void GenerateEntity(Entity entity, int count = 1)
        {
            if (entity == Entity.EMPTY)
            {
                return;
            }

            while (count-- > 0)
            {
                Cell cell;
            
                do
                {
                    cell = Cells.Values.ToList()[random.Next() % Cells.Count];
                }
                while (cell.Entity != Entity.EMPTY);

                cell.Entity = entity;
            }
        }

        public override bool IsTarget(int x, int y)
        {
            return Cells[(x, y)].Entity == Entity.TARGET;
        }
    }
}
