/*
 * File input format:
 * car type string
 * r -- number of rows in the map
 * c -- number of columns in the map
 * rxc grid with the following characters
 * 0 is wall
 * 1 is dirt
 * 2 is closed gate
 * 3 is hole
 * 4 is starting location
 * 5 is ending location
 * 6 is open gate
 * 
 * This number of tiles is important for the file loading
 * There is a constant that controls is used for error checking
 * the possible tile values. 
 */
  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using System.IO;

namespace CarAssignmentFrameworkPart2
{
    /// <summary>
    /// This class can be considered "the world".
    /// Irovides a code interface between the
    /// user interface and the cars in the world.
    /// </summary>
    class WorldModel
    {
        /// <summary>
        /// The grid of tiles for the map.
        /// </summary>
        private MapTile[,] map;

        /// <summary>
        /// Stores the projectiles on the map
        /// </summary>
        private List<Projectile> _projectiles = new List<Projectile>();

        /// <summary>
        /// Gets all projectiles in the map
        /// </summary>
        public List<Projectile> Projectiles
        {
            get
            {
                return _projectiles;
            }
        }
        
        /// <summary>
        /// This is an important constant that must match
        /// the length of the MapTile enumeration.
        /// </summary>
        private const int NUMBER_OF_TILES_TYPES = 7;

        /// <summary>
        /// The car controlled by the user
        /// </summary>
        private Car _car;

        /// <summary>
        /// Get the car object in the world that is controlled by the user.
        /// </summary>
        public Car TheCar
        {
            get
            {
                return _car;
            }
        }

        /// <summary>
        /// The default projectile size for the world.
        /// </summary>
        private const int DEFAULT_PROJECTILE_SIZE = 5;
        
        /// <summary>
        /// The size of projectiles in the map
        /// </summary>
        private Size _projectileSize;

        /// <summary>
        /// Get or set the projectile size for the world.  The
        /// default projectile size is 5.  Any attempts to assign
        /// a non-positive projectile size will cause the tiles to be
        /// reset to this default.  
        /// </summary>
        public Size ProjectileSize
        {
            get
            {
                return _projectileSize;
            }
            set
            {
                // allow only positive tile sizes
                if (value.Height > 0 && value.Width > 0)
                {
                    _projectileSize = value;
                }
                else
                {
                    _projectileSize = new Size(DEFAULT_PROJECTILE_SIZE, DEFAULT_PROJECTILE_SIZE);
                }
            }
        }


        /// <summary>
        /// The default tile size for the world.
        /// </summary>
        private const int DEFAULT_TILE_SIZE = 20;

        /// <summary>
        /// The tile size in pixels for the world.
        /// </summary>
        private int _tileSize = DEFAULT_TILE_SIZE;

        /// <summary>
        /// Get or set the tile size for the world.  The
        /// default tilesize is 20.  Any attempts to assign
        /// a non-positive tile size will cause the tiles to be
        /// reset to this default.  
        /// </summary>
        public int TileSize
        {
            get
            {
                return _tileSize;
            }
            set
            {
                // allow only positive tile sizes
                if (value > 0)
                {
                    _tileSize = value;
                }
                else
                {
                    _tileSize = DEFAULT_TILE_SIZE;
                }
            }
        }

        /// <summary>
        /// Get the map 2D array for the world.
        /// </summary>
        public MapTile[,] Map
        {
            get
            {
                return map;
            }
        }

        /// <summary>
        /// Creates the world using the default world map.
        /// </summary>
        public WorldModel()
        {
            LoadMapString(Properties.Resources.Map1);
            _projectileSize = new Size(DEFAULT_PROJECTILE_SIZE, DEFAULT_PROJECTILE_SIZE);
        }

