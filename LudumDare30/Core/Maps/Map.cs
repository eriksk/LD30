﻿using Core.Collision;
using Core.Screens;
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
        private CollisionGrid[] collisionGrids;
        private List<Door> doors;

        PlaneState planeState;

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

            /*
            Layer doorsLayer = GetDoorstLayer();
            doors = new List<Door>();
            foreach (var doorObject in doorsLayer.objects)
            {
                var cell = new Cell(GetColumn(doorObject.x), GetRow(doorObject.y));
                var door = new Door(doorObject.name, cell, GetDoorDirection(cell));
                doors.Add(door);
                if (doorObject.properties["flag"] == "true")
                {
                    door.Open(CollisionGrid);
                }
                else 
                {
                    door.Close(CollisionGrid);
                }
            }
             */

        }

        public Rectangle Goal
        {
            get
            {
                foreach (var l in tmxMap.layers)
                {
                    if (LayerTypes.Parse(l.type) == LayerType.ObjectLayer)
                    {
                        foreach (var obj in l.objects)
                        {
                            if (obj.name == "goal")
                            {
                                return new Rectangle((int)obj.x, (int)obj.y, (int)obj.width, (int)obj.height);
                            }
                        }
                    }
                }
                return Rectangle.Empty;
            }
        }

        private DoorType GetDoorDirection(Cell cell)
        {
            if (CollisionGrid[cell.col - 1, cell.row])
            {
                return DoorType.Horizontal;
            }
            return DoorType.Vertical;
        }

        public void SetPlaneState(PlaneState planeState)
        {
            this.planeState = planeState;
        }

        private int GetColumn(float x)
        {
            return (int)(x / 32);
        }

        private int GetRow(float y)
        {
            return (int)(y / 32);
        }

        public CollisionGrid CollisionGrid 
        {
            get
            {
                if (collisionGrids == null)
                    collisionGrids = CreateCollisionGrids();
                return collisionGrids[planeState == PlaneState.Positive ? 0 : 1];
            }
        }

        private CollisionGrid[] CreateCollisionGrids()
        {
            CollisionGrid[] grids = new CollisionGrid[2];
            foreach (var l in tmxMap.layers)
            {
                if (l.name == "positive") 
                {
                    grids[0] = new CollisionGrid(l.data, l.width, l.height);
                }
                if (l.name == "negative") 
                {
                    grids[1] = new CollisionGrid(l.data, l.width, l.height);
                }
            }
            return grids;
        }

        public Vector2 Center { get { return new Vector2((tmxMap.width * tmxMap.tilewidth) / 2, (tmxMap.height * tmxMap.tileheight) / 2); } }

        public Vector2 StartPosition 
        {
            get 
            {
                foreach (var l in tmxMap.layers)
                {
                    if (LayerTypes.Parse(l.type) == LayerType.ObjectLayer) 
                    {
                        foreach (var obj in l.objects)
                        {
                            if (obj.name == "start") 
                            {
                                return new Vector2(obj.x, obj.y);
                            }
                        }
                    }                    
                }
                return Vector2.Zero;
            }
        }

        public void Update(float dt)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var layer in tmxMap.layers)
            {
                if (LayerTypes.Parse(layer.type) == LayerType.TileLayer)
                {
                    if (layer.name == "positive" || layer.name == "negative")
                    {
                        if ((layer.name == "positive" && planeState == PlaneState.Positive) || (layer.name == "negative" && planeState == PlaneState.Negative))
                            DrawLayer(spriteBatch, layer);
                    }
                    else 
                    {
                        DrawLayer(spriteBatch, layer);
                    }
                }
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
