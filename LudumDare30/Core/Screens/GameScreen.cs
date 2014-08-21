using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.Interpolations;
using se.skoggy.utils.Particles;
using se.skoggy.utils.Screens;
using se.skoggy.utils.Tweening.Stock;
using se.skoggy.utils.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Screens
{
    public class GameScreen : BaseScreen
    {
        Text text;
        SpriteFont font;
        ParticleManager particleManager;
        ParticleSystem[] explosions = new ParticleSystem[2];

        public GameScreen(IGameContext context)
            :base(context, "Ludum Dare 30", 1280, 720)
        {
            TransitionDuration = 2000f;
        }

        public override void Load()
        {
            font = content.Load<SpriteFont>(@"fonts/xirod_32");
            text = new Text("Ludum Dare 30", TextAlign.Center);

            Tween(new PositionTween(text, Interpolation.Exp10, TransitionDuration, new Vector2(Left.X, Top.Y), Center));
            Tween(new RotationTween(text, Interpolation.Pow5, TransitionDuration, -1f, 0f));
            Tween(new ScaleXYTween(text, Interpolation.Elastic, TransitionDuration, 0f, 1f));

            particleManager = new ParticleManager();
            particleManager.Load(content);

            ParticleSystemLoader loader = new ParticleSystemLoader(content, "effects");

            explosions[0] = new ParticleSystem(loader.Load("fire_blast"));
            explosions[1] = new ParticleSystem(loader.Load("fire_blast"));
            explosions[0].position.X = Left.X / 2f;
            explosions[1].position.X = Right.X / 2f;
            particleManager.AddSystem(explosions[0]);
            particleManager.AddSystem(explosions[1]);

            base.Load();
        }

        public override void StateChanged()
        {
            base.StateChanged();
            if (TransitioningOut) 
            {
            }

            if (Running) 
            {
                explosions[0].Play();
                explosions[1].Play();
            }
        }

        public override void Update(float dt)
        {
            particleManager.Update(dt);
            base.Update(dt);
        }

        public override void Draw()
        {
            base.Draw();
            context.GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, cam.Projection);
            text.Draw(spriteBatch, font);
            spriteBatch.End();

            particleManager.Draw(spriteBatch, cam);
        }
    }
}
