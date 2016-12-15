/*
 * Henry Gao
 * December 14th, 2016
 * A special car that can tunnel into any other hole on
 * the map.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections;

namespace CarAssignmentFrameworkPart2
{
    class Tunneler : SpecialCar
    {
        /// <summary>
        /// Dynamic array that stores the location of holes on the map.
        /// </summary>
        ArrayList holeLocations = new ArrayList();

        /// <summary>
        /// Stores if the car is tunneling
        /// </summary>
        protected bool _isTunneling;

        /// <summary>
        /// Stores if the car has already tunneled before
        /// </summary>
        private bool _hasTunneled = false;

        /// <summary>
        /// Creates a tunneler with the specified row, column, direction and default color.
        /// </summary>
        /// <param name="row">The row in the grid the car is located</param>
        /// <param name="column">The column in the grid the car is located</param>
        /// <param name="direction">The direction the car is located</param>
        public Tunneler(int row, int col, Direction direction) : base(row, col, direction) {}

        /// <summary>
        /// Creates a tunneler with the specified row, column, direction and default color.
        /// </summary>
        /// <param name="row">The row in the grid the car is located</param>
        /// <param name="column">The column in the grid the car is located</param>
        /// <param name="direction">The direction the car is located</param>
        /// <param name="color">The color of the car</param>
        public Tunneler(int row, int col, Direction direction, Brush color) : base(row, col, direction, color) {}

        /// <summary>
        /// Gets whether the car is tunneling or not.
        /// </summary>
        public bool isTunneling
        {
            //Gets whether the car is tunneling or not.
            get
            {
                //Returns if it is tunneling.
                return _isTunneling;
            }
        }

        /// <summary>
        /// Returns whether the car can move safely or not.
        /// </summary>
        /// <param name="tile">The tile that is in front of the car</param>
        /// <returns>True if it can move safely</returns>
        public override bool CanMoveSafely(MapTile tile)
        {
            //If the car is not broken and the tile in front is not a closed gate or wall, return true
            if (!IsBroken && (tile != MapTile.ClosedGate && tile != MapTile.Wall))
            {
                //Return it can move safely.
                return true;
            }
            //If the car is broken or the tile in front is a closed gate or wall, return false
            else
            {
                //Return it cannot move safely
                return false;
            }
        }

        /// <summary>
        /// Drops dirt in front of car, if able
        /// </summary>
        /// <param name="tile">Reference to the tile in front</param>
        /// <returns>True if it can pick up dirt</returns>
        public override bool DropDirt(ref MapTile tile)
        {
            //If the car is not broken, the next tile is a hole and has dirt, it can drop dirt
            if (!IsBroken && _hasDirt && tile == MapTile.Hole)
            {
                //Changes the tile from a hole to dirt
                tile = MapTile.Dirt;

                //Stores that it is holding dirt
                _hasDirt = false;

                //Holds the location of the tile the car is facing.
                int facingX = Column, facingY = Row;

                //If the car is facing up, move one row up
                if (FacingDirection == Direction.Up)
                {
                    //Subtract the y by one, making the facing location it move one row up
                    --facingY;
                }
                //If the car is facing down, move one row down
                else if (FacingDirection == Direction.Down)
                {
                    //Increase the facing location by one, making the facing location move one row down
                    ++facingY;
                }
                //If the car is facing left, move one column left
                else if (FacingDirection == Direction.Left)
                {
                    //Subtract the facing location by one, making the facing location move one column left
                    --facingX;
                }
                //If the car is facing right, move one column right
                else if (FacingDirection == Direction.Right)
                {
                    //Increase the facing location by one, making the facing location move one column right
                    ++facingX;
                }

                //Loops once for each hole location.
                for (int i = 0; i < holeLocations.Count; ++i)
                {
                    //The location of the next hole in dynamic array
                    Point nextHole = (Point)holeLocations[i];

                    //If the car finds the location of the tile it's facing, it removes it from the array.
                    if (nextHole.X == facingX && nextHole.Y == facingY)
                    {
                        //Removes the hole in the array.
                        holeLocations.RemoveAt(i);
                    }
                }

                //Returns that it can pick up dirt.
                return true;
            }

            //Returns that is cannot pick up dirt.
            return false;
        }

        /// <summary>
        /// Picks up the dirt in front of the car, if able
        /// </summary>
        /// <param name="tile">Reference to the tile in front of the car</param>
        /// <returns>True if the car can pickup dirt</returns>
        public override bool PickUpDirt(ref MapTile tile)
        {
            //If the car is not broken, the next tile is dirt and does not have dirt, it can pick up dirt
            if (!IsBroken && !_hasDirt && tile == MapTile.Dirt)
            {
                //Changes the tile to a hole, and stores that it's holding dirt.
                tile = MapTile.Hole;
                _hasDirt = true;

                //Holds the location of the tile the car is facing.
                int facingX = Column, facingY = Row;

                //If the car is facing up, the facing location is moved up.
                if (FacingDirection == Direction.Up)
                {
                    //Decrease the facing location by one, making it move up.
                    --facingY;
                }
                //If the car is facing down, the facing location is moved down.
                else if (FacingDirection == Direction.Down)
                {
                    //Increase the facing location by one, making it move down.
                    ++facingY;
                }
                //If the car is facing left, the facing location is moved left.
                else if (FacingDirection == Direction.Left)
                {
                    //Decrease the facing location by one, making it move left.
                    --facingX;
                }
                //If the car is facing right, the facing location is moved right.
                else if (FacingDirection == Direction.Right)
                {
                    //Increase the facing location by one, making it move right.
                    ++facingX;
                }

                //Adds the facing location to the array of hole locations.
                holeLocations.Add(new Point(facingX, facingY));

                //Returns that it can drop dirt.
                return true;
            }

            //Returns that it cannot drop dirt.
            return false;
        }

        public override void Move(MapTile tile)
        {
            //If the car is not broken, it can move.
            if (CanMoveSafely(tile))
            {
                //If the car is facing up, move up.
                if (_facingDirection == Direction.Up)
                {
                    //Stores one less row - moves up in the grid.
                    _row--;
                }
                //If the car is facing down, move down.
                else if (_facingDirection == Direction.Down)
                {
                    //Stores one more row - moves down in the grid.
                    _row++;
                }
                //If the car is facing right, move right
                else if (_facingDirection == Direction.Left)
                {
                    //Stores one less column - moves left in the grid.
                    _column--;
                }
                //If the car is facing left, move left
                else if (_facingDirection == Direction.Right)
                {
                    //Stores one more column - moves right in the grid.
                    _column++;
                }
            }
            //If the car cannot move safely onto the next tile, it breaks
            else
            {
                //Stores that it breaks
                _isBroken = true;
            }

            //Stores that the car is no longer tunneling.
            _isTunneling = false;
        }

        public override bool MoveSafely(MapTile tile)
        {
            //If the car is not broken, it can move.
            if (!_isBroken)
            {
                //If the car can't move safely, it returns false.
                if (!CanMoveSafely(tile))
                {
                    //Returns that it cannot move safely
                    return false;
                }

                //Moves the car and return true.
                Move(tile);

                //Returns that it can move safely
                return true;
            }

            //If the car is broken, return that it cannot move safely.
            return false;
        }

        /// <summary>
        /// Instantly tunnels to another hole on the map, if able.
        /// </summary>
        /// <param name="world">The world the car is located in</param>
        /// <returns>True if the car can tunnel</returns>
        public override bool SpecialMove(WorldModel world)
        {
            //The 2D grid of tiles the car is on.
            MapTile[,] map = world.Map;

            //If the car is not broken and has not tunneled before, it find all hole locations on the map.
            if (!IsBroken && !_hasTunneled)
            {
                //Removes all existing holes from pickup dirt.
                holeLocations.Clear();

                //Loops for all x values from the grid
                for (int x = 0; x < map.GetLength(1); ++x)
                {
                    //Loops for all y values from the grid
                    for (int y = 0; y < map.GetLength(0); ++y)
                    {
                        //If the tile at the looped x and y values if a hole, it adds the location to the dynamic array.
                        if (map[y, x] == MapTile.Hole)
                        {
                            //Adds the x and y location to the dynamic array of holes.
                            holeLocations.Add(new Point(x, y));
                        }
                    }
                }

                //Stores that it has already tunneled.
                _hasTunneled = true;
            }

            //If the tile the car is on is a hole, and there is more than one hole, it can tunnel.
            if (map[Row, Column] == MapTile.Hole && holeLocations.Count > 1)
            {
                //Holds the index of the hole the car's on, in the hole location array.
                int holeIndex = -1;

                //Loops from the index of the first hole to the index of the last hole in the hole location array.
                for (int i = 0; i < holeLocations.Count; ++i)
                {
                    //The hole location at the current index (from loop).
                    Point currentHole = (Point)holeLocations[i];

                    //If the car is on the hole, stores the index of the hole
                    if (currentHole.X == Column && currentHole.Y == Row)
                    {
                        //Stores the index of the hole (from loop) as the index of the car's hole.
                        holeIndex = i;

                        //Stops searching the other holes.
                        break;
                    }
                }

                //If the car is not tunneling, move the car to the first hole (or second hole)
                if (!isTunneling)
                {
                    //Of the car is already on the first hole, move it to the second hole.
                    if (holeIndex == 0)
                    {
                        //Sets the index of the car's hole as 1.
                        holeIndex = 1;
                    }
                    //If the car is not on the first hole, move it to the first hole.
                    else
                    {
                        //Sets the index of the car's hole as 0.
                        holeIndex = 0;
                    }

                    //Stores that the car is now tunneling.
                    _isTunneling = true;
                }
                else
                {
                    //Increases the index by one, unless it exceeds the total of holes where it stores 0.
                    holeIndex = (holeIndex + 1) % holeLocations.Count;
                }

                //Holds the location of the next hole in the array of hole locations.
                Point nextPoint = (Point)holeLocations[holeIndex];

                //Sets the car's location to the location of the next hole.
                _column = nextPoint.X;
                _row = nextPoint.Y;

                //Returns that it can tunnel.
                return true;
            }

            //Return that is cannot tunnel.
            return false;
        }
    }
}
