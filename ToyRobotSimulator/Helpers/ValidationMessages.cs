using System;
namespace ToyRobotSimulator
{
	public static class ValidationMessages
	{
        public const string InvalidFileMessage = "Specified file is empty.";
        public const string InvalidFileFormat = "Please specify a .txt filepath.";

        public const string InvalidPlaceCommand = "Invalid Place Command.";
        public const string InvalidSequenceMessage = "Robot has not been placed yet. Please place the robot first.";
        public const string InvalidCommand = "Invalid Command.";
        public const string IgnoreCommand = "Move command ignored as it will make the robot fall.";

    }
}

