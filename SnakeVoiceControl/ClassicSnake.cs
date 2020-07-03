using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeVoiceControl
{
    public class ClassicSnake : Snake
    {
        public override ICollection<Cell> Body { get; protected set; }

        public ClassicSnake(Area area)
            : base(area)
        {
            Body = new LinkedList<Cell>();
            var head = new Cell(_area.WidthInCells / 2, _area.HeightInCells / 2, Entity.SNAKEPART);
            Body.Add(head);
            BringToArea(new[] { head });
        }

        private void BringToArea(ICollection<Cell> cells)
        {
            foreach (var cell in cells)
            {
                _area.Cells[(cell.X, cell.Y)].Entity = cell.Entity;
            }
        }

        protected override void GoTo(int x, int y)
        {
            if (IsDead)
            {
                return;
            }

            if (x < 0) x = _area.WidthInCells - 1;
            if (x >= _area.WidthInCells) x = 0;
            if (y < 0) y = _area.HeightInCells - 1;
            if (y >= _area.HeightInCells) y = 0;

            var head = Body.First();
            var newHead = new Cell(x, y, Entity.SNAKEPART);

            if (_area.CanGo(x, y))
            {
                if (Body.Contains(newHead))
                {
                    IsDead = true;
                    return;
                }

                (Body as LinkedList<Cell>).AddFirst(newHead);

                if (_area.IsTarget(x, y) == false)
                {
                    var snakeEnd = Body.Last();
                    snakeEnd.Entity = Entity.EMPTY;
                    (Body as LinkedList<Cell>).RemoveLast();
                    BringToArea(new[] { snakeEnd });
                }

                BringToArea(Body);
            }
            else
            {
                IsDead = true;
            }
        }
    }
}
