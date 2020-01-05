using HOMM.Mod.UnitProperties;
using HOMM.Objects;

namespace HOMM.Mod.Units
{
    public class UnitZombie : Unit
    {
        public UnitZombie()
            : base("zombie", 20, 5, 5, (2, 3), 4,
                new UnitPropertyUndead()) {}
    }
}