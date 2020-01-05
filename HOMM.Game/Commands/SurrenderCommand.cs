using HOMM.BattleObjects;

namespace HOMM.Game.Commands
{
    public class SurrenderCommand : ICommand
    {
        private readonly BattleArmy _source;
        
        public SurrenderCommand(BattleArmy source)
        {
            _source = source;
        }
        
        public bool Execute()
        {
            _source.GetBattle().Surrender();
            
            return true;
        }

        public string GetMessage()
        {
            return (_source.IsAttacker() ? "Attacker" : "Target") + " surrendered";
        }
    }
}