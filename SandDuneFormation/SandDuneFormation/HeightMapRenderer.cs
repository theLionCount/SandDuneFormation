using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SandDuneFormation
{
    public class HeightMapRenderer
    {
        Texture2D map { get; set; }
        RenderTarget2D preBlur, postBlur;
        Texture2D texture;
        VertexPositionTexture[] vertices;
        VertexBuffer quad, instances;
        IndexBuffer quadIndex;
        int[] indexbuffer;
        int width, height;
        float size, maxheight;
        Effect effect;
        
        Matrix transformation;

        public float MaxHeight
        {
            get { return maxheight; }
            set { maxheight = value; }
        }

        public void DrawColors(GraphicsDevice device, Texture2D hmap, Texture2D shadow, Effect animationEffect, SpriteBatch spriteBatch)
        {
            device.SetRenderTarget(preBlur);

            Matrix projection = Matrix.CreateOrthographicOffCenter(0, device.Viewport.Width, device.Viewport.Height, 0, 0, 1);
            Matrix halfPixelOffset = Matrix.CreateTranslation(-0.5f, -0.5f, 0);
            animationEffect.Parameters["pixelSize"].SetValue(1f / (float)width);
            animationEffect.Parameters["MatrixTransform"].SetValue(halfPixelOffset * projection);
            animationEffect.CurrentTechnique = animationEffect.Techniques["verticalBlur"];
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Opaque, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, animationEffect);
            spriteBatch.Draw(hmap, new Rectangle(0, 0, width, height), Color.White);
            spriteBatch.End();

            device.SetRenderTarget(postBlur);
            animationEffect.CurrentTechnique = animationEffect.Techniques["horizontalBlur"];
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Opaque, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, animationEffect);
            spriteBatch.Draw(preBlur, new Rectangle(0, 0, width, height), Color.White);
            spriteBatch.End();

            device.SetRenderTarget(null);
            effect.CurrentTechnique = effect.Techniques["heightMap"];
            effect.Parameters["height"].SetValue(postBlur);
            if (shadow!=null)
                effect.Parameters["shadow"].SetValue(shadow);
            effect.Parameters["text"].SetValue(texture);
            effect.Parameters["mapsize"].SetValue(size * width);
            effect.Parameters["World"].SetValue(transformation);
            effect.Parameters["maxheight"].SetValue(maxheight);
            effect.Parameters["mpixelsize"].SetValue(1.0f / width);
            effect.Parameters["musize"].SetValue(size);
            effect.CurrentTechnique.Passes[0].Apply();

            device.DrawUserIndexedPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, vertices, 0, width * height, indexbuffer, 0, 2 * (width - 1) * (height - 1));
        }

        public void DrawShadows(GraphicsDevice device, Texture2D hmap)
        {
            effect.CurrentTechnique = effect.Techniques["shadowMap"];
            effect.Parameters["height"].SetValue(hmap);
            effect.Parameters["mapsize"].SetValue(size * width);
            effect.Parameters["World"].SetValue(Matrix.Identity);
            effect.Parameters["maxheight"].SetValue(maxheight);
            effect.Parameters["mpixelsize"].SetValue(1.0f / width);
            effect.Parameters["musize"].SetValue(size);
            effect.CurrentTechnique.Passes[0].Apply();
            device.DrawUserIndexedPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, vertices, 0, width * height, indexbuffer, 0, 2 * (width - 1) * (height - 1));
            
        }

        public HeightMapRenderer(Matrix world, Texture2D heighttext, Texture2D text, int width, int height, float size, float maxheight, Effect effect, GraphicsDevice device)
        {
            this.effect = effect;
            this.transformation = world;
            this.map = heighttext;
            this.texture = text;
            this.maxheight = maxheight;
            this.width = width;
            this.height = height;
            int twidth = map.Width;
            int theight = map.Height;
            this.size = size;
            preBlur = new RenderTarget2D(device, width, height, false, SurfaceFormat.Vector4, DepthFormat.None);
            postBlur = new RenderTarget2D(device, width, height, false, SurfaceFormat.Vector4, DepthFormat.None);
            
            vertices = new VertexPositionTexture[width * height];
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    vertices[i * height + j] = new VertexPositionTexture(new Vector3((i - width / 2.0f), 0, (j - height / 2.0f)) * size, new Vector2(i % 2, j % 2));
                }
            indexbuffer = new int[(width - 1) * (height - 1) * 6];
            for (int i = 0; i < width - 1; i++)
                for (int j = 0; j < height - 1; j++)
                {
                    indexbuffer[(i * (height - 1) + j) * 6] = i * height + j;
                    indexbuffer[(i * (height - 1) + j) * 6 + 1] = i * height + j + 1;
                    indexbuffer[(i * (height - 1) + j) * 6 + 2] = (i + 1) * height + j;
                    indexbuffer[(i * (height - 1) + j) * 6 + 3] = i * height + j + 1;
                    indexbuffer[(i * (height - 1) + j) * 6 + 4] = (i + 1) * height + j + 1;
                    indexbuffer[(i * (height - 1) + j) * 6 + 5] = (i + 1) * height + j;
                }

            for (int i = 0; i < width; i += 2)
                for (int j = 0; j < height; j++)
                {
                    vertices[i * height + j] = new VertexPositionTexture(new Vector3((i - width / 2.0f), 0, (j - height / 2.0f)) * size, new Vector2(i % 2, j % 2));
                }
            indexbuffer = new int[(width - 1) * (height - 1) * 6];
            for (int i = 0; i < width - 1; i++)
                for (int j = 0; j < height - 1; j++)
                {
                    indexbuffer[(i * (height - 1) + j) * 6] = i * height + j;
                    indexbuffer[(i * (height - 1) + j) * 6 + 1] = i * height + j + 1;
                    indexbuffer[(i * (height - 1) + j) * 6 + 2] = (i + 1) * height + j;
                    indexbuffer[(i * (height - 1) + j) * 6 + 3] = i * height + j + 1;
                    indexbuffer[(i * (height - 1) + j) * 6 + 4] = (i + 1) * height + j + 1;
                    indexbuffer[(i * (height - 1) + j) * 6 + 5] = (i + 1) * height + j;
                }
            
        }
    }
}
