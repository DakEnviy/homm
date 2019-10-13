using System;

namespace HOMM
{
    public class UnitsStack
    {
        public static readonly uint MAX_STACK_SIZE = 999_999;
        
        private readonly Unit _unit;
        private readonly uint _amount;

        public UnitsStack(Unit unit, uint amount)
        {
            if (amount > MAX_STACK_SIZE)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Maximum size of stack is 999 999");
            }
            
            _unit = unit;
            _amount = amount;
        }

        public Unit GetUnit() { return _unit; }
        
        public uint GetAmount() { return _amount; }
    }
}