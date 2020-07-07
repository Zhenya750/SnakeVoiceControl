using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SnakeVoiceControl
{
    public class GameProcess
    {
        private Area _area;
        private Snake _snake;
        public ICollection<Cell> Cells { get => _area.Cells.Values; }

        public ISnakeController Controller { get; set; }
        private Direction _lastUsedDirection;

        public GameProcess(Area area, Snake snake, ISnakeController controller)
        {
            _area = area;
            _snake = snake;
            Controller = controller;
            _lastUsedDirection = Direction.Unknown;
        }

        public void GameTick(TimeSpan currentTime)
        {
            _lastUsedDirection = Controller.CanGetDirection() ?
                Controller.GetDirection() :
                _lastUsedDirection;

            switch (_lastUsedDirection)
            {
                case Direction.Up:
                    _snake.GoUp();
                    break;
                case Direction.Down:
                    _snake.GoDown();
                    break;
                case Direction.Left:
                    _snake.GoLeft();
                    break;
                case Direction.Right:
                    _snake.GoRight();
                    break;
            }

            if (currentTime.TotalSeconds % 2 == 0)
            {
                _area.GenerateEntity(Entity.Target);
            }
        }

        public void Restart()
        {
            _area.TransformEntities(Entity.SnakeAliveHead, Entity.Empty);
            _area.TransformEntities(Entity.SnakeStraightBodyPart, Entity.Empty);
            _area.TransformEntities(Entity.SnakeBendBodyPart, Entity.Empty);
            _area.TransformEntities(Entity.SnakeDeadHead, Entity.Empty);
            _area.TransformEntities(Entity.SnakeEndBodyPart, Entity.Empty);
            _area.TransformEntities(Entity.Target, Entity.Empty);
            _area.TransformEntities(Entity.Wall, Entity.Empty);
            _snake = new ClassicSnake(_area);
        }

        public void AddEvent(object sender, EventArgs e)
        {
            Controller.AddEvent(sender, e);
        }
    }
}
