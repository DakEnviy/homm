using HOMM.BattleObjects;
using HOMM.Mod.BattleUnitsStackMods.Property;
using HOMM.Objects;

namespace HOMM.Mod.UnitProperties
{
    public class UnitPropertyUndead : UnitProperty
    {
        public override BattleUnitsStackMod GetMod(BattleUnitsStack stack)
            => new BattleUnitsStackModUndead(stack);
    }
}