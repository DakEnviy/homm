using System;
using System.Collections.Generic;
using HOMM.BattleObjects;
using HOMM.Skills.Unit;

namespace HOMM.Skills
{
    public class Skills
    {
        public static readonly IDictionary<string, Skill> Registry = new Dictionary<string, Skill>();

        public static void RegisterSkill(string key, Skill skill)
        {
            if (Registry.ContainsKey(key))
            {
                throw new ArgumentException($"Skill with key '{key}' already has");
            }
            
            Registry.Add(key, skill);
        }

        public static bool UnregisterSkill(string key)
        {
            return Registry.Remove(key);
        }

        public static Skill GetSkill(string key)
        {
            return Registry[key];
        }

        static Skills()
        {
            RegisterSkill("haste", new SkillHaste());
        }
    }
}