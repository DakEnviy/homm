using System.Collections.Generic;

namespace HOMM
{
    public class Army
    {
        private readonly List<UnitsStack> _stacks;
        
        public Army(List<UnitsStack> stacks)
        {
            _stacks = stacks;
        }

        public List<UnitsStack> GetStacks() { return _stacks; }
    }
}