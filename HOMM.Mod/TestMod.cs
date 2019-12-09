using System;

namespace HOMM.Mod
{
    public class TestMod : HommMod
    {
        public new string Name = "TestMod";
        public new string Version = "1.0.1";
        
        public override void OnEnable()
        {
            Console.WriteLine("TestMod enabled");
        }

        public override void OnDisable()
        {
            Console.WriteLine("TestMod disabled");
        }
    }
}