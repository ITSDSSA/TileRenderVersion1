using Path_AI;
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
    class ConfigureMaze : DrawableGameComponent
    {
        int width = 30;
        int height = 30;
        Point tileSize = new Point(64, 64);
        public Vector2 ViewportCentre
        {
            get
            {
                return new Vector2(GraphicsDevice.Viewport.Width / 2,
                GraphicsDevice.Viewport.Height / 2);
            }
        }

        public int Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
            }
        }

        public int Height
        {
            get
            {
                return height;
            }

            set
            {
                height = value;
            }
        }

        public Point TileSize
        {
            get
            {
                return tileSize;
            }

            set
            {
                tileSize = value;
            }
        }

        // Added as a game component so that it can have access to the components
        public ConfigureMaze(Game g) : base(g)
        {
            g.Components.Add(this);
            //LoadContent();
            Initialize();
        }

        public void addMultiBlocks(Rectangle TileBound, int BlockGap, int xTimes, int yTimes, bool pass )
        {
            //int gap = 2; // gap of two tiles
            int x1 = TileBound.X;
            int x2 = TileBound.Width;
            int y1 = TileBound.Y;
            int y2 = TileBound.Height;
            //int step = 0;
            for (int x = 1; x < xTimes; x++)
            {
                addBlock(false, "blue", x1, y1, x2, y2, new TileRef(4, 2, 0));
                for (int y = 1; y < yTimes; y++)
                {
                    addBlock(false, "blue", x1, y1, x2, y2, new TileRef(4, 2, 0));
                    int ystep = y2 + 1 - y1 + BlockGap;
                    y1 += ystep;
                    y2 += ystep;
                }
                y1 = TileBound.Y;
                y2 = TileBound.Height;
                int xstep = x2 + 1 - x1 + BlockGap;
                x1 += xstep;
                x2 += xstep;
            }

        }

        public void addBlock(bool state, string tileName, int xstart, int ystart, int xend, int yend, TileRef t)
        {
            // Find the renderTiles that match the criterea
            List<RenderTile> renderTiles = (Game.Components.OfType<RenderTile>())
                                            .Where(rt => rt.Tile.X >= xstart &&
                                                    rt.Tile.Y >= ystart &&
                                                    rt.Tile.X <= xend && 
                                                    rt.Tile.Y <= yend).ToList();
            // set their state and name
            foreach (var rtile in renderTiles)
            {
                rtile.Tile.Passable = state;
                rtile.Tile.TileRef = t;
                rtile.Tile.TileName = tileName;
            }
            
        }

        public void setPassableImage(TileRef cell)
        {
            List<RenderTile> renderTiles = (Game.Components.OfType<RenderTile>())
                                .Where(rtile => rtile.Tile.Passable).ToList();
            foreach (RenderTile r in renderTiles)
            {
                r.Tile.TileRef = cell;
            }
        }

        public void replaceByName(Tile t1, Tile t2)
        {
            RenderTile renderTile = (Game.Components.OfType<RenderTile>())
                                        .Where(t => t.Tile.TileName == t1.TileName).FirstOrDefault();
            if(renderTile != null)
                renderTile.Tile = t2;
        }

        public void replaceByLocation(int x, int y, Tile t2)
        {
            RenderTile renderTile = (Game.Components.OfType<RenderTile>())
                                        .Where(t => t.Tile.X == x && 
                                                t.Tile.Y == y).FirstOrDefault();
            if (renderTile != null)
                renderTile.Tile = t2;
        }

        // get all the tiles from the render tiles in a level
        public List<Tile> getTiles()
        {
            List<RenderTile> renderTiles = (Game.Components.OfType<RenderTile>()).ToList();
            return renderTiles.Select(t => t.Tile).ToList();
        }

        public List<Tile> getPassableTiles()
        {
            List<RenderTile> renderTiles = (Game.Components.OfType<RenderTile>()).ToList();
            return renderTiles
                .Where( t => t.Tile.Passable)
                .Select(t => t.Tile).ToList();
        }

        public List<Tile> getImpassableTiles()
        {
            List<RenderTile> renderTiles = (Game.Components.OfType<RenderTile>()).ToList();
            return renderTiles
                .Where(t => t.Tile.Passable == false)
                .Select(t => t.Tile).ToList();
        }

        // To be completed
        public void MakeMaze()
        {
            Random r = new Random();

            List<Tile> tiles = getTiles();

            Tile StartTile = tiles[r.Next(0,tiles.Count -1)]; 


            Stack<Tile> visited = new Stack<Tile>();
            StartTile.Passable = true;
            visited.Push(StartTile);
            PathFinder.GenerateMaze(new List<Tile>() { StartTile }, visited, tiles, StartTile);

            //List<Tile> adj = PathFinder.adjacentTiles(tiles, StartTile);

        }
    }
}
