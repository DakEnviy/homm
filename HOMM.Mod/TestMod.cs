using System;
using HOMM.Mod.Units;
using HOMM.Mod.Skills.BattleUnitsStack;
using HUnits = HOMM.Units.Units;
using HSkills = HOMM.Skills.Skills;

namespace HOMM.Mod
{
    public class TestMod : IHommMod
    {
        public string Name() => "TestMod";
        
        public string Version() => "1.0.1";
        
        public void OnEnable()
        {
            Console.WriteLine("TestMod enabled");
            
            HUnits.RegisterUnit("lich", new UnitLich());
            HSkills.RegisterSkill("resurrect", new SkillResurrect());
        }

        public void OnDisable()
        {
            Console.WriteLine("TestMod disabled");
            
            HUnits.UnregisterUnit("lich");
            HSkills.UnregisterSkill("resurrect");
        }
    }
}