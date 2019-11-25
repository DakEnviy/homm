using HOMM.BattleObjects;

namespace HOMM.Events
{
    public class BeforeAnswerEventArgs : CancelEventArgs
    {
        public BattleUnitsStack Source { get; }
        public BattleUnitsStack Target { get; }
        public ushort HitPoints { get; set; }
        public bool IsAnswered { get; set; }
        
        public BeforeAnswerEventArgs(BattleUnitsStack source, BattleUnitsStack target, ushort hitPoints)
        {
            Source = source;
            Target = target;
            HitPoints = hitPoints;
            IsAnswered = true;
        }
    }
}