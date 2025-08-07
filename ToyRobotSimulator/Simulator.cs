using System;
using System.Text.RegularExpressions;
using ToyRobotSimulator.Interfaces;
using ToyRobotSimulator.Model;

namespace ToyRobotSimulator
{
	public class Simulator
	{
        public RobotState robotState = null!;
        private readonly IRobotPosition _robotPositions;

        public TableDimensions Table=new TableDimensions(5,5);
        public List<string> Message = new List<string>();

        public int x_coordinate;
        public int y_coordinate;
        public Direction facing;

        public Commands cmd;

        public Simulator(IRobotPosition robotPositions)
		{
            _robotPositions = robotPositions;
        }

        public List<string> Invoke(string[] commandList)
        {
            foreach (string command in commandList)
            {
                
                ProcessCommand(command.ToUpper());

                if (robotState==null && Regex.IsMatch(command.ToUpper(), Commands.PLACE.ToString()))
                {
                    Message.Add(ValidationMessages.InvalidSequenceMessage);
                }
            }
            
            return Message;
        }
        
        public void ProcessCommand(string command)
        {
            try
            {
                if (Regex.IsMatch(command, Commands.PLACE.ToString()))
                {
                    string[] coordinates = command.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    if (coordinates.Length != 4)
                    {
                        Message.Add(ValidationMessages.InvalidPlaceCommand);
                        return;
                    }

                    bool int_x = Int32.TryParse(coordinates[1], out x_coordinate);
                    bool int_y = Int32.TryParse(coordinates[2], out y_coordinate);
                    bool enum_direction = Enum.TryParse(coordinates[3].Trim(), true, out Direction facing);

                    if (int_x && int_y && enum_direction)
                    {
                        robotState = Place(x_coordinate, y_coordinate, facing);
                    }

                    if (robotState == null)
                    {
                        Message.Add(ValidationMessages.InvalidPlaceCommand);
                    }
                }
                else if (Regex.IsMatch(command, Commands.MOVE.ToString()) || Regex.IsMatch(command, Commands.RIGHT.ToString()) || Regex.IsMatch(command, Commands.LEFT.ToString()))
                {
                    var bool_cmd = Enum.TryParse<Commands>(command.ToString(), out Commands cmd);
                    if (bool_cmd)
                    {
                        RobotMoves(cmd);
                    }

                }
                else if (Regex.IsMatch(command, Commands.REPORT.ToString()))
                {
                    Message.Add(Report());
                }
                else
                {
                    Message.Add(ValidationMessages.InvalidCommand);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        public RobotState Place(int x, int y, Direction facing)
        {
            if (Table.IsValidLocation(x, y))
            {
                robotState = new RobotState
                {
                    facing = facing,
                    x_Coordinate = x,
                    y_Coordinate = y
                };
            }
            return robotState;
        }

        public void RobotMoves(Commands cmd)
        {
            switch (cmd)
            {
                case Commands.MOVE:
                    var newRobotState=_robotPositions.Move(robotState);
                    if (Table.IsValidLocation(newRobotState.x_Coordinate, newRobotState.y_Coordinate))
                    {
                        robotState = newRobotState;
                    }
                    else
                    {
                        Message.Add(ValidationMessages.IgnoreCommand);
                    }
                    break;
                case Commands.RIGHT:
                    robotState = _robotPositions.TurnRight(robotState);
                    break;
                case Commands.LEFT:
                    robotState = _robotPositions.TurnLeft(robotState);
                    break;
            }
        }

        public string Report()
        {
            return _robotPositions.Report(robotState);
        }
    }
}

