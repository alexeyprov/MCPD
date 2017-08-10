using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ConstantsHelper
/// </summary>
public static class ConstantsHelper
{
	public const string NORTHWIND_CONNECTION_STRING = "Northwind";
	public const string NORTHWIND_ASYNC_CONNECTION_STRING = "NorthwindAsync";
	public const string NORTHWIND_ENTITIES_CONNECTION_STRING = "NorthwindObjectContext";

	public const string APPLICATION_MESSAGE_QUEUE = @".\Private$\AspNetBasicsQueue";
	public const string EMPLPOYEE_IMAGE_CACHE_KEY_PREFIX = "EMPLOYEE_PICTURE_";
	public const string SELECTED_MASTER_KEY = "AB_SELECTED_MASTER";
	public const string SELECTED_LANGUAGE_KEY = "AB_SELECTED_LANGUAGE";

	public const string CUSTOMER_ID_PARAMETER = "CustomerID";
	public const string ORDER_ID_PARAMETER = "OrderID";
}
