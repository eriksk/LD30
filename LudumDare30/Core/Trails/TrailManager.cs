using Core.Characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.GameObjects;
using se.skoggy.utils.Management;
using se.skoggy.utils.Metrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Trails
{
    public class TrailManager
    {
        TimerTrig driftSpawnTrig;
        GameObject template;

        Pool<Trail> trails;

        public TrailManager()
        {
            driftSpawnTrig = new TimerTrig(40f);
        }


        public void Load(ContentManager content) 
        {
            template = new GameObject(content.Load<Texture2D>(@"gfx/trail"));
            template.color = Color.Black;
            template.color.A = 50;
            trails = new Pool<Trail>(512);
        }

        public void Clear() 
        {
            trails.Clear();
        }

        private void SpawnTrail(Vector2 position, float rotation)
        {
            Trail trail = trails.Pop();
            trail.x = position.X;
            trail.y = position.Y;
            trail.rotation = rotation;
            trail.current = 0;
            trail.duration = 3000;
        }

        public void Update(float dt, Character car) 
        {
            if (car.IsDrifting && driftSpawnTrig.IsTrigged(dt)) 
            {
                float rotation = car.rotation;
                SpawnTrail(car.position, rotation);
            }


            for (int i = 0; i < trails.Count; i++)
            {
                Trail t = trails[i];
                t.current += dt;
                if (t.current >= t.duration) 
                {
                    trails.Push(i);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            for (int i = 0; i < trails.Count; i++)
            {
                Trail t = trails[i];
                template.position.X = t.x;
                template.position.Y = t.y;
                template.rotation = t.rotation;
                template.Draw(spriteBatch);
            }
        }
    }
}
