using System.Numerics;

namespace CleaningRobot.DTO;

public struct MovePayload
{
    public Vector2[] Vertices;
    public Vector2 FinalPosition;
}