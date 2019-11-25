using System;
using System.Collections.Generic;
using HOMM.BattleObjects;

namespace HOMM.Utils
{
    public class InitiativeComparer : IComparer<BattleUnitsStack>
    {
        private static InitiativeComparer _instance;

        public static InitiativeComparer GetInstance() =>
            _instance ?? (_instance = new InitiativeComparer());

        public int Compare(BattleUnitsStack x, BattleUnitsStack y)
        {
            if (x == null) throw new ArgumentNullException(nameof(x));
            if (y == null) throw new ArgumentNullException(nameof(y));

            if (x.IsWaiting() && !y.IsWaiting()) return 1;
            if (!x.IsWaiting() && y.IsWaiting()) return -1;

            var delta = x.GetInitiative() - y.GetInitiative();
            var factor = x.IsWaiting() ? 1 : -1;

            var res = Math.Abs(delta) > 0.001F ? delta > 0 ? 1 : -1 : 0;
            if (res == 0) res = x.GetArmy().IsAttacker() ? 1 : -1;

            return res * factor;
        }
    }
}