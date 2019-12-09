using HOMM.BattleObjects;
using HOMM.Mod.BattleUnitsStackMods.Property;

namespace HOMM.Mod.Skills.BattleUnitsStack
{
    public class SkillResurrect : Skill
    {
        public SkillResurrect()
            : base(SkillSourceType.Stack, SkillTargetType.Stack) {}
        
        public override bool Use(BattleObjects.BattleUnitsStack source, BattleObjects.BattleUnitsStack target)
        {
            if (!target.ContainsMod(typeof(BattleUnitsStackModUndead))) return false;
            
            target.Resurrect((ushort) (source.GetAmount() * 50));

            return true;
        }
    }
}