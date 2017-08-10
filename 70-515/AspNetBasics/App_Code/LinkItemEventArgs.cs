using System;
using System.ComponentModel;

/// <summary>
/// Summary description for LinkItemEventArgs
/// </summary>
public class LinkItemEventArgs : CancelEventArgs
{
	private LinkTableItem _selectedItem;

	public LinkItemEventArgs(LinkTableItem selectedItem)
	{
		_selectedItem = selectedItem;
	}

	public LinkTableItem SelectedItem
	{
		get
		{
			return _selectedItem;
		}
	}
}