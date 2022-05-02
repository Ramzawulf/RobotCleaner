using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using CleaningRobot;
using NUnit.Framework;

namespace CleaningRobotTests
{
    /*
     * T: Out of bounds checks
     * All inout should be well formed and syntactically correct
     * No error output
     * T: Output formatting
     * T: Output Trailing Space
     * Multi-valued input example: V1 V2
     * Office is a grid
     */
    public class RobotTests
    {
        private const int MaxH = 100;
        private const int MaxV = 100;

        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Verification for the grid size initial values.
        /// </summary>
        [Test]
        public void BoundaryTest()
        {
            int expectedWidth = 100;
            int expectedHeight = 100;

            var rbt = new Robot(new[] { 10, 10 }, new[] { Instruction.GetEmpty() });

            Assert.AreEqual(expectedHeight, rbt.GridHeight);
            Assert.AreEqual(expectedWidth, rbt.GridWidth);
        }

        /// <summary>
        /// Checks that the origin cannot be bigger than the boundaries.
        /// </summary>
        [Test]
        public void MaxExceededOriginPlacementTest()
        {
            const int expectedX = 0;
            const int expectedY = 0;

            var rbt = new Robot(new[] { int.MaxValue, int.MaxValue }, new[] { Instruction.GetEmpty() });

            Assert.AreEqual(expectedX, rbt.Origin[0]);
            Assert.AreEqual(expectedY, rbt.Origin[1]);
        }

        /// <summary>
        /// Checks that the origin cannot be smaller than the boundaries.
        /// </summary>
        [Test]
        public void MinExceededOriginPlacementTest()
        {
            const int expectedX = 0;
            const int expectedY = 0;

            var rbt = new Robot(new[] { int.MinValue, int.MinValue }, new[] { Instruction.GetEmpty() });

            Assert.AreEqual(expectedX, rbt.Origin[0]);
            Assert.AreEqual(expectedY, rbt.Origin[1]);
        }

        /// <summary>
        /// Test based on the sample data from the problem definition.
        /// Input:
        /// 2
        /// 10 22
        /// E 2
        /// N 1
        /// </summary>
        [Test]
        public void RunBasicTest()
        {
            const int expectedResult = 4;

            var origin = new[] { 10, 22 };
            var instructions = new List<Instruction>()
            {
                new("E",2),
                new("N",1)
            };

            var rbt = new Robot(origin, instructions.ToArray());
            var result = rbt.Run();

            Assert.AreEqual(expectedResult, result.VisitedPoints.Length);
        }

        [Test]
        public void RunBoundaryNorth()
        {
            var origin = new[] { MaxH / 2, MaxV };
            var instructions = new List<Instruction>()
            {
                new("N",1),
                new("N",2),
                new("N",3)
            };

            var rbt = new Robot(origin, instructions.ToArray(), MaxH, MaxV);
            var result = rbt.Run();

            var expectedPosition = new Vector2(MaxH / 2, MaxV);
            var expectedVisitedPositions = 1;

            Assert.AreEqual(expectedPosition, result.FinalPosition);
            Assert.AreEqual(expectedVisitedPositions, result.VisitedPoints.Length);
        }

        [Test]
        public void RunBoundarySouth()
        {
            var origin = new[] { MaxH / 2, 0 };
            var instructions = new List<Instruction>()
            {
                new("S",1),
                new("S",2),
                new("S",3)
            };

            var rbt = new Robot(origin, instructions.ToArray());
            var result = rbt.Run();

            var expectedPosition = new Vector2(MaxH / 2, 0);
            var expectedVisitedPositions = 1;

            Assert.AreEqual(expectedPosition, result.FinalPosition);
            Assert.AreEqual(expectedVisitedPositions, result.VisitedPoints.Length);
        }

        [Test]
        public void RunBoundaryEast()
        {
            var origin = new[] { MaxH, MaxV / 2 };
            var instructions = new List<Instruction>()
            {
                new("E",1),
                new("E",2),
                new("E",3)
            };

            var rbt = new Robot(origin, instructions.ToArray(), MaxH, MaxV);
            var result = rbt.Run();

            var expectedPosition = new Vector2(MaxH, MaxV / 2);
            var expectedVisitedPositions = 1;

            Assert.AreEqual(expectedPosition, result.FinalPosition);
            Assert.AreEqual(expectedVisitedPositions, result.VisitedPoints.Length);
        }

        [Test]
        public void RunBoundaryWest()
        {
            var origin = new[] { 0, MaxV };
            var instructions = new List<Instruction>()
            {
                new("W",1),
                new("W",2),
                new("W",3)
            };

            var rbt = new Robot(origin, instructions.ToArray());
            var result = rbt.Run();

            var expectedPosition = new Vector2(0, MaxV);
            var expectedVisitedPositions = 1;

            Assert.AreEqual(expectedPosition, result.FinalPosition);
            Assert.AreEqual(expectedVisitedPositions, result.VisitedPoints.Length);
        }

        [Test]
        public void RunRevisitedVertices()
        {
            var origin = new[] { MaxH / 2, MaxV / 2 };
            var instructions = new List<Instruction>()
            {
                new("N",1),
                new("E",1),
                new("S",1),
                new("W",1),
                new("E",1),
                new("N",1),
                new("W",1),
                new("S",1)
            };

            var rbt = new Robot(origin, instructions.ToArray());
            var result = rbt.Run();

            var expectedPosition = new Vector2(MaxH / 2, MaxV / 2);
            var expectedVisitedPositions = 4;

            Assert.AreEqual(expectedPosition, result.FinalPosition);
            Assert.AreEqual(expectedVisitedPositions, result.VisitedPoints.Length);
        }


        [Test]
        public void RunNoInstructions()
        {
            var origin = new[] { MaxH / 2, MaxV / 2 };

            var rbt = new Robot(origin, Array.Empty<Instruction>());
            var result = rbt.Run();

            var expectedPosition = new Vector2(MaxH / 2, MaxV / 2);
            var expectedVisitedPositions = 1;

            Assert.AreEqual(expectedPosition, result.FinalPosition);
            Assert.AreEqual(expectedVisitedPositions, result.VisitedPoints.Length);
        }

        [Test]
        public void RunFullTraversal()
        {
            var origin = new[] { 0, 0 };
            var instructions = new List<Instruction>();
            for (int i = 0; i < MaxV; i++)
            {
                instructions.Add(i % 2 == 0 ? new Instruction("E", MaxH) : new Instruction("W", MaxH));
                instructions.Add(new Instruction("N", 1));
                if (i == MaxV - 1)
                    instructions.Add(i % 2 == 0 ? new Instruction("W", MaxH) : new Instruction("E", MaxH));

            }
            var rbt = new Robot(origin, instructions.ToArray(), MaxH, MaxV);
            var result = rbt.Run();

            var expectedPosition = new Vector2(MaxH, MaxV);
            var expectedVisitedPositions = (MaxV + 1) * (MaxH + 1);

            Assert.AreEqual(expectedPosition, result.FinalPosition);
            Assert.AreEqual(expectedVisitedPositions, result.VisitedPoints.Length);
        }

    }
}