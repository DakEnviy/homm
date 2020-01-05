using HOMM.Game.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HOMM.Game.Components
{
    public abstract class ButtonComponent : BaseComponent, IClickable
    {
        private readonly string _text;
        private readonly int _paddingHorizontal;
        private readonly int _paddingVertical;
        private readonly Color _textColor;
        private readonly Color _borderColor;
        private readonly float _thickness;
        
        protected ButtonComponent(
            string text,
            int paddingHorizontal,
            int paddingVertical,
            Color textColor,
            Color borderColor,
            float thickness,
            Microsoft.Xna.Framework.Game game
        )
            : base(game)
        {
            _text = text;
            _paddingHorizontal = paddingHorizontal;
            _paddingVertical = paddingVertical;
            _textColor = textColor;
            _borderColor = borderColor;
            _thickness = thickness;

            UpdateSize();
        }

        public override void Update()
        {
            base.Update();
            
            UpdateSize();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            
            spriteBatch.DrawString(HommGame.Font, _text, Position + new Vector2(_paddingHorizontal, _paddingVertical), _textColor);
            spriteBatch.DrawRectangle(Position, new Vector2(Width, Height), _borderColor, _thickness);
        }

        private void UpdateSize()
        {
            if (HommGame.Font == null) return;
            
            var size = HommGame.Font.MeasureString(_text);

            Width = (int) (size.X + _paddingHorizontal * 2);
            Height = (int) (size.Y + _paddingVertical * 2);
        }

        public abstract void OnLeftClick();

        public abstract void OnRightClick();
    }
}