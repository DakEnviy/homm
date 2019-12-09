using HOMM.BattleObjects;
using HOMM.Mod.BattleUnitsStackMods.Property;

namespace HOMM.Mod.Skills.BattleUnitsStack
{
    public class SkillResurrect : Skill
    {
        public SkillResurrect()
            : base("resurrect", SkillSourceType.Stack, SkillTargetType.Stack) {}
        
        public override bool Use(BattleObjects.BattleUnitsStack source, BattleObjects.BattleUnitsStack target)
        {
            if (!target.ContainsMod(typeof(BattleUnitsStackModUndead))) return false;
            if (source.GetArmy() != target.GetArmy()) return false;
            
            target.Resurrect((ushort) (source.GetAmount() * 50));

            return true;
        }
    }
}