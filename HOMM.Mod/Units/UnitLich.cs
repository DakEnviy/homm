using HOMM.Mod.UnitProperties;
using HOMM.Objects;
using HOMM.UnitProperties;

namespace HOMM.Mod.Units
{
    public class UnitLich : Unit
    {
        public UnitLich()
            : base(50, 15, 15, (12, 17), 10.0f,
                new UnitPropertyShooter(),
                new UnitPropertyUndead(),
                new UnitPropertySkillResurrect()) {}
    }
}