using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TileManagerNS;

namespace TileRenderv1
{
    class RenderTile : DrawableGameComponent
    {
        Tile _tile;
        Rectangle _bound;
        string _sheetName;
        Rectangle _source;
        bool debug = true;
        public Rectangle Bound
        {
            get
            {
                return new Rectangle(Tile.X * Tile.TileWidth,Tile.Y * Tile.TileHeight,Tile.TileWidth,Tile.TileHeight);
            }

            set
            {
                _bound = value;
            }
        }

        public Rectangle Source
        {
            get
            {
                return new Rectangle(Tile.TileRef.SheetPosX * Tile.TileWidth,
                    Tile.TileRef.SheetPosY * Tile.TileHeight, Tile.TileWidth, Tile.TileHeight);
            }

            set
            {
                _source = value;
            }
        }

        public Tile Tile
        {
            get
            {
                return _tile;
            }

            set
            {
                _tile = value;
            }
        }

        public RenderTile(Game g, string TileSheetName, Tile t) : base(g)
        {
            g.Components.Add(this);
            Tile = t;
            _sheetName = TileSheetName;

        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            Camera Cam = Game.Services.GetService<Camera>();
            if (Cam != null)
            {
                GameAssets.spriteBatch.Begin(SpriteSortMode.Immediate,
                            BlendState.AlphaBlend, null, null, null, null,
                                Cam.CurrentCameraTranslation);

                GameAssets.spriteBatch.Draw(GameAssets.TileSheets[_sheetName], Bound, Source, Color.White);
                if(debug)
                    GameAssets.spriteBatch.DrawString(GameAssets._fonts["debug"], Tile.X.ToString() + " , " + Tile.Y.ToString(), new Vector2(Bound.X, Bound.Y), Color.White);
                GameAssets.spriteBatch.End();
            }
            base.Draw(gameTime);
        }
    }
}
