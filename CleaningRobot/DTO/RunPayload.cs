using System.Numerics;

namespace CleaningRobot.DTO;

public struct RunPayload
{
    public Vector2 FinalPosition;
    public Vector2[] VisitedPoints;

}