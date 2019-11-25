using HOMM.BattleObjects;
using HOMM.Events;

namespace HOMM.BattleUnitsStackMods.Property
{
    public class BattleUnitsStackModShooter : BattleUnitsStackMod
    {
        public BattleUnitsStackModShooter(BattleUnitsStack stack)
            : base(BattleUnitsStackModType.Property, -1, stack) {}
        
        public override void Attach()
        {
            EventBus.BeforeAnswer += OnBeforeAnswer;
        }

        public override void Detach()
        {
            EventBus.BeforeAnswer -= OnBeforeAnswer;
        }

        private void OnBeforeAnswer(object sender, BeforeAnswerEventArgs args)
        {
            if (args.Source == GetStack() || args.Target == GetStack())
            {
                args.Cancel = true;
            }
        }
    }
}