using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SnakeVoiceControl
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;

        private SolidColorBrush[] _entityToBrush = new SolidColorBrush[]
        {
            Brushes.Transparent,
            Brushes.Gray,
            Brushes.Red,
            Brushes.Green,
        };

        private delegate void DirectSnake();
        private DirectSnake Go;

        private Dictionary<Cell, Rectangle> _cellToRect;
        private Area _area;
        private Snake _snake;

        public MainWindow()
        {
            InitializeComponent();
            PreviewKeyUp += foo;

            _cellToRect = new Dictionary<Cell, Rectangle>();
            _area = new EmptyArea((int)canvas.Width / 20, (int)canvas.Height / 20);
            _snake = new ClassicSnake(_area);

            DrawCells(_area.Cells.Values);

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(250);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            GameTick();
        }

        private void GameTick()
        {
            Go();
            DrawCells(_area.Cells.Values);
        }

        private void DrawCells(ICollection<Cell> cells)
        {
            foreach (var cell in cells)
            {
                DrawCell(cell);
            }
        }

        private void DrawCell(Cell cell)
        {
            if (_cellToRect == null)
            {
                return;
            }

            Rectangle rect;

            if (_cellToRect.ContainsKey(cell))
            {
                rect = _cellToRect[cell];
                rect.Fill = _entityToBrush[(int)cell.Entity];
            }
            else
            {
                rect = new Rectangle();
                rect.Height = rect.Width = 20;
                rect.Fill = _entityToBrush[(int)cell.Entity];

                Canvas.SetLeft(rect, cell.X * rect.Width);
                Canvas.SetTop(rect, cell.Y * rect.Width);

                canvas.Children.Add(rect);
                _cellToRect.Add(cell, rect);
            }
        }

        private void foo(object sender, KeyEventArgs e)
        {
            bool gameStarted = Go != null;

            switch (e.Key)
            {
                case Key.Up:
                    Go = _snake.GoUp;
                    break;
                case Key.Down:
                    Go = _snake.GoDown;
                    break;
                case Key.Left:
                    Go = _snake.GoLeft;
                    break;
                case Key.Right:
                    Go = _snake.GoRight;
                    break;
                default:
                    break;
            }

            if (gameStarted == false)
            {
                timer.Start();
            }
        }   

    }
}
