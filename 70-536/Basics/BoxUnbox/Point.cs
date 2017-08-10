/*class*/ struct Point : 
	IChangeBoxedPoint,
	System.IEquatable<Point>
{
	private int? _x;
	private int? _y;

	public override string ToString()
	{
		if (this.HasValue)
		{
			return System.String.Format("({0}, {1})", _x, _y);
		}
		return "undefined";
	}

	public void ChangeTo(int x, int y)
	{
		_x = x;
		_y = y;
	}

	public bool Equals(Point p)
	{
		if (this.HasValue && p.HasValue)
		{
			return (this._x == p._x) && (this._y == p._y);
		}
		return false;
	}

	private bool HasValue
	{
		get
		{
			return _x.HasValue && _y.HasValue;
		}
	}
}