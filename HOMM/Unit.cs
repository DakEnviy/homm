namespace HOMM
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
        private readonly uint _hitPoints;
        private readonly uint _attack;
        private readonly uint _defence;
        private readonly (uint, uint) _damage;
        private readonly float _initiative;

        public Unit(UnitType type, uint hitPoints, uint attack, uint defence, (uint, uint) damage, float initiative)
        {
            _type = type;
            _hitPoints = hitPoints;
            _attack = attack;
            _defence = defence;
            _damage = damage;
            _initiative = initiative;
        }

        public new UnitType GetType() { return _type; }

        public uint GetHitPoints() { return _hitPoints; }
        
        public uint GetAttack() { return _attack; }
        
        public uint GetDefence() { return _defence; }
        
        public (uint, uint) GetDamage() { return _damage; }
        
        public float GetInitiative() { return _initiative; }
    }
}