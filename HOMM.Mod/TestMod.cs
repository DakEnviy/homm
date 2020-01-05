using System;
using HOMM.Mod.Skills.BattleUnitsStack;
using HOMM.Mod.Units;
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
            
            HUnits.RegisterUnit(new UnitLich());
            HUnits.RegisterUnit(new UnitZombie());
            HSkills.RegisterSkill(new SkillResurrect());
        }

        public void OnDisable()
        {
            Console.WriteLine("TestMod disabled");
            
            HUnits.UnregisterUnit("lich");
            HUnits.UnregisterUnit("zombie");
            HSkills.UnregisterSkill("resurrect");
        }
    }
}