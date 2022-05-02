using CleaningRobot;
using static CleaningRobot.Utilities.InputFormatter;

Console.WriteLine("Hello and welcome to your cleaning console");
Console.WriteLine("You will be asked some information in order for our robotic staff to do their job.");
Console.WriteLine("The first set of information needed is: How many instructions will be given?");
Console.WriteLine("Please input a number between 0 and 10,000");
Console.WriteLine("Example: \"10\"");
var numInstructions = ParseNumberOfInstructions(Console.ReadLine());
Console.WriteLine("The second set of data that will be needed is the starting point for the cleaner.");
Console.WriteLine("Please input two integers (x, y) separated by a single white space.");
Console.WriteLine("Example: \"10 22\"");
var startCoords = ParseStartingCoordinates(Console.ReadLine());

var instructions = new Instruction[numInstructions];

for (int i = 0; i < numInstructions; i++)
{
    Console.WriteLine("Please type an instruction.");
    Console.WriteLine("The instruction format consists of an upper case character [N, S, E, W] and a number between 0 and 10,000.");
    Console.WriteLine("Example: \"E 35\" ");
    instructions[i] = ParseInstruction(Console.ReadLine());
}
Console.WriteLine($"The cleaning will start at: [{startCoords[0]}, {startCoords[1]}] and will follow {numInstructions} instructions.");
Console.WriteLine("Press any key to start.");
Console.ReadKey();

var robot = new Robot(startCoords, instructions);
var result = robot.Run();
Console.WriteLine($"=> Cleaned: {result.VisitedPoints.Length} ");
Console.ReadKey();


