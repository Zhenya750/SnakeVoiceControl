using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        private Drawer _drawer;

        private Area _area;
        private Snake _snake;
        private ISnakeController _snakeController;
        private Direction _lastUsedDirection;

        private DispatcherTimer _timer;
        private TimeSpan _goneSeconds;

        public MainWindow()
        {
            InitializeComponent();

            InitLogicObjects();
            InitGraphicObjects();
            
            _drawer.DrawCells(_area.Cells.Values);
        }

        #region GameEngine
        private void InitLogicObjects()
        {
            _area = new AreaWithTargets((int)canvas.Width / Drawer.CellSize, (int)canvas.Height / Drawer.CellSize);
            _snake = new ClassicSnake(_area);
            _snakeController = new SnakeKeyController();
            _lastUsedDirection = Direction.UNKNOWN;

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(100);
            _timer.Tick += Timer_Tick;

            _goneSeconds = new TimeSpan();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            GameTick();
            _goneSeconds += _timer.Interval;
        }

        private void GameTick()
        {
            _lastUsedDirection = _snakeController.CanGetDirection() ?
                _snakeController.GetDirection() :
                _lastUsedDirection;

            switch (_lastUsedDirection)
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

            _drawer.DrawCells(_area.Cells.Values);

            if (_goneSeconds.TotalSeconds % 2 == 0)
            {
                _area.GenerateEntity(Entity.Target);
            }
        }

        private void StartGame()
        {
            if (_timer.IsEnabled)
            {
                return;
            }

            _timer.Start();
        }

        private void RestartGame()
        {
            if (_timer.IsEnabled == false)
            {
                return;
            }

            _timer.Stop();
            _goneSeconds = new TimeSpan(0);

            _area.TransformEntities(Entity.SnakeAliveHead, Entity.Empty);
            _area.TransformEntities(Entity.SnakeStraightBodyPart, Entity.Empty);
            _area.TransformEntities(Entity.SnakeBendBodyPart, Entity.Empty);
            _area.TransformEntities(Entity.SnakeDeadHead, Entity.Empty);
            _area.TransformEntities(Entity.SnakeEndBodyPart, Entity.Empty);
            _area.TransformEntities(Entity.Target, Entity.Empty);
            _area.TransformEntities(Entity.Wall, Entity.Empty);
            _snake = new ClassicSnake(_area);

            _drawer.DrawCells(_area.Cells.Values);
        }

        private void InitGraphicObjects()
        {
            _drawer = new Drawer(canvas);
        }

        #endregion

        // events
        private void KeyEventHandler(object sender, KeyEventArgs e)
        {
            _snakeController.AddEvent(sender, e);
            StartGame();
        }

        private void BRestart_Click(object sender, RoutedEventArgs e)
        {
            RestartGame();
        }

        private void BKeyboardArrows_Checked(object sender, RoutedEventArgs e)
        {
            bMicrophone.IsChecked = false;
            _snakeController = new SnakeKeyController();
            KeyDown += KeyEventHandler;
        }

        private void BKeyboardArrows_Unchecked(object sender, RoutedEventArgs e)
        {
            KeyDown -= KeyEventHandler;
        }
    }
}
