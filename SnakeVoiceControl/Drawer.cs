using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SnakeVoiceControl
{
    public class Drawer
    {
        private Canvas _canvas;
        private Dictionary<Cell, Rectangle> _cellToRect;
        private readonly ImageBrush[] _entityToImage;
        private readonly SolidColorBrush[] _entityToBrush;
        public static readonly int CellSize = 20; 

        public Drawer(Canvas canvas)
        {
            _canvas = canvas;
            _cellToRect = new Dictionary<Cell, Rectangle>();

            _entityToBrush = new SolidColorBrush[]
            {
                Brushes.Transparent,
                Brushes.Gray,
                Brushes.Red,
                Brushes.Green,
                Brushes.LightGreen,
                Brushes.Pink,
                Brushes.Yellow,
                Brushes.Blue,
            };

            try
            {
                _entityToImage = new ImageBrush[]
                {
                    null,
                    null,
                    new ImageBrush(new ImageSourceConverter().ConvertFromString("Images/target.png") as ImageSource),
                    new ImageBrush(new ImageSourceConverter().ConvertFromString("Images/SnakeStraightBodyPart.png") as ImageSource),
                    new ImageBrush(new ImageSourceConverter().ConvertFromString("Images/SnakeAliveHead.png") as ImageSource),
                    new ImageBrush(new ImageSourceConverter().ConvertFromString("Images/SnakeDeadHead.png") as ImageSource),
                    new ImageBrush(new ImageSourceConverter().ConvertFromString("Images/SnakeBendBodyPart.png") as ImageSource),
                    new ImageBrush(new ImageSourceConverter().ConvertFromString("Images/SnakeEndBodyPart.png") as ImageSource),
                };
            }
            catch (Exception)
            {
                _entityToImage = new ImageBrush[] { null, null, null, null, null, null, null, null };
            }
        }

        public void DrawCells(ICollection<Cell> cells)
        {
            foreach (var cell in cells)
            {
                DrawCell(cell);
            }
        }

        public void DrawCell(Cell cell)
        {
            Rectangle rect;

            if (_cellToRect.ContainsKey(cell))
            {
                rect = _cellToRect[cell];
            }
            else
            {
                rect = new Rectangle();

                Canvas.SetLeft(rect, cell.X * CellSize);
                Canvas.SetTop(rect, cell.Y * CellSize);

                _canvas.Children.Add(rect);
                _cellToRect.Add(cell, rect);
            }
            
            if (_entityToImage[(int)cell.Entity] != null)
            {
                rect.Fill = _entityToImage[(int)cell.Entity];
            }
            else
            {
                rect.Fill = _entityToBrush[(int)cell.Entity];
            }

            rect.Width = rect.Height = CellSize;
            rect.RenderTransform = new RotateTransform(cell.EntityAngle, rect.Width / 2, rect.Height / 2);
        }
    }
}
