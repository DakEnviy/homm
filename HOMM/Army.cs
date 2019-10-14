using System;
using System.Collections.Generic;

namespace HOMM
{
    public class Army
    {
        public const uint MaxArmySize = 6;

        private readonly IList<UnitsStack> _stacks;
        
        public Army(IList<UnitsStack> stacks)
        {
            if (stacks.Count > MaxArmySize)
            {
                throw new ArgumentOutOfRangeException(nameof(stacks), "Maximum size of army is 6");
            }
            
            _stacks = stacks;
        }

        public IReadOnlyList<UnitsStack> GetStacks() => (IReadOnlyList<UnitsStack>) _stacks;
    }
}