using ToyRobotSimulator.Interfaces;

namespace ToyRobotSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string filePath = string.Empty;

            IRobotPosition robotPositions = new RobotPositions();

            //check if args is provided in command line argument.

            if (args == null || args.Length == 0)
            {
                Console.WriteLine(ValidationMessages.InvalidFileFormat);
                return;
            }
            else
            {
                try
                {
                    filePath = args[0];

                    string[] commands = File.ReadAllLines(filePath);
                    if (commands.Length == 0)
                    {
                        Console.WriteLine(ValidationMessages.InvalidFileMessage);
                        return;
                    }
                    Simulator simulator = new Simulator(robotPositions);
                    var Messages = simulator.Invoke(commands);
                    foreach(var message in Messages)
                    {
                        Console.WriteLine(message);
                    }
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine($"Error: The file '{filePath}' was not found.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                }
        }

    }
    }
}