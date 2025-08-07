using ToyRobotSimulator.Model;

namespace ToyRobotSimulator.Interfaces
{
	public interface IRobotPosition
	{
        public RobotState Move(RobotState robotState);
        public RobotState TurnLeft(RobotState robotState);
        public RobotState TurnRight(RobotState robotState);
        public string Report(RobotState robotState);

    }
}

