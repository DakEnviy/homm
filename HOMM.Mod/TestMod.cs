using System;
using HOMM.Mod.Units;
using HOMM.Mod.Skills.BattleUnitsStack;
using HUnits = HOMM.Units.Units;
using HSkills = HOMM.Skills.Skills;

namespace HOMM.Mod
{
    public class TestMod : HommMod
    {
        public override string Name() => "TestMod";
        
        public override string Version() => "1.0.1";
        
        public override void OnEnable()
        {
            Console.WriteLine("TestMod enabled");
            
            HUnits.RegisterUnit("lich", new UnitLich());
            HSkills.RegisterSkill("resurrect", new SkillResurrect());
        }

        public override void OnDisable()
        {
            Console.WriteLine("TestMod disabled");
            
            HUnits.UnregisterUnit("lich");
            HSkills.UnregisterSkill("resurrect");
        }
    }
}