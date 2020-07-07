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
        private readonly Dictionary<Key, Direction> _keyToDirection; 

        public SnakeKeyController()
        {
            _inputDirections = new Queue<Direction>();
            _keyToDirection = new Dictionary<Key, Direction>
            {
                { Key.Up,    Direction.Up },
                { Key.Down,  Direction.Down },
                { Key.Left,  Direction.Left },
                { Key.Right, Direction.Right },
            };
        }

        public void AddEvent(object sender, EventArgs e)
        {
            if (e is KeyEventArgs)
            {
                if (_keyToDirection.ContainsKey((e as KeyEventArgs).Key))
                {
                    _inputDirections.Enqueue(_keyToDirection[(e as KeyEventArgs).Key]);
                }
            }
        }

        public Direction GetDirection()
        {
            if (_inputDirections.Peek() == Direction.Unknown)
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
