using System;
using System.Collections.Generic;
using HOMM.Skills.Unit;

namespace HOMM.BattleObjects
{
    public class Skill
    {
        public static Dictionary<string, Skill> Skills;

        public static void RegisterSkill(string key, Skill skill)
        {
            if (Skills.ContainsKey(key))
            {
                throw new ArgumentException($"Skill with key '{key}' already has");
            }
            
            Skills.Add(key, skill);
        }

        public static bool UnregisterSkill(string key)
        {
            return Skills.Remove(key);
        }

        public static T GetSkill<T>(string key) where T : Skill
        {
            return (T) Skills[key];
        }

        static Skill()
        {
            RegisterSkill("haste", new SkillHaste("haste"));
        }

        private readonly string _key;

        public Skill(string key)
        {
            _key = key;
        }
        
        public string GetKey() => _key;
    }
}