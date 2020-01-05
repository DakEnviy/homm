namespace HOMM.BattleObjects
{
    public enum BattleUnitsStackModType
    {
        Property,
        Turn,
        Skill,
        Custom
    }
    
    public class BattleUnitsStackMod
    {
        private readonly BattleUnitsStackModType _type;
        private readonly int _rounds;
        private readonly BattleUnitsStack _stack;
        
        private int _roundsLeft;

        public BattleUnitsStackMod(BattleUnitsStackModType type, int rounds, BattleUnitsStack stack)
        {
            _type = type;
            _rounds = rounds;
            _stack = stack;

            _roundsLeft = rounds;
        }

        public virtual void Attach() {}
        
        public virtual void Detach() {}
        
        public virtual void UpdateParams() {}

        public void Round()
        {
            if (_roundsLeft > 0)
            {
                --_roundsLeft;
            }
        }

        public BattleUnitsStackModType GetModType() => _type;
        
        public int GetRounds() => _rounds;
        
        public BattleUnitsStack GetStack() => _stack;
        
        public int GetRoundsLeft() => _roundsLeft;
    }
}