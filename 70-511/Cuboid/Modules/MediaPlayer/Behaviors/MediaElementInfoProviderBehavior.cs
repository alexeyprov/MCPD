using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Threading;

namespace Cuboid.Modules.MediaPlayer.Behaviors
{
    internal sealed class MediaElementInfoProviderBehavior : Behavior<MediaElement>
    {
        #region Private Fields

        private DispatcherTimer _timer;
        private bool _isPositionSetByTimer;
        private EventHandler _refreshing;
        private EventHandler _refreshed;

        #endregion

        #region Public Fields

        public static readonly DependencyProperty DurationProperty;
        public static readonly DependencyProperty PositionProperty;
        
        #endregion

        #region Constructor

        static MediaElementInfoProviderBehavior()
        {
            DurationProperty = DependencyProperty.Register(
                "Duration",
                typeof(int),
                typeof(MediaElementInfoProviderBehavior),
                new PropertyMetadata(0));
            PositionProperty = DependencyProperty.Register(
                "Position",
                typeof(int),
                typeof(MediaElementInfoProviderBehavior),
                new PropertyMetadata(0, OnPositionChanged));
        }

        public MediaElementInfoProviderBehavior()
        {
            _timer = new DispatcherTimer();
        }
        
        #endregion

        #region Events

        public event EventHandler Refreshing
        {
            add
            {
                _refreshing += value;
            }
            remove
            {
                _refreshing -= value;
            }
        }

        public event EventHandler Refreshed
        {
            add
            {
                _refreshed += value;
            }
            remove
            {
                _refreshed -= value;
            }
        }

        #endregion

        #region Public Properties

        public int Duration
        {
            get
            {
                return (int)GetValue(DurationProperty);
            }
            set
            {
                SetValue(DurationProperty, value);
            }
        }

        public int Position
        {
            get
            {
                return (int)GetValue(PositionProperty);
            }
            set
            {
                SetValue(PositionProperty, value);
                _isPositionSetByTimer = true;
            }
        }

        public TimeSpan RefreshInterval
        {
            get;
            set;
        }
        
        #endregion

        #region Event Handlers

        protected override void OnAttached()
        {
            base.OnAttached();

            _timer.Interval = RefreshInterval;
            _timer.Tick += OnTimerTick;

            _timer.Start();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            if (AssociatedObject.NaturalDuration.HasTimeSpan)
            {
                OnRefreshing();

                Duration = (int)AssociatedObject.NaturalDuration.TimeSpan.TotalSeconds;
                Position = (int)AssociatedObject.Position.TotalSeconds;

                OnRefreshed();
            }
        }

        private static void OnPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MediaElementInfoProviderBehavior behavior = (MediaElementInfoProviderBehavior)d;
            Debug.WriteLine(
                "Position changed from {0} to {1} by {2}",
                e.OldValue,
                e.NewValue,
                behavior._isPositionSetByTimer ? "timer" : "user");

            if (behavior._isPositionSetByTimer)
            {
                behavior._isPositionSetByTimer = false;
            }
            else
            {
                //behavior.AssociatedObject.Position = TimeSpan.FromSeconds((int)e.NewValue);
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            _timer.Stop();
            _timer.Tick -= OnTimerTick;
        }
        
        #endregion

        #region Implementation

        private void OnRefreshing()
        {
            if (_refreshing != null)
            {
                _refreshing(this, EventArgs.Empty);
            }
        }

        private void OnRefreshed()
        {
            if (_refreshed != null)
            {
                _refreshed(this, EventArgs.Empty);
            }
        }

        #endregion
    }
}
