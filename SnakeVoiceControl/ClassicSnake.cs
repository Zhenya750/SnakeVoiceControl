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
            var end = new Cell(head.X - 1, head.Y, Entity.SnakeEndBodyPart);
            Body.Add(head);
            Body.Add(end);
            BringToArea(Body);
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
            if (IsDead)
            {
                return;
            }

            if (x < 0) x = _area.WidthInCells - 1;
            if (x >= _area.WidthInCells) x = 0;
            if (y < 0) y = _area.HeightInCells - 1;
            if (y >= _area.HeightInCells) y = 0;

            var oldHead = Body.First();
            var newHead = new Cell(x, y, oldHead.Entity);

            if (_area.CanGo(x, y) == false ||
                Body.Contains(newHead))
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
                snakeEnd.EntityAngle = 0;
                Body.Remove(snakeEnd);
                BringToArea(new[] { snakeEnd });
            }

            Cell thirdPart = Body.Count > 2 ? Body.ElementAt(2) : null;
            SmoothBody(newHead, oldHead, thirdPart);

            if (Body.Count >= 2)
            {
                Body.Last().Entity = Entity.SnakeEndBodyPart;
            }

            BringToArea(Body);
        }

        private void SmoothBody(Cell first, Cell second, Cell third)
        {
            SmoothHead(first, second);

            if (third == null)
            {
                return;
            }

            BendSecondPart(first, second, third);
            SmoothEndPart();
        }

        private void SmoothHead(Cell first, Cell second)
        {
            int w = _area.WidthInCells;
            int h = _area.HeightInCells;
            var (x0, y0) = (first.X, first.Y);
            var (x1, y1) = (second.X, second.Y);
            var (x, y) = (x0 - x1, y0 - y1);

            if (x == 0 && (y == -1 || y == h - 1))
            {
                first.EntityAngle = 270;
                second.EntityAngle = 270;
            }

            if ((x == -1 || x == w - 1) && y == 0)
            {
                first.EntityAngle = 180;
                second.EntityAngle = 180;
            }

            if (x == 0 && (y == 1 || y == 1 - h))
            {
                first.EntityAngle = 90;
                second.EntityAngle = 90;
            }

            if ((x == 1 || x == 1 - w) && y == 0)
            {
                first.EntityAngle = 0;
                second.EntityAngle = 0;
            }
        }

        private void BendSecondPart(Cell first, Cell second, Cell third)
        {
            int w = _area.WidthInCells;
            int h = _area.HeightInCells;
            var (x0, y0) = (first.X, first.Y);
            var (x2, y2) = (third.X, third.Y);
            var (x, y) = (x0 - x2, y0 - y2);

            if (x == -1 && y == -1 ||
                x == -1 && y == h - 1 ||
                x == w - 1 && y == -1)
            {
                second.Entity = Entity.SnakeBendBodyPart;
                second.EntityAngle = second.X == first.X ? 90 : 270;
            }

            if (x == -1 && y == 1 ||
                x == -1 && y == 1 - h ||
                x == w - 1 && y == 1)
            {
                second.Entity = Entity.SnakeBendBodyPart;
                second.EntityAngle = second.X == first.X ? 180 : 0;
            }

            if (x == 1 && y == 1 ||
                x == 1 && y == 1 - h ||
                x == 1 - w && y == 1)
            {
                second.Entity = Entity.SnakeBendBodyPart;
                second.EntityAngle = second.X == first.X ? 270 : 90;
            }

            if (x == 1 && y == -1 ||
                x == 1 && y == h - 1 ||
                x == 1 - w && y == -1)
            {
                second.Entity = Entity.SnakeBendBodyPart;
                second.EntityAngle = second.X == first.X ? 0 : 180;
            }
        }

        private void SmoothEndPart()
        {
            var end = Body.Last();
            if (end.Entity == Entity.SnakeBendBodyPart)
            {
                var beforeEnd = Body.ElementAt(Body.Count - 2);

                if (beforeEnd.Entity != Entity.SnakeBendBodyPart)
                {
                    end.EntityAngle = beforeEnd.EntityAngle;
                }
                else
                {
                    switch (beforeEnd.EntityAngle)
                    {
                        case 0:
                            end.EntityAngle = end.Y == beforeEnd.Y ? 0 : 90;
                            break;
                        case 90:
                            end.EntityAngle = end.Y == beforeEnd.Y ? 180 : 90;
                            break;
                        case 180:
                            end.EntityAngle = end.Y == beforeEnd.Y ? 180 : 270;
                            break;
                        case 270:
                            end.EntityAngle = end.Y == beforeEnd.Y ? 0 : 270;
                            break;
                    }
                }
            }
        }
    }
}
