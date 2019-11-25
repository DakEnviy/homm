using HOMM.BattleObjects;
using HOMM.BattleUnitsStackMods.Property;
using HOMM.Objects;

namespace HOMM.UnitProperties
{
    public class UnitPropertyShooter : UnitProperty
    {
        public override BattleUnitsStackMod GetMod(BattleUnitsStack stack) =>
            new BattleUnitsStackModShooter(stack);
    }
}