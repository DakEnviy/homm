using HOMM.BattleObjects;

namespace HOMM.BattleUnitsStackMods.Skill
{
    public class BattleUnitsStackModHaste : BattleUnitsStackMod
    {
        public BattleUnitsStackModHaste(BattleUnitsStack stack)
            : base(BattleUnitsStackModType.Skill, 3, stack) {}

        public override void UpdateParams()
        {
            var stack = GetStack();
            
            stack.SetInitiative((float) (stack.GetInitiative() * 1.4));
        }
    }
}