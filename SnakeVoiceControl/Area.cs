using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeVoiceControl
{
    public abstract class Area
    {
        public abstract Dictionary<(int, int), Cell> Cells { get; protected set; }
        public int WidthInCells { get; private set; }
        public int HeightInCells { get; private set; }

        public Area(int widthInCells, int heightInCells)
        {
            Cells = new Dictionary<(int, int), Cell>();
            WidthInCells = widthInCells;
            HeightInCells = heightInCells;

            for (int x = 0; x < WidthInCells; x++)
            {
                for (int y = 0; y < HeightInCells; y++)
                {
                    Cells.Add((x, y), new Cell(x, y, Entity.EMPTY));
                }
            }
        }

        public bool CanGo(int x, int y)
        {
            return true;
        }

        public bool IsTarget(int x, int y)
        {
            return false;
        }
    }
}
