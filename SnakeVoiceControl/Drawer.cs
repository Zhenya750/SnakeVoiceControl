﻿using System;
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
        private readonly int[] _entityToSize;
        private readonly int[] _entityToRadius;
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
            };

            _entityToSize = new int[]
            {
                20,
                20,
                20,
                18,
                18,
                18,
                18,
            };

            _entityToRadius = new int[]
            {
                0,
                0,
                7,
                4,
                4,
                4,
                4,
            };

            _entityToImage = new ImageBrush[]
            {
                null,
                null,
                new ImageBrush(new ImageSourceConverter().ConvertFromString("Images/target.png") as ImageSource),
                null,
                null,
                null,
                null,
            };
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

            rect.Width = rect.Height = _entityToSize[(int)cell.Entity];
            rect.RenderTransform = new RotateTransform(cell.EntityAngle, rect.Width / 2, rect.Height / 2);
            rect.RadiusX = rect.RadiusY = _entityToRadius[(int)cell.Entity];
        }
    }
}
