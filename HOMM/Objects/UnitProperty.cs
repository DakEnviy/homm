using HOMM.BattleObjects;

namespace HOMM.Objects
{
    public abstract class UnitProperty
    {
        public virtual BattleUnitsStackMod GetMod(BattleUnitsStack stack) => null;

        public virtual string GetSkill() => null;
    }
}