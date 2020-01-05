using HOMM.BattleObjects;
using HOMM.Game.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HOMM.Game.Components
{
    public class UnitsStackComponent : ContainerComponent, IClickable
    {
        private readonly BattleUnitsStack _stack;
        private readonly UnitComponent _unitComponent;
        
        private bool _isStatsVisible;

        public UnitsStackComponent(BattleUnitsStack stack, Microsoft.Xna.Framework.Game game)
            : base(game)
        {
            _stack = stack;
            
            _unitComponent = new UnitComponent(stack.GetBaseUnit().GetName(), game);

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
            
            var color = _stack.GetArmy().GetBattle().GetCurrentStack() == _stack ? Color.Green : Color.Red;
            spriteBatch.DrawRectangle(Position, new Vector2(Width, Height), color);
            
            spriteBatch.DrawString(HommGame.Font, _stack.GetAmount().ToString(), Position + new Vector2(5, 5), Color.Yellow);
            
            if (!_isStatsVisible) return;
            
            var statsPos = new Vector2(Resolution.VirtualWidth - 200, 5);
            var topHitPointsStr = "Hit Points: " + _stack.GetTopHitPoints();
            var attackStr = "Attack: " + _stack.GetAttack();
            var defenceStr = "Defence: " + _stack.GetDefence();
            var damageStr = "Damage: " + _stack.GetDamage();
            var initiativeStr = "Initiative: " + _stack.GetInitiative();
            
            spriteBatch.DrawString(HommGame.Font, topHitPointsStr, statsPos, Color.White);
            spriteBatch.DrawString(HommGame.Font, attackStr, statsPos + new Vector2(0, 25), Color.White);
            spriteBatch.DrawString(HommGame.Font, defenceStr, statsPos + new Vector2(0, 50), Color.White);
            spriteBatch.DrawString(HommGame.Font, damageStr, statsPos + new Vector2(0, 75), Color.White);
            spriteBatch.DrawString(HommGame.Font, initiativeStr, statsPos + new Vector2(0, 100), Color.White);
        }

        private void UpdatePositionAndSize()
        {
            _unitComponent.SetPosition(Position);
            
            Width = _unitComponent.GetWidth();
            Height = _unitComponent.GetHeight();
        }

        public void OnLeftClick()
        {
            CommandsManager.SelectStack(_stack);
        }

        public void OnRightClick()
        {
            _isStatsVisible = !_isStatsVisible;
        }
    }
}