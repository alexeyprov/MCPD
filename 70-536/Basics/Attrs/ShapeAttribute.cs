using System;

enum ShapeType
{
	Square,
	Circle,
	Triangle,
	Octagon
}

[AttributeUsage(AttributeTargets.All)]
class ShapeAttribute : Attribute
{
	ShapeType _shape = ShapeType.Triangle;
	public int Size;

	
	public ShapeAttribute()
	{
	}

	public ShapeAttribute(ShapeType shape)
	{
		_shape = shape;
	}
}