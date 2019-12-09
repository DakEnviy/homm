using HOMM.BattleObjects;
using HOMM.BattleUnitsStackMods.Skill;

namespace HOMM.Skills.BattleUnitsStack
{
    public class SkillHaste : Skill
    {
        public SkillHaste()
            : base(SkillSourceType.Stack, SkillTargetType.Stack) {}
        
        public override bool Use(BattleObjects.BattleUnitsStack source, BattleObjects.BattleUnitsStack target)
        {
            target.AddMod(new BattleUnitsStackModHaste(target), true);

            return true;
        }
    }
}