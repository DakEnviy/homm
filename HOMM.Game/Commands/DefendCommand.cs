using HOMM.BattleObjects;

namespace HOMM.Game.Commands
{
    public class DefendCommand : ICommand
    {
        private readonly BattleUnitsStack _source;
        
        public DefendCommand(BattleUnitsStack source)
        {
            _source = source;
        }
        
        public bool Execute()
        {
            _source.GetArmy().GetBattle().Defend();
            
            return true;
        }

        public string GetMessage()
        {
            return _source.GetBaseUnit().GetName() + " defended";
        }
    }
}