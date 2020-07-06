using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeVoiceControl
{
    public abstract class Area
    {
        public Dictionary<(int, int), Cell> Cells { get; protected set; }
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

        public abstract void GenerateEntity(Entity entity, int count = 1);

        public abstract bool CanGo(int x, int y);

        public abstract bool IsTarget(int x, int y);
    }
}
