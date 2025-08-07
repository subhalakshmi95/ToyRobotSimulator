namespace ToyRobotSimulator
{
	public class TableDimensions
	{
        public int width;
        public int length;

        public TableDimensions(int width, int length)
        {
            this.width = width;
            this.length = length;
        }

        // Checks if the robot position is within boundaries.
        public bool IsValidLocation(int x_coordinate, int y_coordinate)
        {
            return x_coordinate >= 0 && x_coordinate < width && y_coordinate >= 0 && y_coordinate < length;
        }

    }
}

