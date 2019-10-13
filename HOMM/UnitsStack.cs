namespace HOMM
{
    public class UnitsStack
    {
        private readonly Unit _unit;
        private readonly uint _amount;

        public UnitsStack(Unit unit, uint amount)
        {
            _unit = unit;
            _amount = amount;
        }

        public Unit GetUnit() { return _unit; }
        
        public uint GetAmount() { return _amount; }
    }
}