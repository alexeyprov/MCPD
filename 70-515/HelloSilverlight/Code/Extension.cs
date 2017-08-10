using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace HelloSilverlight.Code
{
	public static class Extension
	{
		public static void Move(this UIElement element, double x, double y)
		{
			element.SetValue(Canvas.LeftProperty, x);
			element.SetValue(Canvas.TopProperty, y);
		}

		public static void Move(this UIElement element, Point point)
		{
			element.Move(point.X, point.Y);
		}

		public static Point GetPosition(this UIElement element)
		{
			return new Point((double) element.GetValue(Canvas.LeftProperty),
							 (double) element.GetValue(Canvas.TopProperty));
		}
	}
}
