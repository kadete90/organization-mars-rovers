using System.Collections.Generic;
using System.Linq;

namespace MarsRovers
{
    public class CoordinatePoint
    {
        private static readonly Dictionary<char, short> CoordinateAngles = new Dictionary<char, short>
        {
            { 'N', 0 },
            { 'E', 90 },
            { 'S', 180 },
            { 'W', 270 }
        };

        private const int SpinAngle = 90;

        public int Angle { get; private set; } // [0, 90, 180, 270]

        public char CPoint => CoordinateAngles.First(c => c.Value == Angle).Key;

        public CoordinatePoint(char value)
        {
            Angle = CoordinateAngles[value];
        }

        public void RotateRight()
        {
            Angle += SpinAngle;

            if (Angle == 360)
            {
                Angle = 0;
            }
        }

        public void RotateLeft()
        {
            Angle -= SpinAngle;

            if (Angle == -90)
            {
                Angle = 270;
            }
        }
    }
}