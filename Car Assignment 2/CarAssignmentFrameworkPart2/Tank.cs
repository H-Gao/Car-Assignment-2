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
            : this(row, column, startDirection, Brushes.Black)
        {
        }

        public Tank(int row, int column, Direction startDirection, Brush color)
            : base(row, column, startDirection, color)
        {
        }

        public abstract void Move(MapTile tile)
        {
            _isBroken = MoveSafely(tile);

            if (_isBroken == false)
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

        public override bool CanMoveSafely(MapTile tile)
        {
            if (_isBroken == false)
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
            return true;
        }

        public override bool SpecialMove(WorldModel world)
        {
            if (IsBroken == false)
            {
                Projectile tankShell = new Projectile(Row, Column, FacingDirection);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
