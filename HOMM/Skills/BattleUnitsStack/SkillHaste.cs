using HOMM.BattleObjects;
using HOMM.BattleUnitsStackMods.Skill;

namespace HOMM.Skills.BattleUnitsStack
{
    public class SkillHaste : Skill
    {
        public override void Use(BattleObjects.BattleUnitsStack source, BattleObjects.BattleUnitsStack target)
        {
            target.AddMod(new BattleUnitsStackModHaste(target));
        }
    }
}