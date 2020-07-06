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
            var head = new Cell(_area.WidthInCells / 2, _area.HeightInCells / 2, Entity.SnakeAliveHead);
            Body.Add(head);
            BringToArea(new[] { head });
        }

        private void BringToArea(ICollection<Cell> cells)
        {
            foreach (var cell in cells)
            {
                _area.Cells[(cell.X, cell.Y)].Entity = cell.Entity;
                _area.Cells[(cell.X, cell.Y)].EntityAngle = cell.EntityAngle;
            }
        }

        protected override void GoTo(int x, int y)
        {
            //if (IsDead)
            //{
            //    return;
            //}

            if (x < 0) x = _area.WidthInCells - 1;
            if (x >= _area.WidthInCells) x = 0;
            if (y < 0) y = _area.HeightInCells - 1;
            if (y >= _area.HeightInCells) y = 0;

            var oldHead = Body.First();
            var newHead = new Cell(x, y, Entity.SnakeAliveHead);

            if (_area.CanGo(x, y))
            {
                if (Body.Contains(newHead))
                {
                    IsDead = true;
                    oldHead.Entity = Entity.SnakeDeadHead;
                    BringToArea(new[] { Body.First() });
                    return;
                }

                oldHead.Entity = Entity.SnakeStraightBodyPart;
                (Body as LinkedList<Cell>).AddFirst(newHead);

                if (_area.IsTarget(x, y) == false)
                {
                    var snakeEnd = Body.Last();
                    snakeEnd.Entity = Entity.Empty;
                    (Body as LinkedList<Cell>).RemoveLast();
                    BringToArea(new[] { snakeEnd });
                }

                if (Body.Count >= 3)
                {
                    SmoothBody();
                }

                BringToArea(Body);
            }
            else
            {
                IsDead = true;
            }
        }

        private void SmoothBody()
        {
            var first = Body.First();
            var second = Body.ElementAt(1);
            var third = Body.ElementAt(2);

            // rotate head
            if (first.X != second.X)
            {
                if (first.X < second.X)
                {
                    first.EntityAngle = 180;
                }

                if (first.X > second.X)
                {
                    first.EntityAngle = 0;
                }
            }
            else
            {
                if (first.Y < second.Y)
                {
                    first.EntityAngle = 90;
                }

                if (first.Y > second.Y)
                {
                    first.EntityAngle = 270;
                }
            }

            // bent second part
            var (x0, y0) = (first.X, first.Y);
            var (x2, y2) = (third.X, third.Y);
            var (x, y) = (x2 - x0, y2 - y0);

            if (x == -1 && y == -1)
            {
                second.Entity = Entity.SnakeBendBodyPart;
                second.EntityAngle = second.X == first.X ? 270 : 90;
            }

            if (x == -1 && y == 1)
            {
                second.Entity = Entity.SnakeBendBodyPart;
                second.EntityAngle = second.X == first.X ? 180 : 0;
            }

            if (x == 1 && y == 1)
            {
                second.Entity = Entity.SnakeBendBodyPart;
                second.EntityAngle = second.X == first.X ? 90 : 270;
            }

            if (x == 1 && y == -1)
            {
                second.Entity = Entity.SnakeBendBodyPart;
                second.EntityAngle = second.X == first.X ? 0 : 180;
            }

            // straight second part
            if (x == 0 && y == -2)
            {
                second.Entity = Entity.SnakeStraightBodyPart;
                second.EntityAngle = 90;
            }

            if (x == 0 && y == 2)
            {
                second.Entity = Entity.SnakeStraightBodyPart;
                second.EntityAngle = 270;
            }

            if (x == -2 && y == 0)
            {
                second.Entity = Entity.SnakeStraightBodyPart;
                second.EntityAngle = 180;
            }

            if (x == 2 && y == 0)
            {
                second.Entity = Entity.SnakeStraightBodyPart;
                second.EntityAngle = 0;
            }
        }
    }
}
