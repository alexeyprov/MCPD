using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace CustomUI
{
    [TemplatePart(Name = "PART_RedSlider", Type = typeof(RangeBase))]
    [TemplatePart(Name = "PART_GreenSlider", Type = typeof(RangeBase))]
    [TemplatePart(Name = "PART_BlueSlider", Type = typeof(RangeBase))]
    [TemplatePart(Name = "PART_PreviewBrush", Type = typeof(Brush))]
    public class ColorPicker : Control
    {
        #region Private Constants

        private const string SLIDER_PART_FORMAT = "PART_{0}Slider";
        private const string BRUSH_PART_NAME = "PART_PreviewBrush";
        
        #endregion

        #region Private Fields

        private Color? _previousColor; 

        #endregion

        #region Public Fields

        public static RoutedEvent ColorChangedEvent;

        public static readonly DependencyProperty ColorProperty;
        public static readonly DependencyProperty RedProperty;
        public static readonly DependencyProperty GreenProperty;
        public static readonly DependencyProperty BlueProperty; 
        
        #endregion

        #region Constructor

        static ColorPicker()
        {
            // register default style (with template)
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(ColorPicker), 
                new FrameworkPropertyMetadata(typeof(ColorPicker)));

            ColorProperty = DependencyProperty.Register(
                "Color",
                typeof(Color),
                typeof(ColorPicker),
                new PropertyMetadata(Colors.Black, OnColorChanged));

            RedProperty = DependencyProperty.Register(
                "Red",
                typeof(byte),
                typeof(ColorPicker),
                new PropertyMetadata(OnRgbChanged));

            GreenProperty = DependencyProperty.Register(
                "Green",
                typeof(byte),
                typeof(ColorPicker),
                new PropertyMetadata(OnRgbChanged));

            ColorChangedEvent = EventManager.RegisterRoutedEvent(
                "ColorChanged",
                RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<Color>),
                typeof(ColorPicker));

            BlueProperty = DependencyProperty.Register(
                "Blue",
                typeof(byte),
                typeof(ColorPicker),
                new PropertyMetadata(OnRgbChanged));

            CommandManager.RegisterClassCommandBinding(
                typeof(ColorPicker),
                new CommandBinding(ApplicationCommands.Undo, OnUndoExecuted, OnUndoCanExecute));
        }

        #endregion

        #region Events

        public event RoutedPropertyChangedEventHandler<Color> ColorChanged
        {
            add
            {
                AddHandler(ColorChangedEvent, value);
            }
            remove
            {
                RemoveHandler(ColorChangedEvent, value);
            }
        }
        
        #endregion

        #region Public Properties

        public Color Color
        {
            get
            {
                return (Color)GetValue(ColorProperty);
            }
            set
            {
                SetValue(ColorProperty, value);
            }
        }

        public byte Red
        {
            get
            {
                return (byte)GetValue(RedProperty);
            }
            set
            {
                SetValue(RedProperty, value);
            }
        }

        public byte Green
        {
            get
            {
                return (byte)GetValue(GreenProperty);
            }
            set
            {
                SetValue(GreenProperty, value);
            }
        }

        public byte Blue
        {
            get
            {
                return (byte)GetValue(BlueProperty);
            }
            set
            {
                SetValue(BlueProperty, value);
            }
        }

        #endregion

        #region Overrides

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            BindSlider("Red");
            BindSlider("Green");
            BindSlider("Blue");

            BindBrush();
        }
 
        #endregion

        #region Implementation

        private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ColorPicker c = (ColorPicker)d;
            Color oldColor = (Color)e.OldValue;
            Color newColor = (Color)e.NewValue;

            if (newColor != oldColor)
            {
                c._previousColor = oldColor;

                c.Red = newColor.R;
                c.Green = newColor.G;
                c.Blue = newColor.B;

                c.RaiseEvent(
                    new RoutedPropertyChangedEventArgs<Color>(oldColor, newColor, ColorChangedEvent));
            }
        }

        private static void OnRgbChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ColorPicker c = (ColorPicker)d;

            Color color = c.Color;

            if (e.Property == RedProperty)
            {
                color.R = (byte)e.NewValue;
            }
            else if (e.Property == GreenProperty)
            {
                color.G = (byte)e.NewValue;
            }
            else if (e.Property == BlueProperty)
            {
                color.B = (byte)e.NewValue;
            }

            c.Color = color;
        }

        private static void OnUndoCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ColorPicker c = (ColorPicker)sender;

            e.CanExecute = c._previousColor != null && c._previousColor != c.Color;
        }

        private static void OnUndoExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            ColorPicker c = (ColorPicker)sender;

            c.Color = c._previousColor.Value;
            c._previousColor = null;
        }

        private void BindBrush()
        {
            BindingBase binding = new Binding("Color")
            {
                Mode = BindingMode.OneWayToSource,
                Source = GetTemplateChild(BRUSH_PART_NAME)
            };

            SetBinding(ColorProperty, binding);
        }

        private void BindSlider(string propertyName)
        {
            FrameworkElement slider = (FrameworkElement)GetTemplateChild(string.Format(SLIDER_PART_FORMAT, propertyName));

            BindingBase binding = new Binding(propertyName)
            {
                Mode = BindingMode.TwoWay,
                Source = this
            };

            slider.SetBinding(RangeBase.ValueProperty, binding);
        }

        #endregion
    }
}
