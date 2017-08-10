using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddressControl : UserControl
{
	#region Properties

	public string AddressLineOne
	{
		get
		{
			return txtAddressOne.Text;
		}
		set
		{
			txtAddressOne.Text = value;
		}
	}

	public string AddressLineTwo
	{
		get
		{
			return txtAddressTwo.Text;
		}
		set
		{
			txtAddressTwo.Text = value;
		}
	}

	public string City
	{
		get
		{
			return txtCity.Text;
		}
		set
		{
			txtCity.Text = value;
		}
	}

	public string State
	{
		get
		{
			return cmbState.SelectedValue;
		}
		set
		{
			cmbState.SelectedValue = value;
		}
	}

	public string ZipCode
	{
		get
		{
			return txtZipCode.Text;
		}
		set
		{
			txtZipCode.Text = value;
		}
	} 

	#endregion

	#region Events

	public event EventHandler CheckZipCode;

	#endregion

	#region Event Handlers

	protected void btnGetCityAndState_Click(object sender, EventArgs e)
	{
		if (CheckZipCode != null)
		{
			CheckZipCode(this, EventArgs.Empty);
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{

	}

	#endregion

}
