using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CarAssignmentFrameworkPart2
{
    class Tank : SpecialCar
    {
        public Tank(int row, int column, Direction startDirection)
            : base(row, column, startDirection)
        {
        }

        public Tank(int row, int column, Direction startDirection, Brush color)
            : base(row, column, startDirection, color)
        {
        }

        public abstract void Move(MapTile tile)
        {
            _isBroken = MoveSafely(tile);

            if (_isBroken != true)
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

        public abstract bool MoveSafely(MapTile tile)
        {
            if (tile != MapTile.Hole)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public abstract bool SpecialMove(WorldModel world)
        {
            Projectile(_row, _column, _facingDirection);
        }
    }
}
