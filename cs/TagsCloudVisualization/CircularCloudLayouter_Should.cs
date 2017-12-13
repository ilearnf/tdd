using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualization
{
	[TestFixture]
	public class CircularCloudLayouter_Should
	{
		private CircularCloudLayouter cloudLayouter;

		[TearDown]
		public void TearDown()
		{
			cloudLayouter.GetVisualization();
			TestContext.Error.WriteLine("Visualization saved to result file.");
		}
		
		[Test]
		public void PlaceFirstRectangleOnCenter()
		{
			var center = new Point(10, 10);
			var size = new Size(4, 4);
			cloudLayouter = new CircularCloudLayouter(center);
			var placedRectangle = cloudLayouter.PutNextRectangle(size);
			placedRectangle.X.Should().Be(8);
			placedRectangle.X.Should().Be(8);
		}

		[Test]
		public void NotIntersectRectangles()
		{
			var center = new Point(10, 10);
			var firstSize = new Size(4, 4);
			var secondSize = new Size(6, 8);
			cloudLayouter = new CircularCloudLayouter(center);
			var firstRectangle = cloudLayouter.PutNextRectangle(firstSize);
			var secondRectangle = cloudLayouter.PutNextRectangle(secondSize);
			firstRectangle.Intersect(secondRectangle);
			firstRectangle.Width.Should().Be(0);
			firstRectangle.Height.Should().Be(0);
		}

		[Test]
		public void PlaceRectanglesWithinCircle()
		{
			var center = new Point(200, 200);
			var size = new Size(10, 10);
			cloudLayouter = new CircularCloudLayouter(center);
			var centerRectangle = cloudLayouter.PutNextRectangle(size);
			var rectangles = new List<Rectangle>();
			for(int i = 0; i < 490; i += 1)
				rectangles.Add(cloudLayouter.PutNextRectangle(size));
			cloudLayouter.GetVisualization();
			rectangles.All(r => Math.Pow(r.X - center.X, 2) + Math.Pow(r.Y - center.Y, 2) < 490000).Should().BeTrue();


		}

		[Test]
		public void PlaceRectanglesNotFarFromEachOtherThan10Px()
		{
			var center = new Point(200, 200);
			var size = new Size(10, 10);
			cloudLayouter = new CircularCloudLayouter(center);
			var centerRectangle = cloudLayouter.PutNextRectangle(size);
			var rectangles = new List<Rectangle>();
			for (int i = 0; i < 490; i += 1)
				rectangles.Add(cloudLayouter.PutNextRectangle(size));
			cloudLayouter.GetVisualization();
			var orderedRectangles = rectangles
				.OrderBy(r => r.X)
				.ToArray();
			orderedRectangles
				.Where((r, i) => i < orderedRectangles.Length - 1)
				.Select((r, i) => new {First = r, Second = orderedRectangles[i + 1]})
				.All(rp => Math.Abs(rp.First.X - rp.Second.X) < 10)
				.Should()
				.BeTrue();
			orderedRectangles = rectangles
				.OrderBy(r => r.Y)
				.ToArray();
			orderedRectangles
				.Where((r, i) => i < orderedRectangles.Length - 1)
				.Select((r, i) => new { First = r, Second = orderedRectangles[i + 1] })
				.All(rp => Math.Abs(rp.First.Y - rp.Second.Y) < 10)
				.Should()
				.BeTrue();
		}
	}
}