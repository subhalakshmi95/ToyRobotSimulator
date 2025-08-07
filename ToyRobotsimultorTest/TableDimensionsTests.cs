using System;
using ToyRobotSimulator;

namespace ToyRobotsimultorTest
{
	public class TableDimensionsTests
	{
		public readonly TableDimensions tableDimensions;
		public TableDimensionsTests()
		{
			tableDimensions = new TableDimensions(5,5);
		}


		[Theory]
		[InlineData(0,1,true)]
        [InlineData(0,0, true)]
        [InlineData(3,2, true)]
        [InlineData(4,4, true)]
        [InlineData(2,3, true)]
        public void valid_table_location(int x, int y, bool expectedValue)
		{
			bool result = tableDimensions.IsValidLocation(x, y);

			Assert.Equal(result, expectedValue);

		}

        [Theory]
        [InlineData(5,4, false)]
        [InlineData(0,5, false)]
        [InlineData(5,5, false)]
        [InlineData(8,6, false)]
        [InlineData(5,3, false)]
        public void inValid_table_location(int x, int y, bool expectedValue)
        {
            bool result = tableDimensions.IsValidLocation(x, y);

            Assert.Equal(result, expectedValue);

        }
    }
}

