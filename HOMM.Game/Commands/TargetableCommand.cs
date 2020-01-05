using HOMM.BattleObjects;

namespace HOMM.Game.Commands
{
    public abstract class TargetableCommand : ICommand
    {
        protected BattleUnitsStack Target;
        
        public BattleUnitsStack GetTarget() => Target;
        
        public void SetTarget(BattleUnitsStack stack)
        {
            Target = stack;
        }

        public abstract bool Execute();
        
        public abstract string GetMessage();
    }
}