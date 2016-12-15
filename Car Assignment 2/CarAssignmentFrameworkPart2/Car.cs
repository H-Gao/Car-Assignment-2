/*
 * Henry Gao
 * Dec 14th, 2016
 * The car class that creates a default car
 * the car can move in the world map to reach
 * an end location.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CarAssignmentFrameworkPart2
{
    class Car
    {
        /// <summary>
        /// Stores the color of the car as a brush
        /// </summary>
        protected Brush _color;

        /// <summary>
        /// Stores the direction that the car is facing
        /// </summary>
        protected Direction _facingDirection;

        /// <summary>
        /// Stores the row and column the car is located in the Map
        /// </summary>
        protected int _row;
        protected int _column;

        /// <summary>
        /// Stores if the car is holding dirt.
        /// </summary>
        protected bool _hasDirt = false;

        /// <summary>
        /// Stores if the car is broken.
        /// </summary>
        protected bool _isBroken = false;

        /// <summary>
        /// Gets the color of the car
        /// </summary>
        public Brush Colour
        {
            //Gets the color of the car
            get
            {
                return _color;
            }
        }

        /// <summary>
        /// Gets the direction the car is facing
        /// </summary>
        public Direction FacingDirection
        {
            //Gets the direction the car is facing.
            get
            {
                return _facingDirection;
            }
        }

        /// <summary>
        /// Gets the row that the car is located on the map
        /// </summary>
        public int Row
        {
            //Gets the row that the car is located on the map
            get
            {
                return _row;
            }
        }

        /// <summary>
        /// Gets the column the car is located on the map
        /// </summary>
        public int Column
        {
            //Gets the column the car is located on the map
            get
            {
                return _column;
            }
        }

        /// <summary>
        /// Gets whether the car is broken or not
        /// </summary>
        public bool IsBroken
        {
            //Gets whether the car is broken or not
            get
            {
                return _isBroken;
            }
        }

        /// <summary>
        /// Initalizes the car with the row, column, facingDirection and default color in the map.
        /// </summary>
        /// <param name="row">The row in the map</param>
        /// <param name="column">The column in the map</param>
        /// <param name="startDirection">The direction the car is facing at first</param>
        public Car(int row, int column, Direction startDirection) : this(row, column, startDirection, Brushes.Black) { }

        /// <summary>
        /// Initalizes the car with the row, column, facingDirection and a color in the map.
        /// </summary>
        /// <param name="row">The row in the map</param>
        /// <param name="column">The column in the map</param>
        /// <param name="startDirection">The direction the car is facing at first</param>
        /// <param name="color">The color of the car</param>
        public Car(int row, int column, Direction startDirection, Brush color)
        {
            //Stores the perimeter data in row, column, facingDirection
            _row = row;
            _column = column;
            _facingDirection = startDirection;

            //Stores the color
            _color = color;
        }

        /// <summary>
        /// Rotates the car left, if able.
        /// </summary>
        public void RotateLeft()
        {
            //If the car is not broken, it can rotate.
            if (!_isBroken)
            {
                //If the car is facing left, the direction is now down
                if (_facingDirection == Direction.Left)
                {
                    //Stores that the car is facing down.
                    _facingDirection = Direction.Down;
                }
                //If the car is facing down, the direction is now right
                else if (_facingDirection == Direction.Down)
                {
                    //Stores that the car is facing right
                    _facingDirection = Direction.Right;
                }
                //If the car is facing up, the direction is now left
                else if (_facingDirection == Direction.Up)
                {
                    //Stores that the car is facing left
                    _facingDirection = Direction.Left;
                }
                //If the car is facing right, the direction is now up
                else if (_facingDirection == Direction.Right)
                {
                    //Stores that the car is now up
                    _facingDirection = Direction.Up;
                }
            }
        }

        /// <summary>
        /// Rotates the car right, if able
        /// </summary>
        public void RotateRight()
        {
            //If the car is not broken, it can rotate
            if (!_isBroken)
            {
                //If it's facing left, it is now up.
                if (_facingDirection == Direction.Left)
                {
                    //Stores the facing direction as up.
                    _facingDirection = Direction.Up;
                }
                //If it's facing down, it is now left.
                else if (_facingDirection == Direction.Down)
                {
                    //Stores the facing direction as left
                    _facingDirection = Direction.Left;
                }
                //If it's facing up, it is now right.
                else if (_facingDirection == Direction.Up)
                {
                    //Stores the facing direction as right
                    _facingDirection = Direction.Right;
                }
                //If it's facing right, it is now dow.
                else if (_facingDirection == Direction.Right)
                {
                    //Stores the facing direction as down
                    _facingDirection = Direction.Down;
                }
            }
        }

        /// <summary>
        /// Moves the car in the facing direction, if able
        /// </summary>
        /// <param name="tile">The tile that the car is facing</param>
        public virtual void Move(MapTile tile)
        {
            //Stores whether the car breaks by moving unsafely
            _isBroken = !CanMoveSafely(tile);

            //If the car is not broken, it can move.
            if (!_isBroken)
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
        }

        /// <summary>
        /// Returns whether the car can move safely onto the next tile.
        /// </summary>
        /// <param name="tile">The tile the car is acing</param>
        /// <returns>True if the car can move safely</returns>
        public virtual bool CanMoveSafely(MapTile tile)
        {
            //If the car is not broken, it can move.
            if (!_isBroken)
            {
                //Returns true, if the car is not on a closed gate, wall or hole.
                return !(tile == MapTile.ClosedGate || tile == MapTile.Wall || tile == MapTile.Hole);
            }

            //If the can is broken, it cannot move. Returns false.
            return false;
        }

        /// <summary>
        /// Moves the car if it can do so safely
        /// </summary>
        /// <param name="tile">The tile the car is facing</param>
        /// <returns>True if the car can move safely</returns>
        public virtual bool MoveSafely(MapTile tile)
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

                //Returns that it can move safely.
                return true;
            }

            //If the car is broken, it cannot move safely
            return false;
        }

        /// <summary>
        /// The car picks up dirt, if able
        /// </summary>
        /// <param name="tile">Reference to the tile that the car is facing</param>
        /// <returns>True if the car can pick up dirt</returns>
        public virtual bool PickUpDirt(ref MapTile tile)
        {
            //If the car is not broken, it can pick up dirt.
            if (!_isBroken)
            {
                //If the car is not facing dirt or is already holding dirt, do not pick up dirt.
                if (tile != MapTile.Dirt || _hasDirt == true)
                {
                    //Return it cannot pickup dirt.
                    return false;
                }

                //Stores that the car is holding dirt.
                _hasDirt = true;

                //Change the tile to a hole, and return it can pickup dirt.
                tile = MapTile.Hole;
                return true;
            }
            return false;
        }

        /// <summary>
        /// The car drops dirt, if able
        /// </summary>
        /// <param name="tile">Reference to the tile the car is facing</param>
        /// <returns>True if it can drop dirt</returns>
        public virtual bool DropDirt(ref MapTile tile)
        {
            //If the car is not broken, it can drop dirt.
            if (!_isBroken)
            {
                //If the tile isn't a hole or it has no dirt, it cannot drop dirt.
                if (tile != MapTile.Hole || _hasDirt == false)
                {
                    //Returns that it cannot drop dirt.
                    return false;
                }

                //Stores that the car has no more dirt.
                _hasDirt = false;

                //Changes the tile to a dirt and returns that it dropped dirt.
                tile = MapTile.Dirt;
                return true;
            }
            return false;
        }

        /// <summary>
        /// The car opens the gate, if able
        /// </summary>
        /// <param name="tile">Reference to the tile the car is facing</param>
        /// <returns>True if it can open a gate</returns>
        public bool OpenGate(ref MapTile tile)
        {
            //If the car is not broken and the tile is a closed gate, it can open a gate.
            if (!_isBroken && tile == MapTile.ClosedGate)
            {
                //Changes the tile to an open gate, and returns it opened a gate.
                tile = MapTile.OpenGate;
                return true;
            }

            //If the car is broken or not facing a closed gate, it returns false.
            return false;
        }

        /// <summary>
        /// The car closes a gate if able.
        /// </summary>
        /// <param name="tile">Reference to the tile the car is facing</param>
        /// <returns>Returns true if it can close the gate</returns>
        public bool CloseGate(ref MapTile tile)
        {
            //If the car is broken and the car is facing an open gate.
            if (!_isBroken && tile == MapTile.OpenGate)
            {
                //Changes the tile to a closed gate and returns it can close the gate.
                tile = MapTile.ClosedGate;
                return true;
            }
            
            //If the car is broken or not facing an open gate, return it cannot close the gate.
            return false;
        }        
    }
}
