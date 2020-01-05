using System;

namespace HOMM.Objects
{
    public class UnitsStack
    {
        public const int MaxStackSize = 999_999;

        private readonly Unit _unit;
        private readonly int _amount;
        
        public UnitsStack(Unit unit, int amount)
        {
            if (amount > MaxStackSize)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), $"Maximum size of stack is {MaxStackSize}");
            }
            
            _unit = unit;
            _amount = amount;
        }

        public Unit GetUnit() => _unit;

        public int GetAmount() => _amount;
    }
}