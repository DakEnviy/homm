using System.Collections.Generic;
using HOMM.Objects;

namespace HOMM.BattleObjects
{
    public enum BattleState
    {
        None,
        InGame,
        Ended
    }
    
    public class Battle
    {
        private readonly BattleArmy _attacker;
        private readonly BattleArmy _target;

        private BattleState _state;
        private int _round;
        private BattleArmy _winner;

        private IList<BattleUnitsStack> _currentStacks;

        public Battle(Army attacker, Army target)
        {
            _attacker = new BattleArmy(attacker, this);
            _target = new BattleArmy(target, this);

            _state = BattleState.None;
            _round = 0;
            _winner = null;

            _currentStacks = null;
        }
        
        public void Attack(BattleUnitsStack target) {}
        
        public void UseSkill(/* Skill */) {}
        
        public void Wait() {}
        
        public void Defend() {}
        
        public void Surrender() {}
        
        public BattleArmy GetAttacker() => _attacker;

        public BattleArmy GetTarget() => _target;

        public BattleState GetBattleState() => _state;
        
        public int GetRound() => _round;
        
        public BattleArmy GetWinner() => _winner;

        public IList<BattleUnitsStack> GetCurrentStacks() => _currentStacks;

        public BattleUnitsStack GetCurrentStack() => _currentStacks?[0];

        public BattleArmy GetCurrentArmy() => GetCurrentStack()?.GetArmy();
    }
}