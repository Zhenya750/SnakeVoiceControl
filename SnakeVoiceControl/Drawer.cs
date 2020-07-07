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
                Brushes.Transparent,    // Empty
                Brushes.Gray,           // Wall
                Brushes.Red,            // Target
                Brushes.Green,          // SnakeStraightbodyPart
                Brushes.LightGreen,     // SnakeAliveHead
                Brushes.Pink,           // SnakeDeadHead
                Brushes.Yellow,         // SnakeBendBodyPart
                Brushes.Blue,           // SnakeEndBodyPart
            };

            _entityToImage = new ImageBrush[]
            {
                null,
                LoadImageBrush("Images/Wall.png"),
                LoadImageBrush("Images/Target.png"),
                LoadImageBrush("Images/SnakeStraightBodyPart.png"),
                LoadImageBrush("Images/SnakeAliveHead.png"),
                LoadImageBrush("Images/SnakeDeadHead.png"),
                LoadImageBrush("Images/SnakeBendBodyPart.png"),
                LoadImageBrush("Images/SnakeEndBodyPart.png"),
            };
        }

        private ImageBrush LoadImageBrush(string path)
        {
            try
            {
                return new ImageBrush(new ImageSourceConverter().ConvertFromString(path) as ImageSource);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
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
