using System;
using System.Linq;
using Xunit;

namespace MarsRovers.Tests
{
    public class MarsRoversTests
    {
        [Fact]
        public void TestAssessment()
        {
            string[] instructions =
            {
                "5 5",
                "1 2 N",
                "LMLMLMLMM",
                "3 3 E",
                "MMRMMRMRRM"
            };

            Plateau plateau = new Plateau(instructions[0]);

            Guid roverOneId = plateau.PlaceNewRover(instructions[1]);
            Guid roverTwoId = plateau.PlaceNewRover(instructions[3]);

            plateau.MoveRover(roverOneId, instructions[2]);
            plateau.MoveRover(roverTwoId, instructions[4]);

            var rovers = plateau.GetRovers();

            Assert.Equal("1 3 N", rovers.First(r => r.Id == roverOneId).ToString());
            Assert.Equal("5 1 E", rovers.First(r => r.Id == roverTwoId).ToString());
        }

        [Fact]
        public void TestInvalidPlacement()
        {
            string[] instructions =
            {
                "5 5",
                "6 2 N",
                "LMLMLMLMM",
                "3 3 E",
                "MMRMMRMRRM"
            };

            Plateau plateau = new Plateau(instructions[0]);

            Assert.Throws<ArgumentException>(() => plateau.PlaceNewRover(instructions[1]));
        }
    }
}
