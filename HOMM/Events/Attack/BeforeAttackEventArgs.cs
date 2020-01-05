using HOMM.BattleObjects;

namespace HOMM.Events.Attack
{
    public class BeforeAttackEventArgs : CancelEventArgs
    {
        public BattleUnitsStack Source { get; }
        public BattleUnitsStack Target { get; }
        public ushort HitPoints { get; set; }
        
        public BeforeAttackEventArgs(BattleUnitsStack source, BattleUnitsStack target, ushort hitPoints)
        {
            Source = source;
            Target = target;
            HitPoints = hitPoints;
        }
    }
}