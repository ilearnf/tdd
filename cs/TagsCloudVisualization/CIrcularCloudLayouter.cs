using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using FluentAssertions;

namespace TagsCloudVisualization
{
	public class CircularCloudLayouter
	{
		private List<Rectangle> rectangles;
		private Point center;
		private double radius;
		private double phi;
		private const double radiusIncrement = .01;
		private const double phiIncrement = Math.PI / 120;

		public CircularCloudLayouter(Point center)
		{
			rectangles = new List<Rectangle>();
			this.center = center;
		}

		public Rectangle PutNextRectangle(Size size)
		{
			do
			{
				phi += phiIncrement;
				radius += radiusIncrement;
			} while(rectangles.Any(r => !Rectangle.Intersect(r, new Rectangle(getNextRectanglePoint(), size)).IsEmpty));
			
			Rectangle placedRectangle;
			if(rectangles.Count == 0)
			    placedRectangle = new Rectangle(center.X - size.Width / 2, center.Y - size.Height / 2, 
					size.Width, size.Height);
			else
			{
				var nextPoint = getNextRectanglePoint();
				placedRectangle = new Rectangle(nextPoint.X, nextPoint.Y, size.Width, size.Height);
			}
			rectangles.Add(placedRectangle);
			return placedRectangle;
		}

		private Point getNextRectanglePoint()
		{
			int x = (int) (center.X + Math.Ceiling(radius * Math.Cos(phi)));
			int y = (int)(center.Y + Math.Ceiling(radius * Math.Sin(phi)));
			return new Point(x, y);
		}

		public void GetVisualization()
		{
			var bm = new Bitmap(1000, 1000);
			var graphics = Graphics.FromImage(bm);
			graphics.DrawRectangles(new Pen(Color.Red), rectangles.ToArray());
			graphics.Dispose();
			bm.Save("C:\\result.bmp", ImageFormat.Bmp);
		}
	}
}