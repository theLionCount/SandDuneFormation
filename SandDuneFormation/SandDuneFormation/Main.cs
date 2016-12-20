using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;

namespace SandDuneFormation
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Main : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Effect rendererEffect, animatorEffect;
        HeightMapRenderer renderer;
        HeightMapAnimator animator;
        Camera camera;
        Texture2D map;
        int frameRate = 0;
        int frameCounter = 0;
        TimeSpan elapsedTime = TimeSpan.Zero;
        bool spaceDown, nDown, first = true, eDown, start = false, enter = false;
        ControlForm cf;
        object loadLock = new object();
        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            cf = new ControlForm(this);
            cf.Show();
        }

        Texture2D loadVectorTexture(string name, bool isfile=false)
        {
            Texture2D it;
            FileStream stream = null;
            if (!isfile)
                it = Content.Load<Texture2D>(name);
            else
            {
                stream = new FileStream(name, FileMode.Open);
                it = Texture2D.FromStream(graphics.GraphicsDevice,stream);
            }
            Texture2D ot = new Texture2D(graphics.GraphicsDevice, it.Width, it.Height, false, SurfaceFormat.Vector4);
            Color[] cdata = new Color[it.Width * it.Height];
            Vector4[] vdata = new Vector4[it.Width * it.Height];
            it.GetData<Color>(cdata);
            for (int i = 0; i < it.Width * it.Height; i++)
                vdata[i] = new Vector4(cdata[i].R / 255.0f, cdata[i].G / 255.0f, cdata[i].B / 255.0f, cdata[i].A / 255.0f);
            ot.SetData<Vector4>(vdata);
            it.Dispose();
            if (stream!=null)
                stream.Close();
            
            return ot;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            camera = new Camera(new Vector3(0, 0, 0),Vector3.Forward,Vector3.Up,(float)Math.PI/2,800.0f/600.0f);
            rendererEffect = Content.Load<Effect>("HeightMapRendererEffect");
            animatorEffect = Content.Load<Effect>("HeightMapAnimatorEffect");
            map = loadVectorTexture("hm4");
            renderer = new HeightMapRenderer(Matrix.Identity, map, Content.Load<Texture2D>("720"), 512, 512, 1, 16,rendererEffect, graphics.GraphicsDevice);
            animator = new HeightMapAnimator(graphics, spriteBatch, 512, 512, renderer, new Vector3(3.732f, 1, 0));//2.639f, 1, 2.639f));//3.732f, 1, 0));//
            animator.setWind(new Vector2(4,0));
            
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            camera.update(gameTime.ElapsedGameTime.Milliseconds);

            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime > TimeSpan.FromSeconds(1))
            {
                elapsedTime -= TimeSpan.FromSeconds(1);
                frameRate = frameCounter;
                Window.Title = "sanddunes at " + frameRate.ToString() + " fps"; 
                frameCounter = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && !enter)
            {
                enter = true;
                try
                {
                    cf.Show();
                }
                catch (Exception ex)
                {
                    cf = new ControlForm(this);
                    cf.Show();
                }
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Enter))
            {
                enter = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                spaceDown = true;
            else if (spaceDown)
            {
                spaceDown = false;
                map.SaveAsPng(new FileStream("outmap.png", FileMode.Create), map.Width, map.Height);
                animator.translateTarget.SaveAsPng(new FileStream("outmaptrans.png", FileMode.Create), map.Width, map.Height);
                animator.endTarget.SaveAsPng(new FileStream("outmapend.png", FileMode.Create), map.Width, map.Height);
                animator.shadowTexture.SaveAsPng(new FileStream("outshadow.png", FileMode.Create), animator.shadowTexture.Width, animator.shadowTexture.Height);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.E))
                eDown = true;
            else if (eDown)
            {
                eDown = false;
                start=!start;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 

        protected override void Draw(GameTime gameTime)
        {
            lock (loadLock)
            {
                frameCounter++;

                if (Keyboard.GetState().IsKeyDown(Keys.N))
                    nDown = true;
                else if (nDown || first)
                {
                    first = false;
                    nDown = false;
                    animator.applyAnimation(animatorEffect, ref map, rendererEffect);
                }
                if (start)
                    animator.applyAnimation(animatorEffect, ref map, rendererEffect);

                GraphicsDevice.Clear(Color.CornflowerBlue);

                rendererEffect.Parameters["View"].SetValue(camera.View);
                rendererEffect.Parameters["Projection"].SetValue(camera.Projection);
                renderer.DrawColors(graphics.GraphicsDevice, map, animator.shadowTarget, animatorEffect, spriteBatch);

                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Opaque, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null);
                spriteBatch.Draw(animator.shadowTexture, new Rectangle(10, 110, 100, 100), Color.White);
                spriteBatch.Draw(map, new Rectangle(10, 10, 100, 100), Color.White);
                spriteBatch.Draw(animator.translateTarget, new Rectangle(10, 220, 100, 100), Color.White);
                spriteBatch.End();

                base.Draw(gameTime);
            }
        }

        public void loadNewHeightMap(string fileName)
        {
            lock (loadLock)
            {
                map = loadVectorTexture(fileName,true);//loadVectorTexture(fileName,true);
                renderer = new HeightMapRenderer(Matrix.Identity, map, Content.Load<Texture2D>("720"), 512, 512, 1, 16, rendererEffect, graphics.GraphicsDevice);
                animator = new HeightMapAnimator(graphics, spriteBatch, 512, 512, renderer, new Vector3(3.732f, 1, 0));//2.639f, 1, 2.639f));//3.732f, 1, 0));//
                first = true;
                start = false;
                animator.setWind(new Vector2(4,0));
            }
        }

        public void setWindDir(Vector2 w)
        {
            animator.setWind(w);
        }

        public void setConditions(float mheight, float cheight, float avheight)
        {
            lock (loadLock)
            {
                renderer.MaxHeight = mheight;
                animator.sandheight = cheight;
                animator.avalancheconditionheight = avheight;
            }
        }

        public void Pause()
        {
            start = false;
        }

        public void Resume()
        {
            start = true;
        }

        public void saveHMap(string FileName)
        {
            lock (loadLock)
            {
                start = false;
                map.SaveAsPng(new FileStream(FileName, FileMode.Create), map.Width, map.Height);
                start = true;
            }
        }

        public void setBlur(int b)
        {
            lock (loadLock)
            {
                animatorEffect.Parameters["blur"].SetValue(b);
            }
        }

        public void setProbability(float p)
        {
            lock (loadLock)
            {
                animatorEffect.Parameters["P"].SetValue(1-p);
            }
        }
    }
}
