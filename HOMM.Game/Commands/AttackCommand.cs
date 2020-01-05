using HOMM.BattleObjects;

namespace HOMM.Game.Commands
{
    public class AttackCommand : TargetableCommand
    {
        private readonly BattleUnitsStack _source;
        private int _damage;
        
        public AttackCommand(BattleUnitsStack source)
        {
            _source = source;
        }

        public override bool Execute()
        {
            var hitPoints = Target.GetHitPoints();
            var result = Target.GetArmy().GetBattle().Attack(Target);

            if (result)
            {
                _damage = hitPoints - Target.GetHitPoints();
            }
            
            return result;
        }

        public override string GetMessage()
        {
            return _source.GetBaseUnit().GetName() + " damaged " + _damage + " HP to " + Target.GetBaseUnit().GetName();
        }
    }
}