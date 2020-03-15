using HOMM.Objects;
using HOMM.UnitProperties;

namespace HOMM.Units
{
    public class UnitRider : Unit
    {
        public UnitRider()
            : base("rider", 10, 20, 5, (20, 30), 20.0f,
                new UnitPropertyShooter()) {}
    }
}