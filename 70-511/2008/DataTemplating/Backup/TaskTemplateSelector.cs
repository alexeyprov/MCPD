using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace DataTemplating
{
	public class TaskTemplateSelector : DataTemplateSelector
	{
		private const string NORMAL_TEMPLATE_KEY = "GridTaskTemplate";
		private const string IMPORTANT_TEMPLATE_KEY = "ImportantTaskTemplate";		

		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			Task t = item as Task;
			Window parentWnd = Application.Current.MainWindow; //container as Window;
			if (null == t || null == parentWnd)
			{
				return base.SelectTemplate(item, container);
			}
			return (DataTemplate)parentWnd.Resources[(Task.HIGH_PRIORITY == t.Priority) ?
				IMPORTANT_TEMPLATE_KEY :
				NORMAL_TEMPLATE_KEY];
		}
	}
}
