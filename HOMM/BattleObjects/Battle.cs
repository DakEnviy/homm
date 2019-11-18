using System.Collections.Generic;
using HOMM.Objects;
using System.Linq;

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

        public void Start()
        {
            _state = BattleState.InGame;
            
            NextRound();
        }

        public void NextRound()
        {
            ++_round;
            _currentStacks = GetSortedStacks();
        }

        public void NextTurn()
        {
            _currentStacks.RemoveAt(0);

            if (_currentStacks.Count == 0)
            {
                NextRound();
            }
        }

        public IList<BattleUnitsStack> GetSortedStacks()
        {
            return _armies.SelectMany(army => army.GetStacks())
                .OrderByDescending(stack => stack.GetInitiative())
                .ToList();
        }

        public void Attack(BattleUnitsStack target)
        {
            var source = GetCurrentStack();
            
            var amount = source.GetAmount();
            var damage = source.GetDamage();
            var attack = source.GetAttack();
            var defence = target.GetDefence();

            var minDamage = attack > defence
                ? amount * damage.Item1 * (1 + 0.05 * (attack - defence))
                : amount * damage.Item1 / (1 + 0.05 * (defence - attack));
            var maxDamage = attack > defence
                ? amount * damage.Item2 * (1 + 0.05 * (attack - defence))
                : amount * damage.Item2 / (1 + 0.05 * (defence - attack));
            
            var finalDamage = (ushort) Math.Round(minDamage + _random.NextDouble() * (maxDamage - minDamage));
            
            target.Damage(finalDamage);
            
            NextTurn();
        }
        
        public void UseSkill(/* Skill */) {}

        public void Wait()
        {
            _currentStacks.Add(GetCurrentStack());
        }

        public void Defend()
        {
            var stack = GetCurrentStack();
            
            stack.SetDefence((int) (stack.GetDefence() * 1.3));
        }

        public void Surrender()
        {
            _armies.Remove(GetCurrentArmy());
        }

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