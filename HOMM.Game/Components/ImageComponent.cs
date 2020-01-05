using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HOMM.Game.Components
{
    public class ImageComponent : BaseComponent
    {
        public Texture2D Image;
        public Color DrawColor = Color.White;
        public Vector2 ImageScale;
        
        public ImageComponent(Microsoft.Xna.Framework.Game game) : base(game) {}

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            
            if (Image != null)
            {
                spriteBatch.Draw(
                    Image,
                    Position, 
                    null,
                    DrawColor,
                    Rotation,
                    Vector2.Zero,
                    ImageScale * Scale,
                    SpriteEffects.None,
                    LayerDepth
                );
            }
        }

        protected void LoadImage(Texture2D image)
        {
            Image = image;

            if (Width == 0) Width = image.Width;
            if (Height == 0) Height = image.Height;

            ImageScale.X = (float) Width / Image.Width;
            ImageScale.Y = (float) Height / Image.Height;
        }
    }
}