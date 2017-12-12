using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
	public class CircularCloudLayouter
	{
		private List<Rectangle> rectangles;
		private Point center;

		public CircularCloudLayouter(Point center)
		{
			rectangles = new List<Rectangle>();
			this.center = center;
		}

		public Rectangle PutNextRectangle(Size size)
		{
			Rectangle placedRectangle;
			if(rectangles.Count == 0)
			    placedRectangle = new Rectangle(center.X - size.Width / 2, center.Y - size.Height / 2, 
					size.Width, size.Height);
			else
			{
				var x = rectangles.Max(r => r.Right);
				var y = rectangles.Max(r => r.Bottom);
				placedRectangle = new Rectangle(x, y, size.Width, size.Height);
			}
			rectangles.Add(placedRectangle);
			return placedRectangle;
		}
	}
}