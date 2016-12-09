using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarAssignmentFrameworkPart2
{
    public partial class MapViewForm : Form
    {
        // World model
        WorldModel world;

        // should the initial text be drawn on the screen
        bool drawInitialText = true;

        // Game font
        Font font = new Font("Arial", 14);

        // The size of each tile
        Size tileSize;

        // the size of each projectile
        SizeF projectileSize;

        Car theCar;

        public MapViewForm()
        {
            InitializeComponent();
            world = new WorldModel();
            theCar = world.TheCar;
            projectileSize = world.ProjectileSize;
            tileSize = new Size(world.TileSize, world.TileSize);
        }

        private void MapViewForm_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void DrawMap()
        {   
            int numRows = world.Map.GetLength(0);
            int numCols = world.Map.GetLength(1);
            MapTile[,] map = world.Map;

            ClientSize = new Size(numCols * world.TileSize, numRows * world.TileSize);
            
            // Create backbuffer for double-buffered graphics
            BufferedGraphics bg = BufferedGraphicsManager.Current.Allocate(this.CreateGraphics(), this.DisplayRectangle);

            for ( int row = 0; row < numRows; row++)
            {
                for ( int col = 0; col < numCols; col++)
                {
                    Rectangle tile = new Rectangle(col * tileSize.Width, row * tileSize.Height, tileSize.Width, tileSize.Height);
                    switch (map[row, col] )
                    {
                        case MapTile.Wall:
                            bg.Graphics.FillRectangle(Brushes.Gray, tile);
                            break;
                        case MapTile.Dirt:  
                            bg.Graphics.FillRectangle(Brushes.Brown, tile);
                            break;
                        case MapTile.ClosedGate:
                            bg.Graphics.FillRectangle(Brushes.Yellow, tile);
                            bg.Graphics.DrawString("X", font, Brushes.Black, tile);
                            break;
                        case MapTile.Hole:
                            bg.Graphics.FillRectangle(Brushes.Brown, tile);
                            bg.Graphics.FillEllipse(Brushes.Black, tile);
                            break;
                        case MapTile.StartingLocation:
                            bg.Graphics.FillRectangle(Brushes.Green, tile);
                            break;
                        case MapTile.EndingLocation:
                            bg.Graphics.FillRectangle(Brushes.Red, tile);
                            break;
                        case MapTile.OpenGate:
                            bg.Graphics.FillRectangle(Brushes.Yellow, tile);
                            break;
                    }
                    
                    bg.Graphics.DrawRectangle(Pens.Black, tile);
                }
            }

            Point[] carPoints = CreateCarGraphic(world.TheCar);
            bg.Graphics.FillPolygon(Brushes.DarkSalmon, carPoints);

            if (world.TheCar.IsBroken)
            {
                Rectangle tile = new Rectangle(theCar.Column * tileSize.Width, theCar.Row * tileSize.Height, tileSize.Width, tileSize.Height);
                bg.Graphics.DrawString("X", font, Brushes.Black, tile);
            }

            // Draw the world's projectiles
            int numProjectiles = world.Projectiles.Count;
            for ( int i =0; i < numProjectiles; i++)
            {
                bg.Graphics.FillEllipse(Brushes.Black, new RectangleF(world.Projectiles[i].Location.X, world.Projectiles[i].Location.Y, projectileSize.Width, projectileSize.Height));
            }


            bg.Render();
            bg.Dispose();
        }

        /// <summary>
        /// Draw the car's graphics
        /// </summary>
        /// <param name="aCar"></param>
        /// <returns></returns>
        private Point[] CreateCarGraphic(Car aCar)
        {
            Point[] carPoints = new Point[3];
            int carRow = world.TheCar.Row;
            int carColumn = world.TheCar.Column;
            int carX = carColumn * tileSize.Width;
            int carY = carRow * tileSize.Height;

            if ( aCar.FacingDirection == Direction.Up)
            {
                carPoints[0] = new Point(carX + tileSize.Width / 2, carY);
                carPoints[1] = new Point(carX, carY + tileSize.Height);
                carPoints[2] = new Point(carX + tileSize.Width, carY + tileSize.Height);
            }
            else if (aCar.FacingDirection == Direction.Down)
            {
                carPoints[0] = new Point(carX + tileSize.Width / 2, carY + tileSize.Height);
                carPoints[1] = new Point(carX, carY);
                carPoints[2] = new Point(carX + tileSize.Width, carY);
            }
            else if (aCar.FacingDirection == Direction.Left)
            {
                carPoints[0] = new Point(carX, carY + tileSize.Height / 2);
                carPoints[1] = new Point(carX + tileSize.Width, carY);
                carPoints[2] = new Point(carX + tileSize.Width, carY + tileSize.Height);
            }
            else
            {
                carPoints[0] = new Point(carX + tileSize.Width, carY + tileSize.Height / 2);
                carPoints[1] = new Point(carX, carY);
                carPoints[2] = new Point(carX, carY + tileSize.Height);
            }

            return carPoints;
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            DrawInitialText(e);
        }

        // Draw the help text
        private void DrawInitialText(PaintEventArgs e)
        {
            if (drawInitialText)
            {
                e.Graphics.DrawString("Press any key to start...", font, Brushes.Black, 0, 0);
                e.Graphics.DrawString("Legend:", font, Brushes.Black, 0, 20);
                e.Graphics.DrawString("Rotate with the Left and Right arrows.", font, Brushes.Black, 0, 40);
                e.Graphics.DrawString("Move without checking with the Space Bar.", font, Brushes.Black, 0, 60);
                e.Graphics.DrawString("Move safely with the Up arrow.", font, Brushes.Black, 0, 80);
                e.Graphics.DrawString("Pick up dirt in front of you with A.", font, Brushes.Black, 0, 100);
                e.Graphics.DrawString("Drop dirt (if you have it) in front of you with D.", font, Brushes.Black, 0, 120);
                e.Graphics.DrawString("Open a gate in front of you with W.", font, Brushes.Black, 0, 140);
                e.Graphics.DrawString("Close a gate in front of you with S.", font, Brushes.Black, 0, 160);
                e.Graphics.DrawString("To start the car's self driving mode, press F10.", font, Brushes.Black, 0, 180);
                e.Graphics.DrawString("To do your car's special move, press M.", font, Brushes.Black, 0, 200);
                e.Graphics.DrawString("To change your car to a Flying Car, press 1.", font, Brushes.Black, 0, 220);
                e.Graphics.DrawString("To change your car to a Tank, press 2.", font, Brushes.Black, 0, 240);
                e.Graphics.DrawString("To change your car to a Teleporter, press 3.", font, Brushes.Black, 0, 260);
                e.Graphics.DrawString("To change your car to a Tunneler, press 4.", font, Brushes.Black, 0, 280);
            }
        }

        // Allow user input
        private void MapViewForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (!drawInitialText)
            {
                if (e.KeyCode == Keys.Left)
                {
                    world.RotateLeft(theCar);
                }
                else if (e.KeyCode == Keys.Right)
                {
                    world.RotateRight(theCar);
                }
                else if (e.KeyCode == Keys.Up)
                {
                    world.MoveSafely(theCar);
                }
                else if (e.KeyCode == Keys.Space)
                {
                    world.Move(theCar);
                }
                else if (e.KeyCode == Keys.A)
                {
                    world.PickUpDirt(theCar);
                }
                else if (e.KeyCode == Keys.D)
                {
                    world.DropDirt(theCar);
                }
                else if (e.KeyCode == Keys.W)
                {
                    world.OpenGate(theCar);
                }
                else if (e.KeyCode == Keys.S)
                {
                    world.CloseGate(theCar);
                }
                else if (e.KeyCode == Keys.M)
                {
                    world.SpecialMove(theCar);
                }
                else if (e.KeyCode == Keys.D1 || e.KeyCode == Keys.NumPad1)
                {
                    world.MakeFlyingCar(ref theCar);
                }
                else if (e.KeyCode == Keys.D2 || e.KeyCode == Keys.NumPad2)
                {
                    world.MakeTank(ref theCar);
                }
                else if (e.KeyCode == Keys.D3 || e.KeyCode == Keys.NumPad3)
                {
                    world.MakeTeleporter(ref theCar);
                }
                else if (e.KeyCode == Keys.D4 || e.KeyCode == Keys.NumPad4)
                {
                    world.MakeTunneler(ref theCar);
                }
                else if (e.KeyCode == Keys.F10)
                {
                    world.SetupDrivingRoute();
                    tmrAutodrive.Start();
                }
                
                DrawMap();

                if (e.KeyCode == Keys.F1)
                {
                    drawInitialText = true;
                    Refresh();
                }
            }
            else
            {
                drawInitialText = false;
                DrawMap();
                tmrProjectiles.Start();
            }
        }

        private void tmrAutodrive_Tick(object sender, EventArgs e)
        {
            world.DriveTheCar();
            DrawMap();
        }

        private void tmrProjectiles_Tick(object sender, EventArgs e)
        {
            world.Update();
            DrawMap();
        }
    }
}
