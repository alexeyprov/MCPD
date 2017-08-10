using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using HelloSilverlight.Code;

namespace HelloSilverlight
{
	public partial class RectanglesPage : BaseShapePage
	{
		#region Private Constants

		private const int DIMENSION = 50;

		#endregion

		#region Private Fields

		private Rectangle _rectangle;

		#endregion

		#region Constructor

		public RectanglesPage()
		{
			InitializeComponent();
		}
		
		#endregion

		#region Overrides

		protected override Panel Root
		{
			get
			{
				return RootCanvas;
			}
		}

		protected override string ShapeName
		{
			get
			{
				return "Rectangle";
			}
		}

		protected override FrameworkElement CreateShape(Random rand)
		{
			Rectangle rect = new Rectangle();
			rect.Fill = _greenBrush;
			rect.Width = rand.Next(DIMENSION) + 5;
			rect.Height = rand.Next(DIMENSION) + 5;
			rect.Move(rand.Next((int)(LayoutRoot.ActualWidth - rect.Width)),
						rand.Next((int)(LayoutRoot.ActualHeight - rect.Height)));
			rect.MouseLeftButtonDown += rectangle_MouseLeftButtonDown;
			return rect;
		}

		#endregion

		#region Event Handlers

		private void rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (_rectangle != null)
			{
				// animation is in progress
				return;
			}

			_rectangle = sender as Rectangle;

			if (_rectangle.Fill != _yellowBrush)
			{
				ColorTransform.SetValue(Storyboard.TargetNameProperty, _rectangle.Name);
				PositionTransform.SetValue(Storyboard.TargetNameProperty, _rectangle.Name);
				PositionTransform.From = (double)_rectangle.GetValue(Canvas.TopProperty);
				PositionTransform.To = RootCanvas.ActualHeight - _rectangle.Height;
				RectangleActions.Begin();
			}
		}

		private void RectangleActions_Completed(object sender, EventArgs e)
		{
			double top = (double) _rectangle.GetValue(Canvas.TopProperty);
			RectangleActions.Stop();
			_rectangle.SetValue(Canvas.TopProperty, top);
			_rectangle.Fill = _yellowBrush;
			_rectangle = null;
		}
 
		#endregion
	}
}
