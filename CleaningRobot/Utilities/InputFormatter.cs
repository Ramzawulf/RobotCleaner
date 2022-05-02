namespace CleaningRobot.Utilities
{
    public class InputFormatter
    {
        private const char SEPARATOR = ' ';

        /// <summary>
        /// Parses Console Input into a number.
        /// </summary>
        /// <param name="consoleInput"></param>
        /// <returns></returns>
        public static int ParseNumberOfInstructions(string? consoleInput)
        {
            return string.IsNullOrEmpty(consoleInput) ? 0 : Convert.ToInt32(consoleInput);
        }

        /// <summary>
        /// Parses Console Input into a set of coordinates.
        /// </summary>
        /// <param name="consoleInput"></param>
        /// <returns></returns>
        public static int[] ParseStartingCoordinates(string? consoleInput)
        {
            if (string.IsNullOrEmpty(consoleInput)) return Array.Empty<int>();

            return new[]
            {
                Convert.ToInt32(consoleInput.Split(SEPARATOR)[0]), Convert.ToInt32(consoleInput.Split(SEPARATOR)[1])
            };


        }

        /// <summary>
        /// Parses Console Input into an Instruction Object.
        /// </summary>
        /// <param name="consoleInput"></param>
        /// <returns></returns>
        public static Instruction ParseInstruction(string? consoleInput)
        {
            if (string.IsNullOrEmpty(consoleInput)) return Instruction.GetEmpty();

            var values = consoleInput.Split(SEPARATOR);
            return new Instruction(values[0], Convert.ToInt32(values[1]));
        }
    }
}
