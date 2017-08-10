using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ComparisonOperatorExtensions
/// </summary>
public static class ComparisonOperatorExtensions
{
	public static string AsString(this ComparisonOperator op)
	{
		switch (op)
		{
			case ComparisonOperator.LessThan:
				return "<";
			case ComparisonOperator.Equals:
				return "=";
			case ComparisonOperator.MoreThan:
				return ">";
			default:
				break;
		}

		return null;
	}

	public static bool Compare<T>(this ComparisonOperator op, T left, T right) where T : IComparable<T>
	{
		int comparisonResult = -1; // less than
		if (left != null)
		{
			comparisonResult = left.CompareTo(right);
		}

		switch (op)
		{
			case ComparisonOperator.LessThan:
				return comparisonResult < 0;
			case ComparisonOperator.Equals:
				return 0 == comparisonResult;
			case ComparisonOperator.MoreThan:
				return comparisonResult > 0;
			default:
				break;
		}

		return false;
	}

}
