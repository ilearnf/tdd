using System.Drawing;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualization
{
	[TestFixture]
	public class CircularCloudLayouter_Should
	{
		private CircularCloudLayouter cloudLayouter;
		
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
		public void PutRectanglesInUpLeftDownRightOrder()
		{
			var center = new Point(10, 10);
			var size = new Size(4, 4);
			cloudLayouter = new CircularCloudLayouter(center);
			var centerRectangle = cloudLayouter.PutNextRectangle(size);
			var firstRectangle = cloudLayouter.PutNextRectangle(size);
			var secondRectangle = cloudLayouter.PutNextRectangle(size);
			var thirdRectangle = cloudLayouter.PutNextRectangle(size);
			var fourthRectangle = cloudLayouter.PutNextRectangle(size);
			firstRectangle.Y.Should().BeLessOrEqualTo(centerRectangle.Y);
			firstRectangle.X.Should().Be(centerRectangle.X);
			secondRectangle.Y.Should().Be(centerRectangle.Y);
			secondRectangle.X.Should().BeLessOrEqualTo(centerRectangle.X);
			thirdRectangle.Y.Should().BeGreaterOrEqualTo(centerRectangle.Y);
			thirdRectangle.X.Should().Be(centerRectangle.X);
			fourthRectangle.Y.Should().Be(centerRectangle.Y);
			fourthRectangle.X.Should().BeGreaterOrEqualTo(centerRectangle.X);
		}

		//[Test]
		//public void PlaceRectanglesOnOneSide_WhenSmallerThatThisSide()
		//{
		//	var center = new Point(10, 10);
		//	var firstSize = new Size(6, 6);
		//	var secondSize = new Size(2, 2);
		//	var thirdSize = new Size(4, 2);
		//	cloudLayouter = new CircularCloudLayouter(center);
		//	var firstRectangle = cloudLayouter.PutNextRectangle(firstSize);
		//	var secondRectangle = cloudLayouter.PutNextRectangle(secondSize);
		//	var thirdRectangle = cloudLayouter.PutNextRectangle(thirdSize);
		//	secondRectangle.Y.Should().Be(0);
		//	firstRectangle.Height.Should().Be(0);
		//}
	}
}