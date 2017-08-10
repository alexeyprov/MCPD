using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

using ReflectionUtilities;

namespace NorthwindMVC.Helpers
{
	public static class ControlHelper
	{
		private const int PAGER_CELL_COUNT = 10;
		private const string CELL_FORMAT = "&nbsp;{0}";
		private const string ELLIPSIS = "...";

		public static MvcHtmlString Pager(this HtmlHelper htmlHelper, string actionName, int currentPage, int totalPages)
		{
			Contract.Requires(currentPage >= 0);
			Contract.Requires(totalPages >= 0);
			Contract.Requires(currentPage < totalPages || 0 == totalPages);
			Contract.Requires(!String.IsNullOrEmpty(actionName));

			Contract.Ensures(Contract.Result<MvcHtmlString>() != null);

			StringBuilder builder = new StringBuilder();

			const int MEDIAN = PAGER_CELL_COUNT / 2;
			const int LEFT_PART_END = PAGER_CELL_COUNT / 3;
			const int RIGHT_PART_START = PAGER_CELL_COUNT - LEFT_PART_END; 

			bool isCloseToBeginning = (currentPage < MEDIAN),
				 isCloseToEnd = (currentPage >= totalPages - MEDIAN),
				 isEllipsisNeeded = (totalPages > PAGER_CELL_COUNT);

			Func<int, bool> ellipsisPredicate = i => false;

			int alphaThreshold = Int32.MaxValue, //start using current page indexes from this cell
			    betaThreshold = Int32.MaxValue; //start using end page indexes from this cell

			if (isEllipsisNeeded)
			{
				if (isCloseToBeginning)
				{
					betaThreshold = RIGHT_PART_START;
					ellipsisPredicate = i => RIGHT_PART_START == i;
				}
				else if (isCloseToEnd)
				{
					betaThreshold = LEFT_PART_END;
					ellipsisPredicate = i => LEFT_PART_END == i;
				}
				else
				{
					alphaThreshold = LEFT_PART_END;
					betaThreshold = RIGHT_PART_START;
					ellipsisPredicate = i => LEFT_PART_END == i || RIGHT_PART_START == i;
				}
			}

			for (int i = 0; i < Math.Min(totalPages, PAGER_CELL_COUNT); ++i)
			{
				int pageNumber = i;

				if (i >= betaThreshold)
				{
					pageNumber += totalPages - PAGER_CELL_COUNT;
				}
				else if (i >= alphaThreshold)
				{
					pageNumber += currentPage - MEDIAN + 1;
				}

				if (pageNumber++ == currentPage)
				{
					builder.AppendFormat(CELL_FORMAT, pageNumber);
				}
				else
				{
					if (ellipsisPredicate(i))
					{
						builder.AppendFormat(CELL_FORMAT, ELLIPSIS);
					}

					builder.AppendFormat(
						CELL_FORMAT,
						htmlHelper.ActionLink(
							pageNumber.ToString(), 
							actionName,
							new
							{
								pageIndex = pageNumber - 1
							}));
				}
			}

			return MvcHtmlString.Create(builder.ToString());
		}

		public static MvcHtmlString Grid<T>(this HtmlHelper htmlHelper, IEnumerable<T> data)
			where T : class
		{
			Contract.Requires(data != null);
			Contract.Requires(Contract.ForAll(data, d => d != null));

			Contract.Ensures(Contract.Result<MvcHtmlString>() != null);

			StringBuilder builder = new StringBuilder("<table><thead>");

			IEnumerable<PropertyMetadata> columns = PropertyMetadata.ParseDataType(typeof(T));

// ReSharper disable PossibleMultipleEnumeration
			foreach (PropertyMetadata column in columns)
// ReSharper restore PossibleMultipleEnumeration
			{
				builder.AppendFormat("<th>{0}</th>", htmlHelper.Encode(column.Caption));
			}

			builder.Append("</thead>");

			foreach (T d in data)
			{
				builder.Append("<tr>");

// ReSharper disable PossibleMultipleEnumeration
				foreach (PropertyMetadata column in columns)
// ReSharper restore PossibleMultipleEnumeration
				{
					builder.AppendFormat("<td>{0}</td>", htmlHelper.Encode(column.GetValue(d)));
				}

				builder.Append("</tr>");
			}

			return MvcHtmlString.Create(builder.ToString());
		}
	}
}