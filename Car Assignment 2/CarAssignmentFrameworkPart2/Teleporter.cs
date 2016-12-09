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
        protected int _fuelRemaining = 6;

        public Teleporter(int row, int column, Direction direction) : base(row, column, direction) {}
        public Teleporter(int row, int column, Direction direction, Brush color) : base(row, column, direction, color) {}

        public int GetFuelRemaining()
        {
            return _fuelRemaining;
        }

        public override bool SpecialMove(WorldModel world)
        {
            throw new NotImplementedException();
        }

        public override bool Move(MapTile tile)
        {
            return true;
        }

        public override bool MoveSafely(MapTile tile)
        {
            return true;
        }
    }
}
