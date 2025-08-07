using ToyRobotSimulator;
using ToyRobotSimulator.Model;

namespace ToyRobotsimultorTest;

public class RobotPositionsTests
{
    private readonly RobotPositions robot;

    public RobotPositionsTests()
    {
        robot = new RobotPositions();
    }

    public static IEnumerable<object[]> GetMoveTestCases()
    {
        yield return new object[]
        {
            new RobotState { x_Coordinate = 0, y_Coordinate = 0, facing = Direction.NORTH },
            new RobotState { x_Coordinate = 0, y_Coordinate = 1, facing = Direction.NORTH }
        };

        yield return new object[]
        {
            new RobotState { x_Coordinate = 1, y_Coordinate = 1, facing = Direction.EAST },
            new RobotState { x_Coordinate = 2, y_Coordinate = 1, facing = Direction.EAST }
        };

        yield return new object[]
        {
            new RobotState { x_Coordinate = 3, y_Coordinate = 3, facing = Direction.WEST },
            new RobotState { x_Coordinate = 2, y_Coordinate = 3, facing = Direction.WEST }
        };

        yield return new object[]
        {
            new RobotState { x_Coordinate = 2, y_Coordinate = 4, facing = Direction.SOUTH },
            new RobotState { x_Coordinate = 2, y_Coordinate = 3, facing = Direction.SOUTH }
        };
    }

    [Theory]
    [MemberData(nameof(GetMoveTestCases))]
    public void Move_Updates_Coordinates(RobotState originalState, RobotState expectedState)
    {

        var result = robot.Move(originalState);

        Assert.Equal(result.x_Coordinate, expectedState.x_Coordinate);
        Assert.Equal(result.y_Coordinate, expectedState.y_Coordinate);
        Assert.Equal(result.facing, expectedState.facing);
    }


    [Theory]
    [InlineData(Direction.NORTH, Direction.WEST)]
    [InlineData(Direction.SOUTH, Direction.EAST)]
    [InlineData(Direction.EAST, Direction.NORTH)]
    [InlineData(Direction.WEST, Direction.SOUTH)]
    public void TurnLeft_Updates_Direction(Direction facing, Direction expectedFacing)
    {

        var state = new RobotState
        {
            x_Coordinate = 0,
            y_Coordinate = 0,
            facing = facing
        };

        var result = robot.TurnLeft(state);

      
        Assert.Equal(expectedFacing, result.facing);

    }

    [Theory]
    [InlineData(Direction.NORTH, Direction.EAST)]
    [InlineData(Direction.SOUTH, Direction.WEST)]
    [InlineData(Direction.EAST, Direction.SOUTH)]
    [InlineData(Direction.WEST, Direction.NORTH)]
    public void TurnRight_Updates_Direction(Direction facing, Direction expectedFacing)
    {

        var state = new RobotState
        {
            x_Coordinate = 0,
            y_Coordinate = 0,
            facing = facing
        };

        var result = robot.TurnRight(state);


        Assert.Equal(expectedFacing, result.facing);

    }

    [Fact]
    public void Test_Report_GeneratesStringWithX_Y_Facing()
    {
        var inputRobotState = new RobotState
        {
            x_Coordinate = 1,
            y_Coordinate = 0,
            facing = Direction.EAST
        };

        var result = robot.Report(inputRobotState);

        Assert.Equal("1,0,EAST", result);
    }

}
