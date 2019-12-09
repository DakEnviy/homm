using System;
using System.Collections.Generic;
using System.Linq;
using HOMM.Objects;

namespace HOMM.BattleObjects
{
    public class BattleArmy
    {
        private const int MaxBattleArmySize = 9;

        private readonly Army _baseArmy;
        private readonly Battle _battle;
        private readonly IList<BattleUnitsStack> _stacks;

        public BattleArmy(Army army, Battle battle)
        {
            _baseArmy = army;
            _battle = battle;
            _stacks = army.GetStacks().Select(stack => new BattleUnitsStack(stack, this)).ToList();
        }

        public Army GetBaseArmy() => _baseArmy;
        
        public Battle GetBattle() => _battle;

        public IList<BattleUnitsStack> GetStacks() => _stacks;
        
        public IList<BattleUnitsStack> GetAliveStacks() =>
            _stacks.Where(stack => stack.IsAlive()).ToList();

        public IList<BattleUnitsStack> GetDeadStacks() =>
            _stacks.Where(stack => stack.IsDead()).ToList();

        public IList<BattleUnitsStack> GetStacksByUnit(Unit unit) =>
            _stacks.Where(stack => stack.GetBaseUnit() == unit).ToList();

        public bool IsAttacker() => this == _battle.GetAttacker();
        
        public bool IsTarget() => this == _battle.GetTarget();

        public void AddStack(BattleUnitsStack stack)
        {
            if (_stacks.Count == MaxBattleArmySize)
            {
                throw new ArgumentOutOfRangeException(nameof(_stacks), $"Maximum size of battle army is {MaxBattleArmySize}");
            }

            _stacks.Add(stack);
        }

        public bool RemoveStack(BattleUnitsStack stack) => _stacks.Remove(stack);

        public BattleUnitsStack GetStack(int index) => _stacks[index];

        public bool ContainsStack(BattleUnitsStack stack) => _stacks.Contains(stack);
    }
}