using System;
using HOMM.Events.Attack;

namespace HOMM.Events
{
    public static class EventBus
    {
        public static event EventHandler<BeforeAttackEventArgs> BeforeAttack;
        public static event EventHandler<AfterAttackEventArgs> AfterAttack;
        public static event EventHandler<BeforeAnswerEventArgs> BeforeAnswer;
        public static event EventHandler<AfterAnswerEventArgs> AfterAnswer;

        public static void OnBeforeAttack(object sender, BeforeAttackEventArgs args)
        {
            BeforeAttack?.Invoke(sender, args);
        }

        public static void OnAfterAttack(object sender, AfterAttackEventArgs args)
        {
            AfterAttack?.Invoke(sender, args);
        }

        public static void OnBeforeAnswer(object sender, BeforeAnswerEventArgs args)
        {
            BeforeAnswer?.Invoke(sender, args);
        }
        
        public static void OnAfterAnswer(object sender, AfterAnswerEventArgs args)
        {
            AfterAnswer?.Invoke(sender, args);
        }
    }
}