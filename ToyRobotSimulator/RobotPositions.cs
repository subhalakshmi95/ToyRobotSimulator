using ToyRobotSimulator.Interfaces;
using ToyRobotSimulator.Model;

namespace ToyRobotSimulator
{
	public class RobotPositions : IRobotPosition
    {
        public RobotPositions()
		{
		}

        public RobotState Move(RobotState robotState)
        {
            RobotState newState = new RobotState
            {
                x_Coordinate = robotState.x_Coordinate,
                y_Coordinate = robotState.y_Coordinate,
                facing = robotState.facing
            };
            

            switch (robotState.facing)
            {
                case Direction.EAST:
                    newState.x_Coordinate += 1;
                    break;
                case Direction.WEST:
                    newState.x_Coordinate -= 1;
                    break;
                case Direction.NORTH:
                    newState.y_Coordinate += 1;
                    break;
                case Direction.SOUTH:
                    newState.y_Coordinate -= 1;
                    break;
            }

            return newState;
        }

        public RobotState TurnLeft(RobotState robotState)
        {
            return new RobotState
            {
                x_Coordinate = robotState.x_Coordinate,
                y_Coordinate = robotState.y_Coordinate,
                facing = (Direction)(((int)robotState.facing + 3) % 4)
            };
        }

        public RobotState TurnRight(RobotState robotState)
        {
            return new RobotState
            {
                x_Coordinate = robotState.x_Coordinate,
                y_Coordinate = robotState.y_Coordinate,
                facing = (Direction)(((int)robotState.facing + 1) % 4)
            };
        }

        public string Report(RobotState robotState)
        {
            return robotState.x_Coordinate + "," + robotState.y_Coordinate + "," + robotState.facing.ToString().ToUpper();
        }
    }
}

