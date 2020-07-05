using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SnakeVoiceControl
{
    public class SnakeKeyController : ISnakeController
    {
        private Queue<Direction> _inputDirections;

        public SnakeKeyController()
        {
            _inputDirections = new Queue<Direction>();
        }

        public void AddEvent(object sender, EventArgs e)
        {
            if (e is KeyEventArgs)
            {
                switch ((e as KeyEventArgs).Key)
                {
                    case Key.Up:
                        _inputDirections.Enqueue(Direction.UP);
                        break;
                    case Key.Down:
                        _inputDirections.Enqueue(Direction.DOWN);
                        break;
                    case Key.Left:
                        _inputDirections.Enqueue(Direction.LEFT);
                        break;
                    case Key.Right:
                        _inputDirections.Enqueue(Direction.RIGHT);
                        break;
                }
            }
        }

        public Direction GetDirection()
        {
            if (_inputDirections.Peek() == Direction.UNKNOWN)
            {
                throw new Exception("Unexpected direction");
            }

            return _inputDirections.Dequeue();
        }

        public bool CanGetDirection()
        {
            return _inputDirections.Count > 0;
        }
    }
}