        /// <summary>
        /// Loads the map string.  See file header for
        /// string format
        /// </summary>
        /// <param name="mapString"></param>
        private void LoadMapString(string mapString)
        {
            // the number of rows and columns in the map
            int rows, cols;
            // split the map string into its rows
            string[] lines = mapString.Split(new char[] { '\n' });

            // if it is not an empty world
            if (lines.Length > 4)
            {
                string carType = lines[0];
                carType = carType.Trim();
                int.TryParse(lines[1], out rows);
                int.TryParse(lines[2], out cols);

                // invalid map size
                if (rows <= 0 || cols <= 0)
                {
                    return;
                }

                // create the world
                map = new MapTile[rows, cols];

                // load the different tiles into the world
                for (int r = 0; r < rows; r++)
                {
                    lines[r + 3] = lines[r + 3].Trim();
                    // the next row in the array
                    // offset by 3 because the first 3 elements in the
                    // array are the size of the world and car type
                    char[] currentLine = lines[r + 3].ToCharArray();

                    // get each tile in the row
                    for (int c = 0; c < cols; c++)
                    {
                        // convert the tile character to the integer
                        int val = (int)char.GetNumericValue(currentLine[c]);

                        // if a valid tile number
                        if (val < NUMBER_OF_TILES_TYPES && currentLine[c] != '\r' && currentLine[c] != '\n')
                        {
                            map[r, c] = (MapTile)val;

                            if (map[r, c] == MapTile.StartingLocation)
                            {
                                _car = CarFactory.MakeCar(carType, r, c, Direction.Up);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Load the map from a text file
        /// </summary>
        /// <param name="mapFile">The name of the map file.</param>
        private void LoadMap(string mapFile)
        {
            // Check that the file exists before attempting to open
            if (File.Exists(mapFile))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(mapFile))
                    {
                        LoadMapString(sr.ReadToEnd());
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Could not read the map file.");
                }
            }
        }

        /// <summary>
        /// Update the world projectiles
        /// </summary>
        public void Update()
        {
            MoveProjectiles();
        }

        /// <summary>
        /// Rotates aCar left.
        /// </summary>
        /// <param name="aCar">The car to rotate.</param>
        public void RotateLeft(Car aCar)
        {
            aCar.RotateLeft();
        }

        /// <summary>
        /// Add a projectile to the world.
        /// </summary>
        /// <param name="p">The projectile to add to the world.</param>
        public void AddProjectile(Projectile p)
        {
            _projectiles.Add(p);
        }

        /// <summary>
        /// Moves the projectiles that are on the map, and removes them
        /// if they are no longer visible or if they collide with a wall.
        /// </summary>
        private void MoveProjectiles()
        {
            for ( int i = _projectiles.Count - 1; i >= 0; i--)
            {
                Point p = _projectiles[i].Move();

                // Calculate which grid row/column the projectile is in
                int pColumn = p.X / TileSize;
                int pRow = p.Y / TileSize;

                // remove the projectile if off-screen
                if (pColumn <0 || pColumn >= map.GetLength(0) || pRow < 0 || pRow >= map.GetLength(1))
                {
                    _projectiles.Remove(_projectiles[i]);
                    return;
                }


                // Destroy walls and closed gates
                if (map[pRow, pColumn] == MapTile.Wall || map[pRow, pColumn] == MapTile.ClosedGate)
                {
                    // Destroy the projectile if it hits a wall
                    if (map[pRow, pColumn] == MapTile.Wall)
                    {
                        _projectiles.Remove(_projectiles[i]);
                    }

                    map[pRow, pColumn] = MapTile.Dirt;
                }
            }
        }

        /// <summary>
        /// Rotates aCar right.
        /// </summary>
        /// <param name="aCar">The car to rotate.</param>
        public void RotateRight(Car aCar)
        {
            aCar.RotateRight();
        }

        /// <summary>
        /// Moves aCar forwards.  It could crash if the tile
        /// in front is not a safe tile to move to.
        /// </summary>
        /// <param name="aCar">The car to move.</param>
        public void Move(Car aCar)
        {
            aCar.Move(GetTileInFront(aCar));
        }

        /// <summary>
        /// Moves aCar forwards.  It will not move if it is unsafe
        /// to do so.
        /// </summary>
        /// <param name="aCar">The car to move.</param>
        /// <returns>true if the car was able to move,
        /// false if it could not.</returns>
        public bool MoveSafely(Car aCar)
        {
            return aCar.MoveSafely(GetTileInFront(aCar));
        }

        
        private bool AutoMarking(DriveCommands command, MapTile desiredStartingTile, MapTile desiredEndingTile, Car aCar)
        {
            // should only work if the car is working and the
            // tile in front is the desired tile
            bool shouldItWork = !aCar.IsBroken && map[GetRowInFront(TheCar), GetColumnInFront(TheCar)] == desiredStartingTile;

            // perform the command to see if it works
            bool result = false;
            if (command == DriveCommands.CloseGate)
            {
                result = aCar.CloseGate(ref map[GetRowInFront(TheCar), GetColumnInFront(TheCar)]);
            }
            else if (command == DriveCommands.OpenGate)
            {
                result = aCar.OpenGate(ref map[GetRowInFront(TheCar), GetColumnInFront(TheCar)]);
            }
            else if (command == DriveCommands.DropDirt)
            {
                result = aCar.DropDirt(ref map[GetRowInFront(TheCar), GetColumnInFront(TheCar)]);
            }
            else if (command == DriveCommands.PickupDirt)
            {
                result = aCar.PickUpDirt(ref map[GetRowInFront(TheCar), GetColumnInFront(TheCar)]);
            }

            // car did as it should have
            if (result == shouldItWork)
            {
                // car didn't alter the world despite saying it did
                if (map[GetRowInFront(TheCar), GetColumnInFront(TheCar)] != desiredEndingTile)
                {
                    // modify world to continue test
                    //map[GetRowInFront(TheCar), GetColumnInFront(TheCar)] = desiredEndingTile;
                    // TAKE OFF MARK
                    TakeOffMark("Did not successfully " + ConvertDrivingCommand(command) + " at (" + GetRowInFront(aCar) + ", " + GetColumnInFront(aCar) + ")");
                }
            }
            // the car did not work as it should
            else
            {
                TakeOffMark("Should not be able to " + ConvertDrivingCommand(command) + " at (" + GetRowInFront(aCar) + ", " + GetColumnInFront(aCar) + ")");
            }

            return result;
        }

        private string ConvertDrivingCommand(DriveCommands command)
        {
            if (command == DriveCommands.CloseGate)
            {
                return " close gate";
            }
            else if (command == DriveCommands.OpenGate)
            {
                return " open gate";
            }
            else if (command == DriveCommands.PickupDirt)
            {
                return " pick up dirt";
            }
            else
            {
                return " drop dirt";
            }
        }

        private void TakeOffMark(string error)
        {
            using (System.IO.StreamWriter sw = new StreamWriter("marking.txt", true))
            {
                sw.WriteLine("-1 mark: " + error);
            }
        }

        /// <summary>
        /// Pick up dirt in front of the car.
        /// </summary>
        /// <param name="aCar">The car that will pick up dirt.</param>
        /// <returns>true if the car was able to pick up dirt,
        /// false if it could not.</returns>
        public bool PickUpDirt(Car aCar)
        {
            return AutoMarking(DriveCommands.PickupDirt, MapTile.Dirt, MapTile.Hole, TheCar);
        }

        /// <summary>
        /// Drop dirt in front of the car if it has some.
        /// </summary>
        /// <param name="aCar">The car that will drop dirt.</param>
        /// <returns>true if the car was able to drop dirt,
        /// false if it could not.</returns>        
        public bool DropDirt(Car aCar)
        {
            return AutoMarking(DriveCommands.DropDirt, MapTile.Hole, MapTile.Dirt, TheCar);
        }

        /// <summary>
        /// Open the gate in front of the car if there is one.
        /// </summary>
        /// <param name="aCar">The car that will open the gate.</param>
        /// <returns>true if the car was able to open the gate,
        /// false if it could not.</returns>        
        public bool OpenGate(Car aCar)
        {
            return AutoMarking(DriveCommands.OpenGate, MapTile.ClosedGate, MapTile.OpenGate, TheCar);
        }

        /// <summary>
        /// Open the gate in front of the car if there is one.
        /// </summary>
        /// <param name="aCar">The car that will open the gate.</param>
        /// <returns>true if the car was able to open the gate,
        /// false if it could not.</returns> 
        public bool CloseGate(Car aCar)
        {
            return AutoMarking(DriveCommands.CloseGate, MapTile.OpenGate, MapTile.ClosedGate, TheCar);
        }

        public bool SpecialMove(Car aCar)
        {
            return aCar.SpecialMove(this);
        }

        /// <summary>
        /// Get the tile that is in front of the car.
        /// </summary>
        /// <param name="aCar">The car to check with.</param>
        /// <returns>The MapTile in front of the car</returns>
        private MapTile GetTileInFront(Car aCar)
        {
            // Check which direction the car is facing, and
            // return the appropriate tile in front
            if (aCar.FacingDirection == Direction.Down)
            {
                return (map[aCar.Row + 1, aCar.Column]);
            }
            else if (aCar.FacingDirection == Direction.Up)
            {
                return (map[aCar.Row - 1, aCar.Column]);
            }
            else if (aCar.FacingDirection == Direction.Left)
            {
                return (map[aCar.Row, aCar.Column - 1]);
            }
            else if (aCar.FacingDirection == Direction.Right)
            {
                return (map[aCar.Row, aCar.Column + 1]);
            }
            return MapTile.Wall;
        }

        /// <summary>
        /// Get the number of the row in front of the car.
        /// If the car is facing left or right, it will be the
        /// same row.  If the car is facing up it will be the
        /// current row -1, and if facing down it will be +1
        /// </summary>
        /// <param name="aCar">The car to check with.</param>
        /// <returns>The row number in front of the car.</returns>
        private int GetRowInFront(Car aCar)
        {
            if (aCar.FacingDirection == Direction.Down)
            {
                return aCar.Row + 1;
            }
            else if (aCar.FacingDirection == Direction.Up)
            {
                return aCar.Row - 1;
            }
            return aCar.Row;
        }

        /// <summary>
        /// Get the number of the column in front of the car.
        /// If the car is facing up or down, it will be the
        /// same column.  If the car is facing left it will be the
        /// current row -1, and if facing rightit will be +1
        /// </summary>
        /// <param name="aCar">The car to check with.</param>
        /// <returns>The column number in front of the car.</returns>
        private int GetColumnInFront(Car aCar)
        {
            if (aCar.FacingDirection == Direction.Left)
            {
                return aCar.Column - 1;
            }
            else if (aCar.FacingDirection == Direction.Right)
            {
                return aCar.Column + 1;
            }
            return aCar.Column;
        }


        public enum DriveCommands
        {
            Move,
            RotateLeft,
            RotateRight,
            PickupDirt,
            DropDirt,
            OpenGate,
            CloseGate,
            SpecialMove
        }

        private Queue<DriveCommands> drivingCommands = new Queue<DriveCommands>();

        public void SetupDrivingRoute()
        {
            for (int i = 0; i < 3; i++) drivingCommands.Enqueue(DriveCommands.Move);
            drivingCommands.Enqueue(DriveCommands.RotateLeft);
            for (int i = 0; i < 12; i++) drivingCommands.Enqueue(DriveCommands.Move);
            drivingCommands.Enqueue(DriveCommands.RotateLeft);
            drivingCommands.Enqueue(DriveCommands.PickupDirt);
            drivingCommands.Enqueue(DriveCommands.RotateRight);
            drivingCommands.Enqueue(DriveCommands.DropDirt);
            for (int i = 0; i < 1; i++) drivingCommands.Enqueue(DriveCommands.Move);
            drivingCommands.Enqueue(DriveCommands.RotateRight);
            for (int i = 0; i < 5; i++) drivingCommands.Enqueue(DriveCommands.Move);
            drivingCommands.Enqueue(DriveCommands.OpenGate);
            for (int i = 0; i < 2; i++) drivingCommands.Enqueue(DriveCommands.Move);
            drivingCommands.Enqueue(DriveCommands.RotateRight);
            drivingCommands.Enqueue(DriveCommands.RotateRight);
            drivingCommands.Enqueue(DriveCommands.CloseGate);
            drivingCommands.Enqueue(DriveCommands.RotateLeft);
            for (int i = 0; i < 21; i++) drivingCommands.Enqueue(DriveCommands.Move);
        }

        public void MakeTank()
        {
            _car = CarFactory.MakeTank(_car.Row, _car.Column, _car.FacingDirection, _car.Colour);
        }

        public void MakeFlyingCar()
        {
            _car = CarFactory.MakeFlyingCar(_car.Row, _car.Column, _car.FacingDirection, _car.Colour);
        }

        public void MakeTunneler()
        {
            _car = CarFactory.MakeTunneler(_car.Row, _car.Column, _car.FacingDirection, _car.Colour);
        }

        public void MakeTeleporter()
        {
            _car = CarFactory.MakeTeleporter(_car.Row, _car.Column, _car.FacingDirection, _car.Colour);
        }

        public void DriveTheCar()
        {
            if (drivingCommands.Count > 0)
            {
                DriveCommands command = drivingCommands.Dequeue();
                
                if (command == DriveCommands.Move)
                {
                    TheCar.Move(map[GetRowInFront(TheCar), GetColumnInFront(TheCar)]);
                    
                }
                else if (command == DriveCommands.CloseGate)
                {
                    TheCar.CloseGate(ref map[GetRowInFront(TheCar), GetColumnInFront(TheCar)]);
                }
                else if (command == DriveCommands.OpenGate)
                {
                    TheCar.OpenGate(ref map[GetRowInFront(TheCar), GetColumnInFront(TheCar)]);
                }
                else if (command == DriveCommands.PickupDirt)
                {
                    TheCar.PickUpDirt(ref map[GetRowInFront(TheCar), GetColumnInFront(TheCar)]);
                }
                else if (command == DriveCommands.DropDirt)
                {
                    TheCar.DropDirt(ref map[GetRowInFront(TheCar), GetColumnInFront(TheCar)]);
                }
                else if (command == DriveCommands.RotateLeft)
                {
                    TheCar.RotateLeft();
                }
                else if (command == DriveCommands.RotateRight)
                {
                    TheCar.RotateRight();
                }
            }
        }
    }
}
