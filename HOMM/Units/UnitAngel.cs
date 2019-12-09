using HOMM.Objects;
using HOMM.UnitProperties;

namespace HOMM.Units
{
    public class UnitAngel : Unit
    {
        public UnitAngel()
            : base(180, 27, 27, (45, 45), 11.0f,
                new UnitPropertySkillHaste()) {}
    }
}