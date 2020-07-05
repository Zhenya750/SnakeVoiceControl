using Microsoft.Speech.Recognition;
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

        private Dictionary<Cell, Rectangle> _cellToRect;

        private Area _area;
        private Snake _snake;
        private ISnakeController _snakeController;

        public MainWindow()
        {
            InitializeComponent();

            PreviewKeyDown += KeyEventHandler;

            _cellToRect = new Dictionary<Cell, Rectangle>();
            _area = new AreaWithTargets((int)canvas.Width / 20, (int)canvas.Height / 20);
            _area.GenerateEntity(Entity.TARGET, 10);
            _snake = new ClassicSnake(_area);
            _snakeController = new SnakeKeyController();

            DrawCells(_area.Cells.Values);



            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(50 * 4);
            timer.Tick += Timer_Tick;

            //System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ru-ru");
            //SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
            //recEngine.SetInputToDefaultAudioDevice();

            //recEngine.SpeechRecognized += Recognized;

            //Choices commands = new Choices();
            //commands.Add(new[] { "влево", "вправо", "вверх", "вниз" });

            //GrammarBuilder gb = new GrammarBuilder();
            //gb.Append(commands);
            //Grammar grammar = new Grammar(gb);

            //recEngine.LoadGrammar(grammar);
            //recEngine.RecognizeAsync(RecognizeMode.Multiple);
        }


        #region firstpart
        private TimeSpan _goneSeconds = new TimeSpan();
        private void Timer_Tick(object sender, EventArgs e)
        {
            GameTick();
            _goneSeconds += timer.Interval;
        }

        private Direction _lastUsedDirection;
        private void GameTick()
        {
            Direction direction = _snakeController.CanGetDirection() ?
                _snakeController.GetDirection() :
                _lastUsedDirection;

            switch (direction)
            {
                case Direction.UP:
                    _snake.GoUp();
                    break;
                case Direction.DOWN:
                    _snake.GoDown();
                    break;
                case Direction.LEFT:
                    _snake.GoLeft();
                    break;
                case Direction.RIGHT:
                    _snake.GoRight();
                    break;
            }

            _lastUsedDirection = direction;

            DrawCells(_area.Cells.Values);

            if (_goneSeconds.TotalSeconds % 4 == 0)
            {
                _area.GenerateEntity(Entity.TARGET);
            }
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
                rect.Height = rect.Width = 18;
                rect.RadiusX = rect.RadiusY = 4;
                rect.Fill = _entityToBrush[(int)cell.Entity];

                Canvas.SetLeft(rect, cell.X * 20);
                Canvas.SetTop(rect, cell.Y * 20);

                canvas.Children.Add(rect);
                _cellToRect.Add(cell, rect);
            }
        }

        private void KeyEventHandler(object sender, KeyEventArgs e)
        {
            _snakeController.AddEvent(sender, e);

            if (timer.IsEnabled == false)
            {
                timer.Start();
            }
        }

        #endregion

        #region speechpart

        // private void Recognized(object sender, SpeechRecognizedEventArgs e)
        //{
        //    Console.WriteLine("recognized");
        //    Console.WriteLine(e.Result.Text);
        //}

        #endregion
    }
}
