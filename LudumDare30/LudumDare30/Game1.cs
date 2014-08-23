#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using se.skoggy.utils.Screens;
using Core.Bootstrap;
#endregion

namespace LudumDare30
{
    public class Game1 : Game, IGameContext
    {
        GraphicsDeviceManager graphics;

        IScreen currentScreen;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.Window.Title = "GLENN";
        }

        #region IGameContext

        public void ChangeScreen(IScreen screen)
        {
            screen.Load();
            currentScreen = screen;
        }

        public IServiceProvider ServiceProvider
        {
            get { return Services; }
        }

        public string ContentRoot
        {
            get { return "Content/assets"; }
        }

        public int Width
        {
            get { return GraphicsDevice.Viewport.Width; }
        }

        public int Height
        {
            get { return GraphicsDevice.Viewport.Height; }
        }

        #endregion

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            ChangeScreen(new DefaultScreenSelector(this).CreateDefaultScreen());
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            currentScreen.Update((float)gameTime.ElapsedGameTime.TotalMilliseconds);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);

            currentScreen.Draw();
        }
    }
}
