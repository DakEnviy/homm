namespace HOMM
{
    public class Unit
    {
        private readonly string _type;
        private readonly int _hitPoints;
        private readonly int _attack;
        private readonly int _defence;
        private readonly (int, int) _damage;
        private readonly float _initiative;

        public Unit(string type, int hitPoints, int attack, int defence, (int, int) damage, float initiative)
        {
            _type = type;
            _hitPoints = hitPoints;
            _attack = attack;
            _defence = defence;
            _damage = damage;
            _initiative = initiative;
        }

        public new string GetType() { return _type; }

        public int GetHitPoints() { return _hitPoints; }
        
        public int GetAttack() { return _attack; }
        
        public int GetDefence() { return _defence; }
        
        public (int, int) GetDamage() { return _damage; }
        
        public float GetInitiative() { return _initiative; }
    }
}