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
        private readonly IList<BattleUnitsStack> _stacks;

        public BattleArmy(Army army)
        {
            _baseArmy = army;
            _stacks = army.GetStacks().Select(stack => new BattleUnitsStack(stack)).ToList();
        }

        public Army GetBaseArmy() => _baseArmy;

        public IList<BattleUnitsStack> GetStacks() => _stacks;

        public IList<BattleUnitsStack> GetStacksByUnitType(UnitType unitType) =>
            _stacks.Where(stack => stack.GetBaseUnit().GetUnitType() == unitType).ToList();

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