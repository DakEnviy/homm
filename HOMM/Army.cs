using System;
using System.Collections.Generic;

namespace HOMM
{
    public class Army
    {
        public static readonly uint MAX_ARMY_SIZE = 6;
        
        private readonly List<UnitsStack> _stacks;
        
        public Army(List<UnitsStack> stacks)
        {
            if (stacks.Count > MAX_ARMY_SIZE)
            {
                throw new ArgumentOutOfRangeException(nameof(stacks), "Maximum size of army is 6");
            }
            
            _stacks = stacks;
        }

        public List<UnitsStack> GetStacks() { return _stacks; }
    }
}