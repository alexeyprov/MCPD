using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DataTemplating
{
	public enum TaskCategory
	{
		Home,
		Work
	}

	public class Task : INotifyPropertyChanged
	{
		public const int HIGH_PRIORITY = 1;

		#region .ctor
		public Task(string name, string descr, int priority, TaskCategory category)
		{
			_name = name;
			_descr = descr;
			_priority = priority;
			_category = category;
		}
		public Task()
		{
			_name = "(empty)";
			_descr = "(empty)";
			_priority = 5;
			_category = TaskCategory.Home;
		}
		#endregion

		#region Properties
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
				OnPropertyChanged("Name");
			}
		}
		public string Description
		{
			get
			{
				return _descr;
			}
			set
			{
				_descr = value;
				OnPropertyChanged("Description");
			}
		}
		public int Priority
		{
			get
			{
				return _priority;
			}
			set
			{
				_priority = value;
				OnPropertyChanged("Priority");
			}
		}
		public TaskCategory Category
		{
			get
			{
				return _category;
			}
			set
			{
				_category = value;
				OnPropertyChanged("Category");
			}
		}
		#endregion

		protected void OnPropertyChanged(string propName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propName));
			}
		}

		public override string ToString()
		{
			return _name;
		}

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		#region Data Members
		string _name;
		string _descr;
		int _priority;
		TaskCategory _category;
		#endregion
	}
}
