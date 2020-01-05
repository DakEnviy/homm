using System;
using System.Collections.Generic;
using HOMM.BattleObjects;
using HOMM.Skills.BattleUnitsStack;

namespace HOMM.Skills
{
    public static class Skills
    {
        public static readonly IDictionary<string, Skill> Registry = new Dictionary<string, Skill>();

        public static void RegisterSkill(Skill skill)
        {
            var key = skill.GetName();
            
            if (Registry.ContainsKey(key))
            {
                throw new ArgumentException($"Skill with key '{key}' already has");
            }
            
            Registry.Add(skill.GetName(), skill);
        }

        public static bool UnregisterSkill(string key) => Registry.Remove(key);

        public static Skill GetSkill(string key) => Registry[key];

        static Skills()
        {
            RegisterSkill(new SkillHaste());
        }
    }
}