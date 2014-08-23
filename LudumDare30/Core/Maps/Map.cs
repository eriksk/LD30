using Core.TMX;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Maps
{
    public class Map
    {
        TmxMapData tmxMap;
        Texture2D tiles;
        Rectangle[] sources;

        public Map(TmxMapData tmxMap) 
        {
            this.tmxMap = tmxMap;
        }

        public void Load(ContentManager content) 
        {
            foreach (var tileset in tmxMap.tilesets)
            {
                tiles = content.Load<Texture2D>(@"gfx/" + tileset.name);
            }

            SpriteSheet spriteSheet = new SpriteSheet(tiles.Width, tiles.Height, 32, 32);

            sources = new Rectangle[(tiles.Width / 32) * (tiles.Height / 32)];
            for (int col = 0; col < (tiles.Width / 32); col++)
            {
                for (int row = 0; row < (tiles.Height / 32); row++)
                {
                    sources[col + row * (tiles.Width / 32)] = new Rectangle(col * 32, row * 32, 32, 32);   
                }                
            }
        }

        public Vector2 Center { get { return new Vector2((tmxMap.width * tmxMap.tilewidth) / 2, (tmxMap.height * tmxMap.tileheight) / 2); } }

        public void Update(float dt)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var layer in tmxMap.layers)
            {
                DrawLayer(spriteBatch, layer);
            }
        }

        private void DrawLayer(SpriteBatch spriteBatch, Layer layer)
        {
            Color color = Color.White * layer.opacity;
            color.A = 255;

            for (int col = 0; col < layer.width; col++)
            {
                for (int row = 0; row < layer.height; row++)
                {
                    int cell = layer.data[col + row * layer.width];
                    if (cell > 0) 
                    {
                        spriteBatch.Draw(tiles, new Rectangle(col * 32, row * 32, 32, 32), sources[cell - 1], color, 0, Vector2.Zero, SpriteEffects.None, 0);
                    }
                }
            }
        }
    }
}
