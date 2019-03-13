using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MarsRovers
{
    public class Plateau
    {
        private readonly int _sizeX;
        private readonly int _sizeY;

        List<Rover> Rovers { get; }

        private readonly Regex _rgxCoordinates = new Regex(@"^(\d) (\d)$");

        public Plateau(string upperRightCoordinates)
        {
            var match = _rgxCoordinates.Match(upperRightCoordinates);

            if (!match.Success)
            {
                throw new ArgumentException("Invalid upper right coordinates of the plateau", nameof(upperRightCoordinates));
            }

            this._sizeX = Convert.ToInt32(match.Groups[1].Value);
            this._sizeY = Convert.ToInt32(match.Groups[2].Value);

            Rovers = new List<Rover>();
        }

        private bool LocationIsValid(int nextX, int nextY)
        {
            return nextX >= 0 && nextX <= _sizeX &&
                   nextY >= 0 && nextY <= _sizeY;
        }

        public Guid PlaceNewRover(string location)
        {
            var rover = new Rover(location);

            if (!LocationIsValid(rover.X, rover.Y))
            {
                // TODO review what to do in this situation
                throw new ArgumentException();
            }

            Rovers.Add(rover);

            return rover.Id;
        }

        public void MoveRover(Guid id, string actions)
        {
            var rover = Rovers.FirstOrDefault(r => r.Id == id);
            if (rover != null)
            {
                char[] actionsAr = actions.ToCharArray();

                foreach (var action in actionsAr)
                {
                    rover.DoAction(action, LocationIsValid);
                }
            }
        }

        public void PrintRoverCoordinates()
        {
            Rovers.ForEach(Console.WriteLine);
        }

        public List<Rover> GetRovers()
        {
            return Rovers;
        }
    }
}
