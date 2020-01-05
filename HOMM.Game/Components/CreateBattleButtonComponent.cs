using Microsoft.Xna.Framework;

namespace HOMM.Game.Components
{
    public class CreateBattleButtonComponent : ButtonComponent
    {
        public CreateBattleButtonComponent(Microsoft.Xna.Framework.Game game)
            : base("Start", 30, 10, Color.White, Color.White, 2.0f, game) {}

        public override void OnLeftClick()
        {
            HommGame.Instance.StartBattle();
        }

        public override void OnRightClick() {}
    }
}