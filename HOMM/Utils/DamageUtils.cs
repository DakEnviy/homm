using System;
using HOMM.BattleObjects;

namespace HOMM.Utils
{
    public static class DamageUtils
    {
        private static readonly Random Random = new Random();
        
        public static ushort CalcDamageHitPoints(BattleUnitsStack source, BattleUnitsStack target)
        {
            var amount = source.GetAmount();
            var (minDamage, maxDamage) = source.GetDamage();
            var attack = source.GetAttack();
            var defence = target.GetDefence();

            var minHitPoints = attack > defence
                ? amount * minDamage * (1 + 0.05 * (attack - defence))
                : amount * minDamage / (1 + 0.05 * (defence - attack));
            var maxHitPoints = attack > defence
                ? amount * maxDamage * (1 + 0.05 * (attack - defence))
                : amount * maxDamage / (1 + 0.05 * (defence - attack));

            if (Environment.GetEnvironmentVariable("is-testing") == "true")
            {
                return (ushort) minHitPoints;
            }

            return (ushort) Math.Round(minHitPoints + Random.NextDouble() * (maxHitPoints - minHitPoints));
        }

        // Warming up Random
        static DamageUtils()
        {
            var _ = Random.NextDouble();
            var __ = Random.NextDouble();
            var ___ = Random.NextDouble();
            var ____ = Random.NextDouble();
        }
    }
}