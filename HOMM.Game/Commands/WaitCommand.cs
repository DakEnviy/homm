using HOMM.BattleObjects;

namespace HOMM.Game.Commands
{
    public class WaitCommand : ICommand
    {
        private readonly BattleUnitsStack _source;
        
        public WaitCommand(BattleUnitsStack source)
        {
            _source = source;
        }
        
        public bool Execute()
        {
            _source.GetArmy().GetBattle().Wait();
            
            return true;
        }

        public string GetMessage()
        {
            return _source.GetBaseUnit().GetName() + " is waiting";
        }
    }
}