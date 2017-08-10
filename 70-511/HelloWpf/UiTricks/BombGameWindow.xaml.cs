using System;
using System.Collections.Generic;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace HelloWpf.UiTricks
{
    /// <summary>
    /// Interaction logic for BombGameWindow.xaml
    /// </summary>
    public partial class BombGameWindow : Window
    {
        #region Private Constants

        private const double DEFAULT_SECONDS_BETWEEN_BOMBS = 1.3;
        private const double DEFAULT_SECONDS_TO_FALL = 3.5;

        private const double MIN_SECONDS_BETWEEN_BOMBS = 0.5;
        private const double MIN_SECONDS_TO_FALL = 0.5;

        private const double SECONDS_BETWEEN_ADJUSTMENTS = 15.0;

        private const int MAX_DROPPED_BOMBS = 5; 

        #endregion

        #region Private Fields

        private readonly DispatcherTimer _bombTimer;
        private readonly IDictionary<BombControl, Storyboard> _animations;
        private readonly SoundPlayer _player; 

        private int _droppedCount;
        private int _savedCount;

        private double _secondsBetweenBombs;
        private double _secondsToFall;

        private DateTime _lastAdjustmentTime;

        #endregion

        #region Constructor

        public BombGameWindow()
        {
            InitializeComponent();

            _bombTimer = new DispatcherTimer();
            _bombTimer.Tick += BombTimer_Tick;

            _lastAdjustmentTime = DateTime.Now.AddSeconds(-2 * SECONDS_BETWEEN_ADJUSTMENTS);

            _animations = new Dictionary<BombControl, Storyboard>();
            _player = new SoundPlayer
            {
                Stream = Properties.Resources.TadaSound
            };
            _player.LoadCompleted += Player_LoadCompleted;
        }

        #endregion

        #region Event Handlers

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _player.LoadAsync();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            _player.LoadCompleted -= Player_LoadCompleted;
            _player.Dispose();
        }

        private void Player_LoadCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            _player.Play();
        }

        private void BombTimer_Tick(object sender, EventArgs e)
        {
            // Adjust speeds
            AdjustTimings();

            // Create and position a new bomb
            BombControl bomb = CreateBomb();

            // Create animations
            Storyboard storyboard = CreateAnimations(bomb);

            _animations.Add(bomb, storyboard);
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {
            Storyboard storyboard = (Storyboard)((ClockGroup)sender).Timeline;
            BombControl bomb = (BombControl)Storyboard.GetTarget(storyboard.Children[0]);

            if (bomb.IsFalling)
            {
                _droppedCount++;
            }
            else
            {
                _savedCount++;
            }

            StatusLabel.Text = string.Format(
                "You dropped {0} bombs and saved {1}.",
                _droppedCount,
                _savedCount);

            if (_droppedCount >= MAX_DROPPED_BOMBS)
            {
                StatusLabel.Text += "\n\nGame Over!";
                StartButton.IsEnabled = true;

                _bombTimer.Stop();

                foreach (KeyValuePair<BombControl, Storyboard> animation in _animations)
                {
                    RemoveBomb(animation.Value, animation.Key);
                }

                _animations.Clear();
            }
            else
            {
                RemoveBomb(storyboard, bomb);

                _animations.Remove(bomb);
            }
        }

        private void Bomb_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BombControl bomb = (BombControl)sender;
            bomb.IsFalling = false;

            double position = Canvas.GetLeft(bomb) + (bomb.Width / 2);

            Storyboard storyboard = _animations[bomb];

            storyboard.Stop();
            storyboard.Children.Clear();

            DoubleAnimation removeAnimation = new DoubleAnimation()
            {
                Duration = TimeSpan.FromMilliseconds(200.0),
                To = (position < GameCanvas.ActualWidth / 2) ?
                    -bomb.Width :
                    GameCanvas.ActualWidth
            };

            Storyboard.SetTarget(removeAnimation, bomb);
            Storyboard.SetTargetProperty(removeAnimation, new PropertyPath("(Canvas.Left)"));

            storyboard.Duration = removeAnimation.Duration;
            storyboard.Children.Add(removeAnimation);
            storyboard.Begin();
        }

        private void GameCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GameCanvas.Clip = new RectangleGeometry(
                new Rect(0, 0, GameCanvas.ActualWidth, GameCanvas.ActualHeight));
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StartButton.IsEnabled = false;

            // reset the game
            _droppedCount = _savedCount = 0;

            _secondsBetweenBombs = DEFAULT_SECONDS_BETWEEN_BOMBS;
            _secondsToFall = DEFAULT_SECONDS_TO_FALL;

            _bombTimer.Interval = TimeSpan.FromSeconds(DEFAULT_SECONDS_BETWEEN_BOMBS);
            _bombTimer.Start();
        }

        #endregion

        #region Implementation

        private Storyboard CreateAnimations(BombControl bomb)
        {
            Storyboard storyboard = new Storyboard()
            {
                Duration = TimeSpan.FromSeconds(_secondsToFall)
            };

            DoubleAnimation fallAnimation = new DoubleAnimation()
            {
                From = 0.0,
                To = GameCanvas.ActualHeight - bomb.Height,
                Duration = storyboard.Duration
            };

            Storyboard.SetTarget(fallAnimation, bomb);
            Storyboard.SetTargetProperty(fallAnimation, new PropertyPath("(Canvas.Top)"));

            DoubleAnimation wiggleAnimation = new DoubleAnimation()
            {
                To = 30.0,
                Duration = TimeSpan.FromMilliseconds(200.0),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            };

            Storyboard.SetTarget(wiggleAnimation, bomb);
            Storyboard.SetTargetProperty(wiggleAnimation, new PropertyPath("RenderTransform.Children[0].Angle"));

            storyboard.Children.Add(fallAnimation);
            storyboard.Children.Add(wiggleAnimation);
            storyboard.Completed += Storyboard_Completed;
            storyboard.Begin();
            return storyboard;
        }

        private BombControl CreateBomb()
        {
            BombControl bomb = new BombControl()
            {
                IsFalling = true
            };

            Canvas.SetBottom(bomb, 0.0);
            Canvas.SetLeft(bomb, (new Random()).Next((int)(GameCanvas.ActualWidth - bomb.Width)));

            GameCanvas.Children.Add(bomb);

            bomb.MouseLeftButtonDown += Bomb_MouseLeftButtonDown;

            return bomb;
        }

        private void AdjustTimings()
        {
            if ((DateTime.Now - _lastAdjustmentTime).Seconds > SECONDS_BETWEEN_ADJUSTMENTS)
            {
                _lastAdjustmentTime = DateTime.Now;

                _secondsToFall = Math.Max(_secondsToFall - 0.1, MIN_SECONDS_TO_FALL);
                _secondsBetweenBombs = Math.Max(_secondsBetweenBombs - 0.1, MIN_SECONDS_BETWEEN_BOMBS);

                _bombTimer.Interval = TimeSpan.FromSeconds(_secondsBetweenBombs);

                RateLabel.Text = string.Format("A bomb is released each {0} seconds", _secondsBetweenBombs);
                SpeedLabel.Text = string.Format("Each bomb takes {0} seconds to fall", _secondsToFall);
            }
        }

        private void RemoveBomb(Storyboard storyboard, FrameworkElement bomb)
        {
            storyboard.Stop();

            GameCanvas.Children.Remove(bomb);
        }

        #endregion
    }
}
