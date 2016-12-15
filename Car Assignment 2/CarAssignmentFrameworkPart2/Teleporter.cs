/*
 * Henry Gao
 * December 14th, 2016
 * A special car that holds fuel, and can
 * use the fuel to teleport to up to 3 tiles away.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CarAssignmentFrameworkPart2
{
    class Teleporter : SpecialCar
    {
        /// <summary>
        /// The amount of fuel remaining, starts at 6
        /// </summary>
        protected int _fuelRemaining = 6;

        /// <summary>
        /// Creates a teleporter with the specified row, column, direction and default color.
        /// </summary>
        /// <param name="row">The row in the grid the car is located</param>
        /// <param name="column">The column in the grid the car is located</param>
        /// <param name="direction">The direction the car is located</param>
        public Teleporter(int row, int column, Direction direction) : base(row, column, direction) {}

        /// <summary>
        /// Creates a teleporter with the specified row, column, direction and default color.
        /// </summary>
        /// <param name="row">The row in the grid the car is located</param>
        /// <param name="column">The column in the grid the car is located</param>
        /// <param name="direction">The direction the car is located</param>
        /// <param name="color">The color of the car</param>
        public Teleporter(int row, int column, Direction direction, Brush color) : base(row, column, direction, color) {}

        /// <summary>
        /// Gets the amount of fuel remaining
        /// </summary>
        public int FuelRemaining
        {
            //Gets the amount of fuel remaining.
            get
            {
                //Returns the amount of fuel remaining.
                return _fuelRemaining;
            }
        }

        /// <summary>
        /// Gets whether the fuel is empty or not.
        /// </summary>
        public bool FuelIsEmpty
        {
            //Gets whether the fuel is empty.
            get
            {
                //If there is no fuel left, return true.
                if (_fuelRemaining <= 0)
                {
                    //Return true
                    return true;
                }
                //If there is fuel left, return false.
                else
                {
                    //Return false
                    return false;
                }
            }
        }

        /// <summary>
        /// Teleports up to three spaces and uses one fuel if successful
        /// </summary>
        /// <param name="world">The world the car is located in</param>
        /// <returns>True if teleports successfully</returns>
        public override bool SpecialMove(WorldModel world)
        {
            //If the car is not broken and has fuel, it can teleport.
            if (!IsBroken && !FuelIsEmpty)
            {
                //Stores the 2D grid of tiles.
                MapTile[,] worldMap = world.Map;

                //Unit vectors for each direction (up, left, down, right in order).
                int[,] unitVectors = { { -1, 0 }, { 0, -1 }, { 1, 0 }, { 0, 1 } };

                //Loops three times, checks from 3 to 1 tiles from the teleport's location to see if it can teleport there.
                for (int spacesMoved = 3; spacesMoved > 0; --spacesMoved)
                {
                    //Holds the row and column of the location after teleporting by the loop's counter, in the facing direction.
                    int tempRow = Row + spacesMoved * unitVectors[(int)FacingDirection, 0], tempCol = Column + spacesMoved * unitVectors[(int)FacingDirection, 1];

                    //Checks to see if the teleported location is in bounds of the map.
                    if (tempRow >= 0 && tempRow < worldMap.GetLength(0) && tempCol >= 0 && tempCol < worldMap.GetLength(1))
                    {
                        //Holds the tile on the map at the teleported location.
                        MapTile nextTile = worldMap[tempRow, tempCol];

                        //If the teleporter can move safely, it teleports there.
                        if (CanMoveSafely(nextTile))
                        {
                            //Stores the teleported location as the car's location.
                            _row = tempRow;
                            _column = tempCol;

                            //Take away one fuel and returns true.
                            --_fuelRemaining;
                            return true;
                        }
                    }
                }
            }

            //If the car is broken, has no fuel or cannot teleport safely, returns false.
            return false;
        }
    }
}
