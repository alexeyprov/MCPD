using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LayoutSystem
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		Path _boundingRect;

		public Window1()
		{
			InitializeComponent();
		}

		private void btnShowBounds_Click(object sender, RoutedEventArgs e)
		{
			if (null == _boundingRect)
			{
				_boundingRect = new Path();
				_boundingRect.Stroke = Brushes.LightGoldenrodYellow;
				_boundingRect.StrokeThickness = 5;
				Grid.SetColumn(_boundingRect, 0);
				Grid.SetRow(_boundingRect, 0);
				grdContents.Children.Add(_boundingRect);
			}

			RectangleGeometry logicalRect = new RectangleGeometry(LayoutInformation.GetLayoutSlot(txtHello));
			_boundingRect.Data = logicalRect;
			txtOutput.Text = "Layout slot is equal to " + logicalRect.Rect.ToString();
		}
	}
}
