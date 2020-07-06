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
            KeyDown += KeyEventHandler;
            

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
                _area.GenerateEntity(Entity.TARGET);
            }
        }

        private void KeyEventHandler(object sender, KeyEventArgs e)
        {
            _snakeController.AddEvent(sender, e);
            StartGame();
        }

        private void StartGame()
        {
            if (_timer.IsEnabled == false)
            {
                _timer.Start();
            }
        }

        private void StopGame()
        {
            if (_timer.IsEnabled)
            {
                _timer.Stop();
            }
        }

        private void InitGraphicObjects()
        {
            _drawer = new Drawer(canvas);
        }

        


        // private void Recognized(object sender, SpeechRecognizedEventArgs e)
        //{
        //    Console.WriteLine("recognized");
        //    Console.WriteLine(e.Result.Text);
        //}
        
    }
}
