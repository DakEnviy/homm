using System;

namespace HOMM
{
    public class UnitsStack
    {
        public const uint MaxStackSize = 999_999;

        private readonly Unit _unit;
        private readonly uint _amount;
        
        public UnitsStack(Unit unit, uint amount)
        {
            if (amount > MaxStackSize)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Maximum size of stack is 999 999");
            }
            
            _unit = unit;
            _amount = amount;
        }

        public Unit GetUnit() => _unit;

        public uint GetAmount() => _amount;
    }
}