using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HelloSilverlight.Code
{
	/// <summary>
	/// Base class for shape pages
	/// </summary>
	/// <remarks>
	/// This class should have been made abstract, but doing it screws up VS designer
	/// </remarks>
	public class BaseShapePage : Page
	{
		protected Brush _yellowBrush;
		protected Brush _greenBrush;

		protected BaseShapePage()
		{
			_yellowBrush = new SolidColorBrush(Colors.Yellow);
			_greenBrush = new SolidColorBrush(Colors.Green);
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);

			ShapeCountDialog dialog = new ShapeCountDialog(ShapeName);
			dialog.Closed += delegate
			{
				if (dialog.DialogResult ?? false)
				{
					Random rand = new Random();
					for (int i = 0; i < dialog.Count; ++i)
					{
						FrameworkElement shape = CreateShape(rand);
						shape.Name = ShapeName + i;
						Root.Children.Add(shape);
					}
				}
			};

			dialog.Show();
		}

		protected virtual Panel Root
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		protected virtual string ShapeName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		protected virtual FrameworkElement CreateShape(Random rand)
		{
			throw new NotImplementedException();
		}
	}
}
