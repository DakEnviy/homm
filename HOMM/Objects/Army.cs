using System;
using System.Collections.Generic;
using System.Linq;

namespace HOMM.Objects
{
    public class Army
    {
        private const int MaxArmySize = 6;

        private readonly IReadOnlyList<UnitsStack> _stacks;
        
        public Army(IList<UnitsStack> stacks)
        {
            if (stacks.Count > MaxArmySize)
            {
                throw new ArgumentOutOfRangeException(nameof(stacks), $"Maximum size of army is {MaxArmySize}");
            }
            
            _stacks = stacks.ToList();
        }

        public IReadOnlyList<UnitsStack> GetStacks() => _stacks;
    }
}