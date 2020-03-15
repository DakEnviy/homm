using System;
using System.Collections.Generic;
using HOMM.Objects;

namespace HOMM.Units
{
    public static class Units
    {
        public static readonly IDictionary<string, Unit> Registry = new Dictionary<string, Unit>();

        public static void RegisterUnit(Unit unit)
        {
            var key = unit.GetName();
            
            if (Registry.ContainsKey(key))
            {
                throw new ArgumentException($"Unit with key '{key}' already has");
            }
            
            Registry.Add(key, unit);
        }

        public static bool UnregisterUnit(string key) => Registry.Remove(key);

        public static Unit GetUnit(string key) => Registry[key];

        static Units()
        {
            RegisterUnit(new UnitAngel());
            RegisterUnit(new UnitSkeleton());
            RegisterUnit(new UnitRider());
        }
    }
}