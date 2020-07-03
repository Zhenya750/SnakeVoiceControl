using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeVoiceControl
{
    public abstract class Snake
    {
        protected Area _area;
        protected abstract ICollection<(int, int)> Body { get; set; }
        protected bool IsDead { get; private set; }

        public Snake(Area area)
        {
            _area = area;
            IsDead = false;
        }

        public void GoTo(int x, int y)
        {
            var (headX, headY) = Body.First();

            if (_area.CanGo(x, y))
            {
                if (Body.Contains((x, y)))
                {
                    IsDead = true;
                    return;
                }

                Body.Prepend((x, y));
                Body.Remove(Body.Last());
            }
            else
            {
                IsDead = true;
            }
        }

        public void GoLeft()
        {
            var (headX, headY) = Body.First();
            GoTo(headX - 1, headY);
        }

        public void GoRight()
        {
            var (headX, headY) = Body.First();
            GoTo(headX + 1, headY);
        }

        public void GoUp()
        {
            var (headX, headY) = Body.First();
            GoTo(headX, headY - 1);
        }

        public void GoDown()
        {
            var (headX, headY) = Body.First();
            GoTo(headX, headY + 1);
        }
    }
}
