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
            if (entity == Entity.Empty)
            {
                return;
            }

            List<Cell> emptyEntities = Cells.Values
                .Where(x => x.Entity == Entity.Empty)
                .ToList();

            while (count-- > 0)
            {
                Cell cell;
                cell = emptyEntities[random.Next() % emptyEntities.Count];
                cell.Entity = entity;
            }
        }

        public override bool CanGo(int x, int y)
        {
            return Cells[(x, y)].Entity != Entity.Wall;
        }

        public override bool IsTarget(int x, int y)
        {
            return Cells[(x, y)].Entity == Entity.Target;
        }
    }
}
