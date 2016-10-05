using UnityEngine;
using System.Collections;

public struct Size
{
	public int Area
	{
		get { return width * height; }
	}

	public int Perimeter
	{
		get { return 2 * (width + height); }
	}

	public int width;
	public int height;

	public Size(int width, int height)
	{
		this.width = width;
		this.height = height;
	}

	public static Size operator +(Size a, Size b)
	{
		return new Size(a.width + b.width, a.height + b.height);
	}

	public static Size operator -(Size a, Size b)
	{
		return new Size(a.width - b.width, a.height - b.height);
	}

	public static Size operator *(Size a, int b)
	{
		return new Size(a.width * b, a.height * b);
	}
	
	public static Size operator /(Size a, int b)
	{
		return new Size(a.width / b, a.height / b);
	}
	
	public static bool operator ==(Size a, Size b)
	{
		return a.width == b.width && a.height == b.height;
	}

	public static bool operator !=(Size a, Size b)
	{
		return a.width != b.width || a.height != b.height;
	}

	public static bool operator >(Size a, Size b)
	{
		return a.Area > b.Area;
	}

	public static bool operator <(Size a, Size b)
	{
		return a.Area > b.Area;
	}

	public override bool Equals(object obj)
	{
		return base.Equals(obj);
	}

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	public override string ToString()
	{
		return string.Format("{0}x{1}", width, height);
	}
}
