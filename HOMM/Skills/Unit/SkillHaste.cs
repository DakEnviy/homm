using HOMM.BattleObjects;
using HOMM.BattleUnitsStackMods.Skill;

namespace HOMM.Skills.Unit
{
    public class SkillHaste : Skill
    {
        public SkillHaste(string key) : base(key) {}

        public void Use(BattleUnitsStack target)
        {
            target.AddMod(new BattleUnitsStackModHaste(target));
        }
    }
}