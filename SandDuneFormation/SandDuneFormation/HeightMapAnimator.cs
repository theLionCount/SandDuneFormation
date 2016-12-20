using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SandDuneFormation
{
    class HeightMapAnimator
    {
        int parity;
        public float sandheight;
        public float avalancheconditionheight;
        GraphicsDeviceManager graphics;
        public RenderTarget2D tempTarget, translateTarget, shadowTarget, shadowTarget2, endTarget, avalanche1target, avalanche2target;
        SpriteBatch spriteBatch;
        int width, height;
        HeightMapRenderer renderer;
        Vector3 windVector;
        Vector2 hopvector;
        object l = new object();
        public Texture2D mapb;
        public Texture2D shadowTexture
        {
            get
            {
                return shadowTarget2;
            }
        }

        public void setWind(Vector2 wind)
        {
            lock (l)
            {
                Vector3 norm = new Vector3(wind.X, 0, wind.Y);
                norm.Normalize();
                norm.Y = (float)(Math.Tan(Math.PI / 12));
                windVector = norm;
                hopvector = -wind;
            }
        }

        public HeightMapAnimator(GraphicsDeviceManager graphics, SpriteBatch sprite, int width, int height, HeightMapRenderer renderer, Vector3 windShadowDir)
        {
            sandheight = 8;
            avalancheconditionheight = 2;
            windVector = windShadowDir;
            parity = 0;
            this.width = width;
            this.height = height;
            this.graphics = graphics;
            this.spriteBatch = sprite;
            translateTarget = new RenderTarget2D(graphics.GraphicsDevice, width, height, false, SurfaceFormat.Vector4, DepthFormat.None);
            tempTarget = new RenderTarget2D(graphics.GraphicsDevice, width, height, false, SurfaceFormat.Vector4, DepthFormat.None);
            shadowTarget = new RenderTarget2D(graphics.GraphicsDevice, (int)(width*1.25), (int)(height*1.25), false, SurfaceFormat.Color, DepthFormat.None);
            shadowTarget2 = new RenderTarget2D(graphics.GraphicsDevice, width, height, false, SurfaceFormat.Vector4, DepthFormat.None);
            endTarget = new RenderTarget2D(graphics.GraphicsDevice, width, height, false, SurfaceFormat.Vector4, DepthFormat.None);
            avalanche1target = new RenderTarget2D(graphics.GraphicsDevice, width, height, false, SurfaceFormat.Vector4, DepthFormat.None);
            avalanche2target = new RenderTarget2D(graphics.GraphicsDevice, width, height, false, SurfaceFormat.Vector4, DepthFormat.None);
            this.renderer = renderer;
            aOrder = CreateAvalancheOrder();
        }

        Random r = new Random();
        private Vector2[] aOrder;

        public void applyAnimation(Effect animationEffect, ref Texture2D map, Effect rendererEffect)
        {
            lock (l)
            {
                graphics.GraphicsDevice.SetRenderTarget(shadowTarget);
                graphics.GraphicsDevice.Clear(Color.Black);
                rendererEffect.Parameters["View"].SetValue(Matrix.Identity);
                rendererEffect.Parameters["Projection"].SetValue(createShadowProjection(windVector, width, -height));
                renderer.DrawShadows(graphics.GraphicsDevice, map);     
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
                spriteBatch.Draw(map, new Rectangle(width / 8, height / 8, width, height), Color.White);
                spriteBatch.End();

                graphics.GraphicsDevice.SetRenderTarget(shadowTarget2);

                Matrix projection = Matrix.CreateOrthographicOffCenter(0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height, 0, 0, 1);
                Matrix halfPixelOffset = Matrix.CreateTranslation(-0.5f, -0.5f, 0);
                animationEffect.Parameters["pixelSize"].SetValue(1f / width);
                animationEffect.Parameters["MatrixTransform"].SetValue(halfPixelOffset * projection);
                animationEffect.CurrentTechnique = animationEffect.Techniques["shadowCorrection"];
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Opaque, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, animationEffect);
                spriteBatch.Draw(shadowTarget, new Rectangle(0, 0, width, height), Color.White);
                spriteBatch.End();

                parity++;
                graphics.GraphicsDevice.SetRenderTarget(translateTarget);
                graphics.GraphicsDevice.Clear(Color.Black);

                float[] rnd = new float[32];
                for (int i = 0; i < 32; i++)
                {
                    rnd[i] = (float)r.NextDouble();
                }

                int a = r.Next(24) * 4;
               
                animationEffect.Parameters["avalancheOrder"].SetValue(new Vector2[]{aOrder[a],aOrder[a+1],aOrder[a+2],aOrder[a+3]});
                animationEffect.Parameters["sandHeight"].SetValue(sandheight / 255f);
                animationEffect.Parameters["hopDistance"].SetValue(hopvector / new Vector2((float)width, (float)height));
                animationEffect.Parameters["RandomSeed"].SetValue(rnd);
                animationEffect.CurrentTechnique = animationEffect.Techniques["preAnimate"];


                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Opaque, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, animationEffect);
                spriteBatch.Draw(shadowTarget2, new Rectangle(0, 0, width, height), Color.White);
                spriteBatch.End();
                graphics.GraphicsDevice.SetRenderTarget(endTarget);
                mapb = parity % 2 == 0 ? translateTarget : tempTarget; 

                animationEffect.CurrentTechnique = animationEffect.Techniques["postAnimate"];

                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Opaque, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, animationEffect);
                spriteBatch.Draw(translateTarget, new Rectangle(0, 0, width, height), Color.White);
                spriteBatch.End();

                graphics.GraphicsDevice.SetRenderTarget(avalanche1target);

                animationEffect.CurrentTechnique = animationEffect.Techniques["preAvalanche"];
                animationEffect.Parameters["avalancheHeight"].SetValue(avalancheconditionheight*sandheight / 255f);


                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Opaque, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, animationEffect);
                spriteBatch.Draw(endTarget, new Rectangle(0, 0, width, height), Color.White);
                spriteBatch.End();

                graphics.GraphicsDevice.SetRenderTarget(avalanche2target);

                animationEffect.CurrentTechnique = animationEffect.Techniques["avalanche"];

                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Opaque, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, animationEffect);
                spriteBatch.Draw(avalanche1target, new Rectangle(0, 0, width, height), Color.White);
                spriteBatch.End();

                graphics.GraphicsDevice.SetRenderTarget(null);

                map = avalanche2target; 
            }
        }

        public static Matrix createShadowProjection(Vector3 wind, float width, float height)
        {
            float ww = width*1.25f / 2, hh = height*1.25f / 2, x = wind.X, y = wind.Y, z = wind.Z;
            Matrix m = new Matrix(
                    1 / ww,             0,              0,              0,
                    -x / (y * ww),      -z / (y * hh),  -0.001f,        0,
                    0,                  1 / hh,         0,              0,
                    0,                  0,              1,              1
                );
            return m;
        }

        public Vector2[] CreateAvalancheOrder()
        {
            Vector2[] ret = new Vector2[24*4];
            float pixel = 1f/width;
            Vector2[] s = new Vector2[4];
            s[0] = new Vector2(pixel, 0);
            s[1] = new Vector2(-pixel, 0);
            s[2] = new Vector2(0,pixel);
            s[3] = new Vector2(0,-pixel);

            int index = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (j != i)
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            if (j != k && i != k)
                            {
                                for (int l = 0; l < 4; l++)
                                {
                                    if (l != i && l != j && l != k)
                                    {
                                        ret[index] = s[i];
                                        ret[index + 1] = s[j];
                                        ret[index + 2] = s[k];
                                        ret[index + 3] = s[l];
                                        index+=4;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return ret;
        }
    }
}

