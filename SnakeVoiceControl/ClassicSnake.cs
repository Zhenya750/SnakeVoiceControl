using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeVoiceControl
{
    public class ClassicSnake : Snake
    {
        protected override ICollection<(int, int)> Body { get; set; }

        public ClassicSnake(Area area)
            : base(area)
        {
            Body = new LinkedList<(int, int)>();
        }
    }
}
