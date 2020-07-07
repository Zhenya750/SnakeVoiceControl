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
        private DispatcherTimer _timer;
        private GameProcess _process;
        private TimeSpan _gone;

        public MainWindow()
        {
            InitializeComponent();

            var area = new AreaWithTargets((int)canvas.Width / Drawer.CellSize, (int)canvas.Height / Drawer.CellSize);
            var snake = new ClassicSnake(area);
            _process = new GameProcess(area, snake, null);

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(100);
            _timer.Tick += TimerTick;
            _gone = new TimeSpan(0);

            _drawer = new Drawer(canvas);
            _drawer.DrawCells(_process.Cells);
            
        }

        private void TimerTick(object sender, EventArgs e)
        {
            _gone += _timer.Interval;
            _process.GameTick(_gone);
            _drawer.DrawCells(_process.Cells);
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
            _timer.Stop();
            _gone = new TimeSpan(0);
            _process.Restart();
            _drawer.DrawCells(_process.Cells);
        }

        private void KeyEventHandler(object sender, KeyEventArgs e)
        {
            _process.AddEvent(sender, e);
            StartGame();
        }

        private void BRestart_Click(object sender, RoutedEventArgs e)
        {
            RestartGame();
        }

        private void BKeyboardArrows_Checked(object sender, RoutedEventArgs e)
        {
            _process.Controller = new SnakeKeyController();
            KeyDown += KeyEventHandler;
        }

        private void BKeyboardArrows_Unchecked(object sender, RoutedEventArgs e)
        {
            KeyDown -= KeyEventHandler;
        }
    }
}
