using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CarAssignmentFrameworkPart2
{
    class Projectile : Tank
    {
        private const int SPEED = 3;

        public void Projectile(int x, int y, Direction facing)
        {
            _row = x;
            _column = y;
            _facingDirection = facing;
        }

        public Direction Direction
        {
            get
            {
                return _facingDirection;
            }
        }

        public Point Location
        {
            get
            {
                return _row;
                return _column; 
            }
        }

        
    }
}
