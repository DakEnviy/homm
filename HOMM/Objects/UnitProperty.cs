using HOMM.BattleObjects;

namespace HOMM.Objects
{
    public class UnitProperty
    {
        public virtual BattleUnitsStackMod GetMod(BattleUnitsStack stack) => null;

        public virtual string GetSkill() => null;
    }
}