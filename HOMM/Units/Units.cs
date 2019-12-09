using System;
using System.Collections.Generic;
using HOMM.Objects;

namespace HOMM.Units
{
    public class Units
    {
        public static readonly IDictionary<string, Unit> Registry = new Dictionary<string, Unit>();

        public static void RegisterUnit(string key, Unit unit)
        {
            if (Registry.ContainsKey(key))
            {
                throw new ArgumentException($"Unit with key '{key}' already has");
            }
            
            Registry.Add(key, unit);
        }

        public static bool UnregisterUnit(string key)
        {
            return Registry.Remove(key);
        }

        public static Unit GetUnit(string key)
        {
            return Registry[key];
        }

        static Units()
        {
            RegisterUnit("angel", new UnitAngel());
            RegisterUnit("skeleton", new UnitSkeleton());
        }
    }
}