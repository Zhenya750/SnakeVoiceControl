using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeVoiceControl
{
    public abstract class Area
    {
        private Entity[,] _area;
        private Dictionary<(int, int), Entity> _targets;

        public Area(int widthInCells, int heightInCells)
        {
            _area = new Entity[widthInCells, heightInCells];
            _targets = new Dictionary<(int, int), Entity>();
        }

        public bool CanGo(int x, int y)
        {
            return true;
        }

        public bool IsTarget(int x, int y)
        {
            return false;
        }

        public (int widthInCells, int heightInCells) GetBounds()
        {
            return (_area.GetLength(0), _area.GetLength(1));
        }

    }
}
