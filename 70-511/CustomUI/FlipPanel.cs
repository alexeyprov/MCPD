using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace CustomUI
{
    [TemplateVisualState(Name=NORMAL_STATE, GroupName="ViewStates")]
    [TemplateVisualState(Name=FLIPPED_STATE, GroupName="ViewStates")]
    [TemplatePart(Name=FLIP_BUTTON_PART, Type=typeof(ToggleButton))]
    [TemplatePart(Name=ALT_BUTTON_PART, Type=typeof(ToggleButton))]
    public class FlipPanel : Control
    {
        private const string FLIP_BUTTON_PART = "FlipButton";
        private const string ALT_BUTTON_PART = "FlipButtonAlternate";

        private const string NORMAL_STATE = "Normal";
        private const string FLIPPED_STATE = "Flipped";

        public static readonly DependencyProperty FrontContentProperty;
        public static readonly DependencyProperty BackContentProperty;
        public static readonly DependencyProperty IsFlippedProperty;
        public static readonly DependencyProperty CornerRadiusProperty;

        static FlipPanel()
        {
            FrontContentProperty = DependencyProperty.Register(
                "FrontContent",
                typeof(object),
                typeof(FlipPanel));

            BackContentProperty = DependencyProperty.Register(
                "BackContent",
                typeof(object),
                typeof(FlipPanel));

            IsFlippedProperty = DependencyProperty.Register(
                "IsFlipped",
                typeof(bool),
                typeof(FlipPanel),
                new PropertyMetadata(false));

            CornerRadiusProperty = DependencyProperty.Register(
                "CornerRadius",
                typeof(CornerRadius),
                typeof(FlipPanel));

            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlipPanel),
                new FrameworkPropertyMetadata(typeof(FlipPanel)));
        }

        public object FrontContent
        {
            get
            {
                return (object)GetValue(FrontContentProperty);
            }
            set
            {
                SetValue(FrontContentProperty, value);
            }
        }

        public object BackContent
        {
            get
            {
                return (object)GetValue(BackContentProperty);
            }
            set
            {
                SetValue(BackContentProperty, value);
            }
        }

        public bool IsFlipped
        {
            get
            {
                return (bool)GetValue(IsFlippedProperty);
            }
            set
            {
                SetValue(IsFlippedProperty, value);
                ChangeVisualState(true);
            }
        }

        public CornerRadius CornerRadius
        {
            get
            {
                return (CornerRadius)GetValue(CornerRadiusProperty);
            }
            set
            {
                SetValue(CornerRadiusProperty, value);
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // wire up flip buttons event
            HookUpButton(FLIP_BUTTON_PART);
            HookUpButton(ALT_BUTTON_PART);

            ChangeVisualState(false);
        }

        private void ChangeVisualState(bool useTransitions)
        {
            VisualStateManager.GoToState(
                this,
                IsFlipped ? FLIPPED_STATE : NORMAL_STATE,
                useTransitions);
        }

        private void HookUpButton(string partName)
        {
            ToggleButton flipButton = (ToggleButton)GetTemplateChild(partName);

            if (flipButton != null)
            {
                flipButton.Click += OnFlipButtonClick;
            }
        }

        private void OnFlipButtonClick(object sender, RoutedEventArgs e)
        {
            IsFlipped = !IsFlipped;
        }
    }

}
