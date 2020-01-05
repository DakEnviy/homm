using System;
using HOMM.BattleObjects;
using HOMM.Game.Components;
using HOMM.Game.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HOMM.Game.Scenes
{
    public class BattleScene : BaseScene
    {
        private readonly Battle _battle;

        public BattleScene(Battle battle, Microsoft.Xna.Framework.Game game)
            : base(game)
        {
            _battle = battle;
            
            var actionsComponent = new ActionsComponent(game);
            actionsComponent.SetPosition(new Vector2(
                0, Resolution.VirtualHeight - actionsComponent.GetHeight()
            ));

            var attackerComponent = new ArmyComponent(_battle.GetAttacker(), game);
            attackerComponent.SetPosition(new Vector2(
                Resolution.VirtualWidth / 2 - attackerComponent.GetWidth() / 2,
                Resolution.VirtualHeight - attackerComponent.GetHeight() - actionsComponent.GetHeight() - 20
            ));
            
            var targetComponent = new ArmyComponent(_battle.GetTarget(), game);
            targetComponent.SetPosition(new Vector2(
                Resolution.VirtualWidth / 2 - targetComponent.GetWidth() / 2,
                20
            ));
            
            AddComponent(actionsComponent);
            AddComponent(attackerComponent);
            AddComponent(targetComponent);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            
            var stateStr = "None";
            if (_battle.GetBattleState() == BattleState.InGame)
            {
                stateStr = "In Game";
            }
            else if (_battle.GetBattleState() == BattleState.Ended)
            {
                stateStr = "Won : " + (_battle.GetWinner() == _battle.GetAttacker() ? "Attacker" : "Target");
            }
            
            var roundStr = "Round: " + _battle.GetRound();
            var turnStr = "Turn: " + (_battle.GetCurrentArmy() == _battle.GetAttacker() ? "Attacker" : "Target");
            
            spriteBatch.DrawString(HommGame.Font, stateStr, new Vector2(5, 5), Color.White);
            spriteBatch.DrawString(HommGame.Font, roundStr, new Vector2(5, 30), Color.White);
            spriteBatch.DrawString(HommGame.Font, turnStr, new Vector2(5, 55), Color.White);
            
            var history = CommandsManager.GetHistory();
            var historyPos = new Vector2(5, 90);
            var historyFrom = Math.Max(history.Count - 10, 0);
            var historyLimit = history.Count - historyFrom;
            for (var i = 0; i < historyLimit; ++i)
            {
                spriteBatch.DrawString(HommGame.Font, history[historyFrom + i].GetMessage(), historyPos + new Vector2(0, 25 * (historyLimit - i)), Color.Aqua);
            }
            
            var initiativePos = new Vector2(Resolution.VirtualWidth - 200, 150);
            var currentStacks = _battle.GetCurrentStacks();
            for (var i = 0; i < currentStacks.Count; ++i)
            {
                spriteBatch.DrawString(HommGame.Font, currentStacks[i].GetBaseUnit().GetName(), initiativePos + new Vector2(0, 25 * i), Color.Pink);
            }
        }
    }
}