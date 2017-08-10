using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;


namespace DataTemplating
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private ObservableCollection<Task> _tasks;

		public ObservableCollection<Task> Tasks
		{
			get
			{
				if (null == _tasks)
				{
					XmlSerializer ser = new XmlSerializer(typeof(ObservableCollection<Task>));
					_tasks = (ObservableCollection<Task>)
						ser.Deserialize(Assembly.GetExecutingAssembly().GetManifestResourceStream("DataTemplating.Tasks.xml"));
				}
				return _tasks;
			}
		}
	}
}
