using System;
using System.Text.RegularExpressions;

namespace MarsRovers
{
    public class Rover
    {
        public Guid Id { get; }

        public int X { get; private set; }

        public int Y { get; private set; }

        private readonly CoordinatePoint _orientation;

        private readonly Regex _rgxCoordinates = new Regex(@"^(\d) (\d) (\w{1})$");

        public Rover(string location)
        {
            this.Id = Guid.NewGuid();

            var match = _rgxCoordinates.Match(location);
            if (!match.Success)
            {
                throw new ArgumentException("Invalid coordinates of the rover", nameof(location));
            }

            this.X = Convert.ToInt32(match.Groups[1].Value);
            this.Y = Convert.ToInt32(match.Groups[2].Value);

            _orientation = new CoordinatePoint(Convert.ToChar(match.Groups[3].Value));
        }

        public void DoAction(char action, Func<int, int, bool> validLocationOnPlateau)
        {
            switch (action)
            {
                case 'L':
                    _orientation.RotateLeft();
                    break;
                case 'R':   
                    _orientation.RotateRight();
                    break;

                case 'M':
                    switch (_orientation.Angle)
                    {
                        case 0:
                            if (validLocationOnPlateau(this.X, this.Y + 1))
                            {
                                this.Y++;
                            }
                            break;
                        case 90:
                            if (validLocationOnPlateau(this.X + 1, this.Y))
                            {
                                this.X++;
                            }
                            break;
                        case 180:
                            if (validLocationOnPlateau(this.X, this.Y - 1))
                            {
                                this.Y--;
                            }
                            break;
                        case 270:
                            if (validLocationOnPlateau(this.X - 1, this.Y))
                            {
                                this.X--;
                            }
                            break;
                    }

                    break;
            }
        }

        public override string ToString()
        {
            return $"{this.X} {this.Y} {_orientation.CPoint}";
        }
    }
}
