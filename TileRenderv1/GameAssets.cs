using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TileManagerNS;

namespace TileRenderv1
{
    public struct TileIndex {
        int X;
        int y;
            }

    public static class GameAssets
    {
        public static Dictionary<string, Texture2D> TileSheets = new Dictionary<string, Texture2D>();
        public static SpriteBatch spriteBatch;
        public static Dictionary<TileIndex, Tile> _passable = new Dictionary<TileIndex, Tile>();
        public static Dictionary<TileIndex, Tile> _nonPassable = new Dictionary<TileIndex, Tile>();
        public static Dictionary<string, SpriteFont> _fonts = new Dictionary<string, SpriteFont>();


    }
}
