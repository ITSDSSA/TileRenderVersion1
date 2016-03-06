using Engine.Engines;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TileManagerNS;

namespace TileRenderv1
{
    class Level1 : DrawableGameComponent
    {
        ConfigureMaze level1Configuration; 
            
        public  Level1(Game g ):base(g)
        {
            g.Components.Add(this);
            Initialize();

        }

        public override void Initialize()
        {
            level1Configuration = new ConfigureMaze(Game);
            // Load the current tile sheet            
            GameAssets.TileSheets.Add("background", Game.Content.Load<Texture2D>(@"Tiles\tank tiles 64 x 64"));
            GameAssets._fonts.Add("debug", Game.Content.Load<SpriteFont>("message"));
            // Set up the tiles
            for (int x = 0; x < level1Configuration.Width; x++)
            {
                for (int y = 0; y < level1Configuration.Height; y++)
                {
                    TileRef tref = new TileRef(4, 3, 0);
                    new RenderTile(Game, "background",
                        new Tile()
                        {
                            Id = 0,
                            Passable = true,
                            X = x,
                            Y = y,
                            TileHeight = level1Configuration.TileSize.X,
                            TileWidth = level1Configuration.TileSize.Y,
                            TileName = "background",
                            TileRef = tref
                        });
                }
            }
            // impassable blue tiles
            // add a block of unpassable tiles
            level1Configuration.addBlock(false, "blue", 0, 0, level1Configuration.Width, 0, new TileRef(4, 2, 0));
            level1Configuration.addBlock(false, "blue", 0, 0, 0, level1Configuration.Height, new TileRef(4, 2, 0));
            // add four blue blocks across
            // add tiles in the shape of a rectangle, 
            // Gap, number in x direction, number in y direction, passable
            level1Configuration.addMultiBlocks(new Rectangle(5, 5, 8, 8), 2, 4, 4, false);
            //setPassableImage(new TileRef(5, 3, 0));
            //addMultiBlocks(new Rectangle(7, 25, 10, 29), 2, 4, 0, false);
            //addBlock(false, "blue", 10, 10, 20, 20, new TileRef(4, 2, 0));

            // TODO: use this.Content to load your game content here

            base.LoadContent();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create and add the Camera            
            Game.Services.AddService<Camera>(
                new Camera(Game, level1Configuration.ViewportCentre,
                        new Vector2(level1Configuration.Width * level1Configuration.TileSize.X,
                                    level1Configuration.Height * level1Configuration.TileSize.Y), GraphicsDevice.Viewport)
                                    {
                                        CamPos = Vector2.Zero,
                                    }
                );
            // Add input engine
            new InputEngine(Game);
        }
        public override void Update(GameTime gameTime)
        {
            Camera cam = Game.Services.GetService<Camera>();
            if (InputEngine.IsKeyHeld(Keys.D))
                cam.move(new Vector2(10, 0), Game.GraphicsDevice.Viewport);
            if (InputEngine.IsKeyHeld(Keys.A))
                cam.move(new Vector2(-10, 0), Game.GraphicsDevice.Viewport);
            if (InputEngine.IsKeyHeld(Keys.W))
                cam.move(new Vector2(0, -10), Game.GraphicsDevice.Viewport);
            if (InputEngine.IsKeyHeld(Keys.S))
                cam.move(new Vector2(0, 10), Game.GraphicsDevice.Viewport);
            if (InputEngine.IsKeyHeld(Keys.Space))
                changeImpassibleRender(new TileRef(0, 1, 0));
                base.Update(gameTime);
        }

        private void changeImpassibleRender(TileRef toTileRef)
        {
            List<Tile> Impassable = level1Configuration.getImpassableTiles();
            foreach (var item in Impassable)
            {
                item.TileRef = toTileRef;
            }

        }
    }
}
