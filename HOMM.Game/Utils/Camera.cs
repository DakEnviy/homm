using System;
using Microsoft.Xna.Framework;

//Thanks to David Amador and o KB o. Static since we will only have one camera.

namespace HOMM.Game.Utils
{
    static class Camera
    {
        private static Matrix _transformMatrix; //A transformation matrix containing info on our position, how much we are rotated and zoomed etc.
        private static Vector2 _position;
        private static float _rotation;
        private static float _zoom;

        private static bool _updateYAxis = true; //Should the camera move along on the y axis?
        private static bool _updateXAxis = true; //Should the camera move along on the x axis?

        public static void Initialize()
        {
            _zoom = 1.0f;
            _rotation = 0.0f;

            //Start the camera at the center of the screen:
            _position = new Vector2(Resolution.VirtualWidth / 2, Resolution.VirtualHeight / 2);
        }

        /// <summary>
        /// This rectangle covers the entire screen based on where the camera is, useful if you need to determine what is currently viewable to the player, or the position of the camera.
        /// </summary>
        public static Rectangle ScreenRect { get; private set; }

        public static float Zoom
        {
            get { return _zoom; }
            set
            {
                _zoom = value;
                
                if (_zoom < 0.1f)
                {
                    _zoom = 0.1f; //Negative zoom will flip image.
                }
            }
        }

        public static void Update(Vector2 follow)
        {
            UpdateMovement(follow);
            CalculateMatrixAndRectangle();
        }

        private static void UpdateMovement(Vector2 follow)
        {
            //Make the camera center on and follow the position:
            if (_updateXAxis)
                _position.X += ((follow.X - _position.X)); //Camera will follow the position passed in

            if (_updateYAxis)
                _position.Y += ((follow.Y - _position.Y)); //Camera will follow the position passed in
        }

        /// <summary>
        /// Immediately sets the camera to look at the position passed in.
        /// </summary>
        public static void LookAt(Vector2 lookAt)
        {
            //Immediately looks at the vector passed in:
            if (_updateXAxis)
                _position.X = lookAt.X;
            if (_updateYAxis)
                _position.Y = lookAt.Y;
        }

        private static void CalculateMatrixAndRectangle()
        {
            //The math involved with calculated our transformMatrix and screenRect is a little intense, so instead of calling the math whenever we need these variables,
            //we'll calculate them once each frame and store them... when someone needs these variables we will simply return the stored variable instead of re cauclating them every time.

            //Calculate the camera transform matrix:
            _transformMatrix = Matrix.CreateTranslation(new Vector3(-_position, 0)) * Matrix.CreateRotationZ(_rotation) *
                        Matrix.CreateScale(new Vector3(_zoom, _zoom, 1)) * Matrix.CreateTranslation(new Vector3(Resolution.VirtualWidth
                            * 0.5f, Resolution.VirtualHeight * 0.5f, 0));

            //Now combine the camera's matrix with the Resolution Manager's transform matrix to get our final working matrix:
            _transformMatrix *= Resolution.GetTransformationMatrix();

            //Round the X and Y translation so the camera doesn't jerk as it moves:
            _transformMatrix.M41 = (float)Math.Round(_transformMatrix.M41, 0);
            _transformMatrix.M42 = (float)Math.Round(_transformMatrix.M42, 0);

            //Calculate the rectangle that represents where our camera is at in the world:
            ScreenRect = VisibleArea();
        }

        /// <summary>
        /// Calculates the screenRect based on where the camera currently is.
        /// </summary>
        private static Rectangle VisibleArea()
        {
            Matrix inverseViewMatrix = Matrix.Invert(_transformMatrix);
            Vector2 tl = Vector2.Transform(Vector2.Zero, inverseViewMatrix);
            Vector2 tr = Vector2.Transform(new Vector2(Resolution.VirtualWidth, 0), inverseViewMatrix);
            Vector2 bl = Vector2.Transform(new Vector2(0, Resolution.VirtualHeight), inverseViewMatrix);
            Vector2 br = Vector2.Transform(new Vector2(Resolution.VirtualWidth, Resolution.VirtualHeight), inverseViewMatrix);
            Vector2 min = new Vector2(
                MathHelper.Min(tl.X, MathHelper.Min(tr.X, MathHelper.Min(bl.X, br.X))),
                MathHelper.Min(tl.Y, MathHelper.Min(tr.Y, MathHelper.Min(bl.Y, br.Y))));
            Vector2 max = new Vector2(
                MathHelper.Max(tl.X, MathHelper.Max(tr.X, MathHelper.Max(bl.X, br.X))),
                MathHelper.Max(tl.Y, MathHelper.Max(tr.Y, MathHelper.Max(bl.Y, br.Y))));
            return new Rectangle((int)min.X, (int)min.Y, (int)(Resolution.VirtualWidth / _zoom), (int)(Resolution.VirtualHeight / _zoom));
        }

        public static Matrix GetTransformMatrix()
        {
            return _transformMatrix; //Return the transformMatrix we calculated earlier in this frame.
        }
    }
}
