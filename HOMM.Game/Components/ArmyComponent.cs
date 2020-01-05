using System.Linq;
using HOMM.BattleObjects;
using Microsoft.Xna.Framework;

namespace HOMM.Game.Components
{
    public class ArmyComponent : ContainerComponent
    {
        private const float Margin = 5;
        
        private readonly BattleArmy _army;
        
        public ArmyComponent(BattleArmy army, Microsoft.Xna.Framework.Game game)
            : base(game)
        {
            _army = army;
            
            SetChildren(army.GetStacks().Select(stack => new UnitsStackComponent(stack, game)));

            UpdatePositionsAndSize();
        }

        public override void Update()
        {
            base.Update();
            
            UpdatePositionsAndSize();
        }

        private void UpdatePositionsAndSize()
        {
            BaseComponent child = null;
            for (var i = 0; i < Children.Count; ++i)
            {
                child = Children[i];
                child.SetPosition(Position + new Vector2((child.GetWidth() + Margin) * i, 0));
            }

            if (child == null) return;
            
            Width = (int) ((child.GetWidth() + Margin) * Children.Count - Margin);
            Height = child.GetHeight();
        }
    }
}