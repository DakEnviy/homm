using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HOMM.Game.Components
{
    public abstract class BaseComponent
    {
        protected readonly Microsoft.Xna.Framework.Game Game;
        protected Vector2 Position = Vector2.Zero;
        protected Vector2 Center = Vector2.Zero;
        protected int Width;
        protected int Height;
        protected float Scale = 1.0f;
        protected float Rotation;
        protected float LayerDepth = 0.5f;
        private bool _initialized;
        private bool _enabled = true;
        private bool _visible = true;

        protected BaseComponent(Microsoft.Xna.Framework.Game game)
        {
            Game = game;
        }

        public virtual void Initialize()
        {
            CalculateCenter();

            _initialized = true;
        }

        public virtual void LoadContent()
        {
        }

        public virtual void UnloadContent()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }

        public bool Contains(Vector2 point)
        {
            return Position.X <= point.X &&
                   point.X < Position.X + Width &&
                   Position.Y <= point.Y &&
                   point.Y < Position.Y + Height;
        }

        public bool Contains(int x, int y)
        {
            return Position.X <= x &&
                   x < Position.X + Width &&
                   Position.Y <= y &&
                   y < Position.Y + Height;
        }

        public Microsoft.Xna.Framework.Game GetGame() => Game;

        public Vector2 GetPosition() => Position;

        public void SetPosition(Vector2 position)
        {
            Position = position;
        }

        public Vector2 GetCenter() => Center;

        public void SetCenter(Vector2 origin)
        {
            Center = origin;
        }

        public int GetWidth() => Width;

        public void SetWidth(int width)
        {
            Width = width;
        }

        public int GetHeight() => Height;

        public void SetHeight(int height)
        {
            Height = height;
        }

        public float GetScale() => Scale;

        public void SetScale(float scale)
        {
            Scale = scale;
        }

        public float GetRotation() => Rotation;

        public void SetRotation(float rotation)
        {
            Rotation = rotation;
        }

        public float GetLayerDepth() => LayerDepth;

        public void SetLayerDepth(float layerDepth)
        {
            LayerDepth = layerDepth;
        }

        public bool IsInitialized() => _initialized;

        public bool IsEnabled() => _enabled;

        public void SetEnabled(bool enabled)
        {
            _enabled = enabled;
        }

        public bool IsVisible() => _visible;

        public void SetVisible(bool visible)
        {
            _visible = visible;
        }

        private void CalculateCenter()
        {
            Center.X = (float) Width / 2;
            Center.Y = (float) Height / 2;
        }
    }
}