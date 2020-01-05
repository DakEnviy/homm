using System;
using HOMM.BattleObjects;

namespace HOMM.Events.Turn
{
    public class NextTurnEventArgs : EventArgs
    {
        public Battle Battle { get; }
        
        public NextTurnEventArgs(Battle battle)
        {
            Battle = battle;
        }
    }
}