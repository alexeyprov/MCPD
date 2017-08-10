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

namespace HelloSilverlight
{
	public partial class ShapeCountDialog : ChildWindow
	{
		private byte _count;

		public ShapeCountDialog(string shapeName)
		{
			InitializeComponent();
			this.Title = String.Format((string)App.Current.Resources["ShapeInitializationCaptionFormat"], shapeName);
			CountLabel.Text = String.Format((string)App.Current.Resources["ShapeInitializationLabelFormat"], shapeName);
		}

		public byte Count
		{
			get
			{
				return _count;
			}
		}

		private void OKButton_Click(object sender, RoutedEventArgs e)
		{
			if (!Byte.TryParse(CountTextBox.Text, out _count) || 0 == _count)
			{
				MessageBox.Show((string)App.Current.Resources["InvalidInputWarning"]);
				return;
			}
			
			this.DialogResult = true;
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			_count = 0;
			this.DialogResult = false;
		}
	}
}

