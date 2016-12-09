using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace CarAssignmentFrameworkPart2
{
    class CarFactory
    {
        public static string MAKE_TUNNELER = "Tunneler";
        public static string MAKE_FLYING_CAR = "FlyingCar";
        public static string MAKE_TANK = "Tank";
        public static string MAKE_TELEPORTER = "Teleporter";

        public static Car MakeCar(string carType, int row, int col, Direction startDir, Color colour)
        {
            if (carType == MAKE_FLYING_CAR)
            {
                return MakeFlyingCar(row, col, startDir, colour);
            }
            else if (carType == MAKE_TANK)
            {
                return MakeTank(row, col, startDir, colour);
            }
            else if (carType == MAKE_TELEPORTER)
            {
                return MakeTeleporter(row, col, startDir, colour);
            }
            else if (carType == MAKE_TUNNELER)
            {
                return MakeTunneler(row, col, startDir, colour);
            }
            return null;
        }

        public static Car MakeCar(string carType, int row, int col, Direction startDir)
        {
            if ( carType == MAKE_FLYING_CAR)
            {
                return MakeFlyingCar(row, col, startDir);
            }
            else if (carType == MAKE_TANK)
            {
                return MakeTank(row, col, startDir);
            }
            else if (carType == MAKE_TELEPORTER)
            {
                return MakeTeleporter(row, col, startDir);
            }
            else if (carType == MAKE_TUNNELER)
            {
                return MakeTunneler(row, col, startDir);
            }
            return null;
        }

        public static Car MakeFlyingCar(int row, int col, Direction startDir, Color colour)
        {
            return new FlyingCar(row, col, startDir, colour);
        }

        public static Car MakeFlyingCar(int row, int col, Direction startDir)
        {
            return new FlyingCar(row, col, startDir);
        }

        public static Car MakeTeleporter(int row, int col, Direction startDir, Color colour)
        {
            return new Teleporter(row, col, startDir, colour);
        }

        public static Car MakeTeleporter(int row, int col, Direction startDir)
        {
            return new Teleporter(row, col, startDir);
        }

        public static Car MakeTank(int row, int col, Direction startDir)
        {
            return new Tank(row, col, startDir);
        }

        public static Car MakeTank(int row, int col, Direction startDir, Color colour)
        {
            return new Tank(row, col, startDir, colour);
        }

        public static Car MakeTunneler(int row, int col, Direction startDir)
        {
            return new Tunneler(row, col, startDir);
        }

        public static Car MakeTunneler(int row, int col, Direction startDir, Color colour)
        {
            return new Tunneler(row, col, startDir, colour);
        }

    }
}
