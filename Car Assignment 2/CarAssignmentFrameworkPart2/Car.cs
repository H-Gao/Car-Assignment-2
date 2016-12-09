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
        protected Brush _colour;
        protected Direction _facingDirection;
        protected int _row;
        protected int _column;

        protected bool _hasDirt = false;

        protected bool _isBroken = false;

        public Brush Colour
        {
            get
            {
                return _colour;
            }
        }

        public bool IsBroken
        {
            get
            {
                return _isBroken;
            }
        }

        public Direction FacingDirection
        {
            get
            { 
                return _facingDirection;
            }
        }

        public int Row
        {
            get
            {
                return _row;
            }
        }

        public int Column
        {
            get
            {
                return _column;
            }
        }

        public Car(int row, int col, Direction start) : this(row, col, start, Brushes.Black) {}

        public Car(int row, int col, Direction start, Brush colour)
        {
            _row = row;
            _column = col;
            _facingDirection = start;
            _colour = colour;
        }

        public void RotateLeft()
        {
            if (!_isBroken)
            {
                if (_facingDirection == Direction.Left)
                {
                    _facingDirection = Direction.Down;
                }
                else if (_facingDirection == Direction.Down)
                {
                    _facingDirection = Direction.Right;
                }
                else if (_facingDirection == Direction.Up)
                {
                    _facingDirection = Direction.Left;
                }
                else if (_facingDirection == Direction.Right)
                {
                    _facingDirection = Direction.Up;
                }
            }
        }

        public void RotateRight()
        {
            if (!_isBroken)
            {
                if (_facingDirection == Direction.Left)
                {
                    _facingDirection = Direction.Up;
                }
                else if (_facingDirection == Direction.Down)
                {
                    _facingDirection = Direction.Left;
                }
                else if (_facingDirection == Direction.Up)
                {
                    _facingDirection = Direction.Right;
                }
                else if (_facingDirection == Direction.Right)
                {
                    _facingDirection = Direction.Down;
                }
            }
        }

        public void Move(MapTile tile)
        {

            _isBroken = !CanMoveSafely(tile);

            if (!_isBroken)
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

        public bool CanMoveSafely(MapTile tile)
        {
            if (!_isBroken)
            {
                return !(tile == MapTile.ClosedGate || tile == MapTile.Wall || tile == MapTile.Hole);
            }
            return false;
        }

        public bool MoveSafely(MapTile tile)
        {
            if (!_isBroken)
            {
                if (!CanMoveSafely(tile))
                {
                    return false;
                }

                Move(tile);

                return true;
            }
            return false;
        }

        public bool PickUpDirt(ref MapTile tile)
        {
            if (!_isBroken)
            {
                if (tile != MapTile.Dirt || _hasDirt == true)
                {
                    return false;
                }
                _hasDirt = true;
                tile = MapTile.Hole;
                return true;
            }
            return false;
        }

        public bool DropDirt(ref MapTile tile)
        {
            if (!_isBroken)
            {
                if (tile != MapTile.Hole || _hasDirt == false)
                {
                    return false;
                }
                _hasDirt = false;
                tile = MapTile.Dirt;
                return true;
            }
            return false;
        }

        public bool OpenGate(ref MapTile tile)
        {
            if (!_isBroken)
            {
                tile = MapTile.OpenGate;
                return true;
            }
            return false;
        }

        public bool CloseGate(ref MapTile tile)
        {
            if (!_isBroken)
            {
                tile = MapTile.ClosedGate;
                return true;
            }
            return false;
        }        
    }
}
