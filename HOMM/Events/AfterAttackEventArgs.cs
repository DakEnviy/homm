using System;
using HOMM.BattleObjects;

namespace HOMM.Events
{
    public class AfterAttackEventArgs : EventArgs
    {
        public BattleUnitsStack Source { get; }
        public BattleUnitsStack Target { get; }
        public ushort HitPoints { get; }
        
        public AfterAttackEventArgs(BattleUnitsStack source, BattleUnitsStack target, ushort hitPoints)
        {
            Source = source;
            Target = target;
            HitPoints = hitPoints;
        }
    }
}