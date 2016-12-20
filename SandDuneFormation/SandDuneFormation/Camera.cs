using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SandDuneFormation
{
    class Camera
    {
        const float moveSpeed = 0.003f;
        const float turnSpeed = 0.03f;

        public Matrix View { get; set; }
        public Matrix Projection { get; set; }
        public Vector3 position { get; set; }
        public Vector3 lookat { get; set; }
        public float fov { get; set; }
        public float aspectRatio { get; set; }
        public Vector3 upvector { get; set; }
        public Vector3 up { get; set; }
        public Vector2 mousepos { get; set; }
        public KeyboardState keyboardState { get; set; }
        public Camera(Vector3 position, Vector3 target, Vector3 up, float fov, float aspectRatio)
        {
            this.aspectRatio = aspectRatio;
            this.fov = fov;
            this.position = position;
            this.lookat = target - position;
            this.lookat.Normalize();
            this.up = up;
            upvector = up;
            Projection = Matrix.CreatePerspectiveFieldOfView(fov, aspectRatio, 0.05f, 500.0f);

        }

        public void update(long elapsedTime)
        {
            this.keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.W))
            {
                position += lookat * moveSpeed * elapsedTime;
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                position -= lookat * moveSpeed * elapsedTime;
            }

            mousepos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y) - mousepos;
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                Vector3 x = -Vector3.Cross(up, lookat);
                Vector3 y = Vector3.Cross(lookat, x);
                x.Normalize();
                y.Normalize();
                lookat += x * mousepos.X * turnSpeed + y * mousepos.Y * turnSpeed;
                lookat.Normalize();
            }
            mousepos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            View = Matrix.CreateLookAt(position, position + lookat, up);
        }

        public Vector3 getDirection()
        {
            return position + lookat;
        }
    }
}
