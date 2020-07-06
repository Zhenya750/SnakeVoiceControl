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
        public abstract ICollection<Cell> Body { get; protected set; }
        public bool IsDead { get; protected set; }

        public Snake(Area area)
        {
            _area = area;
            IsDead = false;
        }

        protected abstract void GoTo(int x, int y);

        public virtual void GoLeft()
        {
            var head = Body.First();
            GoTo(head.X - 1, head.Y);
        }

        public virtual void GoRight()
        {
            var head = Body.First();
            GoTo(head.X + 1, head.Y);
        }

        public virtual void GoUp()
        {
            var head = Body.First();
            GoTo(head.X, head.Y - 1);
        }

        public virtual void GoDown()
        {
            var head = Body.First();
            GoTo(head.X, head.Y + 1);
        }
    }
}
