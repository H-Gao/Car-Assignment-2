using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CarAssignmentFrameworkPart2
{
    abstract class SpecialCar : Car
    {
        protected SpecialCar(int row, int column, Direction direction) : base(row, column, direction) {}
        protected SpecialCar(int row, int column, Direction direction, Brush color) : base(row, column, direction, color) { }

        public abstract bool SpecialMove(WorldModel world);
    }
}
