using System.Numerics;
using CleaningRobot.DTO;

namespace CleaningRobot
{
    public class Robot
    {
        public readonly int GridWidth;
        public readonly int GridHeight;

        public readonly int[] Origin;
        public readonly Instruction[] Instructions;

        public Robot(int[] origin, Instruction[] instructions, int gridHSize = 100, int gridVSize = 100)
        {
            GridHeight = gridVSize;
            GridWidth = gridHSize;
            Origin = InitOrigin(origin);
            Instructions = instructions;
        }

        /// <summary>
        /// Executes all the instructions on the current setup of the Robot, returning the outcome.
        /// </summary>
        /// <returns></returns>
        public RunPayload Run()
        {
            var currentPos = new Vector2(Origin[0], Origin[1]);
            var visitedPoints = new HashSet<Vector2> { currentPos };


            foreach (var instruction in Instructions)
            {
                var result = TryMove(currentPos, instruction);
                currentPos = new Vector2(result.FinalPosition.X, result.FinalPosition.Y);
                result.Vertices.ToList().ForEach(v => visitedPoints.Add(v));
            }

            return new RunPayload() { FinalPosition = currentPos, VisitedPoints = visitedPoints.ToArray() };

        }

        /// <summary>
        /// Executes one set of Instructions and returns the outcome.
        /// </summary>
        /// <param name="currentPos"></param>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private MovePayload TryMove(Vector2 currentPos, Instruction instruction)
        {
            var vertices = new HashSet<Vector2> { currentPos };
            var pos = currentPos;
            var newPosition = pos;

            for (int s = 0; s < instruction.Stride; s++)
            {
                switch (instruction.Direction)
                {
                    case Instruction.Directions.North:
                        newPosition = new Vector2(
                            pos.X,
                            Convert.ToInt32(Math.Clamp(pos.Y + 1, 0, GridWidth))
                        );
                        break;
                    case Instruction.Directions.South:
                        newPosition = new Vector2(
                            pos.X,
                            Convert.ToInt32(Math.Clamp(pos.Y - 1, 0, GridWidth))
                        );
                        break;
                    case Instruction.Directions.East:
                        newPosition = new Vector2(
                            Convert.ToInt32(Math.Clamp(pos.X + 1, 0, GridHeight)),
                            pos.Y);
                        break;
                    case Instruction.Directions.West:
                        newPosition = new Vector2(
                            Convert.ToInt32(Math.Clamp(pos.X - 1, 0, GridHeight)),
                            pos.Y);
                        break;
                    case Instruction.Directions.Empty:
                        break;
                }
                vertices.Add(newPosition);
                pos = newPosition;
            }

            return new MovePayload() { FinalPosition = pos, Vertices = vertices.ToArray() };
        }

        /// <summary>
        /// Processes the values intended to generate the initial position of the Robot, it keeps the values within the grid.
        /// </summary>
        /// <param name="inOrigin"></param>
        /// <returns></returns>
        private int[] InitOrigin(IReadOnlyList<int> inOrigin)
        {
            return new[]
                {
                    inOrigin[0] <= GridWidth && inOrigin[0] >= 0 ? inOrigin[0]: 0,
                    inOrigin[1] <= GridHeight && inOrigin[1] >= 0?    inOrigin[1]:0
                };
        }
    }
}
