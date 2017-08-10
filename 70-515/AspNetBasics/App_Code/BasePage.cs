using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

/// <summary>
/// Base page class to override master page and themes
/// </summary>
public class BasePage : Page
{
	private const string MASTER_PAGE_NAME_PREFIX = "~/Master/Default";
	private const string MASTER_PAGE_NAME_SUFFIX = ".master";
	private const string MASTER_PAGE_NAME_FORMAT = "{0}{1}{2}";
	private const string COLOR_PROFILE_PROPERTY = "SchemeColor";

	protected override void OnPreInit(EventArgs e)
	{
		string masterPagePath = Session[ConstantsHelper.SELECTED_MASTER_KEY] as string;
		string theme = null;

		if (String.IsNullOrEmpty(masterPagePath))
		{
			theme = (string) Context.Profile.GetPropertyValue(COLOR_PROFILE_PROPERTY);
			if (String.IsNullOrEmpty(theme))
			{
				base.OnPreInit(e);
				return;
			}

			masterPagePath = String.Format(MASTER_PAGE_NAME_FORMAT, MASTER_PAGE_NAME_PREFIX, theme, MASTER_PAGE_NAME_SUFFIX);
			Session[ConstantsHelper.SELECTED_MASTER_KEY] = masterPagePath;
		}
		else
		{
			int startIndex = masterPagePath.IndexOf(MASTER_PAGE_NAME_PREFIX) + MASTER_PAGE_NAME_PREFIX.Length;
			int endIndex = masterPagePath.IndexOf(MASTER_PAGE_NAME_SUFFIX);

			if (endIndex > startIndex)
			{
				theme = masterPagePath.Substring(startIndex, endIndex - startIndex);
			}
		}

		Theme = theme;
		MasterPageFile = PostProcessMasterPagePath(masterPagePath);

		base.OnPreInit(e);
	}

	protected override void InitializeCulture()
	{
		base.InitializeCulture();

		string culture = Session[ConstantsHelper.SELECTED_LANGUAGE_KEY] as string;

		if (!String.IsNullOrEmpty(culture))
		{
			Page.Culture = culture;
			Page.UICulture = culture;
		}
	}

	protected virtual string PostProcessMasterPagePath(string masterPagePath)
	{
		return masterPagePath;
	}
}