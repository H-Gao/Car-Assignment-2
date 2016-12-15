/*
 * Henry Gao
 * December 14th, 2016
 * The special car that has access to a special move
 * as defined by the child classes.
*/

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
        /// <summary>
        /// Creates a special car with the specified row, column, direction and default color.
        /// </summary>
        /// <param name="row">The row in the grid the car is located</param>
        /// <param name="column">The column in the grid the car is located</param>
        /// <param name="direction">The direction the car is located</param>
        protected SpecialCar(int row, int column, Direction direction) : base(row, column, direction) {}

        /// <summary>
        /// Creates a special car with the specified row, column, direction and default color.
        /// </summary>
        /// <param name="row">The row in the grid the car is located</param>
        /// <param name="column">The column in the grid the car is located</param>
        /// <param name="direction">The direction the car is located</param>
        /// <param name="color">The color of the car</param>
        protected SpecialCar(int row, int column, Direction direction, Brush color) : base(row, column, direction, color) { }

        /// <summary>
        /// Uses the car's special move
        /// </summary>
        /// <param name="world">The world the car is located in</param>
        /// <returns>True if the special move succeeded</returns>
        public abstract bool SpecialMove(WorldModel world);
    }
}
