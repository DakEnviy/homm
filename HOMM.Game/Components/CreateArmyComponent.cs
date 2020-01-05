using System.Linq;
using HOMM.Objects;
using Microsoft.Xna.Framework;

namespace HOMM.Game.Components
{
    public class CreateArmyComponent : ContainerComponent
    {
        private const float Margin = 5;
        
        public CreateArmyComponent(Microsoft.Xna.Framework.Game game)
            : base(game)
        {
            SetChildren(Units.Units.Registry.Select(unit => new CreateUnitsStackComponent(unit.Value, game)));
            
            UpdatePositionsAndSize();
        }
        
        public override void Update()
        {
            base.Update();
            
            UpdatePositionsAndSize();
        }

        public Army CreateArmy()
        {
            return new Army(Children.OfType<CreateUnitsStackComponent>()
                .Where(component => component.GetAmount() > 0)
                .Select(component => component.CreateStack())
                .ToList()
            );
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