using HOMM.BattleObjects;

namespace HOMM.Mod.BattleUnitsStackMods.Property
{
    public class BattleUnitsStackModUndead : BattleUnitsStackMod
    {
        public BattleUnitsStackModUndead(BattleUnitsStack stack)
            : base(BattleUnitsStackModType.Property, -1, stack) {}
    }
}