using NSubstitute;
using ToyRobotSimulator;
using ToyRobotSimulator.Interfaces;
using ToyRobotSimulator.Model;

namespace ToyRobotsimultorTest
{
	public class SimulatorTests
	{
		private readonly IRobotPosition robotPositionsMock;
        private readonly TableDimensions table = new TableDimensions(5, 5);
        private readonly Simulator simulator;

		public SimulatorTests()
		{
			robotPositionsMock = Substitute.For<IRobotPosition>();
			simulator = new Simulator(robotPositionsMock);
		}

        //Test data
        public static IEnumerable<object[]> GetValidPlaceTestCases()
        {
            yield return new object[]
            {
            0,1,Direction.NORTH,new RobotState { x_Coordinate = 0, y_Coordinate = 1, facing = Direction.NORTH }
            };

            yield return new object[]
            {
            2,1,Direction.EAST,
            new RobotState { x_Coordinate = 2, y_Coordinate = 1, facing = Direction.EAST }
            };

            yield return new object[]
            {
            2,3,Direction.WEST,
            new RobotState { x_Coordinate = 2, y_Coordinate = 3, facing = Direction.WEST }
            };

            yield return new object[]
            {
            2,3,Direction.SOUTH,
            new RobotState { x_Coordinate = 2, y_Coordinate = 3, facing = Direction.SOUTH }
            };
        }


        public static IEnumerable<object[]> GetInvalidPlaceTestCases()
        {
            yield return new object[]
            {
            5,5,Direction.NORTH, null!
            };

            yield return new object[]
            {
            5,6,Direction.SOUTH, null!
            };
        }

        //Test Methods

        [Fact]
        public void ProcessCommand_ValidPlaceCommand_SetsRobotState()
        {
            simulator.ProcessCommand("PLACE 1,2,EAST");

            Assert.NotNull(simulator.robotState);
            Assert.Equal(1, simulator.robotState.x_Coordinate);
            Assert.Equal(2, simulator.robotState.y_Coordinate);
            Assert.Equal(Direction.EAST, simulator.robotState.facing);
        }

        [Fact]
        public void ProcessCommand_InvalidPlaceCommand_AddsMessage()
        {
            simulator.ProcessCommand("PLACE 1,2,NORTHEAST");

            Assert.Null(simulator.robotState);
            Assert.Contains(ValidationMessages.InvalidPlaceCommand, simulator.Message);
        }

        [Fact]
        public void ProcessCommand_Right_CallsTurnRight()
        {
            // Arrange
            var initialState = new RobotState { x_Coordinate = 1, y_Coordinate = 1, facing = Direction.NORTH };
            var finalState = new RobotState { x_Coordinate = 1, y_Coordinate = 1, facing = Direction.EAST };

            simulator.Place(1, 1, Direction.NORTH);
            robotPositionsMock.TurnRight(Arg.Any<RobotState>()).Returns(finalState);
            //simulator.ProcessCommand("PLACE 1,1,NORTH");
            // Act
            simulator.ProcessCommand("RIGHT");

            // Assert
            robotPositionsMock.Received(1).TurnRight(Arg.Any<RobotState>());
            Assert.Equal(Direction.EAST, simulator.robotState.facing);
        }

        [Fact]
		public void Test_Report_GeneratesStringWithX_Y_Facing()
		{
			var robotState = new RobotState
			{
				x_Coordinate = 1,
				y_Coordinate = 0,
				facing = Direction.EAST
			};
			robotPositionsMock.Report(Arg.Any<RobotState>()).Returns("1,0,EAST");

            var result = simulator.Report();

			Assert.Equal("1,0,EAST", result);
		}
	}
}

