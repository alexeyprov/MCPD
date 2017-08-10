function SwapImage(id, src)
{
	var elem = document.getElementById(id);
	if (elem != null && typeof(elem) != "undefined")
	{
		elem.src = src;
	}
}