using System;
using CleaningRobot.Utilities;
using NUnit.Framework;

namespace CleaningRobotTests
{
    public class InputFormatterTests
    {
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Checks that the default value (0) is returned when an empty string is passed.
        /// </summary>
        [Test]
        public void GetNumberOfInstructions_DefaultValue()
        {
            var result = InputFormatter.ParseNumberOfInstructions("");
            var expected = 0;
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Checks that the default value (empty int array) is returned when an empty string is passed.
        /// </summary>
        [Test]
        public void GetStartingCoordinates_DefaultValue()
        {
            var result = InputFormatter.ParseStartingCoordinates("");
            var expected = Array.Empty<int>();
            CollectionAssert.AreEqual(expected, result);
        }

        /// <summary>
        /// Checks that the default value ("empty" Instruction object) is returned when an empty string is passed.
        /// </summary>
        [Test]
        public void GetInstruction_DefaultValue()
        {
            var result = InputFormatter.ParseInstruction("");
            Assert.IsTrue(result.IsEmpty());
        }

    }
}

