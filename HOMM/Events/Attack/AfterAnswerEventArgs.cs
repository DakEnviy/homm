using System;
using HOMM.BattleObjects;

namespace HOMM.Events.Attack
{
    public class AfterAnswerEventArgs : EventArgs
    {
        public BattleUnitsStack Source { get; }
        public BattleUnitsStack Target { get; }
        public ushort HitPoints { get; }
        public bool IsAnswered { get; }
        
        public AfterAnswerEventArgs(BattleUnitsStack source, BattleUnitsStack target, ushort hitPoints, bool isAnswered)
        {
            Source = source;
            Target = target;
            HitPoints = hitPoints;
            IsAnswered = isAnswered;
        }
    }
}