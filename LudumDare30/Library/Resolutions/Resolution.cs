using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Resolutions
{
    class Resolution
    {
        private GraphicsDeviceManager graphicsDeviceManager = null;

        private int width = 800;
        private int height = 600;
        private int virtualWidth = 1024;
        private int virtualHeight = 768;
        private Matrix scaleMatrix;
        private bool isFullScreen = false;
        private bool dirty = true;

        public void Init(GraphicsDeviceManager device)
        {
            width = device.PreferredBackBufferWidth;
            height = device.PreferredBackBufferHeight;
            graphicsDeviceManager = device;
            dirty = true;
            ApplyResolutionSettings();
        }


        public Matrix getTransformationMatrix()
        {
            if (dirty) RecreateScaleMatrix();

            return scaleMatrix;
        }

        public void SetResolution(int Width, int Height, bool FullScreen)
        {
            width = Width;
            height = Height;

            isFullScreen = FullScreen;

            ApplyResolutionSettings();
        }

        public void SetVirtualResolution(int Width, int Height)
        {
            virtualWidth = Width;
            virtualHeight = Height;

            dirty = true;
        }

        private void ApplyResolutionSettings()
        {
            // If we aren't using a full screen mode, the height and width of the window can
            // be set to anything equal to or smaller than the actual screen size.
            if (isFullScreen == false)
            {
                if ((width <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width)
                    && (height <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height))
                {
                    graphicsDeviceManager.PreferredBackBufferWidth = width;
                    graphicsDeviceManager.PreferredBackBufferHeight = height;
                    graphicsDeviceManager.IsFullScreen = isFullScreen;
                    graphicsDeviceManager.ApplyChanges();
                }
            }
            else
            {
                // If we are using full screen mode, we should check to make sure that the display
                // adapter can handle the video mode we are trying to set.  To do this, we will
                // iterate through the display modes supported by the adapter and check them against
                // the mode we want to set.
                foreach (DisplayMode dm in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
                {
                    // Check the width and height of each mode against the passed values
                    if ((dm.Width == width) && (dm.Height == height))
                    {
                        // The mode is supported, so set the buffer formats, apply changes and return
                        graphicsDeviceManager.PreferredBackBufferWidth = width;
                        graphicsDeviceManager.PreferredBackBufferHeight = height;
                        graphicsDeviceManager.IsFullScreen = isFullScreen;
                        graphicsDeviceManager.ApplyChanges();
                    }
                }
            }

            dirty = true;

            width = graphicsDeviceManager.PreferredBackBufferWidth;
            height = graphicsDeviceManager.PreferredBackBufferHeight;
        }

        /// <summary>
        /// Sets the device to use the draw pump
        /// Sets correct aspect ratio
        /// </summary>
        public void BeginDraw()
        {
            // Start by reseting viewport to (0,0,1,1)
            FullViewport();
            // Clear to Black
            graphicsDeviceManager.GraphicsDevice.Clear(Color.Black);
            // Calculate Proper Viewport according to Aspect Ratio
            ResetViewport();
            // and clear that
            // This way we are gonna have black bars if aspect ratio requires it and
            // the clear color on the rest
            graphicsDeviceManager.GraphicsDevice.Clear(Color.CornflowerBlue);
        }

        private void RecreateScaleMatrix()
        {
            dirty = false;
            scaleMatrix = Matrix.CreateScale(
                           (float)graphicsDeviceManager.GraphicsDevice.Viewport.Width / virtualWidth,
                           (float)graphicsDeviceManager.GraphicsDevice.Viewport.Width / virtualWidth,
                           1f);
        }


        public void FullViewport()
        {
            Viewport vp = new Viewport();
            vp.X = vp.Y = 0;
            vp.Width = width;
            vp.Height = height;
            graphicsDeviceManager.GraphicsDevice.Viewport = vp;
        }

        /// <summary>
        /// Get virtual Mode Aspect Ratio
        /// </summary>
        /// <returns>aspect ratio</returns>
        public float getVirtualAspectRatio()
        {
            return (float)virtualWidth / (float)virtualHeight;
        }

        public void ResetViewport()
        {
            float targetAspectRatio = getVirtualAspectRatio();
            // figure out the largest area that fits in this resolution at the desired aspect ratio
            int width = graphicsDeviceManager.PreferredBackBufferWidth;
            int height = (int)(width / targetAspectRatio + .5f);
            bool changed = false;

            if (height > graphicsDeviceManager.PreferredBackBufferHeight)
            {
                height = graphicsDeviceManager.PreferredBackBufferHeight;
                // PillarBox
                width = (int)(height * targetAspectRatio + .5f);
                changed = true;
            }

            // set up the new viewport centered in the backbuffer
            Viewport viewport = new Viewport();

            viewport.X = (graphicsDeviceManager.PreferredBackBufferWidth / 2) - (width / 2);
            viewport.Y = (graphicsDeviceManager.PreferredBackBufferHeight / 2) - (height / 2);
            viewport.Width = width;
            viewport.Height = height;
            viewport.MinDepth = 0;
            viewport.MaxDepth = 1;

            if (changed)
            {
                dirty = true;
            }

            graphicsDeviceManager.GraphicsDevice.Viewport = viewport;
        }

    }
}
