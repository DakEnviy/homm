using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HOMM.Game.Components
{
    public class RectangleComponent : BaseComponent
    {
        Texture2D dummyTexture;
        Rectangle dummyRectangle;
        Color Colori;

        public RectangleComponent(Rectangle rect, Color colori, Microsoft.Xna.Framework.Game game)
            : base(game)
        {
            dummyRectangle = rect;
            Colori = colori;
        }

        public override void LoadContent()
        {
            dummyTexture = new Texture2D(Game.GraphicsDevice, 1, 1);
            dummyTexture.SetData(new[] { Color.White });
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(dummyTexture, dummyRectangle, Colori);
        }
    }
}