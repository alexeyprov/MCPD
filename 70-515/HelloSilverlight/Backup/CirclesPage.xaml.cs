using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;

using HelloSilverlight.Code;

namespace HelloSilverlight
{
	public partial class CirclesPage : BaseShapePage
	{
		#region Private Constants

		private const int CIRCLE_RADIUS = 15;
		
		#endregion

		#region Private Fields

		private Ellipse _movingEllipse;
		private Point _mousePosition;
		
		#endregion

		#region Constructor

		public CirclesPage()
		{
			InitializeComponent();
			_yellowBrush = new SolidColorBrush(Colors.Yellow);
			_greenBrush = new SolidColorBrush(Colors.Green);
		} 

		#endregion

		#region Overrides

		protected override Panel Root
		{
			get
			{
				return LayoutRoot;
			}
		}

		protected override string ShapeName
		{
			get
			{
				return "Circle";
			}
		}

		protected override FrameworkElement CreateShape(Random rand)
		{
			Ellipse circle = new Ellipse();
			circle.Fill = _greenBrush;
			circle.Width = circle.Height = CIRCLE_RADIUS * 2;
			circle.Move(rand.Next((int)(LayoutRoot.ActualWidth - circle.Width)),
						rand.Next((int)(LayoutRoot.ActualHeight - circle.Height)));
			return circle;
		}

		#endregion

		#region Event Handlers

		private void Page_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			// find elliplse
			_movingEllipse = (from control in VisualTreeHelper.FindElementsInHostCoordinates(e.GetPosition(null), LayoutRoot)
							  where control is Ellipse
							  select control as Ellipse).FirstOrDefault();
			if (_movingEllipse != null)
			{
				_movingEllipse.Fill = _yellowBrush;
				_mousePosition = e.GetPosition(LayoutRoot);

				_movingEllipse.CaptureMouse();
			}
		}

		private void Page_MouseMove(object sender, MouseEventArgs e)
		{
			_mousePosition = MoveEllipse(e, true);
		}

		private void Page_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (_movingEllipse != null)
			{
				MoveEllipse(e, false);
				_movingEllipse.Fill = _greenBrush;
				_movingEllipse.ReleaseMouseCapture();

				_mousePosition = default(Point);
				_movingEllipse = null;
			}
		}

		#endregion

		#region Implementation

		private Point MoveEllipse(MouseEventArgs e, bool checkPosition)
		{
			Point newPosition = default(Point);

			if (_movingEllipse != null)
			{
				newPosition = e.GetPosition(LayoutRoot);

				if (newPosition != _mousePosition || !checkPosition)
				{
					Point ellipseTopLeft = _movingEllipse.GetPosition();
					ellipseTopLeft.X += newPosition.X - _mousePosition.X;
					ellipseTopLeft.Y += newPosition.Y - _mousePosition.Y;

					_movingEllipse.Move(ellipseTopLeft);

					_mousePosition = newPosition;
				}
			}

			return newPosition;
		}

		#endregion
	}
}
