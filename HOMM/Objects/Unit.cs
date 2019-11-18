namespace HOMM.Objects
{
    public enum UnitType
    {
        CrossbowMan,
        Skeleton,
        Fury,
        Hydra,
        Angel,
        BoneDragon,
        Devil,
        Cyclope,
        Shaman,
        Lich
    }
    
    public class Unit
    {
        private readonly UnitType _type;
        private readonly int _hitPoints;
        private readonly int _attack;
        private readonly int _defence;
        private readonly (int, int) _damage;
        private readonly float _initiative;

        public Unit(UnitType type, int hitPoints, int attack, int defence, (int, int) damage, float initiative)
        {
            _type = type;
            _hitPoints = hitPoints;
            _attack = attack;
            _defence = defence;
            _damage = damage;
            _initiative = initiative;
        }

        public UnitType GetUnitType() => _type;

        public int GetHitPoints() => _hitPoints;

        public int GetAttack() => _attack;

        public int GetDefence() => _defence;

        public (int, int) GetDamage() => _damage;

        public float GetInitiative() => _initiative;
    }
}