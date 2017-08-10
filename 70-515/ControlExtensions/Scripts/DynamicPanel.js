var DP_ID_SEPARATOR = "$";

function DynamicPanelCallback(result, context)
{
	if (result != null)
	{
		// split the server response
		var separatorIdx = result.indexOf(DP_ID_SEPARATOR);

		if (separatorIdx > 0)
		{
			// get panel element by ID
			var element = document.getElementById(result.substr(0, separatorIdx));

			if (element != null)
			{
				// set inner HTML of the panel element
				element.innerHTML = result.substr(separatorIdx + DP_ID_SEPARATOR.length);
			}
		}
	}
}