using HOMM.BattleObjects;
using HOMM.Game.Components;
using HOMM.Game.Utils;
using Microsoft.Xna.Framework;

namespace HOMM.Game.Scenes
{
    public class CreateScene : BaseScene
    {
        private readonly CreateArmyComponent _attackerComponent;
        private readonly CreateArmyComponent _targetComponent;
        private readonly CreateBattleButtonComponent _buttonComponent;
        
        public CreateScene(Microsoft.Xna.Framework.Game game)
            : base(game)
        {
            _attackerComponent = new CreateArmyComponent(game);
            _attackerComponent.SetPosition(new Vector2(10, 10));
            
            _targetComponent = new CreateArmyComponent(game);
            _targetComponent.SetPosition(new Vector2(10, _attackerComponent.GetHeight() + 20));
            
            _buttonComponent = new CreateBattleButtonComponent(game);
            
            AddComponent(_attackerComponent);
            AddComponent(_targetComponent);
            AddComponent(_buttonComponent);
            
            UpdatePositions();
        }

        public override void Update()
        {
            base.Update();
            
            UpdatePositions();
        }

        public Battle CreateBattle()
        {
            var attacker = _attackerComponent.CreateArmy();
            var target = _targetComponent.CreateArmy();
            
            return new Battle(attacker, target);
        }

        private void UpdatePositions()
        {
            _buttonComponent.SetPosition(new Vector2(10, Resolution.VirtualHeight - _buttonComponent.GetHeight() - 10));
        }
    }
}