using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CarAssignmentFrameworkPart2
{
    class FlyingCar : SpecialCar
    {
        // Create a protected variable for 
        // the amount of fuel remaining in the flying car
        protected int _fuelRemaining = 10;

        // Create a protected varaible for
        // if the flying car is flying or not
        protected bool _isFlying = false;

        /// <summary>
        /// Constructor for the flying car, without colour
        /// </summary>
        /// <param name="row">The starting row</param>
        /// <param name="column">The starting column</param>
        /// <param name="startDirection">The direction the car starts in</param>
        public FlyingCar(int row, int column, Direction startDirection)
            :this(row, column, startDirection, Brushes.Black)
        {
        }

        /// <summary>
        /// Constructor for flying car, with colour
        /// </summary>
        /// <param name="row">The starting row</param>
        /// <param name="column">The starting column</param>
        /// <param name="startDirection">The direction the car starts in</param>
        /// <param name="color">The colour of the car</param>
        public FlyingCar(int row, int column, Direction startDirection, Brush color)
            :base(row, column, startDirection, color)
        {
        }

        // Gets true or false if fuel is empty or not
        public bool FuelIsEmpty
        {
            get
            {
                if (_fuelRemaining == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        // Gets the amount of fuel remaining
        public int FuelRemaining
        {
            get
            {
                return _fuelRemaining;
            }
        }

        // 
        public bool IsFlying
        {
            get
            {
                return _isFlying;
            }
        }

        public override bool CanMoveSafely(MapTile tile)
        {
            if(_isFlying == false)
            {
                if (tile != MapTile.Hole && tile != MapTile.Wall && tile != MapTile.ClosedGate)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if(_isFlying == true)
            {
                if(tile != MapTile.Hole && tile != MapTile.Wall && tile != MapTile.ClosedGate && tile != MapTile.OpenGate && tile != MapTile.Dirt && tile != MapTile.EndingLocation && tile != MapTile.StartingLocation)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public override void Move(MapTile tile)
        {
            if (IsBroken == false)
            {
                if (CanMoveSafely(tile) == true)
                {
                    if (IsFlying == true)
                    {
                        if (_facingDirection == Direction.Up)
                        {
                            _row--;
                            _fuelRemaining--;
                        }
                        else if (_facingDirection == Direction.Down)
                        {
                            _row++;
                            _fuelRemaining--;
                        }
                        else if (_facingDirection == Direction.Left)
                        {
                            _column--;
                            _fuelRemaining--;
                        }
                        else if (_facingDirection == Direction.Right)
                        {
                            _column++;
                            _fuelRemaining--;
                        }
                    }
                    else
                    {
                        if (_facingDirection == Direction.Up)
                        {
                            _row--;
                        }
                        else if (_facingDirection == Direction.Down)
                        {
                            _row++;
                        }
                        else if (_facingDirection == Direction.Left)
                        {
                            _column--;
                        }
                        else if (_facingDirection == Direction.Right)
                        {
                            _column++;
                        }
                    }
                }
                else if (CanMoveSafely(tile) == false)
                {
                    if (IsFlying == true)
                    {
                        _isBroken = true;
                        if (_facingDirection == Direction.Up)
                        {
                            _row--;
                            _fuelRemaining--;
                        }
                        else if (_facingDirection == Direction.Down)
                        {
                            _row++;
                            _fuelRemaining--;
                        }
                        else if (_facingDirection == Direction.Left)
                        {
                            _column--;
                            _fuelRemaining--;
                        }
                        else if (_facingDirection == Direction.Right)
                        {
                            _column++;
                            _fuelRemaining--;
                        }
                    }
                    else
                    {
                        _isBroken = true;

                        if (_facingDirection == Direction.Up)
                        {
                            _row--;
                        }
                        else if (_facingDirection == Direction.Down)
                        {
                            _row++;
                        }
                        else if (_facingDirection == Direction.Left)
                        {
                            _column--;
                        }
                        else if (_facingDirection == Direction.Right)
                        {
                            _column++;
                        }
                    }
                }
            }

        }

        public override void MoveSafely(MapTile tile)
        {
            if (IsBroken == false)
            {
                if (CanMoveSafely(tile) == true)
                {
                    if (IsFlying == true)
                    {
                        if (_facingDirection == Direction.Up)
                        {
                            _row--;
                            _fuelRemaining--;
                        }
                        else if (_facingDirection == Direction.Down)
                        {
                            _row++;
                            _fuelRemaining--;
                        }
                        else if (_facingDirection == Direction.Left)
                        {
                            _column--;
                            _fuelRemaining--;
                        }
                        else if (_facingDirection == Direction.Right)
                        {
                            _column++;
                            _fuelRemaining--;
                        }
                    }
                    else
                    {
                        if (_facingDirection == Direction.Up)
                        {
                            _row--;
                        }
                        else if (_facingDirection == Direction.Down)
                        {
                            _row++;
                        }
                        else if (_facingDirection == Direction.Left)
                        {
                            _column--;
                        }
                        else if (_facingDirection == Direction.Right)
                        {
                            _column++;
                        }
                    }
                }
            }
        }

        public override bool SpecialMove(WorldModel world)
        {
            if (IsFlying == false)
            {
                if (FuelRemaining > 2)
                {
                    _isFlying = true;
                    _fuelRemaining = -2;
                    return IsFlying;
                }
                else
                {
                    return IsFlying;
                }
            }
            else
            {
                if (world.Map[Column, Row] == MapTile.Hole || world.Map[Column, Row] == MapTile.Wall || world.Map[Column, Row] == MapTile.ClosedGate)
                {
                    _isBroken = true;
                }
                _isFlying = false;
                return IsFlying;
            }
        }
    }
}
