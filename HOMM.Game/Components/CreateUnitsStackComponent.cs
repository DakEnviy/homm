using HOMM.Game.Utils;
using HOMM.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HOMM.Game.Components
{
    public class CreateUnitsStackComponent : ContainerComponent, IClickable
    {
        private readonly Unit _unit;
        private int _amount;
        private readonly UnitComponent _unitComponent;
        
        public CreateUnitsStackComponent(Unit unit, Microsoft.Xna.Framework.Game game)
            : base(game)
        {
            _unit = unit;
            _unitComponent = new UnitComponent(unit.GetName(), game);

            AddChild(_unitComponent);
            UpdatePositionAndSize();
        }
        
        public override void Update()
        {
            base.Update();
            
            UpdatePositionAndSize();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.DrawRectangle(Position, new Vector2(Width, Height), Color.Yellow);
            spriteBatch.DrawString(HommGame.Font, _amount.ToString(), Position + new Vector2(5, 5), Color.Yellow);
        }

        public UnitsStack CreateStack() => new UnitsStack(_unit, _amount);
        
        public int GetAmount() => _amount;
        
        private void UpdatePositionAndSize()
        {
            _unitComponent.SetPosition(Position);
            
            Width = _unitComponent.GetWidth();
            Height = _unitComponent.GetHeight();
        }

        public void OnLeftClick()
        {
            if (_amount == UnitsStack.MaxStackSize) return;
            
            ++_amount;
        }

        public void OnRightClick()
        {
            if (_amount == 0) return;
            
            --_amount;
        }
    }
}