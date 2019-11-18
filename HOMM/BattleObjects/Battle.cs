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
        private readonly Random _random;

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

        public void Attack(BattleUnitsStack target)
        {
            var amount = _currentStack.GetAmount();
            var damage = _currentStack.GetDamage();
            var attack = _currentStack.GetAttack();
            var defence = target.GetDefence();

            var minDamage = attack > defence
                ? amount * damage.Item1 * (1 + 0.05 * (attack - defence))
                : amount * damage.Item1 / (1 + 0.05 * (defence - attack));
            var maxDamage = attack > defence
                ? amount * damage.Item2 * (1 + 0.05 * (attack - defence))
                : amount * damage.Item2 / (1 + 0.05 * (defence - attack));
            
            var finalDamage = (ushort) Math.Round(minDamage + _random.NextDouble() * (maxDamage - minDamage));
            
            target.Damage(finalDamage);
        }
        
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