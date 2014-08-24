using Core.Conversations;
using Core.Gui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using se.skoggy.utils.GameObjects;
using se.skoggy.utils.Metrics;
using se.skoggy.utils.Particles;
using se.skoggy.utils.Screens;
using se.skoggy.utils.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Screens
{
    public class IntroScreen : BaseScreen
    {
        DrawableText text, spaceToSkipText;
        SpriteFont font;

        GameObject overlay, background;

        DialogueWindow dialogueWindow;

        ParticleManager particleManager;

        public IntroScreen(IGameContext context)
            : base(context, "Intro", Resolution.Width, Resolution.Height)
        {
        }

        public override void Load()
        {
            font = content.Load<SpriteFont>(@"fonts/xirod_32");

            spaceToSkipText = new DrawableText("space: skip", TextAlign.Right);
            spaceToSkipText.SetPosition(Right.X - 16f, Bottom.Y - 16f);
            spaceToSkipText.SetScale(0.6f);

            Texture2D pixel = new Texture2D(context.GraphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[]{ Color.White });
            
            string[] strings = JsonConvert.DeserializeObject<string[]>(File.ReadAllText(string.Concat(content.RootDirectory, "/dialogue/introScene.json")));

            dialogueWindow = new DialogueWindow(pixel, new Conversation(strings, 4000), 0, 0) 
            {
                DrawBubble = false
            };
            Audio.Audio.I.PlayLooped("menu_song");

            overlay = new GameObject(content.Load<Texture2D>(@"gfx/overlay"));
            background = new GameObject(content.Load<Texture2D>(@"gfx/background"));

            particleManager = new ParticleManager();
            particleManager.Load(content);

            var speckle = new ParticleSystem(new ParticleSystemLoader(content, "effects").Load("retro_speckle"));
            particleManager.AddSystem(speckle);
            speckle.Play();
            

            base.Load();
        }

        public override void StateChanged()
        {
            base.StateChanged();
            if (Done) 
            {
                context.ChangeScreen(new GameScreen(context));
            }
        }

        KeyboardState keys, oldKeys;

        public override void Update(float dt)
        {
            oldKeys = keys;
            keys = Keyboard.GetState();

            if (Running)
            {
                if (dialogueWindow.Done)
                {
                    TransitionOut();
                }
                else
                {
                    dialogueWindow.Update(dt);
                    if (keys.IsKeyDown(Keys.Space) && oldKeys.IsKeyUp(Keys.Space)) 
                    {
                        dialogueWindow.Next();
                    }
                }
            }

            particleManager.Update(dt);
            base.Update(dt);
        }

        public override void Draw()
        {
            base.Draw();

            context.GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, cam.Projection);
            background.Draw(spriteBatch);
            dialogueWindow.Draw(spriteBatch, font);
            spaceToSkipText.Draw(spriteBatch, font);
            spriteBatch.End();

            particleManager.Draw(spriteBatch, cam);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointClamp, null, null, null, cam.Projection);
            overlay.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
