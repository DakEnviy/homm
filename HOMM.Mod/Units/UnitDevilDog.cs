using HOMM.Mod.UnitProperties;
using HOMM.Objects;
using HOMM.UnitProperties;

namespace HOMM.Mod.Units
{
    public class UnitDevilDog : Unit
    {
        public UnitDevilDog()
            : base("devilDog", 50000, 1500, 150, (12, 17), 10.0f,
                new UnitPropertyUndead(),
                new UnitPropertySkillResurrect()) {}
    }
}