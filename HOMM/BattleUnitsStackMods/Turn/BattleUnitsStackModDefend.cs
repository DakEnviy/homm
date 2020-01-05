using HOMM.BattleObjects;

namespace HOMM.BattleUnitsStackMods.Turn
{
    public class BattleUnitsStackModDefend : BattleUnitsStackMod
    {
        public BattleUnitsStackModDefend(BattleUnitsStack stack)
            : base(BattleUnitsStackModType.Turn, 1, stack) {}

        public override void UpdateParams()
        {
            var stack = GetStack();

            stack.SetDefence((int) (stack.GetDefence() * 1.3));
        }
    }
}