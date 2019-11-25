using HOMM.BattleObjects;
using HOMM.BattleUnitsStackMods.Skill;

namespace HOMM.Skills.Unit
{
    public class SkillHaste : Skill
    {
        public SkillHaste() : base("haste", SkillSourceType.Stack, SkillTargetType.Stack) {}

        public override void Use(BattleUnitsStack source, BattleUnitsStack target)
        {
            target.AddMod(new BattleUnitsStackModHaste(target));
        }
    }
}