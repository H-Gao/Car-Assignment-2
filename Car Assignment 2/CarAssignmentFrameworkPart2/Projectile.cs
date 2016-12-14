using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CarAssignmentFrameworkPart2
{
    class Projectile
    {
        // Create a private contstant variable for 
        // the speed of the projectile
        private const int SPEED = 3;
        
        // Create a private direction variable for
        // the direction of the projectile
        private Direction _facing;
        
        // Create a private point variable for
        // the location of the projectile
        private Point _location;

        public Projectile(int x, int y, Direction facing)
        {
            _facing = facing;
            _location.X = x;
            _location.Y = y;
        }

        public Direction Direction
        {
            get
            {
                return _facing;
            }
        }

        public Point Location
        {
            get
            {
                return _location;
            }
        }

        public Point Move()
        {
            if (_facing == Direction.Up)
            {
                _location.X =- SPEED;
                return _location;
            }
            else if (_facing  == Direction.Down)
            {
                _location.X =+ SPEED;
                return _location;
            }
            else if (_facing == Direction.Left)
            {
                _location.Y =- SPEED;
                return _location;
            }
            else if (_facing == Direction.Right)
            {
                _location.Y =+ SPEED;
                return _location;
            }
            return _location;
        }
    }
}
