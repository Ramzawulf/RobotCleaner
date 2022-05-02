namespace CleaningRobot;
public class Instruction
{
    public enum Directions
    {
        North,
        South,
        East,
        West,
        Empty
    }

    public readonly Directions Direction;
    public readonly int Stride;
    
    public Instruction(string direction, int stride)
    {
        Direction = ParseDirection(direction);
        Stride = stride;
    }

    private static Directions ParseDirection(string rawDirection)
    {
        if (string.Equals(rawDirection, "N"))
            return Directions.North;
        if (string.Equals(rawDirection, "S"))
            return Directions.South;
        if (string.Equals(rawDirection, "E"))
            return Directions.East;
        if (string.Equals(rawDirection, "W"))
            return Directions.West;
        return Directions.Empty;
    }

    /// <summary>
    /// Returns an object with a configuration considered "Empty"
    /// </summary>
    /// <returns></returns>
    public static Instruction GetEmpty()
    {
        return new Instruction("", 0);
    }

    /// <summary>
    /// Returns True if the current configuration is that of an "Empty" instruction.
    /// </summary>
    /// <returns></returns>
    public bool IsEmpty()
    {
        return Direction == Directions.Empty && Stride == 0;
    }
}