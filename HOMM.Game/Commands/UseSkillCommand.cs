using HOMM.BattleObjects;

namespace HOMM.Game.Commands
{
    public class UseSkillCommand : TargetableCommand
    {
        private readonly BattleUnitsStack _source;
        private readonly string _skillName;
        
        public UseSkillCommand(BattleUnitsStack source, string skillName)
        {
            _source = source;
            _skillName = skillName;
        }
        
        public override bool Execute()
        {
            return Target.GetArmy().GetBattle().UseSkill(Skills.Skills.GetSkill(_skillName), Target);
        }

        public override string GetMessage()
        {
            return _source.GetBaseUnit().GetName() + " used skill " + _skillName + " to " + Target.GetBaseUnit().GetName();
        }
    }
}