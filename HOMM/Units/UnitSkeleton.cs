using HOMM.Objects;
using HOMM.UnitProperties;

namespace HOMM.Units
{
    public class UnitSkeleton : Unit
    {
        public UnitSkeleton()
            : base(5, 1, 2, (1, 1), 10.0f,
                new UnitPropertyShooter()) {}
    }
}